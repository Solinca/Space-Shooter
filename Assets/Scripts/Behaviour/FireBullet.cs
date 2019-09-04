using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public Collider2D bullet;

    private Transform canvas;
    private readonly float distanceFromCenter = 40f;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !PauseMenu.isPaused && !WaveManager.Instance.isPaused)
        {
            Instantiate(bullet, transform.position + transform.up * distanceFromCenter, transform.rotation, canvas);
        }
    }
}
