using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Lv_MenuManager : MonoBehaviour
{
    public GameObject HintCanvas;
    public GameObject level2Lock;
    public GameObject level3Lock;

    GameManager gm;

    public int boolToInt(bool val)
    {
        if (val) return 1;
        else return 0;
    }

    public bool intToBool(int val)
    {
        if (val != 0) return true;
        else return false;
    }

    public void saveData()
    {
        PlayerPrefs.SetInt("lv3UnLocked", boolToInt(GuideBox2.lv3UnLocked));
        PlayerPrefs.SetInt("lv2UnLocked", boolToInt(Lv1_Manager.lv2UnLocked));
    }

    public void loadData()
    {
        GuideBox2.lv3UnLocked = intToBool(PlayerPrefs.GetInt("lv3UnLocked", 0));
        Lv1_Manager.lv2UnLocked = intToBool(PlayerPrefs.GetInt("lv2UnLocked", 0));
    }

    void Start()
    {
        gm = GameManager.instance;

        loadData();
        level2Lock.gameObject.SetActive(true);
        level3Lock.gameObject.SetActive(true);
        HintCanvas.gameObject.SetActive(false);
    }


    void Update()
    {
        if (Lv1_Manager.lv2UnLocked == true)
        {
            level2Lock.gameObject.SetActive(false);
        }

        if (GuideBox2.lv3UnLocked == true)
        {
            level2Lock.gameObject.SetActive(false);
        }
    }

    public void hint()
    {
        sfxManager.instance.buttonSoundPlay();
        HintCanvas.gameObject.SetActive(true);
    }

    public void quitHint()
    {
        sfxManager.instance.buttonSoundPlay();
        HintCanvas.gameObject.SetActive(false);
    }
}
