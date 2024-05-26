using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstallingManager : MonoBehaviour
{
    public static bool onImage = false;
    public void ColicdedHive()
    {
        if(HiveNumber.newHive > 0)
        {
            onImage = true;
        }
        
        
    }

}
