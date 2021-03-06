﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : Agent
{
    [Header("Player")]
    [SerializeField] private float knockBackDuration;
    [SerializeField] float         wallDragForce;
    [SerializeField] private float jumpSpeed = 200.0f;
    [SerializeField] private int   hitDamage;
    [Space]
    [SerializeField] Collider2D    groundCollider;
    [SerializeField] Collider2D    airCollider;
    [SerializeField] Collider2D    weapon;
    [SerializeField] Transform     wallCheck;
    [SerializeField] Transform     mantleCheckPos;
    public ParticleSystem          walkingDust;

    [Header("Player Animator Variables")]
    [SerializeField] private float landingDistance;
    [SerializeField] private float minVelocityToLand;

    Animator             animator;
    private float        hAxis;
    private bool         jumpPressed;
    private bool         doubleJump;
    private bool         attackPressed;
    private float        knockBackTimer;
    private float        attackTimer;
    private float        attackDuration = 0.25f;
    private RaycastHit2D mantleHit;

    [Header("Life Points")]
    // Variables having to do with HP instantiate
    [SerializeField] private GameObject lifePointPrefab;
    [SerializeField] private float      lifePointShootSpeed;
    [SerializeField] private Transform  lifePointShootPoint;

    // Properties of player character
    // Is the player on ground
    private bool IsOnGround
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle
                (transform.position, 2.5f, LayerMask.GetMask("Ground"));

            return (collider != null);
        }
    }

    // Is the player on a wall
    private bool IsOnWall
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle
                (wallCheck.position, 2.0f, LayerMask.GetMask("Ground"));
            return (collider != null);
        }
    }

    // Is the player Capable of Mantling
    private bool CanMantle
    {
        get
        {
            mantleHit = Physics2D.Raycast(mantleCheckPos.position,
                transform.up * -1, 35, LayerMask.GetMask("Ground"));

            if (Mathf.Abs(mantleHit.point.y - mantleCheckPos.position.y) > 21)
                return mantleHit;
            else
                return false;
        }
    }

    private bool LandDistance
    {
        get
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position,
                transform.up * -1, 60, LayerMask.GetMask("Ground"));
            float distance = Mathf.Abs(hit.point.y - transform.position.y);

            if (rb.velocity.y < -minVelocityToLand)
            {
                Debug.Log(rb.velocity.y);
                return (distance < landingDistance);
            }
            else return false;
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();

        LvlManager.instance.SetPlayerMaxHP(maxHP);
        LvlManager.instance.SetPlayerCurrentHP(currentHP);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (knockBackTimer <= 0.0f)
        {

            rb.gravityScale = 1;
            Vector2 currentVelocity = rb.velocity;

            currentVelocity.x = moveSpeed * hAxis * Time.fixedDeltaTime;

            if (jumpPressed)
            {
                jumpPressed = false;
                currentVelocity.y = jumpSpeed;
            }
            else if (jumpPressed && IsOnWall)
                rb.drag = 0;

            // If the player is in contact with a wall
            if (IsOnWall && Mathf.Abs(hAxis) > 0.1)
            {
                if (!IsOnGround)
                {
                    rb.drag = wallDragForce;
                    doubleJump = false;
                }
                // Check for ledges
                if (CanMantle)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0;
                    animator.SetBool("CanClimb", CanMantle);
                }
            }
            else
            {
                rb.drag = 0;
            }

            rb.velocity = currentVelocity;
        }
        else
        {
            rb.gravityScale = 2.0f;
        }

        animator.SetBool("Land", LandDistance);
        // Collisions
        groundCollider.enabled = IsOnGround;
        airCollider.enabled = !IsOnGround;
    }

    protected override void Update()
    {
        if (currentHP < 0) return;

        hAxis = Input.GetAxis("Horizontal");
        attackPressed = Input.GetButtonDown("Fire1");

        if (IsOnGround) doubleJump = false;

        if (IsOnGround && Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
        else if (Input.GetButtonDown("Jump") && !doubleJump)
        {
            jumpPressed = true;
            doubleJump = true;
        }

        if ((hAxis < 0.0f) && (transform.right.x > 0.0f))
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if ((hAxis > 0.0f) && (transform.right.x < 0.0f))
        {
            transform.rotation = Quaternion.identity;
        }

        if (attackPressed && attackTimer <= 0.0f)
        {
            animator.SetTrigger("Attack");
            attackTimer = attackDuration;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }

        // Animator values
        animator.SetFloat("AbsVelocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("VelocityY", rb.velocity.y);
        animator.SetBool("IsOnGround", IsOnGround);

        base.Update();

        if (knockBackTimer > 0.0f)
        {
            knockBackTimer -= Time.deltaTime;
        }

        var emission = walkingDust.emission;

        emission.enabled = IsOnGround;
    }

    public override void TakeHit(int nDamage, Vector2 hitDirection)
    {
        if (IsInvulnerable) return;

        EZCameraShake.CameraShaker.Instance.ShakeOnce(2.0f, 2.0f, 0.05f, 0.2f);

        currentHP -= nDamage;
        LvlManager.instance.SetPlayerCurrentHP(currentHP);
        if (currentHP < 0)
        {
            OnDie();
        }
        else
        {
            IsInvulnerable = true;

            for (int i = 0; i < nDamage; i++)
                ReleaseLife();
            OnHit(hitDirection);
        }
    }

    protected override void OnHit(Vector2 hitDirection)
    {
        knockBackTimer = knockBackDuration;
        rb.velocity = hitDirection * jumpSpeed * 0.75f;
    }

    void ReleaseLife()
    {
        float finalSpeed;
        finalSpeed = Random.Range
            (lifePointShootSpeed - 100, lifePointShootSpeed);

        GameObject newLifePoint = Instantiate(lifePointPrefab,
            lifePointShootPoint.position, lifePointShootPoint.rotation);

        Rigidbody2D rb = newLifePoint.GetComponent<Rigidbody2D>();
        rb.velocity = finalSpeed * newLifePoint.transform.up +
            finalSpeed / Random.Range(3, 6) * Vector3.right * Random.Range(-1, 2);
    }

    public void MantleLedge()
    {
       
        transform.position = mantleHit.point;
        rb.gravityScale = 1;
        animator.SetBool("CanClimb", false);
        
    }
    public void ActivateHit()
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.ClearLayerMask();
        contactFilter.SetLayerMask(LayerMask.GetMask("Agent") | LayerMask.GetMask("Spawners"));

        Collider2D[] results = new Collider2D[2];
        Collider2D collider;

        int nCollisions = Physics2D.OverlapCollider(weapon, contactFilter, results);
        if (nCollisions > 0)
        {
            for (int i = 0; i < nCollisions; i++)
            {
                collider = results[i];

                Spawner spawner = collider.GetComponent<Spawner>();
                Agent agent = collider.GetComponent<Agent>();
                if ((agent) && (agent.faction != faction))
                {
                    Vector3 hitDir = (agent.transform.position - transform.position).normalized;
                    agent.TakeHit(hitDamage, hitDir);
                }
                if (spawner)
                {
                    spawner.Spawn();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        //  gizmos help
        Ray ray = new Ray(mantleCheckPos.position,
                    transform.up * -1);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 2);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(wallCheck.position, 2);
        Gizmos.DrawRay(ray.origin, ray.direction * 35);
    }

    protected override void OnDie()
    {
        LvlManager.instance.LoseLife();
    }
}