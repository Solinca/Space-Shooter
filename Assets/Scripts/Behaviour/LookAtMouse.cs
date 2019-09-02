using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private new Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (PauseMenu.isPaused || WaveManager.Instance.isPaused)
        {
            return;
        }

        var mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }
}
