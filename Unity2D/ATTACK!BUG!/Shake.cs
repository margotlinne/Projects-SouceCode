using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{

    public static Shake instance;
    public Transform target;

    private float shakeTimeRemaining, shakePower, shakeFadeTime, shakeRotation;
    public float rotationMultiplier;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (target != null && shakeTimeRemaining <= 0)
        {
            transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
        }
    }

    private void LateUpdate()
    {
        if(shakeTimeRemaining > 0 && !GameManager.isCanvasOn)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-.1f, .1f) * shakePower;
            float yAmount = Random.Range(-.1f, .1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
        }

        transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));
    }

    public void StartShake(float length, float power)
    {
        shakeTimeRemaining = length;
        shakePower = power;

        shakeFadeTime = power / length;

        shakeRotation = power * rotationMultiplier;
    }
}


