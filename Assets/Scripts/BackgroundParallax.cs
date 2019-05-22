using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public Camera masterCamera;
    public float parallaxScale = 0.1f;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 newPos = masterCamera.transform.position * parallaxScale;
        newPos.z = transform.position.z;
        transform.position = newPos;
           
    }

}