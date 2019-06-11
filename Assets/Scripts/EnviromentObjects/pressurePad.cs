using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : SoundProducer
{
    [SerializeField] protected float minForceToPress;

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        Agent agent = other.collider.GetComponent<Agent>();
        if (agent)
            if (Mathf.Abs(other.relativeVelocity.y) >= minForceToPress)
            {
                Spawn();
            }
    }
}