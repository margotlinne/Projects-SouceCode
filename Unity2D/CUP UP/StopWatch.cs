using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour
{
    private float timer;
    private float seconds;
    private float minutes;
    private float hours;
    public string totalTime;

    public Text timerTxt;


    private void Start()
    {
        timer = 0;
       
    }

    private void Update()   
    {
        StopWatchFunc();
    }

    void StopWatchFunc()
    {
        timer += Time.deltaTime;


        seconds = (int)(timer % 60);
        minutes = (int)(timer / 60);
        hours = (int)(timer / 3600);

        timerTxt.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
        totalTime = timerTxt.text.ToString();
        
        PlayerPrefs.SetString("Time", totalTime);
    }
}
