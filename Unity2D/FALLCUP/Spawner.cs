using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 해당 프로젝트는 첫 번째로 제작해본 프로젝트이며, 코드에서 미숙한 부분이 보일 수 있습니다. **/

public class Spawner : MonoBehaviour
{
    public float maxTime = 2;
    private float timer = 0;

    public GameObject[] items;
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
        	GameObject newItem = Instantiate(items[i]);
         	newItem.transform.position = transform.position + new Vector3(Random.Range(-2, 2), Random.Range(0, 10), 0);
        }       
    }

    void Update()
    {
        if (timer > maxTime)
        {
            for(int i = 0; i < 5; i++)
            {
        	    GameObject newItem = Instantiate(items[i]);
         	    newItem.transform.position = transform.position + new Vector3(Random.Range(-2, 2), Random.Range(0, 10), 0);
            }
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}