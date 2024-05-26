using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public static int currentHealth;
    public GameObject bloodEffect;

    public GameManager gameManager;

    private Animator anim;

    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;

        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            Die();
        }
        
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    
    void Die()
    {
        GameObject effect = Instantiate(bloodEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        gameManager.gameOver();
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Level1" || collision.gameObject.tag == "Level2" || collision.gameObject.tag == "Level3")
        {
            takeDamage(10);           
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bullet of enemy"))
        {
            takeDamage(10);
        }
    }


    void takeDamage(int damage)
    {
        currentHealth -= damage;
        int lostHeart = damage / 10;
        health -= lostHeart;
        anim.SetTrigger("takeDamage");
        Audio.getAttackSoundPlay();
    }
}




