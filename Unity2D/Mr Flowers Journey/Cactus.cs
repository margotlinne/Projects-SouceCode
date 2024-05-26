using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 해당 프로젝트는 초창기 제작된 프로젝트로, 코드에서 미숙한 부분이 보일 수 있습니다. **/

public class Cactus : MonoBehaviour
{
    private Animator anim;
    public static bool playerInCactus;
    public static bool FirstGrow;
    public static bool SecondGrow;
    public static bool Dead;
    public static bool Done;
    public static bool killBug;

    private float Timer = 0;
    private float maxTime = 1.3f;

    public static int WaterStage;

    private void Start()
    {
        WaterStage = 0;
        Done = false;
        FirstGrow = false;
        SecondGrow = false;
        Dead = false;
        playerInCactus = false;
        killBug = false;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(playerInCactus == true && Input.GetKeyDown(KeyCode.Space) && Player.isWatering == true)
        {
            WaterStage++;
        }

        if(WaterStage == 1)
        {
            FirstGrowing();
            FirstGrow = true;
        }

        if(WaterStage == 2 && FirstGrow)
        {
            SecondGrowing();
            SecondGrow = true;
        }

        if(WaterStage == 3 && SecondGrow)
        {
            ThirdGrowing();
            Dead = true;
        }
    }

        
    void FirstGrowing()
    {
        Audio.bloomingSoundPlay();
        anim.SetTrigger("FirstGrow");
        killBug = true;       
    }

    void SecondGrowing()
    {
        anim.SetTrigger("SecondGrow");
        Audio.bloomingSoundPlay();
    }


    void ThirdGrowing()
    {
        anim.SetTrigger("Dead");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInCactus = true;
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInCactus = false;
        }
    }
}