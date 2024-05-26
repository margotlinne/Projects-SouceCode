using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform GunPoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    void Update()
    {
        if(GameManager.isCanvasOn == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }

        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, GunPoint.position, GunPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(GunPoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
