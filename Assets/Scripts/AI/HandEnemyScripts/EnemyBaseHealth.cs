using UnityEngine;

public class EnemyBaseHealth : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 20;
    protected int currentHealth;
    protected bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"{gameObject.name} initialized with {currentHealth} health.");
    }

    public virtual void TakeDamage(int damage, GameObject sender)
    {
        if (isDead) return;

        Debug.Log($"{gameObject.name} takes {damage} damage from {sender.name}.");

        currentHealth -= damage;
        if (currentHealth > 0)
        {
            OnHit(sender);
        }
        else
        {
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
}
