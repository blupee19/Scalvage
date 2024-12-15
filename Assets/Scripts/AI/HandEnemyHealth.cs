using UnityEngine;
using UnityEngine.Events;

public class HandEnemyHealth : MonoBehaviour
{
    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;
    public GameObject HandEnemy;  // Reference to the hand enemy
    public Collider2D handCollider;  // Collider of the hand enemy

    [SerializeField] private int currentHealth = 10, maxHealth = 10;
    [SerializeField] private bool isDead = false;
    [SerializeField] private float destroyDelay = 2f;
    [SerializeField] private float hitForce = 20f;  // The recoil force when hit
    private Rigidbody2D rb;  // Rigidbody for applying forces    
    private HandEnemyAI handEnemyAI;  // Reference to the HandEnemyAI script

    void Start()
    {
        rb = HandEnemy.GetComponent<Rigidbody2D>();
        handEnemyAI = GetComponent<HandEnemyAI>();
    }

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }

    public void GetHit(int amount, GameObject sender)
    {
        // Debug: Check if the enemy is dead already
        if (isDead)
        {
            Debug.Log("Enemy is already dead. No damage can be applied.");
            return;
        }

        // Debug: Check if the sender is on the same layer as the enemy
        if (sender.layer == gameObject.layer)
        {
            Debug.Log("Sender is on the same layer. No damage applied.");
            return;
        }

        // Debug: Display the current health before applying damage
        Debug.Log("Before damage: Current Health = " + currentHealth);

        currentHealth -= amount;  // Reduce the health by the damage amount
        ApplyRecoil(sender);  // Apply recoil force in the opposite direction

        // Debug: Display the new health after damage
        Debug.Log("After damage: Current Health = " + currentHealth);

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;

            // Debug: Display when the enemy dies
            Debug.Log("Enemy is dead");

            // Disable AI, stop movement, etc.
            DisableAI();
            handCollider.isTrigger = false;  // Ensure collider is not a trigger on death
            //AnimationCalls();  // Trigger death animation

            Invoke(nameof(DestroyHand), destroyDelay);
        }
    }


    private void DestroyHand()
    {
        Destroy(HandEnemy);
    }

    private void DisableAI()
    {
        handEnemyAI.enabled = false;
    }
    

    private void ApplyRecoil(GameObject sender)
    {
        // Calculate the direction from the sender to the hand enemy (opposite of the hit direction)
        Vector2 hitDirection = (HandEnemy.transform.position - sender.transform.position).normalized;

        // Apply a recoil force in the opposite direction of the hit
        Vector2 recoilForce = hitDirection * hitForce;
        rb.AddForce(recoilForce, ForceMode2D.Impulse);
    }
}
