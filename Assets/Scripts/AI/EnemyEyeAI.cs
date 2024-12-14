using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.IO;

public class EnemyEyeAI : MonoBehaviour
{
    public Transform target; // The target the enemy will follow
    public float speed = 200f; // Movement speed
    public float nextWaypointDistance = 3f; // Distance to the next waypoint before moving to the next one
    public float detectionRadius = 10f; // Detection radius for the enemy

    public Transform enemyGFX; // Graphic of the enemy to flip when changing direction

    Pathfinding.Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool targetDetected = false; // To check if the target is within the detection radius

    [SerializeField] private int damage = 1;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        // Check if the target is within the detection radius
        if (Vector2.Distance(rb.position, target.position) <= detectionRadius)
        {
            targetDetected = true;

            if (seeker.IsDone())
                seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
        else
        {
            targetDetected = false;
            path = null; // Reset path if the target moves out of detection range
        }
    }

    void OnPathComplete(Pathfinding.Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (targetDetected) // Follow the path only if the target is detected
            Follow();
    }

    void Follow()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Flip the enemy sprite based on movement direction
        if (force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the detection radius in the scene view for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
