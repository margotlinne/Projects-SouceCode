using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 해당 프로젝트는 첫 번째로 제작해본 프로젝트이며, 코드에서 미숙한 부분이 보일 수 있습니다. **/

public class Flycup : MonoBehaviour
{
    public GameManager gameManager;
    private Animator anim;
    private Rigidbody2D rb;
    public HealthBar healthbar;

    public float velocity = 1;
    public int maxHealth = 100;
    private float timer = 0;
    private float maxTime = 0.1f;
    public float speed;
    public static float currentHealth;

    bool isLeft = true;
    public static bool startOff;


    void Start()
    {
        Audio.UISoundPlay();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }


    void Update()
    {
        // so player stops when game over
        if(GameManager.gameOver)
        {
            anim.speed = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }

        if (Input.GetMouseButtonDown(0) && gameManager.StartCanvas)
        {
            anim.speed = 1f;
            rb.constraints = RigidbodyConstraints2D.None;
            gameManager.StartCanvas.SetActive(false);
            startOff = true;
            Time.timeScale = 1;
        }
        // remake now and it doesn't work
        // because i didn't attatch audio files to main camera
        if (Input.GetMouseButtonDown(0) && !gameManager.isCanvasOn)
        {
            // Jump
            rb.velocity = Vector2.up * velocity;
        }


        if (isLeft == true && !GameManager.gameOver && startOff) // go left if it hits left collider
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if (isLeft == false && !GameManager.gameOver && startOff)    // go right if it hits right collider
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }



        if (timer > maxTime && !GameManager.gameOver && startOff)
        {
            TakeDamage(0.8f);
            timer = 0;
        }
        timer += Time.deltaTime;



        if (currentHealth <= 0)
        {
            gameManager.GameOver();
            Audio.HealthSoundPlay();
            currentHealth = 100;
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BottomCollider")
        {
            gameManager.GameOver();
            currentHealth = 100;
            Audio.fallSoundPlay();
        }

        if (collision.gameObject.name == "LeftCollider")
        {
            isLeft = false;
        }

        if (collision.gameObject.name == "RightCollider")
        {
            isLeft = true;
        }

        if (collision.gameObject.tag == "Orange")
        {
            TakeRecover(5f);
            Score.score++;
            Audio.ItemSoundPlay();
            if (currentHealth > 100)
            {
                currentHealth = 100;
            }
        }

        if (collision.gameObject.tag == "Lemon")
        {
            TakeRecover(10f);
            Score.score++;
            Audio.ItemSoundPlay();
            if (currentHealth > 100)
            {
                currentHealth = 100;

            }
        }
        if (collision.gameObject.tag == "Peach")
        {
            TakeRecover(15f);
            Score.score++;
            Audio.ItemSoundPlay();
            if (currentHealth > 100)
            {
                currentHealth = 100;
            }
        }

        if (collision.gameObject.tag == "Coffee")
        {
            TakeRecover(20f);
            Score.score++;
            Audio.ItemSoundPlay();
            if (currentHealth > 100)
            {
                currentHealth = 100;
            }
        }

        if (collision.gameObject.tag == "Rock")
        {
            TakeDamage(10f);
            Audio.RockSoundPlay();
        }

        if (collision.gameObject.tag == "FireRock")
        {
            currentHealth = 0;
            gameManager.GameOver();
        }
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }

    void TakeRecover(float recover)
    {
        currentHealth += recover;
        healthbar.SetHealth(currentHealth);
    }
}