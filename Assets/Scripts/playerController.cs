using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private float  moveSpeed = 4000.0f;
    [SerializeField] private float  jumpSpeed = 200.0f;
    [SerializeField] Collider2D     groundCollider;
    [SerializeField] Collider2D     airCollider;

    Animator        animator;
    Rigidbody2D     rb;
    private float   hAxis;
    bool            jumpPressed;
    bool            attackPressed;
    int             jumpCount;
    private         Vector3 climbPos;

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

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.gameObject.layer == LayerMask.NameToLayer("Ledge")) && !(IsOnGround)) 
        {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                GetComponent<Rigidbody2D>().gravityScale = 0;
                climbPos = GetComponent<Transform>().position;
                GetComponent<Transform>().position = new Vector3(climbPos.x + 10, climbPos.y + 10, 0);
                GetComponent<Rigidbody2D>().gravityScale = 1;
  
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

        animator.SetFloat("AbsVelocityX", Mathf.Abs(rb.velocity.x));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 2);
    }
}
