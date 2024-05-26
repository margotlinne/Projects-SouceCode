using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarGenerator : MonoBehaviour
{
    
    public GameObject spawnPos;
    public GameObject sugarCube;
    GameObject cube;
    GameObject newCube;
    private float timer = 0;
    private float maxTime = 5f;
    private bool isFirstGenerated;
    private bool isBroken;

    Vector3 pos;
    void Start()
    {
        isFirstGenerated = false;
        isBroken = false;
    }

    void Update()
    {
        if (!isFirstGenerated)
        {
            spawnPos.transform.position = new Vector3(spawnPos.transform.position.x, spawnPos.transform.position.y, 70);
            newCube = Instantiate(sugarCube, spawnPos.transform.position, Quaternion.identity);
            newCube.transform.parent = this.gameObject.transform;

            cube = newCube;
            isFirstGenerated = true;
        }   

        if (cube.GetComponent<SugarCube>().broken)
        {
            cube.SetActive(false);
            isBroken = true;        
        }

        if (isBroken)
        {
            if (timer > maxTime)
            {
                GameObject nextCube = Instantiate(sugarCube, spawnPos.transform.position, Quaternion.identity);
                cube = nextCube;
                cube.SetActive(true);
                Destroy(newCube);
                timer = 0;
            }
            timer += Time.deltaTime;
            isBroken = false;
        }
    }
}
