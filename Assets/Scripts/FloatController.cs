using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float amplitude;
//    [SerializeField] private float fallDrag;
    private Vector3                posZero;
    private Vector3                tempPos;
    private Vector3                v0;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        v0 = rb.velocity;
    }

    void FixedUpdate()
    {
        if (rb.velocity.y <= -(v0.y / 4))
        {
            rb.gravityScale = 0;
            rb.drag = 8;
 //           StartFloating();
        }
    }

    void StartFloating()
    {
        rb.gravityScale = Mathf.Sin(speed * Time.time) / 2;
    }
}
