using UnityEngine;

public class DamageSystemEnemy : MonoBehaviour
{
    private PlayerHealth playerHealth;

    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth.TakeDamage(10);
        }
    }    
}
