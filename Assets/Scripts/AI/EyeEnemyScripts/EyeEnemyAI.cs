using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.IO;
using System.Security.Cryptography;

public class EyeEnemyA1 : MonoBehaviour
{
    public Transform target;
    public float speed = 2000f;
    public float nextWaypointDistance = 3f;
    public float detectionRadius = 25f;

    public Transform eyeGFX;

    Pathfinding.Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool targetDetected = false;
    [SerializeField] private float avoidRadius = 1.5f;


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
        AvoidOtherEnemies();
        if (targetDetected)
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
        Vector2 force = direction * speed * 2 * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        //Flip the enemy sprite based on movement direction
        if (force.x >= 0.01f)
        {
            eyeGFX.localScale = new Vector3(-Mathf.Abs(eyeGFX.localScale.x), eyeGFX.localScale.y, eyeGFX.localScale.z);
        }
        else if (force.x <= -0.01f)
        {
            eyeGFX.localScale = new Vector3(Mathf.Abs(eyeGFX.localScale.x), eyeGFX.localScale.y, eyeGFX.localScale.z);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the detection radius in the scene view for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void AvoidOtherEnemies()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, avoidRadius);
        foreach (var enemy in nearbyEnemies)
        {
            if (enemy.gameObject != gameObject && enemy.CompareTag("Enemy"))
            {
                Vector2 avoidDirection = (transform.position - enemy.transform.position).normalized;
                rb.linearVelocity += avoidDirection * speed * Time.deltaTime; // Move slightly away
            }
        }
    }
}
