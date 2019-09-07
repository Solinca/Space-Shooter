using UnityEngine;

public class Ennemy : MonoBehaviour, IShip
{
    public GameObject HealthBar;
    public GameObject Bar;

    private float health = 20f;
    private readonly float maxHealth = 20f;
    private bool isDead = false;
    private readonly float healthBarPosition = -40f;

    private void OnEnable()
    {
        isDead = false;
        health = maxHealth;
        Bar.transform.localScale = Vector3.one;
        HealthBar.transform.rotation = Quaternion.identity;
        HealthBar.transform.position = transform.position + new Vector3(0, healthBarPosition);
    }

    public void GetDamaged (int damage, bool isPlayerBullet)
    {
        health -= damage;
        Bar.transform.localScale = new Vector3(health / maxHealth, 1, 1);

        if (health <= 0 && !isDead)
        {
            isDead = true;
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
        ObjectPoolManager.Instance.SpawnFromPool("Coin", transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
