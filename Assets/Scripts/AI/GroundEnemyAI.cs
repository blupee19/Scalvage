using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using System.IO;


public class GroundEnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 2f;
    public float patrolDistance = 5f;
    public float detectionRadius = 10f;

    [SerializeField] private int damage = 1;

    private Vector2 startingPosition;
    private bool movingRight = true;
    private bool targetDetected = false;
    private bool hasEmerged = false;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        animator = GetComponentInChildren<Animator>();    
    }


    void FixedUpdate()
    {        
        if (targetDetected)
        {
            animator.SetBool("NearPlayer", true);
            ChasePlayer();
        }
        else
        {
            animator.SetBool("NearPlayer", false);
            Patrol();
        }

        DetectPlayer();
    }

    void Patrol()
    {
        //Determine the patrol bounds
        float leftBound = startingPosition.x - patrolDistance;
        float rightBound = startingPosition.x + patrolDistance;

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
            if(transform.position.x <= leftBound)
            {
                movingRight = true;
            }
        }
    }
    
    void DetectPlayer()
    {
        //Check if the player is withing the detection radius
        if (Vector2.Distance(new Vector2(transform.position.x, 0f), new Vector2(target.position.x, 0f)) <= detectionRadius)
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector2(startingPosition.x - patrolDistance, transform.position.y),
                        new Vector2(startingPosition.x + patrolDistance, transform.position.y));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
