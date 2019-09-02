using UnityEngine;

public class FireRocket : MonoBehaviour
{
    public Rocket rocket;
    private Transform canvas;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && !PauseMenu.isPaused && !WaveManager.Instance.isPaused && PlayerManager.Instance.CanShootRocket())
        {
            Instantiate(rocket, transform.position + transform.up * 40f, transform.rotation, canvas);
            PlayerManager.Instance.ConsumeOneRocket();
        }
    }
}
