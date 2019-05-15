﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4000.0f;
    [SerializeField] private float jumpSpeed = 200.0f;
    [SerializeField] Collider2D groundCollider;
    [SerializeField] Collider2D airCollider;

    Animator      animator;
    Rigidbody2D   rb;
    private float hAxis;
    bool          jumpPressed;

    // Properties of player character
    private bool IsOnGround
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle
                (transform.position, 2.0f, LayerMask.GetMask("Ground"));

            return (collider != null);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currentVelocity = rb.velocity;

        currentVelocity.x = moveSpeed * hAxis * Time.fixedDeltaTime;

        if (jumpPressed)
        {
            if (IsOnGround)
            {
                currentVelocity.y = jumpSpeed;
            }
        }

        groundCollider.enabled = IsOnGround;
        airCollider.enabled = !IsOnGround;

        rb.velocity = currentVelocity;
    }

    private void Update()
    {
        jumpPressed = Input.GetButtonDown("Jump");
        hAxis = Input.GetAxis("Horizontal");
        Vector2 currentVelocity = rb.velocity;

        if ((hAxis < 0.0f) && (transform.right.x > 0.0f))
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if ((hAxis > 0.0f) && (transform.right.x < 0.0f))
        {
            transform.rotation = Quaternion.identity;
        }

        animator.SetFloat("AbsVelocityX", Mathf.Abs(rb.velocity.x));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 2);
    }
}
