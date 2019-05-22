using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePoint : MonoBehaviour
{
    [SerializeField] private bool ShouldFloat = true;
    private bool isFloating = false;
    private Vector3 v0;
    Rigidbody2D rb;

    private bool HealthPickUp
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle
                (transform.position, 3.0f, LayerMask.GetMask("Player"));

            return (collider != null);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        v0 = rb.velocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ShouldFloat)
        {
            if (rb.velocity.y <= -(v0.y / 4))
            {
                rb.gravityScale = 0;
                rb.drag = 8;
                isFloating = true;
            }
        }

        if (HealthPickUp && isFloating)
        {
            // Get-s picked up by the player
            Destroy(gameObject);
        }
    }

    public override string ToString()
    {
        return "Picked Up";
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 3.0f);
    }
}
