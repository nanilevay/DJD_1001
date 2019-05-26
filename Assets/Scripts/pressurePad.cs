using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePad : MonoBehaviour
{

    public AudioSource KeySound;
    private Rigidbody2D body;
    public float force;
    


    void Start()
    {
        KeySound = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "MainPlayer")
        {
            KeySound.Play();
            body.AddForce(transform.up * force);            
        }
    }


    

}
    

