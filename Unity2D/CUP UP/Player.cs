using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static bool isStarted = false;
    public static bool onGround = false;
    public static bool finishedGame = false;
    public static bool isDark = false;

    public GameObject player;
    public float velocity = 2;
    public static Rigidbody2D rb;
    public int maxEnergy = 100;
    public int currentEnergy;
    public EnergyBar energyBar;
    public GameObject startCanvas;

    private bool playOntime = false;
    private Animator anim;
    private float timer = 0;
    private float maxTime = 1f;
    private bool isFlying = false;    
    private bool isMoving = false;
    // for Start Canvas
    private bool firstTouched = false;
    // knockdown
    private bool knocdown = false;

    // touch to move
    bool right_down;
    bool right_up;
    bool left_down;
    bool left_up;



    Vector3 lastPos;

    void Start()
    {
        startCanvas.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        // health //
        currentEnergy = maxEnergy;
        energyBar.setMaxEnergy(maxEnergy);
        anim = GetComponent<Animator>();
        // audio //
        right_up = true;
        left_up = true;
    }


    private void Update()
    {
       if(Input.GetMouseButtonDown(0) && (right_down || left_down))
        {
            Audio.JumSoundPlay();
        }

        if (left_down == true && currentEnergy > 0 && !knocdown)
        {
            isFlying = true;
            rb.velocity = new Vector2(-3, 5) * velocity;
            if (timer > maxTime*.04)
            {
                losingEnergy(1);
                timer = 0;
                energyBar.setEnergy(currentEnergy);
            }
            timer += Time.deltaTime;
        }
        

        if (right_down == true && currentEnergy > 0 && !knocdown)
        {
            isFlying = true;
            rb.velocity = new Vector2(3, 5) * velocity;
            if (timer > maxTime*.04)
            {
                losingEnergy(1);
                timer = 0;
                energyBar.setEnergy(currentEnergy);
            }
            timer += Time.deltaTime;
        }

        if (left_up == true && right_up == true)
        {
            isFlying = false;
        }


        if (isFlying == false && currentEnergy < 100 && onGround == true && isMoving == false && !knocdown)
        {
            if (timer > maxTime*.01)
            {
                gainingEnergy(1);
                timer = 0;
                energyBar.setEnergy(currentEnergy);
            }
            timer += Time.deltaTime;
        }


        if (lastPos == player.transform.position)
        {
            isMoving = false;
        }
        else isMoving = true;

        lastPos = player.transform.position;


      

        if (firstTouched)
        {
            startCanvas.SetActive(false);
        }

        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
            energyBar.setEnergy(currentEnergy);
        }



        // idle animation starts during knockdown -> because I used "SetTrigger"
        if (currentEnergy == 0)
        {
            knocdown = true;
            playOntime = false;
            currentEnergy += 1;
        }
        if (knocdown)
        {
            if (!playOntime)
            {
                Audio.knockbackSoundPlay();
                playOntime = true;
            }
            
            anim.SetBool("knockback", true);

            if (timer > maxTime * 2f)
            {
                knocdown = false;                
            }
            timer += Time.deltaTime;
        }
        if (!knocdown)
        {
            anim.SetBool("knockback", false);
        }
        
        

       
        


    }
    

    void losingEnergy(int usedEnergy)
    {
        currentEnergy -= usedEnergy;
    }

    void gainingEnergy(int energy)
    {
        currentEnergy += energy;
        if (currentEnergy >= 100)
        {
            currentEnergy = 100;
        }
    }

    public void ButtonDown(string type)
    {
        if(firstTouched == false)
        {
            firstTouched = true;
        }

        switch(type)
        {
            case "L":
                left_down = true;
                left_up = false;
                break;
            case "R":
                right_down = true;
                right_up = false;
                break;
        }
    }

    public void ButtonUp(string type)
    {
        if(firstTouched == false)
        {
            firstTouched = true;
        }
        switch (type)
        {
            case "L":
                left_up = true;
                left_down = false;
                break;
            case "R":
                right_up = true;
                right_down = false;
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {        

        if(collision.gameObject.tag == "DarkArea")
        {
            isDark = true;
        }
        if(collision.gameObject.tag == "Goal")
        {
            Audio.GoalSoundPlay();
            finishedGame = true;
        }

        if(collision.gameObject.tag == "Fruit1")
        {
            Audio.fruitCollectSoundPlay();
            currentEnergy += 20;
            energyBar.setEnergy(currentEnergy);
        }

        if (collision.gameObject.tag == "Fruit2")
        {
            Audio.fruitCollectSoundPlay();
            currentEnergy += 40;
            energyBar.setEnergy(currentEnergy);
        }

        if (collision.gameObject.tag == "Fruit3")
        {
            Audio.fruitCollectSoundPlay();
            currentEnergy += 100;
            energyBar.setEnergy(currentEnergy);
        }

       if(collision.gameObject.tag == "zoom")
        {
            CameraZoom.ZoomActive = true;
        }


        if (collision.gameObject.tag == "Knockback" && !knocdown)
        {
            playOntime = false;
            currentEnergy = 0;
            energyBar.setEnergy(currentEnergy);
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "zoom")
        {
            CameraZoom.ZoomActive = false;
        
        }

        if (collision.gameObject.tag == "DarkArea")
        {
            isDark = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            onGround = false;
        }
       
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            Audio.LandSoundPlay();

            onGround = true;
        }

        if(collision.transform.tag == "Box")
        {

            Audio.LandSoundPlay();
        }
        if (collision.transform.tag == "Rock" && !knocdown)
        {
            playOntime = false;
            currentEnergy = 0;
            energyBar.setEnergy(currentEnergy);
        }
    }

} 
