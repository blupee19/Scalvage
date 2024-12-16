using UnityEngine;

public class Spikes : MonoBehaviour
{
    private PlayerHealth health;
    public GameObject player;
    public Transform respawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            health.TakeDamage(1);
            player.transform.position = respawn.transform.position;
        }
    }
}
