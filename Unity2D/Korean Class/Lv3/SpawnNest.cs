using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNest : MonoBehaviour
{
    public Lv3Manager levelManager;
    public MainCamera cam;
    public VocaInput vocaInput;
    public GameObject Nest;
    public GameObject firstSpawnPosition;

    public List<GameObject> nestMade = new List<GameObject>();

    public int nestNum;
    public bool isMade;
    public bool removeDone;
    public int deleteNum;
    public int cameraCheck;
    public int wrongAnswerCount;
    

    void Start()
    {
        wrongAnswerCount = 0;
        removeDone = false;
        isMade = false;
        nestNum = 0;
    }

    private void setDeleteNum() 
    {
        for (int i = nestMade.Count - 1; i >= 0; i--)
        {
            // 가장 최근에 추가된 요소부터 검사하며 null이 아닌 오브젝트를 오답 시 지워질 요소로 지정
            if (nestMade[i] != null)
            {
                deleteNum = i;
                break;
            }
            else
            {
                continue;
            }
        }
        // 지울 둥지가 가장 첫 번째 둥지 오브젝트가 아닐 때, 즉 여러 개의 둥지가 쌓여 있는 경우
        if (deleteNum != 0)
        {
            for (int i = deleteNum - 1; i >= 0; i--)
            {
                /* 최신 둥지가 지워지고 나면 카메라가 바라볼 대상이 없어지므로 그 다음으로 지워질 대상을 찾아 카메라가 
                해당 오브젝트를 바라보게 함 */
                if (nestMade[i] != null)
                {
                    cameraCheck = i;
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
    }

    void Update()
    {
        if (Score.score < 0)
        {
            levelManager.GameOver();
        }

        if (vocaInput.isWrong == false && vocaInput.typeEnd == true)
        {
            // 이전에 만들어둔 둥지가 없는, 처음에 정답일 때는 가장 첫 번째 위치에 둥지 생성
            if (nestNum == 0)
            {
                // 둥지 오브젝트 인스턴스화
                GameObject newNest = (GameObject)Instantiate(Nest, firstSpawnPosition.transform.position, Quaternion.identity);
                nestMade.Add(newNest);
            }
            // 만들어진 둥지가 있는 경우 오답 시 지워질 둥지 오브젝트의 스폰 포지션에 둥지 성
            else if (nestNum > 0)
            {
                GameObject newNest = (GameObject)Instantiate(Nest, nestMade[deleteNum].GetComponent<Nest>().nestPoint.transform.position, Quaternion.identity);
                nestMade.Add(newNest);
            }            

            nestNum++;
            Score.score += 10;

            vocaInput.typeEnd = false;
            isMade = true;
            removeDone = false;
            cam.isgonnabeDeleted = false;

            setDeleteNum();
            
        }

        if (vocaInput.isWrong == true && vocaInput.typeEnd == true)
        {
            // 처음부터 오답일 때
            if (nestNum  == 0)
            {
                levelManager.GameOver();
                Score.score -= 5;
                vocaInput.typeEnd = false;
            }
            // 만들어진 둥지가 있을 때
            else if(nestNum > 0 && !removeDone)
            {
                cam.isgonnabeDeleted = true;

                setDeleteNum();

                nestMade[deleteNum].GetComponent<Nest>().animStart();

                if (nestMade[deleteNum].GetComponent<Nest>().nestDisappeared == true)
                {
                    Score.score -= 5;
                    Destroy(nestMade[deleteNum]);
                    nestNum--;

                    vocaInput.typeEnd = false;
                    isMade = false;
                    removeDone = true;
                }
            }
        }
    }
}

