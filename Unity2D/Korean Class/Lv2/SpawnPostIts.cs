using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPostIts : MonoBehaviour
{
    private float timer = 0;
    private float maxTime = 3;
    private float StickerTimer = 0;
    private float maxTimeForSticker = 4;

    public GameObject post_it;
    public GameObject[] Flowers;

    Vector2 screenHalfSizeWorldUnits;

    void Start()
    {
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);    
    }

    void Update()
    {
        if(RadialBar.hintReady == true)
        {
            maxTime = 1f;
        }
        else if (RadialBar.hintReady == false)
        {
            maxTime = 3f;
        }

        if (lv2Manager.lv2_Start == true && timer > maxTime)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-screenHalfSizeWorldUnits.x + .5f, screenHalfSizeWorldUnits.x - .5f), screenHalfSizeWorldUnits.y + 1f);
            GameObject newPostIts = (GameObject)Instantiate(post_it, spawnPosition, Quaternion.identity, GameObject.Find("Spawn Post-it Canvas").transform);
            timer = 0;
        }
        timer += Time.deltaTime;

        if(lv2Manager.lv2_Start == true && StickerTimer > maxTimeForSticker && RadialBar.hintReady == false)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-screenHalfSizeWorldUnits.x + .5f, screenHalfSizeWorldUnits.x - .5f), screenHalfSizeWorldUnits.y + 1f);
            GameObject newStickers = (GameObject)Instantiate(Flowers[Random.Range(0,6)], spawnPosition, Quaternion.identity, GameObject.Find("Spawn Post-it Canvas").transform);
            StickerTimer = 0;
        }
        StickerTimer += Time.deltaTime;
    }
}
