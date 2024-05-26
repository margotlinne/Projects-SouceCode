using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public SpawnNest spawnNest;
    private GameManager gm;

    public bool isgonnabeDeleted;


    void Start()
    {
        gm = GameManager.instance;
        isgonnabeDeleted = false;
    }

    void Update()
    {
        /* 만들어진 둥지 오브젝트가 하나밖에 없었는데 오답으로 해당 둥지가 없어지면 카메라가 바라볼 대상이 없으므로 
        첫 번째 둥지 오브젝트가 생성될 스폰 포지션을 바라보게 함 */
        if (gm.spawnNest.nestNum == 0)
        {
            transform.position = Vector3.Lerp(transform.position,
                new Vector2(0, spawnNest.firstSpawnPosition.transform.position.y + 3f), 1.5f * Time.deltaTime);
        }
        else if (gm.spawnNest.nestNum > 0)
        {
            transform.position = Vector3.Lerp(transform.position,
                new Vector2(0, spawnNest.nestMade[gm.spawnNest.cameraCheck].transform.position.y + 3f), 1.5f * Time.deltaTime);                
        }        
    }
}
