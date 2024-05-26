using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarCube : MonoBehaviour
{
    private bool breakstart = false;
    private bool firstbreak = false;
    private bool secondbreak = false;
    public bool broken = false;
    Animator anim;

    public GameObject detectSugar;
    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (detectSugar.GetComponent<Sugar_detect>().sugarDetected && Player.onGround)
        {
            anim.SetBool("breaking", true);

            if (breakstart)
            {
                anim.SetTrigger("first_break");
            }

            if (firstbreak)
            {
                anim.SetTrigger("second_break");
            }

            if (secondbreak)
            {
                anim.SetBool("breaking", false);
            }
        }

        
       
    }



    void breakStart()
    {
        breakstart = true;
    }

    void firstBreak()
    {
        firstbreak = true;
    }

    void secondBreak()
    {
        secondbreak = true;
    }

    void Broken()
    {
        broken = true;
    }
}
