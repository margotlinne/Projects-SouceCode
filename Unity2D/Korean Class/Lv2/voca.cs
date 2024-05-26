using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class voca : MonoBehaviour
{
    public Text vocaText;
    public int randomN;
    public bool isWrong = false;
    public Image image;
    public string txt;

    GameManager gm;


    void Start()
    {
        randomN = Random.Range(0, 2);
        gm = GameManager.instance;     
    }

    void Update()
    {      
        // 랜덤 수가 1로 나오면 오답 단어 리스트 중에 하나 선택
        if (randomN == 1)
        {
            txt = gm.vocaDataManager.wrongWord[Random.Range(0, gm.vocaDataManager.total)];
            vocaText.text = txt.ToString();
            isWrong = true;            
        }
        // 랜덤 수가 0으로 나오면 정답 단어 리스트 중에 하나 선택
        else if (randomN == 0)
        {
            txt = gm.vocaDataManager.correctWord[Random.Range(0, gm.vocaDataManager.total)];
            vocaText.text = txt.ToString();
            isWrong = false;           
        }
        // 변수 값을 -1로 설정해 단어가 무한대로 랜덤하게 선택되는 것 방지
        randomN = -1;


        // 정답/오답에 따라 색 변경
        if(isWrong == true)
        {
            if (RadialBar.hintReady == true)
            {
                image.color = new Color(0.817f, 0.298f, 0.462f, 1);
            }
            else if (RadialBar.hintReady == false)
            {
                image.color = new Color(0.3962264f, 0.3962264f, 0.3962264f, 1);
            }
        }

        if(isWrong == false)
        {
            if (RadialBar.hintReady == true)
            {
                image.color = new Color(0.3094339f, 0.7103096f, 0.7735849f, 1);
            }
            else if (RadialBar.hintReady == false)
            {
                image.color = new Color(0.3962264f, 0.3962264f, 0.3962264f, 1);
            }
        }
    }
}


    

