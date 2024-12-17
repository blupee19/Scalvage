using UnityEngine;
using UnityEngine.Events;

public class EnemyBaseHealth : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 10;
    protected int currentHealth;
    protected bool isDead = false;
    public bool canDamage = false;

    private HandEnemyAI handEnemy;   
    private Animator animator;
    
    [SerializeField] private int damage = 5;
    
    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"{gameObject.name} initialized with {currentHealth} health.");
        animator = GetComponentInChildren<Animator>();
        handEnemy = GetComponent<HandEnemyAI>(); 
    }

    void Update()
    {
        AnimationCalls();
    }

    public virtual void GetHit(int damage, GameObject sender)
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

    public virtual void OnHit(GameObject sender)
    {
        Debug.Log($"{gameObject.name} hit by {sender.name}, remaining health: {currentHealth}");
    }

    public virtual void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} has died!");
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        if ((collision.gameObject.CompareTag("Player") && canDamage))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }

    public virtual void AnimationCalls()
    {
        if (isDead)
        {
            animator.SetBool("isDead", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<PlayerHealth>().TakeDamage(1);

        if (collision.CompareTag("Projectile"))
        {
            currentHealth -= 5;
            if (currentHealth <= 0) 
                Die();            
        }
        if (isDead)
        {
            collision.enabled = false;
        }
        
    }   

}
