using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    
    public int isCollected = 0;
    public int gemNumber;
    private void Start()
    {
        gemNumber = 0;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Audio.CollectSoundPlay();
            GemPoint.Point += 1;
            gemNumber = 1;

        }
    }

    

}
