using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hive : MonoBehaviour
{
    public int residents = 0;
    public GameObject bee;
    GameManager gameManager;
    private Animator anim;
    private bool HiveFull;
    public static bool isStarted;
    private bool isCollided;
    public GameObject spawnPoint;
    private bool bornedBee;
    public float timer = 0;
    public float maxTime = 0.5f;
    Bee beeObj;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        bornedBee = false;
        isStarted = false;
        HiveFull = false;
        isCollided = false;
    }

    void makingBee()
    {
        if (!bornedBee)
        {
            GameObject newBee = Instantiate(bee);
            newBee.transform.position = spawnPoint.transform.position;
            BeeNumber.bee += 1;
            residents += 1;
            if (!isStarted)
            {
                if (HiveFull == false)
                {
                    anim.SetTrigger("Plus");
                }
                else
                {
                    anim.SetTrigger("FullPlus");
                }
                bornedBee = true;
            }
        }
    }

    void Update()
    {
        if (isStarted == true)
        {
            makingBee();
        }
        isStarted = false;

        // 이 벌집이 5마리의 벌 거주자를 가지게 되면
        if (residents == 5 && !HiveFull)
        {
            // 설치 가능한 벌집 하나 추가
            HiveNumber.newHive += 1;
            Audio.HiveSoundPlay();
            anim.SetTrigger("HiveFull");
            HiveFull = true;
        }

        // 벌집의 거주자가 5마리 이상으로 가득 차지 않았을 때는 새 벌 탄생
        if (isCollided == true && beeObj.GetComponent<Bee>().isResting && !HiveFull)
        {
            if (timer > maxTime)
            {
                makingBee();
                timer = 0;
            }
            timer += Time.deltaTime;
        }        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Colliding"))
        {
            beeObj = other.GetComponent<ColliderManager>().owner.GetComponent<Bee>();
            if (beeObj.carryingHoney)
            {
                bornedBee = false;
                isCollided = true;
            }
        }
    }
}




