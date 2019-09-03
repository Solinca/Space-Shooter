using UnityEngine;

public class Bullet : MonoBehaviour
{
    private readonly float velocity = 500f;
    private readonly int damage = 5;
    public bool isPlayerBullet;

    private void Start()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.up * velocity;
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
