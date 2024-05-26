using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool isSaved;
    public bool deletedData;
    public bool sound;
    public GameObject playerPos;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        sound = true;
        isSaved = false;
        deletedData = false;
    }

    private void Update()
    {     
        if (isSaved)
        {
            
            anim.SetBool("saved", true);
            if (sound)
            {
                Audio.CheckPointSoundPlay();
                sound = false;
            }
        }
        

        if (deletedData)
        {
            anim.SetBool("saved", false);
            deletedData = false;
        }      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.gameObject.tag == "Player")
        {
            // after deleting data this doesn't work
            // because used "isDeleted" bool from Sidebar and it kept calling since it was vague to set it false.
            isSaved = true;
        }
    }
}
