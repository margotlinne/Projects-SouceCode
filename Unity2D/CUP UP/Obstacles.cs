using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{

    public Vector2 playerPos;
    private bool colliding;
    public GameObject particle;


    private void Update()
    {
        if (colliding)
        {
            GameObject effect = Instantiate(particle, playerPos, Quaternion.identity);
            Destroy(effect, 3f);
            colliding = false;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            ContactPoint2D contact = collision.contacts[0];
            playerPos = contact.point;    
            colliding = true;
          
        }
    }
}
