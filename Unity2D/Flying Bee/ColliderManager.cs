using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    public GameObject owner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Flower"))
        {
            owner.GetComponent<Bee>().collidingFlower = true;
        }

        if (other.CompareTag("Hive"))
        {
            owner.GetComponent<Bee>().collidingHive = true;
        }

        if (other.CompareTag("Colliding"))
        {  
            if(other.GetComponent<ColliderManager>().owner.GetComponent<Bee>().isResting == false && owner.GetComponent<Bee>().isResting == false)
            {
                Audio.gameOverSoundPlay();
                FindObjectOfType<GameManager>().gameOver();
            }               
        }

        if (other.CompareTag("Death"))
        {
            Audio.losingScoreSoundPlay();
            BeeNumber.bee--;
            Score.score -= 5;
            Destroy(owner);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Flower"))
        {
            owner.GetComponent<Bee>().collidingFlower = false;
        }

        if (collision.CompareTag("Hive"))
        {
            owner.GetComponent<Bee>().collidingHive = false;
        }
    }
}
