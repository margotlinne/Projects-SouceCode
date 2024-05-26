using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemPoint : MonoBehaviour
{
    public Text pointTxt;
    public static int Point = 0;

    private void Start()
    {
       Point = PlayerPrefs.GetInt("Point");


    }

    private void Update()
    {
        pointTxt.text = Point.ToString();
        PlayerPrefs.SetInt("Point", Point);
    }


    
}
