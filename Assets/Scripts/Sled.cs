using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sled : MonoBehaviour
{
    [SerializeField] LayerMask layer;

    const float skinWidth = .015f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    float horizontalRaySpacing;
    public float verticalRaySpacing;

    RaycastOrigins raycastOrigins;

    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 minSpeed;
    [SerializeField] Vector2 boostForce;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D collider2d;

    public bool start = false;
    public bool boost = true;

    public SpriteRenderer spriteRenderer;
    [SerializeField] float rayLength = 1.5f;

    Transform hitObject;
    Vector2 contactPoint;

    void Start()
    {
        collider2d = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;

        UpdateRaycastOrigins();
        CalculateRaySpacing();

        var groundCheck = DoubleRayControl();

        if (start)
            Move();
        else
            rb.velocity = new Vector2(0, rb.velocity.y);

        RotationFix(groundCheck);

        JitteringAvoid(position, groundCheck);
        rb.constraints = RigidbodyConstraints2D.None;
    }

    private void RotationFix(List<bool> groundCheck)
    {
        foreach (var check in groundCheck)
        {
            if (check) return;
        }

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void JitteringAvoid(Vector2 position, List<bool> groundCheck)
    {
        if (groundCheck[1])
            transform.position = new Vector2(transform.position.x, position.y);
    }

    private List<bool> DoubleRayControl() 
    { 
        Debug.DrawRay(raycastOrigins.topLeft + Vector2.right * verticalRaySpacing * 0, Vector2.up * -2);
        Debug.DrawRay(raycastOrigins.topLeft + Vector2.right * verticalRaySpacing * 1, Vector2.up * -2);
        Debug.DrawRay(raycastOrigins.topLeft + Vector2.right * verticalRaySpacing * 2, Vector2.up * -2);

        var leftHit = Physics2D.Raycast(raycastOrigins.topLeft + Vector2.right * verticalRaySpacing * 0, Vector2.up * -2, rayLength, layer);
        var middleHit = Physics2D.Raycast(raycastOrigins.topLeft + Vector2.right * verticalRaySpacing * 1, Vector2.up * -2, rayLength, layer);
        var rightHit = Physics2D.Raycast(raycastOrigins.topLeft + Vector2.right * verticalRaySpacing * 2, Vector2.up * -2, rayLength, layer);

        List<bool> list = new List<bool>();
        list.Add(leftHit);
        list.Add(middleHit);
        list.Add(rightHit);

        return list;
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


    void UpdateRaycastOrigins()
    {
        Bounds bounds = GetComponent<Collider2D>().bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = GetComponent<Collider2D>().bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitObject = collision.transform;
        contactPoint = collision.GetContact(collision.contactCount / 2).point;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        hitObject = collision.transform;
        contactPoint = collision.GetContact(collision.contactCount / 2).point;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        hitObject = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("puan gelsin");
    }
}
