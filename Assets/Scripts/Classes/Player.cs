using UnityEngine;

public class Player : MonoBehaviour, IShip
{
    public void GetDamaged(int damage, bool isPlayerBullet)
    {
        PlayerManager.Instance.LoseHealth(damage);
    }
}
