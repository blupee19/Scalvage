using System.Collections;
using UnityEngine;

public class HeadEnemyAI : AIEnemyBase
{
    [SerializeField] private float rotationSpeed = 200f; // Rotation speed for the rolling effect
    private bool isKnockedBack = false;

    public override void Patrol()
    {
        if(isKnockedBack) return;
        // Patrol as usual, but add rotation for rolling effect
        float leftBound = startingPosition.x - patrolDistance;
        float rightBound = startingPosition.x + patrolDistance;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (movingRight)
        {
              
            rb.linearVelocity= new Vector2(speed, 0f);
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime); // Roll clockwise
            if (transform.position.x >= rightBound)
            {
                movingRight = false;
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0f);
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); // Roll counterclockwise
            if (transform.position.x <= leftBound)
            {
                movingRight = true;
            }
        }
    }

    public override void ChasePlayer()
    {
        if (isKnockedBack) return;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // Chase the player with rolling effect
        float direction = Mathf.Sign(target.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(direction * 13f, 0f);
        

        // Apply rolling based on movement direction
        if (direction > 0)
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime); // Roll clockwise when moving right
        }
        else if (direction < 0)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); // Roll counterclockwise when moving left
        }

        // Stop movement and rotation if the enemy reaches the player's position
        if (Mathf.Abs(target.position.x - transform.position.x) < 0.1f)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    public void ApplyKnockback()
    {
        isKnockedBack = true;
        StartCoroutine(ResetKnockback());
    }

    private IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(0.2f);
        isKnockedBack = false;
    }


}

