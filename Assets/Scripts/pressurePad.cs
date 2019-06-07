using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePad : SoundProducer
{
    private Rigidbody2D rb;
    float forceY;

    private bool IsPressed
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle
               (transform.position, 2.0f, LayerMask.GetMask("Agent"));

            return (collider != null);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (IsPressed) Debug.Log(IsPressed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 2);
        Gizmos.color = Color.yellow;
    }
}