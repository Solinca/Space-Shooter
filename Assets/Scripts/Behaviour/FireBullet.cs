using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private ObjectPoolManager objectPoolManager;
    private readonly float distanceFromCenter = 40f;

    private void Start()
    {
        objectPoolManager = ObjectPoolManager.Instance;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !PauseMenu.isPaused && !WaveManager.Instance.isPaused)
        {
            objectPoolManager.SpawnFromPool("Bullet", transform.position + transform.up * distanceFromCenter, transform.rotation);
        }
    }
}
