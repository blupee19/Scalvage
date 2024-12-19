using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    public Transform posA, posB;
    public float speed = 7f; // Set a default speed
    private Vector2 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        if (posA == null || posB == null)
        {
            Debug.LogError("Positions posA and posB must be assigned in the inspector.");
            return;
        }

        // Start by moving towards posB
        targetPos = posB.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the trap towards the current target position
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // Check if the trap has reached the target position
        if (Vector2.Distance(transform.position, targetPos) < 0.01f)
        {
            // Switch target position
            targetPos = (targetPos == (Vector2)posA.position) ? posB.position : posA.position;
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the path between posA and posB in the editor
        if (posA != null && posB != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(posA.position, posB.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 6;
            collision.gameObject.GetComponent<PlayerController>().jumpForce = 20;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
            collision.gameObject.GetComponent<PlayerController>().jumpForce = 18;


        }
    }


}
