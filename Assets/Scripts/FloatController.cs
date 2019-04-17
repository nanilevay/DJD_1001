using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float amplitude;
    private Vector3                posZero;
    private Vector3                tempPos;
    private bool                   isTemp;
    private Vector2                v0;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        isTemp = false;
        rb = GetComponent<Rigidbody2D>();
        v0 = rb.velocity;
    }

    void FixedUpdate()
    {
        if (rb.velocity.y <= -(v0.y/3.0f))
        {
            StartFloating();
        }
    }

    void StartFloating()
    {
        rb.velocity = Vector3.zero;

        if (!isTemp)
        {
            posZero = transform.position;
            isTemp = true;
        }
        tempPos = posZero;
        tempPos.y += amplitude * Mathf.Sin(Mathf.PI * speed * Time.fixedTime);
        transform.position = tempPos;
    }
}
