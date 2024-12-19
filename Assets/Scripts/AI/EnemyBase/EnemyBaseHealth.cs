using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBaseHealth : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 10;
    protected int currentHealth;
    protected bool isDead = false;
    public bool canDamage = false;
    AudioManager manager;

    private AIEnemyBase enemy;   
    private Animator animator;
    
    [SerializeField] private int damage = 5;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"{gameObject.name} initialized with {currentHealth} health.");
        animator = GetComponentInChildren<Animator>();
        enemy = GetComponent<AIEnemyBase>(); 
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
        manager.PlaySFX(manager.enemyHurt);
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
        Destroy(gameObject, 1f);
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
            gameObject.GetComponent<Collider2D>().enabled = false;
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
        
    }   

}
