using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePoint : PickUp
{
    [SerializeField] private bool       shouldFloat;
    [SerializeField] private Collider2D pickUpTrigger;
    [SerializeField] private float      timeAlive;

    private Vector3     v0;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private float liveTimer;
    private bool isFloating;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        pickUpTrigger.enabled = false;
        v0 = rb.velocity;
        liveTimer = timeAlive;
        isFloating = false;
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
                isFloating = true;
            }
        }

        if (isFloating)
        {
            if (liveTimer > 0.0f)
            {
                liveTimer -= Time.deltaTime;

                if (liveTimer < timeAlive / 4)
                {
                    sprite.enabled = (Mathf.FloorToInt
                        (liveTimer * (20 - liveTimer)) % 3) > 0;
                }                
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    protected override void SendPickUp(Agent agent)
    {
        agent.RecoverHP(valueToAdd);
    }
}
