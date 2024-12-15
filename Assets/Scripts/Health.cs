using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{    
    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;
    public GameObject EnemyEye;
    //public GameObject blood;
    public Collider2D enemyCollider;


    [SerializeField] private int currentHealth = 10, maxHealth = 10;
    [SerializeField] private bool isDead = false;
    [SerializeField] private float destroyDelay = 2f;
    [SerializeField] private float hitForce = 20f, hitTorque = 2.5f;
    private Rigidbody2D rb;
    private Animator animator;    
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

        ApplyRecoil(sender);

        if (currentHealth > 0)
        {
            //BloodSplash(); // Trigger blood splash on hit
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;

            //BloodSplash(); // Trigger blood splash on death
            DisableAI();
            EnableGravity();
            RollAfterDeath(sender);
            AnimationCalls();
            Physics2D.IgnoreLayerCollision(10,11,true);

            Invoke(nameof(DestroyEnemy), destroyDelay);
        }
    }


    //private void DestroyBloodParticles()
    //{
    //    if (blood != null)
    //    {
    //        Destroy(blood, 2f); // Delay destruction of the blood particles
    //    }
    //}

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

    private void AnimationCalls()
    {
        if (currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
        }
    }

    private void ApplyRecoil(GameObject sender)
    {
        Vector2 hitDirection = (EnemyEye.transform.position - sender.transform.position).normalized;

        Vector2 recoilForce = hitDirection * 5f;
        rb.AddForce(recoilForce, ForceMode2D.Impulse);
    }

    //private void BloodSplash()
    //{
    //    // Instantiate the blood particle prefab at the current position
    //    GameObject instantiatedBlood = Instantiate(blood, transform.position, Quaternion.identity);

    //    // Destroy the instantiated blood particle after 2 seconds
    //    Destroy(instantiatedBlood, 2f);
    //}

}