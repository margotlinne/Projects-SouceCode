using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    public GameObject[] gems;

    public static int gemTotal;
    //save
    public int[] gemNum;
    public string strArr;

    //load
    public string[] dataArr;
    public int[] gemData;


    private void Start()
    {
        loadData();

    }


    private void Update()
    {
        gemNum = new int[gems.Length];
        strArr = "";


        for (int i = 0; i < gems.Length; i++)
        {
            // 1 means true, gem is collected
            if (gems[i].GetComponent<Gem>().gemNumber == 1 || gemData[i] == 1)
            {

                gemNum[i] = 1;
                saveData(i);
                gems[i].SetActive(false);


            }
            else if (gems[i].GetComponent<Gem>().gemNumber == 0 || gemData[i] == 0)
            {
                gemNum[i] = 0;
                saveData(i);
                gems[i].SetActive(true);


            }
        }
    }
    void saveData(int i)
    {
        strArr += gemNum[i];
        if (i < gemNum.Length - 1)
        {
            strArr += ",";
        }
        PlayerPrefs.SetString("GemData", strArr);
    }

    void loadData()
    {

        dataArr = PlayerPrefs.GetString("GemData").Split(',');
        gemData = new int[dataArr.Length];
        for (int i = 0; i < dataArr.Length; i++)
        {
            // input string error sometimes show and sometiems not
            gemData[i] = System.Convert.ToInt32(dataArr[i]);
            // so instead of code "gems[i].setActive("false") here, used load data line 40, 48, "|| gemData[i] == 0" or "1"
            // but this caused an issue, when even try to delete data it still has this value so it recognises player already collected gem
        }
    }

    public void deleteData()
    {
        for (int i = 0; i < gems.Length; i++)
        {
            gems[i].GetComponent<Gem>().gemNumber = 0;
            // had removed but added this again
            gemNum[i] = 0;
            //***************** because I didn't changed data for load, it kept load previous data so it delete button seemed not working.
            gemData[i] = 0;
            // *****************
            saveData(i);
            gems[i].SetActive(true);
            // so it works now but when load game again it doesn't recognise as player already collected
        }
    }
}