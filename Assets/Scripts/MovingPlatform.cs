using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour
{
    public Transform posA, posB;  // Start and end positions
    public float Speed = 5f;     // Speed of movement
    private Vector2 targetPos;   // Current target position
    private Rigidbody2D rb;      // Reference to Rigidbody2D

    public GameObject player;    // Reference to the player object

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = posB.position; // Start by moving towards posB
    }

    void FixedUpdate()
    {
        // Check if the platform is close to the target position
        if (Vector2.Distance(transform.position, targetPos) < 0.05f)
        {
            // Toggle the target position when the platform reaches it
            targetPos = targetPos == (Vector2)posA.position ? posB.position : posA.position;
        }

        // Calculate the direction and set the velocity
        Vector2 direction = ((Vector2)targetPos - rb.position).normalized;
        rb.linearVelocity = direction * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);  // Parent the player to the platform
            player.GetComponent<PlayerController>().moveSpeed = player.GetComponent<PlayerController>().moveSpeed + 5; // Adjust player's speed if needed
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);  // Unparent the player
            player.GetComponent<PlayerController>().moveSpeed = 10; // Reset player's speed
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the path between posA and posB for visualization in the editor
        Gizmos.color = Color.yellow;
        if (posA != null && posB != null)
        {
            Gizmos.DrawLine(posA.position, posB.position);
        }
    }
}
