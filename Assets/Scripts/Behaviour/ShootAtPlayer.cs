using UnityEngine;
using UnityEngine.UI;

public class ShootAtPlayer : MonoBehaviour
{
    public Bullet bullet;
    public Slider HealthBar;

    private readonly float fireRate = 0.5f;
    private readonly float healthBarPosition = -40f;
    private float lastShot = 0f;
    private Transform canvas;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    private void FixedUpdate()
    {
        if (Time.time > fireRate + lastShot)
        {
            GameObject target = GameObject.Find("Player");

            transform.up = target.transform.position - transform.position;
            HealthBar.transform.rotation = Quaternion.identity;
            HealthBar.transform.position = transform.position + new Vector3(0, healthBarPosition);

            Instantiate(bullet, transform.position, transform.rotation, canvas);

            lastShot = Time.time;
        }
    }
}
