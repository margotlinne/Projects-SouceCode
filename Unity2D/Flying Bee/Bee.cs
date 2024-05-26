using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Transactions;

public class Bee : MonoBehaviour
{
    public GameObject hive;
    public GameObject flower;

    public bool collidingFlower = false;
    public bool collidingHive = false;
    public bool carryingHoney = false;
    public bool isResting = false;
    public static int Honey = 0;
    private Animator anim;


    public Transform[] target;
    private int randomSpot;
    private bool flyingRandom;
    public float speed = 3f;
    public Rigidbody2D rb;
    private bool drawing = false;
    public int lastIndex = 0;

    [Header("Components")]
    public LineRenderer lr;


    //Not customizable:
    public int currentWayPoint = 0;
    private int wayIndex;
    private bool touchStartedOnPlayer;
    private int lrCounter = 0;
    public List<GameObject> wayPoints;
    public List<GameObject> lastPoints;

    [Header("Prefabs")]
    public GameObject wayPoint;
    public GameObject lastPoint;
    public GameObject NectarParticle;

    private Vector2 position;
    private float rotationSpeed = 5f;
    private bool isRDflying;
    private bool beforeFinish = false;

    void Start()
    {
        isRDflying = true;
        flyingRandom = false;
        position = transform.position;
        isResting = false;
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        wayIndex = 1;
        touchStartedOnPlayer = false;
    }

    private void OnMouseDown()
    {
        if (lastPoints.Count != 0)
        {
            resetPoints();
        }

        lr.enabled = true;

        touchStartedOnPlayer = true;
        wayIndex = 1;

        lr.positionCount = 1;
        lr.SetPosition(0, transform.position);

        lrCounter = 0;
        currentWayPoint = 0;
        lastIndex = 0;

        beforeFinish = false;
    }

    private void resetPoints()
    {
        Destroy(lastPoints[0]);
        lastPoints.Clear();

        for (int i = 0; i < wayPoints.Count; i++)
        {
            if (wayPoints[i])
            {
                Destroy(wayPoints[i]);
            }
        }
        wayPoints.Clear();
    }

    private void randomNum()
    {
        randomSpot = Random.Range(0, target.Length);
        flyingRandom = false;
    }
    private void randomFly()
    {
        if (flyingRandom)
        {
            randomNum();
        }

        if (isRDflying)
        {
            Vector3 direct = target[randomSpot].position - transform.position;
            float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            transform.position = Vector2.MoveTowards(transform.position, target[randomSpot].position, speed * Time.deltaTime);
        }
    }


    void Update()
    {
        // 꽃이나 벌집에 들어가서 쉬는 중일 때
        if (isResting == false)
        {
            randomFly();

            // 웨이포인트가 1개 이상 있을 때 즉 그려진 경로가 있을 때
            if (wayPoints.Count >= 1)
            {
                followLine();
            }

            // 그리기 시작
            if (Input.GetMouseButton(0) && touchStartedOnPlayer == true && !beforeFinish)
            {
                drawLine();
            }

            // 그리기 끝
            if (Input.GetMouseButtonUp(0))
            {
                touchStartedOnPlayer = false;
                if (drawing == true && wayPoints.Count >= 1)
                {
                    lastIndex = wayPoints.Count - 1;

                    // 마지막 끝점 추가
                    GameObject newLastPoint = Instantiate(lastPoint, wayPoints[lastIndex].transform.position, Quaternion.identity);
                    lastPoints.Add(newLastPoint);
                }
                drawing = false;
            }

            // 꿀을 가진 상태
            if (carryingHoney == true && isResting == false)
            {
                NectarParticle.SetActive(true);
            }
        }
        // 쉬는 중이 아닐 때 해당 변수를 참으로 하여 나중에 다시 쉬게 됐을 때 랜덤한 값을 randomNum에서 다시 정할 수 있도록 조건 설정
        else
        {
            flyingRandom = true;
        }

        if ((collidingHive || collidingFlower) && (lastIndex != 0 && currentWayPoint == lastIndex))
        {
            // 꿀을 가지고 벌집 도착
            if (carryingHoney && collidingHive)
            {
                Score.score += 10;
                HoneyNumber.honey += 1;
                NectarParticle.SetActive(false);
                Audio.ScoreSoundPlay();
                anim.SetTrigger("Arrive");
                isResting = true;
                carryingHoney = false;
            }
            // 꿀 없이 꽃 도착
            if (!carryingHoney && collidingFlower)
            {
                Audio.ScoreSoundPlay();
                anim.SetTrigger("Arrive");
                isResting = true;
                carryingHoney = true;
            }                        

            if (wayPoints.Count >= 1)
            {
                lr.positionCount = 1;
                lr.enabled = false;
                resetPoints();
            }           
        }
    }

