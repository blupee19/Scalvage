using UnityEngine;
using UnityEngine.Events;

public class EnemyBaseHealth : MonoBehaviour
{
    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;
    [SerializeField] protected int maxHealth = 20;
    protected int currentHealth;
    protected bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"{gameObject.name} initialized with {currentHealth} health.");
    }

    public virtual void GetHit(int damage, GameObject sender)
    {
        if (isDead) return;

        Debug.Log($"{gameObject.name} takes {damage} damage from {sender.name}.");

        currentHealth -= damage;
        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
            OnHit(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            Die();
        }
    }

    protected virtual void OnHit(GameObject sender)
    {
        Debug.Log($"{gameObject.name} hit by {sender.name}, remaining health: {currentHealth}");
    }

    protected virtual void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} has died!");
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }


}
