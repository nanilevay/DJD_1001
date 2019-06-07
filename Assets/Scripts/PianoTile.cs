using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoTile : PressurePad
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxOffset = 9f;

    private Collider2D selfCollider;
    private Vector3    restPos;
    private Collider2D lastCollider;
    private float      resetTimer;

    private void Start()
    {
        restPos = transform.position;
        selfCollider = GetComponent<Collider2D>();
        resetTimer = 10000.0f;
    }

    private void FixedUpdate()
    {
        if (lastCollider != null)
        {
            if (!lastCollider.IsTouching(selfCollider))
            {
                resetTimer = 0.0f;
                lastCollider = null;
            }
            else
            {
                resetTimer = 0.0f;
            }
        }
        else
        {
            resetTimer += Time.deltaTime;
            if (resetTimer > 1.0f)
            {
                transform.position = Vector3.Lerp(transform.position, restPos, Time.deltaTime);
            }
        }
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (resetTimer < 1.0f) return;

        float velocityY = Mathf.Abs(other.relativeVelocity.y);

        if (velocityY < minSpeed) return;

        float totalOffset = maxOffset * velocityY / maxSpeed;

        Agent agent = other.collider.GetComponent<Agent>();
        if (agent)
        {
            transform.position = restPos + Vector3.down * totalOffset;
            lastCollider = other.collider;

            if (Mathf.Abs(velocityY) >= minForceToPress)
            {
                Spawn();
            }

            resetTimer = 0.0f;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        lastCollider = other.collider;
    }
}
