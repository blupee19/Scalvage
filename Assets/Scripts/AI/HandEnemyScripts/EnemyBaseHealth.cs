using UnityEngine;
using UnityEngine.Events;

public class EnemyBaseHealth : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 20;
    protected int currentHealth;
    protected bool isDead = false;
    protected bool canDamage = false;

    private HandEnemyAI handEnemy;   
    private Animator animator;
    private float animationStartRadius = 8f;
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
        if ((collision.gameObject.CompareTag("Player") && canDamage))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }

    void AnimationCalls()
    {
        if (Vector2.Distance(new Vector2(transform.position.x, 0f), new Vector2(handEnemy.target.position.x, 0f)) <= animationStartRadius) 
        { 
            canDamage = true;
            animator.SetBool("isNearPlayer", canDamage);
        }
        else
        {
            animator.SetBool("isNearPlayer", false);
        }

        if (isDead)
        { 
            canDamage = false;        
            animator.SetBool("isDead", true);
        }
    }


}
