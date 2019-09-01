using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            PlayerManager.Instance.AddCoin();
            Destroy(gameObject);
        }
    }
}
