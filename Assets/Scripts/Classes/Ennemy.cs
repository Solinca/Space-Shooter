using UnityEngine;

public class Ennemy : MonoBehaviour, IShip
{
    public Coin coin;
    private int health = 20;
    private Transform canvas;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    public void GetDamaged (int damage, bool isPlayerBullet)
    {
        health -= damage;

        if (health <= 0)
        {
            DestroyEnnemy(isPlayerBullet);
        }
    }

    private void DestroyEnnemy(bool isPlayerBullet)
    {
        if (isPlayerBullet)
        {
            PlayerManager.Instance.AddKill();
        }

        Instantiate(coin, transform.position, transform.rotation, canvas);
        Destroy(gameObject);
    }
}
