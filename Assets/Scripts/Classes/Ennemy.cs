using UnityEngine;
using UnityEngine.UI;

public class Ennemy : MonoBehaviour, IShip
{
    public Coin coin;
    public Slider HealthBar;
    private float health = 20f;
    private readonly float maxHealth = 20f;
    private Transform canvas;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        HealthBar.value = health;
    }

    public void GetDamaged (int damage, bool isPlayerBullet)
    {
        health -= damage;
        HealthBar.value = health / maxHealth;

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

        WaveManager.Instance.DestroyedEnnemy();

        Instantiate(coin, transform.position, Quaternion.identity, canvas);
        Destroy(gameObject);
    }
}
