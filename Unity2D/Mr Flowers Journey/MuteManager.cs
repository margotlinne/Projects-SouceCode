using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 해당 프로젝트는 초창기 제작된 프로젝트로, 코드에서 미숙한 부분이 보일 수 있습니다. **/

public class MuteManager : MonoBehaviour
{
    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    private bool muted = false;


    private void Start()
    {
        if(!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
        }
        else
        {
            Load();
        }

        UpdateButtonIcon();
        AudioListener.pause = muted;
    }



    public void OnButtonPress()
    {
        if(muted == false)
        {
            Audio.UISoundPlay();
            muted = true;
            AudioListener.pause = true;
        }

        else
        {
            Audio.UISoundPlay();
            muted = false;
            AudioListener.pause = false;
        }

        Save();
        UpdateButtonIcon();
    }


    private void UpdateButtonIcon()
    {
        if(muted==false)
        {
            soundOnIcon.enabled = true;
            soundOffIcon.enabled = false;
        }

        else
        {
            soundOnIcon.enabled = false;
            soundOffIcon.enabled = true;
        }
    }


    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;

    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }

}