    // 경로를 그리는 함수
    private void drawLine()
    {
        drawing = true;
        Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        // 첫 점이면서 사이 거리가 1보다 클 때 또는 첫 점이 아니고 사이 거리가 0.6보다 클 때
        if ((wayIndex == 1 && Vector2.Distance(worldMousePos, transform.position) > 1f) ||
             (wayIndex > 1 && wayPoints[wayIndex - 2] && Vector2.Distance(worldMousePos, wayPoints[wayIndex - 2].transform.position) > .6f))
        {
            GameObject newWaypoint = Instantiate(wayPoint, worldMousePos, Quaternion.identity);
            wayPoints.Add(newWaypoint);
            lr.positionCount = wayIndex + 1;
            lr.SetPosition(wayIndex, newWaypoint.transform.position);
            wayIndex++;
        }
    }

    // 경로를 따라가는 함수
    private void followLine()
    {
        // 랜덤한 방향으로 이동시키는 randomFly 함수 내에서 각도를 설정하고 이동하는 부분이 실행되지 않도록 조건 설정
        isRDflying = false;

        // 해당 웨이포인트 지점이 있다면 그 지점으로 이동
        if (wayPoints[currentWayPoint])
        {
            Vector3 direct = wayPoints[currentWayPoint].transform.position - transform.position;
            float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis,
                rotationSpeed * Time.deltaTime);
            transform.rotation = rotation;
            transform.position =
                Vector2.MoveTowards(transform.position, wayPoints[currentWayPoint].transform.position, 3f * Time.deltaTime);

            // 해다 웨이포인트 지점에 도착했다면 해당 웨이 포인트는 제거하고 이전 라인렌더러의 위치를 옮겨서 지나간 자리는 지워진 것처럼 구현
            if (transform.position == wayPoints[currentWayPoint].transform.position)
            {
                if (currentWayPoint + 1 < wayPoints.Count)
                {
                    Destroy(wayPoints[currentWayPoint]);

                    lrCounter = currentWayPoint;
                    for (int p = 0; p < lrCounter; p++)
                    {
                        lr.SetPosition(p, lr.GetPosition(lrCounter));
                    }
                    currentWayPoint++;
                }
            }

            // 마우스를 뗀 경우
            if ((lastIndex != 0 || (lastIndex == 0 && wayPoints.Count == 1)) && transform.position == wayPoints[lastIndex].transform.position)
            {
                lr.positionCount = 1;
                Destroy(lastPoints[0]);
                lastPoints.Clear();
                lr.enabled = false;

                Destroy(wayPoints[lastIndex]);

                wayPoints.Clear();

                isRDflying = true;
            }

            // 마우스를 떼지 않은 경우, 마지막 웨이포인트에 도달했을 때 -> path를 계속 그리는 중이지만 벌이 마우스 위치(마지막 웨이포인트)까지 따라온 경우 
            if ((lastIndex == 0 && wayPoints.Count > 1) && transform.position == wayPoints[wayPoints.Count - 1].transform.position)
            {
                beforeFinish = true;
                lr.positionCount = 1;
                lr.enabled = false;

                Destroy(wayPoints[wayPoints.Count - 1]);
                wayPoints.Clear();
                isRDflying = true;
            }
        }
    }

    // 벌이 꽃이나 벌집에서 쉬고 난 뒤 다시 나타나는 애니메이션 끝에서 호출되는 함수
    void stopResting()
    {
        isResting = false;
        isRDflying = true;
        Audio.EmergeSoundPlay();
    }
}