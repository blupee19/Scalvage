using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour
{
    public Transform posA, posB;  // Start and end positions
    public float Speed = 5f;     // Speed of movement
    private Vector2 targetPos;   // Current target position
    private Rigidbody2D rb;
    public bool playerOnPlatform = false;

    public Rigidbody2D playerrb;    // Reference to the player object

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = posB.position; // Start by moving towards posB
    }


    void FixedUpdate()
    {
        // Check if the platform is close to the target position
        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
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
            playerOnPlatform = true;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 6;
            collision.gameObject.GetComponent<PlayerController>().jumpForce = 20;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerOnPlatform = false;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
            collision.gameObject.GetComponent<PlayerController>().jumpForce = 18;

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
