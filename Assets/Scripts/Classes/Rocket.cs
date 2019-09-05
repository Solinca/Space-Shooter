using UnityEngine;

public class Rocket : MonoBehaviour
{
    private readonly float velocity = 600f;
    private readonly int damage = 20;

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IShip ship = collision.GetComponent<IShip>();

        if (ship != null)
        {
            ship.GetDamaged(damage, true);
        }

        gameObject.SetActive(false);
    }
}
