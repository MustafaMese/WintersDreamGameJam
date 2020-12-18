using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sled : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 minSpeed;
    [SerializeField] Vector2 boostForce;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D collider2d;

    public bool start = false;
    public bool boost = true;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (start)
            Move();
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void Move()
    {
        float speedX = rb.velocity.x;
        if (speedX > maxSpeed.x)
            speedX = maxSpeed.x;
        else if (speedX < minSpeed.x)
            speedX = minSpeed.x;
        else
            Boost();

        rb.velocity = new Vector2(speedX, rb.velocity.y);
    }

    private void Boost()
    {
        if(boost)
            rb.AddForce(boostForce);
    }
}
