using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isPlayerBullet;

    private readonly float velocity = 500f;
    private readonly int damage = 5;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IShip ship = collision.GetComponent<IShip>();

        if (ship != null)
        {
            ship.GetDamaged(damage, isPlayerBullet);
        }

        Destroy(gameObject);
    }
}
