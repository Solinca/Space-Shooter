using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private readonly float speed = 350f;
    private Vector2 velocity;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        velocity = input * speed;
    }

    private void FixedUpdate()
    {
        if (WaveManager.Instance.isPaused)
        {
            return;
        }

        rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime);
    }
}
