using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 해당 프로젝트는 초창기 제작된 프로젝트로, 코드에서 미숙한 부분이 보일 수 있습니다. **/

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    private Animator anim;

    public float jumpForce = 6;
    public float speed = 10;
    private Rigidbody2D rb;

    public Transform groundPos;
    private bool isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;
    private bool isJumping;


    private bool isSwimming;
    public static bool isGoal;
    public static bool isCollected;
    public static bool isPowerful;
    public static bool getWateringCan;
    public static bool isWatering;
    public static bool cantMove;
    public static bool inWaterZone;
    public static bool inMWaterZone;
    public static bool isFilling;
    public static bool inCactusWaterZone;
    public static bool inFillZone;
    public float timer = 0;
    public float maxTime = 1;


    public static int Water;



    private void Start()
    {
        inCactusWaterZone = false;
        inMWaterZone = false;
        Water = 0;
        isFilling = false;
        inWaterZone = false;
        cantMove = false;
        isWatering = false;
        getWateringCan = false;
        isPowerful = false;
        isSwimming = false;
        isCollected = false;
        isGoal = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, whatIsGround);

        if(GameManager.isDone == false)
        {
            // Jump //
            if (isGrounded == true && Input.GetKeyDown(KeyCode.UpArrow) && isSwimming == false && cantMove == false)
            {
                if(isPowerful == false)
                {
                    anim.SetTrigger("takeOff");
                }
                
                isJumping = true;
                rb.velocity = Vector2.up * jumpForce;
            }
            else if(isSwimming == true && !isGrounded)
            {
                float UDmoveInput = Input.GetAxisRaw("Vertical");
                rb.velocity = new Vector2(rb.velocity.x, UDmoveInput * speed/2);
            }

            if (isGrounded == true)
            {
                anim.SetBool("isJumping", false);
            }
            else
            {
                anim.SetBool("isJumping", true);
            }

            if (Input.GetKeyUp(KeyCode.UpArrow) && cantMove == false && isSwimming == false)
            {
                isJumping = false;
            }

            float moveInput = Input.GetAxisRaw("Horizontal");
            if(cantMove == false)
            {
                rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            }           

            if (moveInput == 0)
            {
                anim.SetBool("isRunning", false);
            }
            else
            {
                anim.SetBool("isRunning", true);
            }

            if (moveInput < 0 && cantMove == false)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (moveInput > 0 && cantMove == false)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        // water
        if(getWateringCan == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && (inWaterZone == true || inMWaterZone == true || inCactusWaterZone == true) && Water > 0 && isPowerful == false)
            {
                if((FlowerBloom.playerInFlower == true && FlowerBloom.itBloomed == false) || (MonsterFlowerBloom.playerInMFlower == true && MonsterFlowerBloom.MonsteritBloomed == false) || ((Cactus.playerInCactus == true && (Cactus.FirstGrow == false ||  Cactus.SecondGrow == false || Cactus.Dead == false))))
                {
                     anim.SetTrigger("Watering");
                     cantMove = true;
                     isWatering = true;
                     Water --;
                }
            }
            if(Input.GetKey(KeyCode.Space) && inFillZone == true && isPowerful == false)
            {
                anim.SetTrigger("Watering");
                cantMove = true;
                isFilling = true;
                Water ++;
            }
            if(cantMove == true)
            {
                if (timer > maxTime)
                {
                    isWatering = false;
                    isFilling = false;
                    cantMove = false;
                    timer = 0;
                }timer += Time.deltaTime;
            }
        }

        if (isPowerful == true)
       {
            anim.SetBool("isPowerful", true);
       }
       else
       {
            anim.SetBool("isPowerful", false);
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("GameOver") && isGoal == false)
        {
            gameManager.GameOver();
        }

        if(collision.CompareTag("Finish") && Collect.score == Collect.neededSeed)
        {
            if(GameManager.isCanvasOn == false)
            {
                Audio.GoalSoundPlay();
            }
            isGoal = true;
        }

        if(collision.CompareTag("Item"))
        {
            isCollected = true;
        }

        if(collision.CompareTag("Water"))
        {
            if(isPowerful == false)
            {
                Audio.gettingPowerSoundPlay();
            }           
            isSwimming = true;
            isPowerful = true;
        }

        if(collision.CompareTag("Air"))
        {
            isSwimming = false;   
        }

        if(collision.CompareTag("Cloud") || collision.CompareTag("Sunlight"))
        {
            if (isPowerful == true)
            {
                Audio.losingPowerSoundPlay();
            }
            isPowerful = false;
            isSwimming = false;            
        }

        if (collision.CompareTag("WateringCan"))
        {
            getWateringCan = true;  
        }

        if (isPowerful == false && collision.CompareTag("Enemy") && isGoal == false)
        {
            gameManager.GameOver();
            
        }

        if ((collision.CompareTag("Sun") || collision.CompareTag("ThirdBug")) && isGoal == false)
        {
            gameManager.GameOver();            
        }


        if (isPowerful == true && collision.CompareTag("Enemy"))
        {
            Audio.KillSoundPlay();
        }

        //water the flower

        if(collision.CompareTag("WaterZone"))
        {
            inWaterZone = true;
        }

        if (collision.CompareTag("Monster Water Zone"))
        {
            inMWaterZone = true;
        }

        if (collision.CompareTag("Cactus Water Zone"))
        {
            inCactusWaterZone = true;
        }

        //fill the watering can
        if (collision.CompareTag("FillWateringCan"))
        {
            inFillZone = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("WaterZone"))
        {            
            inWaterZone = false;
        }

        if (collision.CompareTag("Monster Water Zone"))
        {
            inMWaterZone = false;
        }

        if (collision.CompareTag("FillWateringCan"))
        {
            inFillZone = false;
        }
    }
}
