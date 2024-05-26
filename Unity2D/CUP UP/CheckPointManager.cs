using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public GameObject[] ckpt;
    public int recentPoint;
    public GameObject Player;


    private void Start()
    {
        loadData();
    }

    private void Update()
    {
        saveData();      

        // spawn player to recent check point
        if (SideBar.toCheckPoint)
        {
            if(recentPoint != 0)
            {
                Player.transform.position = new Vector3
                    (ckpt[recentPoint - 1].GetComponent<CheckPoint>().playerPos.transform.position.x, ckpt[recentPoint - 1].GetComponent<CheckPoint>().playerPos.transform.position.y, -10);
            }
            
            SideBar.toCheckPoint = false;
        }
    }

    void saveData()
    {
        for (int i = 0; i < ckpt.Length; i++)
        {
            if (ckpt[i].GetComponent<CheckPoint>().isSaved)
            {
                recentPoint = i + 1;
                PlayerPrefs.SetInt("CheckPoint", recentPoint);
            }
        }
      
    }

    public void deleteData()
    {
        if(recentPoint != 0)
        {
            for (int i = 0; i < ckpt.Length; i++)
            {
                if (ckpt[i].GetComponent<CheckPoint>().isSaved)
                {
                    ckpt[i].GetComponent<CheckPoint>().isSaved = false;
                    ckpt[i].GetComponent<CheckPoint>().deletedData = true;
                }
            }

            recentPoint = 0;
            PlayerPrefs.SetInt("CheckPoint", recentPoint);
        }
       
    }

    void loadData()
    {
        recentPoint = PlayerPrefs.GetInt("CheckPoint");

        // to make plag moving animation starts when game restarted and it's already saved before
        if (PlayerPrefs.GetInt("CheckPoint") != 0)
        {
            for (int i = 0; i < recentPoint; i++)
            {
                ckpt[i].GetComponent<CheckPoint>().isSaved = true;
            }
        }
    }
}
