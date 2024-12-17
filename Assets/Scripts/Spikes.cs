using UnityEngine;

public class Spikes : MonoBehaviour
{
    private PlayerHealth health; // Reference to the PlayerHealth component
    public GameObject player; // Assign this in the inspector
    public Transform respawn; // Assign the respawn point in the inspector

    // Start is called before the first frame update
    private void Start()
    {
        // Ensure the PlayerHealth component is fetched from the player GameObject
        if (player != null)
        {
            health = player.GetComponent<PlayerHealth>();
        }

        if (health == null)
        {
            Debug.LogError("PlayerHealth component not found on the player object!");
        }
    }

    // Trigger detection
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Check if the health component is valid before using it
            if (health != null)
            {
                health.TakeDamage(1);
            }
            else
            {
                Debug.LogError("PlayerHealth reference is missing!");
            }

            // Move player to the respawn position
            if (respawn != null)
            {
                player.transform.position = respawn.position;
            }
            else
            {
                Debug.LogError("Respawn point is not assigned!");
            }
        }
    }
}
