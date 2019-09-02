using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private readonly float velocity = 500f;
    private readonly int damage = 5;
    private bool isPlayerBullet = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.up * velocity;
    }

    public void SetPlayerBullet(bool isPlayer)
    {
        isPlayerBullet = isPlayer;
    }

    public void ChangeImage(Sprite sprite)
    {
        Image image = GetComponent<Image>();
        image.sprite = sprite;
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
