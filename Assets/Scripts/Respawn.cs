using UnityEngine;

public class Respawn : MonoBehaviour
{

    public GameObject player;
    public GameObject respawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.position = respawnPoint.transform.position;
            other.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }
}