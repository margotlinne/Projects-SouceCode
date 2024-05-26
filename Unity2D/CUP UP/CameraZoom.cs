using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraZoom : MonoBehaviour
{
    public static bool ZoomActive;

    public CinemachineVirtualCamera vCam;

    public float speed;

    public void LateUpdate()
    {
        // when it's in dark and narrow area
        if (ZoomActive)
        {
            vCam.m_Lens.OrthographicSize = Mathf.Lerp(vCam.m_Lens.OrthographicSize, 2.5f, speed);
        }
        else
        {
            vCam.m_Lens.OrthographicSize = Mathf.Lerp(vCam.m_Lens.OrthographicSize, 5, speed);
        }
    }
}
