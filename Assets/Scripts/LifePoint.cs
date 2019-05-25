using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePoint : PickUp
{
    [SerializeField] private bool       shouldFloat;
    [SerializeField] private Collider2D pickUpTrigger;

    private Vector3     v0;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pickUpTrigger.enabled = false;
        v0 = rb.velocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shouldFloat)
        {
            if (rb.velocity.y <= -(v0.y / 4))
            {
                rb.gravityScale = 0;
                rb.drag = 8;
                pickUpTrigger.enabled = true;
            }
        }
    }

    protected override void SendPickUp(Agent agent)
    {
        agent.RecoverHP(valueToAdd);
    }
}
