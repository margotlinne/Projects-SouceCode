using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SideBar : MonoBehaviour
{
    
    public static bool isDeleted;
    public static bool isActivated;
    public static bool toCheckPoint;

    public GameObject HintCanvas;
    public GameObject HintBtn;
    public GameObject ckptManager;
    public GameObject gemManager;

    private bool hintCanvasOn = false;
    private Animator anim;
    public GameObject rightArrow;
    public GameObject leftArrow;

    public bool isOut = false;
    public bool isIn = false;

    public static bool musicOnOffMuted;
    public bool effectOnOffMuted;

    public Image mutedEffect;
    public Image mutedMusic;
    public MusicManager musicManager;

    public bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;

    }
    public int boolToInt(bool val)
    {
        if (val)
            return 1;

        else
            return 0;
    }


    void Start()
    {
        isDeleted = false;
        anim = GetComponent<Animator>();
        isActivated = false;
        toCheckPoint = false;

        loadData();

        if (effectOnOffMuted == true)
        {
            mutedEffect.gameObject.SetActive(true);
        }
        else
        {
            mutedEffect.gameObject.SetActive(false);
        }
        if (musicOnOffMuted == true)
        {
            musicManager.musicStop();
            mutedMusic.gameObject.SetActive(true);
        }
        else
        {
            musicManager.musicPlay();
            mutedMusic.gameObject.SetActive(false);
        }
    }



    void Update()
    {
        if (isOut == true)
        {
            rightArrow.SetActive(false);
            leftArrow.SetActive(true);
          //  btnTxt.text = "¢∏";
        }

        if (isIn == true)
        {
            rightArrow.SetActive(true);
            leftArrow.SetActive(false);
           // btnTxt.text = "¢∫";
        }

        loadData();
    }


    // when press the btn at first it doesn't work hm....
    // because I made it "activated" true when isActivated is true which was opposite
    // and there's no delay if uncheck "Exit Time"
    public void SideBtn()
    {

        Audio.UISoundPlay();


        if (isActivated == true)
        {
            anim.SetBool("activated", false);
            isActivated = false;
        }
        else
        {
            anim.SetBool("activated", true);
            isActivated = true;

        }


    }

    public void Out()
    {
        if (!hintCanvasOn)
        {
            isOut = true;
            isIn = false;
        }
        
    }
    public void In()
    {
        if (!hintCanvasOn)
        {
            isIn = true;
            isOut = false;
        }
        
    }


    public void Restart()
    {
        Audio.UISoundPlay();
        SceneManager.LoadScene("Game");

    }

    public void effectBtn()
    {

        if (effectOnOffMuted == false)
        {
            Audio.UISoundPlay();
            effectOnOffMuted = true;
            mutedEffect.gameObject.SetActive(true);
        }
        else if (effectOnOffMuted == true)
        {
            effectOnOffMuted = false;
            mutedEffect.gameObject.SetActive(false);
        }
        saveData();
    }

    public void musicBtn()
    {
        if (musicOnOffMuted == false)
        {
            Audio.UISoundPlay();
            musicManager.musicStop();
            musicOnOffMuted = true;
            mutedMusic.gameObject.SetActive(true);
            //music off
        }
        else if (musicOnOffMuted == true)
        {
            Audio.UISoundPlay();
            musicManager.musicPlay();
            musicOnOffMuted = false;
            mutedMusic.gameObject.SetActive(false);
            // music on
        }
        saveData();
    }

    public void Delete()
    {
        Audio.UISoundPlay();

        // gem
        GemPoint.Point = 0;
        PlayerPrefs.SetInt("Point", GemPoint.Point);

        // checkpoint
        ckptManager.GetComponent<CheckPointManager>().deleteData();
        gemManager.GetComponent<GemManager>().deleteData();
        PlayerPrefs.DeleteKey("GemData");
        Debug.Log("ªË¡¶");
    }

    public void CheckPoint()
    {
        Audio.UISoundPlay();
        toCheckPoint = true;
    }


    public void HintOpen()
    {
        Audio.UISoundPlay();
        Audio.UISoundPlay();
        HintCanvas.SetActive(true);
        HintBtn.SetActive(false);
        hintCanvasOn = true;
    }

    public void HintClosed()
    {
        Audio.UISoundPlay();
        Audio.UISoundPlay();
        HintCanvas.SetActive(false);
        HintBtn.SetActive(true);
        hintCanvasOn = false;
    }

    void saveData()
    {
        PlayerPrefs.SetInt("Effect", boolToInt(effectOnOffMuted));

        PlayerPrefs.SetInt("Music", boolToInt(musicOnOffMuted));
    }

    void loadData()
    {
        effectOnOffMuted = intToBool(PlayerPrefs.GetInt("Effect", 0));
        musicOnOffMuted = intToBool(PlayerPrefs.GetInt("Music", 0));
    }

      
}
