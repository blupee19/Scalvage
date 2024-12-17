using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.IO;
using System.Security.Cryptography;

public class EyeEnemyA1 : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float detectionRadius = 10f;

    public Transform eyeGFX;

    Pathfinding.Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool targetDetected = false;

    [SerializeField] private int damage = 1;

    Seeker seeker;
    Rigidbody2D rb;
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    // Update is called once per frame
    void UpdatePath()
    {
        //Check if the target is within the detection Radius
        if (Vector2.Distance(rb.position, target.position) <= detectionRadius)
        {
            targetDetected = true;

            if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
            else
            {
                targetDetected = false;
                path = null; //Reset the path if the target moves out of detection range
            }
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
        if (targetDetected);
        Follow();
    }

    void Follow()
    {
        if (path == null)
            return;
        if(currentWaypoint >= path.vectorPath.Count)
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

        //Flip the enemy sprite based on movement direction
        if(force.x >= 0.01f)
        {
            eyeGFX.localScale = new Vector3(-1f, 1f, 1f);
        }

        else if(force.x <= -0.01f)
        {
            eyeGFX.localScale = new Vector3(1f, 1f, 1f);
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
        if (collision.CompareTag("Player"))
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);

    }
}
