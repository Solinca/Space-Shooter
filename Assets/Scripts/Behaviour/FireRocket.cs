using UnityEngine;

public class FireRocket : MonoBehaviour
{
    private ObjectPoolManager objectPoolManager;
    private readonly float distanceFromCenter = 40f;

    private void Start()
    {
        objectPoolManager = ObjectPoolManager.Instance;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && !PauseMenu.isPaused && !WaveManager.Instance.isPaused && PlayerManager.Instance.CanShootRocket())
        {
            objectPoolManager.SpawnFromPool("Rocket", transform.position + transform.up * distanceFromCenter, transform.rotation);
            PlayerManager.Instance.ConsumeOneRocket();
        }
    }
}
