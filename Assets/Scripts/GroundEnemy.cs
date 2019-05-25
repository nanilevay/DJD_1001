using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : Agent
{
    [Header("Ground Enemy")]
    [SerializeField] Transform     wallSensor;
    [SerializeField] Transform     backSensor;
    [SerializeField] Transform     groundSensor;
    [SerializeField] Collider2D    damageSensor;
    [SerializeField] GameObject    deathFxPrefab;
    [SerializeField] private float frontChaseRange;
    [SerializeField] private float backChaseRange;
    [SerializeField] private float speedBoost;
    [SerializeField] private int   unitDamage;

    private float     moveDirect = 1.0f;
    private Transform target;

    private void FixedUpdate()
    {
        RaycastHit2D frontHit;
        RaycastHit2D backHit;

        frontHit = Physics2D.Raycast(wallSensor.position, transform.right * -1.0f,
                        frontChaseRange, LayerMask.GetMask("Agent"));

        backHit = Physics2D.Raycast(backSensor.position, transform.right,
            backChaseRange, LayerMask.GetMask("Agent"));

        if (!target)
        {
            if (frontHit)
            {
                Agent agent = frontHit.collider.GetComponent<Agent>();
                if (agent && agent.faction == Faction.Player)
                {
                    target = agent.transform;
                }
            }
            else if (backHit)
            {
                Agent agent = backHit.collider.GetComponent<Agent>();
                if (agent && agent.faction == Faction.Player)
                {
                    target = agent.transform;
                }
            }
        }
        else
        {
            // Check if there is range for a melee attack
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        Vector2 currentVelocity = rb.velocity;

        if (target)
        {
            Vector3 chaseDir = target.position - transform.position;

            currentVelocity.x = chaseDir.normalized.x * (moveSpeed + speedBoost);

            if (chaseDir.x < 0.0f) transform.rotation = Quaternion.identity;
            else transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            currentVelocity.x = moveDirect * moveSpeed;
            if (moveDirect < 0.0f) transform.rotation = Quaternion.identity;
            else transform.rotation = Quaternion.Euler(0, 180, 0);

            Collider2D tileCollider = Physics2D.OverlapCircle
                (groundSensor.position, 2.0f, LayerMask.GetMask("Ground"));
            if (tileCollider == null)
            {
                moveDirect = -moveDirect;
            }
            else
            {
                tileCollider = Physics2D.OverlapCircle
                    (wallSensor.position, 2.0f, LayerMask.GetMask("Ground"));
                if (tileCollider != null)
                {
                    moveDirect = -moveDirect;
                }
            }
        }

        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.ClearLayerMask();
        contactFilter.SetLayerMask(LayerMask.GetMask("Agent"));

        Collider2D[] results = new Collider2D[8];
        Collider2D collider;

        int nCollisions = Physics2D.OverlapCollider(damageSensor, contactFilter, results);
        if (nCollisions > 0)
        {
            for (int i = 0; i < nCollisions; i++)
            {
                collider = results[i];

                Agent character = collider.GetComponent<Agent>();
                if ((character) && (character.faction != faction))
                {
                    Vector2 hitDirection = (character.transform.position - transform.position).normalized;
                    hitDirection.y = 2.0f;

                    character.TakeHit(unitDamage, hitDirection);
                }
            }
        }

        rb.velocity = currentVelocity;

        base.Update();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(wallSensor.position, 2);
        Gizmos.DrawSphere(groundSensor.position, 2);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(backSensor.position, transform.right * backChaseRange);
        Gizmos.DrawRay(wallSensor.position, -1.0f * transform.right * frontChaseRange);
    }
}
