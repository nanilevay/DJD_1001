using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    [SerializeField] private float jumpRayLenght;
    [SerializeField] private float wallJumpSpeed;
    private RaycastHit             hit;
    private LayerMask              layer;
    private Rigidbody2D            rb;


    private void Start()
    {
        layer = 0 << 9;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 foward = transform.right * jumpRayLenght;
        Debug.DrawRay(transform.position, foward, Color.blue);
        Physics.Raycast(transform.position, foward, out hit, 30);

        if (hit.collider != null && Input.GetButton("Jump"))
        {
            rb.velocity += wallJumpSpeed * Vector2.up;
        }
    }
}
