using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public Collider2D bullet;
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
            Collider2D bulletClone = Instantiate(bullet, transform.position + transform.up * 40f, transform.rotation, canvas);
            Physics2D.IgnoreCollision(bulletClone, collider);
        }
    }
}
