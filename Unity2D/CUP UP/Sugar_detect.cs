using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugar_detect : MonoBehaviour
{
    public bool sugarDetected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sugarDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sugarDetected = false;
        }
    }
}
