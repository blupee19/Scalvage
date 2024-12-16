using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using System.IO;
using UnityEngine.Rendering;
using Unity.VisualScripting;


public class HandEnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 2f;
    public float increasedSpeed = 5f;
    public float patrolDistance = 5f;
    public float detectionRadius = 10f;
    public bool isEmerging = false;

    [SerializeField] private int damage = 1;

    private Vector2 startingPosition;
    private bool movingRight = true;
    private bool targetDetected = false;
    private bool canDamage = false;


    private Rigidbody2D rb;
    private Animator animator;
    private HandEnemyHealth handEnemyHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        animator = GetComponentInChildren<Animator>();
        handEnemyHealth = GetComponent<HandEnemyHealth>();
    }


    void FixedUpdate()
    {
        DetectPlayer();
        if (targetDetected)
        {
            // If the distance to the player is 4 units or less, trigger the animation
            float distanceToPlayer = Vector2.Distance(new Vector2(transform.position.x, 0f), new Vector2(target.position.x, 0f));
            if (distanceToPlayer <= 8f)
            {
                isEmerging = true;
            }
            else
            {
                isEmerging = false;
            }
            ChasePlayer();
        }
        else
        {
            // Stop chasing and return to patrol
            Patrol();
        }
        AnimationCalls();

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
            if (transform.position.x <= leftBound)
            {
                movingRight = true;
            }
        }
    }

    public void EnableDamage()
    {
        canDamage = true;
    }

    // Function called by the Animation Event to disable damage
    public void DisableDamage()
    {
        canDamage = false;
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

    public void AnimationCalls()
    {
        // Handle death animation
        if (handEnemyHealth.isDead)
        {
            animator.SetBool("isDead", true);
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // Handle proximity animation
        float distanceToPlayer = Vector2.Distance(new Vector2(transform.position.x, 0f), new Vector2(target.position.x, 0f));
        bool isNearPlayer = distanceToPlayer <= 8f;
           

        if (animator.GetBool("NearPlayer") != isNearPlayer) // Avoid redundant updates
        {
            animator.SetBool("NearPlayer", isNearPlayer);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && canDamage)
        {
            Debug.Log("damages player");
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }           
    }
   
}