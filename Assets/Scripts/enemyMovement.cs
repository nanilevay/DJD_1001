using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    // Declare Variables
    [SerializeField] float      movementSpeed;
    //[SerializeField] float      enemyFollowSpeed;
    [SerializeField] Transform  tileSensor;
    [SerializeField] Transform  tileWallSensor;
    [SerializeField] Collider2D damageSensor;
    

    Rigidbody2D rigidBody;
    float moveDirect = -1.0f;
    private Transform enemyTarget;


    // Start
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        enemyTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentVelocity = rigidBody.velocity;
        currentVelocity.x = moveDirect * movementSpeed;

        rigidBody.velocity = currentVelocity;

        // If position of target is smaller than 50 follow
        if (Vector2.Distance(transform.position, enemyTarget.position) < 50)
        {
            //Vector2.MoveTowards(from, to, speed)
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget.position, movementSpeed = Time.deltaTime);

            currentVelocity.x = moveDirect * movementSpeed;

            rigidBody.velocity = currentVelocity;
        }

        // If gizmos hits tile, turn enemy 180
        if (moveDirect < 0.0f) transform.rotation = Quaternion.identity;
        else transform.rotation = Quaternion.Euler(0, 180, 0);

    
        Collider2D tileCollider = Physics2D.OverlapCircle(tileSensor.position, 2.0f, LayerMask.GetMask("Ground"));
        if (tileCollider == null)
        {
            moveDirect = -moveDirect;
        }
        else
        {
            tileCollider = Physics2D.OverlapCircle(tileWallSensor.position, 2.0f, LayerMask.GetMask("Ground"));
            if (tileCollider != null)
            {
                moveDirect = -moveDirect;
            }
        } // End of enemy turning point
       
        
    }

    private void OnDrawGizmos()
    {
        if (tileSensor)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(tileSensor.position, 2.0f);
        }

        if (tileWallSensor)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(tileWallSensor.position, 2.0f);
        }
    }
}
