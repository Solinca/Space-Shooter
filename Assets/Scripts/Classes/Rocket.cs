﻿using UnityEngine;

public class Rocket : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private readonly float velocity = 600f;
    private readonly int damage = 20;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.up * velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IShip ship = collision.GetComponent<IShip>();

        if (ship != null)
        {
            ship.GetDamaged(damage, true);
        }

        Destroy(gameObject);
    }
}