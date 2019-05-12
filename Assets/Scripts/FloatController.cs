using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float amplitude;
    private Vector3                posZero;
    private Vector3                tempPos;
    private Vector3                v0;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        v0 = rb.velocity;
    }

    void Update()
    {
        if (rb.velocity.y <= -(v0.y / 4))
        {
            rb.gravityScale = 0;
            rb.drag = 8;
        }
    }
}
