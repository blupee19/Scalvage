using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Respawn respawn;
    private BoxCollider2D boxCollider;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Respawn>();       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            respawn.respawnPoint = this.gameObject;
            boxCollider.enabled = false;
        }
    }
}
