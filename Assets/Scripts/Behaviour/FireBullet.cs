using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public Bullet bullet;
    private Transform canvas;
    private new Collider2D collider;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !PauseMenu.isPaused && !WaveManager.Instance.isPaused)
        {
            Bullet bulletClone = Instantiate(bullet, transform.position + transform.up * 40f, transform.rotation, canvas);
            Physics2D.IgnoreCollision(bulletClone.GetComponent<Collider2D>(), collider);
            bulletClone.SetPlayerBullet(true);
        }
    }
}
