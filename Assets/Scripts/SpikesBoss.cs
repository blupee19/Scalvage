using UnityEngine;

public class SpikesBoss : MonoBehaviour
{
    private PlayerHealth health; // Reference to the PlayerHealth component
    public GameObject player; // Assign this in the inspector
    public Transform respawn; // Assign the respawn point in the inspector
    public Respawn checkpoint;
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

            if (health.currentHealth <= 0)
            {
                health.RespawnPlayer();
            }

        }
    }
}
