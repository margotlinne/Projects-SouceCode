using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource BGM;

    public void ChangeBGM(AudioClip music)
    {
        if (!SideBar.musicOnOffMuted)
        {
            if (BGM.clip.name == music.name)
            {
                return;
            }
            BGM.Stop();
            BGM.clip = music;
            BGM.Play();
        }
     
    }

    public void musicStop()
    {
        BGM.Stop();
    }

    public void musicPlay()
    {
        BGM.Play();
    }
}
