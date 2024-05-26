using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{ 
    /* 아래의 클래스 변수들은 다른 여러 군데의 스크립트에서 해당 클래스의 데이터에 접근하고자 하여 static으로 구현하거나 각 싱글톤으로 만드는 대신 
    게임 매니저에서 관리하도록 함 */
    public VocaDataManager vocaDataManager;
    public Lv_MenuManager lvManager;
    public SpawnNest spawnNest;
    public SpawnAlphabets spawnAlphabets;

    // 싱글톤 구현
    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }       
    }

    void Start()
    {
        lvManager.loadData();
        SetResolution();
    }

    public void GoToLevelMenu()
    {
        sfxManager.instance.buttonSoundPlay();
        SceneManager.LoadScene("Level_Menu");
    }

    public void Quit()
    {
        sfxManager.instance.buttonSoundPlay();
        Application.Quit();
    }

    public void GameInfo()
    {
        sfxManager.instance.buttonSoundPlay();
        SceneManager.LoadScene("Information");
    }

    public void BackToMenu()
    {
        sfxManager.instance.buttonSoundPlay();
        SceneManager.LoadScene("Menu");
    }

    public void Level1()
    {
        sfxManager.instance.buttonSoundPlay();
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        if (Lv1_Manager.lv2UnLocked == true)
        {
            sfxManager.instance.buttonSoundPlay();
            SceneManager.LoadScene("Level2");
        }
    }

    public void Level3()
    {
        if(GuideBox2.lv3UnLocked==true)
        {
            sfxManager.instance.buttonSoundPlay();
            SceneManager.LoadScene("Level3");
        }
    }

    public void SetResolution()
    {
        int setWidth = 540;
        int setHeight = 960;

        Screen.SetResolution(setWidth, setHeight, true);
    }
}
