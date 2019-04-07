using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatController : MonoBehaviour
{
    public float speed;
    public float amplitude;
    private Vector3 tempPos;
    private float y0;

    // Start is called before the first frame update
    void Start()
    {
        float y0 = transform.position.y;
    }

    void Update()
    {
        tempPos.y = y0 + amplitude * Mathf.Sin(speed * Time.time);
        transform.position = tempPos;
    }
}
