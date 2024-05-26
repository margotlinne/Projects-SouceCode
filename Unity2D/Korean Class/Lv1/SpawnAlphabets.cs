using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAlphabets : MonoBehaviour
{
    public int total;
    public int check;
    public int max;
    public int[] record = { -1 };
    public int emptyPos;

    public GameObject[] Alphabets;
    public GameObject[] SpawnPositions;

    private int index;
    private int count;
    private int randomNum;

    

    void Start()
    {
        emptyPos = 0;
        total = 0;
        max = 0;
        check = 0;
        count = 0;
        index = 0;
    }

    void Update()
    {
        // 총 생성된 자모(max)가 24보다 작으면서 화면에 나타날 자모 숫자(check)가 11보다 작을 때 생성
        if (GuideBox.lv1_Start == true && check < 11 && max < 24)
        {            
            // 자모가 24개이므로 사이의 값 중 랜덤하게 하나 선택
            randomNum = Random.Range(0, 24);
            for(int i = 0 ;  i< 24 ; )
            { 
                // 선택된 랜덤한 숫자가 이미 생성된 적이 있다면
                if(randomNum == record[i])
                {
                    // 다시 생성 후 i를 0으로 초기화하여 for문 계속
                    randomNum = Random.Range(0, 24);
                    i = 0;
                    continue;
                }
                else
                {
                    // 마지막까지 확인했는데 중복된 숫자가 없다면
                    if(i == 23)
                    {
                        // 기록 배열에 추가
                        record[index] = randomNum;
                        index++;
                        break;
                    }
                    // 아직 마지막까지 확인 안 했다면 계속해서 확인
                    else
                    {
                        i++;
                        continue;
                    }
                    
                }
            }

            sfxManager.instance.buttonSoundPlay();

            // 해당 랜덤한 숫자 순서의 자모 요소를 인스턴스화
            GameObject newAlphabet = Instantiate(Alphabets[randomNum]);

            // 초반 11개는 순서대로 빈 자리에 배치
            if(count >= 10)
            {
                Debug.Log(emptyPos);
                newAlphabet.transform.position = SpawnPositions[emptyPos].transform.position;
                newAlphabet.GetComponent<Alphabet>().pos = emptyPos;
            }
            // 그 후부터 정답을 맞추어 사라진 자모의 자리에 새 자모 배치
            else
            {
                newAlphabet.transform.position = SpawnPositions[count].transform.position;
                newAlphabet.GetComponent<Alphabet>().pos = count;
                count++;
            }

            check++;
            max++;
        }
    }
}
