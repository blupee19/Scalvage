using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using System.IO;

public class LegEnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 2f;
    public float detectionRadius = 10f;
    public float patrolDistance = 5f;
    public bool targetDetected = false;

    private Vector2 startingPosition;
    private bool movingLeft = true;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position; // Initialize patrol start position
    }

    private void FixedUpdate()
    {
        DetectPlayer();

        if (targetDetected)
        {
            MoveTowardsPlayer();
        }
        else
        {
            Patrol();
        }
    }

    void DetectPlayer()
    {
        // Check if the player is within the detection radius
        targetDetected = Vector2.Distance(transform.position, target.position) <= detectionRadius;
    }

    void MoveTowardsPlayer()
    {
        float direction = Mathf.Sign(target.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    void Patrol()
    {
        float leftBound = startingPosition.x - patrolDistance;
        float rightBound = startingPosition.x + patrolDistance;

        // Move in current direction
        if (movingLeft)
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            if (transform.position.x <= leftBound)
            {
                movingLeft = false; // Switch direction
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            if (transform.position.x >= rightBound)
            {
                movingLeft = true; // Switch direction
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector2(startingPosition.x - patrolDistance, transform.position.y),
                        new Vector2(startingPosition.x + patrolDistance, transform.position.y));
    }
}

