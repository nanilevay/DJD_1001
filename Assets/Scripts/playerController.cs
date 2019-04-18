using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]private float speed;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xaxis = Input.GetAxis("Horizontal") * speed;

        Vector2 VelocityX = rb.velocity;
        VelocityX.x = xaxis;
        rb.velocity = VelocityX;

        if (rb.velocity.x < 0.0f)
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if (rb.velocity.x > 0.0f)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        //lock move speed  
        if (rb.velocity.magnitude > speed)
        {
            VelocityX.x *= speed / rb.velocity.magnitude;
            rb.velocity = VelocityX;
        }
    }
}
