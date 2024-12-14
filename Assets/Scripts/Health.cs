using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int currentHealth, maxHealth;
    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;
    public GameObject EnemyEye;
    [SerializeField] private bool isDead = false;
    [SerializeField] private float destroyDelay = 2f;
    private Rigidbody2D rb;
    public CircleCollider2D enemyCollider;

    [SerializeField] private float hitForce, hitTorque;

    private EnemyEyeAI enemyAI; 


    void Start()
    {
        rb = EnemyEye.GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyEyeAI>();
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
}