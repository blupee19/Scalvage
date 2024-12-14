using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int currentHealth = 10, maxHealth = 10;
    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;
    public GameObject EnemyEye;
    [SerializeField] private bool isDead = false;
    [SerializeField] private float destroyDelay = 2f;
    private Rigidbody2D rb;
    private Animator animator; 
    

    public Collider2D enemyCollider;
    //public Animator animator;

    [SerializeField] private float hitForce = 20f, hitTorque = 2.5f;

    private EnemyEyeAI enemyAI; 


    void Start()
    {
        rb = EnemyEye.GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyEyeAI>();
        animator = GetComponentInChildren<Animator>();
    }
    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;

        currentHealth -= amount;
        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            
            
            DisableAI();

            EnableGravity();
            RollAfterDeath(sender);
            Invoke(nameof(DestroyEnemy), destroyDelay);
        }


    }

    private void EnableGravity()
    {

        if (currentHealth <= 0)
        {
            rb.gravityScale = 9;
            enemyCollider.isTrigger = false;
            rb.freezeRotation = false;
            AnimationCalls();

        }
    }

    private void DestroyEnemy()
    {
        Destroy(EnemyEye);
    }

    private void RollAfterDeath(GameObject sender)
    {
        Vector2 hitDirection = (EnemyEye.transform.position - sender.transform.position).normalized;

        rb.AddForce(hitDirection * hitForce, ForceMode2D.Impulse);

        if(hitDirection.x > 0)
        {
            rb.AddTorque(-hitTorque, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddTorque(hitTorque, ForceMode2D.Impulse);
        }         
    }

    private void DisableAI()
    {
        enemyAI.enabled = false;              
    }

    private void AnimationCalls()
    {
        if (currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
        }
    }
}