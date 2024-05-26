using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VocaDataManager : MonoBehaviour
{
    AllData datas;

    public TextAsset data;
    public List<string> wrongWord = new List<string>();
    public List<string> correctWord = new List<string>();
    public List<string> sgWord = new List<string>();
    public int total;

    private void Awake()
    {
        // json 파일 읽어오기
        datas = JsonUtility.FromJson<AllData>(data.text);
        total = datas.level1.Length;
    }

    // 단어의 양이 많으므로 해당 단어가 필요한 레벨에서만 리스트에 추가
     private void Start()
    {
       if (SceneManager.GetActiveScene().name == "Level2")
        {
            // datas나 datas.level1이 null인 경우 등의 예외 처리
            try
            {
                for (int i = 0; i < datas.level1.Length; i++)
                {
                    wrongWord.Add(datas.level1[i].wrong);
                    correctWord.Add(datas.level1[i].correct);
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError("An error occurred: " + ex.Message);
            }
        }
       else if (SceneManager.GetActiveScene().name == "Level3")
       {
            try
            {
                for (int i = 0; i < datas.level2.Length; i++)
                {
                    sgWord.Add(datas.level2[i].SG);
                }
            }
            catch (Exception ex) 
            {
                Debug.Log("An error occurred: " + ex.Message);
            }
       }
    }
}

[System.Serializable]
public class AllData
{
    public VocaData[] level1;
    public ConsonantData[] level2;
}

[System.Serializable]
public class VocaData
{
    public string wrong;
    public string correct;
}

[System.Serializable]
public class ConsonantData
{
    public string SG;
}