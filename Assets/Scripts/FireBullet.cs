using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation, GameObject.FindGameObjectWithTag("Canvas").transform);
        }
    }
}
