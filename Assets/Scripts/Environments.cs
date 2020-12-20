using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environments : MonoBehaviour
{
    private Sled sled;
    private bool near;

    [SerializeField, Range(0, 35f)] float distance = 10f;
    [SerializeField] float speed;

    void Start()
    {
        near = false;
        sled = FindObjectOfType<Sled>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!near && Vector3.Distance(transform.position, sled.transform.position) < distance)
            near = true;

        if (near)
            Move();
    }

    private void Move()
    {
        transform.position += transform.right * -1 * speed * Time.deltaTime;
    }
}
