using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using System.IO;
using JetBrains.Annotations;
using System.Linq.Expressions;

public class HandEnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float patrolDistance = 5f;
    [SerializeField] private float detectionRadius = 10f;
    
    private bool targetDetected = false;
    private bool movingRight = true;

    private Rigidbody2D rb;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DetectPlayer();
        if (targetDetected)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        //Determining the patrol bounds 
        float leftBound = transform.position.x - patrolDistance;
        float rightBound = transform.position.x + patrolDistance;

        //move in the current direction
        if (movingRight)
        {
            rb.linearVelocity = new Vector2(speed, 0f);
            if (transform.position.x >= rightBound)
            {
                movingRight = false;
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0f);
            if (transform.position.x <= leftBound)
            {
                movingRight = true;
            }
        }
    }

    public void DetectPlayer()
    {
        if(Vector2.Distance(new Vector2(transform.position.x, 0f), new Vector2(target.position.x, 0f)) <= detectionRadius)
        {
            targetDetected = true;
        }
        else
        {
            targetDetected = false;
        }
    }

    void ChasePlayer()
    {
        //Move towards the player's position on X-axis
        float direction = Mathf.Sign(target.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(direction * speed, 0f);

        if(target.position.x == transform.position.x)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector2(transform.position.x - patrolDistance, transform.position.y),
                        new Vector2(transform.position.x + patrolDistance, transform.position.y));
    }

}
