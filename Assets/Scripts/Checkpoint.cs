using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    AudioManager manager;
    [SerializeField] private PlayerHealth health;
    private Respawn respawn;
    public BoxCollider2D boxCollider;

    void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Respawn>();       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            respawn.respawnPoint = this.gameObject;
            boxCollider.enabled = false;
            manager.PlaySFX(manager.checkpont);
            health.currentHealth = 3f;

        }

    }
}
