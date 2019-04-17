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

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        isTemp = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.y <= 0.0f)
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
        tempPos.y += amplitude * Mathf.Sin(speed * Time.time);
        transform.position = tempPos;
    }
}
