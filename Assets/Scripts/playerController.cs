using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4000.0f;
    [SerializeField] private float jumpSpeed = 200.0f;
    [SerializeField] Collider2D groundCollider;
    [SerializeField] Collider2D airCollider;
    [SerializeField] Collider2D weapon;
    [SerializeField] private float mantleRayHeight = 10.0f;
    [SerializeField] private float climbForce;
    [SerializeField] Transform climbHandPos;
    [SerializeField] Transform wallCheck;

    Animator        animator;
    Rigidbody2D     rb;
    private Vector3 targetPoint;
    private float   hAxis;
    private bool    jumpPressed;
    private bool    attackPressed;
    private int     jumpCount;


    // Properties of player character
        // Is the player on ground
    private bool IsOnGround
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle
                (transform.position, 2.0f, LayerMask.GetMask("Ground"));

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
            Vector3 rayPosition;
            Vector3 rayOffset = new Vector3(0.0f, mantleRayHeight, 0.0f);
            rayPosition = rayOffset + wallCheck.position;
            RaycastHit2D hit = Physics2D.Raycast(rayPosition, 
                transform.right, 3.0f, LayerMask.GetMask("Ground"));

            return (!hit);
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
                jumpCount = 1;
            }
            else if (currentVelocity.y <= 0 && jumpCount == 1)
            {
                currentVelocity.y = jumpSpeed;
                jumpCount = 0;
            }
        }

        // If the player is in contact with a wall
        if (IsOnWall)
        {
            // Check for ledges
            if (CanMantle)
            {
                // Climb ledge
                MantleLedge();
            }
            
            // Wall Grab
        }
       
        groundCollider.enabled = IsOnGround;
        airCollider.enabled = !IsOnGround;

        rb.velocity = currentVelocity;
    }

    private void Update()
    {
        jumpPressed = Input.GetButton("Jump");
        hAxis = Input.GetAxis("Horizontal");
        attackPressed = Input.GetButtonDown("Fire1");
        Vector2 currentVelocity = rb.velocity;

        if ((hAxis < 0.0f) && (transform.right.x > 0.0f))
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if ((hAxis > 0.0f) && (transform.right.x < 0.0f))
        {
            transform.rotation = Quaternion.identity;
        }

        if (attackPressed)
            animator.SetTrigger("Attack");

        // Animator values
        animator.SetFloat("AbsVelocityX", Mathf.Abs(rb.velocity.x));
    }

    public void MantleLedge()
    {

        targetPoint = climbHandPos.position + new Vector3(5.0f * transform.right.normalized.x, 0.0f, 0.0f);

        // PROBLEMA AQUI NAO SEI ONDE, A POSICAO NAO ESTA A DAR LERP CORRETAMENTE
        // O SUPOSTO E PASSAR PELA HANDPOSITION E COLOCAR SE EM CIMA DO PROXIMO QUADRADO
        transform.position = Vector3.Lerp(transform.position, (targetPoint - transform.rotation * climbHandPos.localPosition), Time.deltaTime * climbForce);
        
        // Set animation for mantle ledge
    }
    public void ActivateHit()
    {
        weapon.enabled = !weapon.enabled;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 2);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(wallCheck.position, 2);
        Gizmos.DrawRay(wallCheck.position + new Vector3 
            (0.0f, mantleRayHeight, 0.0f), transform.right * 10.0f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(climbHandPos.position, 1.0f);
    }
}


