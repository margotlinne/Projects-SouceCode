using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 해당 프로젝트는 첫 번째로 제작해본 프로젝트이며, 코드에서 미숙한 부분이 보일 수 있습니다. **/

public class BackgroundMove : MonoBehaviour
{
    private MeshRenderer render;
    public GameManager gameManager;

    public float speed;
    private float offset;

    void Start()
    {
        render = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (!gameManager.isCanvasOn && Flycup.startOff)
        {
            offset += Time.deltaTime * speed;
            render.material.mainTextureOffset = new Vector2(0, offset);
        }               
    }
}
