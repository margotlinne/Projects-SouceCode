using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxManager : MonoBehaviour
{
    public static sfxManager instance = null;
    
    AudioSource audioSource;

    [HideInInspector]
    public AudioClip buttonClip;
    [HideInInspector]
    public AudioClip correctClip;
    [HideInInspector]
    public AudioClip wrongClip;
    [HideInInspector]
    public AudioClip stickerClip;

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
        audioSource = GetComponent<AudioSource>();
        buttonClip = Resources.Load<AudioClip>("DM-CGS-32");
        correctClip = Resources.Load<AudioClip>("DM-CGS-26");
        wrongClip = Resources.Load<AudioClip>("DM-CGS-03");
        stickerClip = Resources.Load<AudioClip>("DM-CGS-21");
    }

    public void buttonSoundPlay()
    {
        audioSource.PlayOneShot(buttonClip);
    }

    public void correctSoundPlay()
    {
        audioSource.PlayOneShot(correctClip);
    }

    public void wrongSoundPlay()
    {
        audioSource.PlayOneShot(wrongClip);
    }

    public void stickerSoundPlay()
    {
        audioSource.PlayOneShot(stickerClip);
    }

}
