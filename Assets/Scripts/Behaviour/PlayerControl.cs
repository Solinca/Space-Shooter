using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private float speed = 250f;
    private Vector2 velocity;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (PauseMenu.isPaused)
        {
            return;
        }

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        velocity = input * speed;
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime);
    }
}
