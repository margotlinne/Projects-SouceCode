using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 해당 프로젝트는 첫 번째로 제작해본 프로젝트이며, 코드에서 미숙한 부분이 보일 수 있습니다. **/

public class CameraResolution : MonoBehaviour
{
    void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)9 / 16);    // width / length
        float scalewidth = 1f / scaleheight;
        
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect;
    }
}