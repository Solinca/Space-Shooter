using UnityEngine;
using UnityEngine.UI;

public class ShootNearbyUnit : MonoBehaviour
{
    public Bullet bullet;
    private readonly float fireRate = 0.5f;
    private float lastShot = 0f;
    private Transform canvas;
    private new Collider2D collider;
    public Sprite bulletImage;
    public Slider HealthBar;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    private void FixedUpdate()
    {
        if (Time.time > fireRate + lastShot)
        {
            GameObject target = gameObject;
            float maxDistance = Mathf.Infinity;
            var ships = GameObject.FindGameObjectsWithTag("Ship");

            foreach (GameObject ship in ships)
            {
                if (ship.Equals(gameObject))
                {
                    continue;
                }

                Vector2 distance = ship.transform.position - transform.position;

                if (distance.sqrMagnitude < maxDistance * maxDistance)
                {
                    target = ship;
                    maxDistance = distance.magnitude;
                }
            }

            Vector2 direction = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
            transform.up = direction;
            HealthBar.transform.rotation = Quaternion.identity;
            HealthBar.transform.position = transform.position + new Vector3(0, -39f);

            Bullet bulletClone = Instantiate(bullet, transform.position, transform.rotation, canvas);
            Physics2D.IgnoreCollision(bulletClone.GetComponent<Collider2D>(), collider);
            bulletClone.ChangeImage(bulletImage);

            lastShot = Time.time;
        }
    }
}
