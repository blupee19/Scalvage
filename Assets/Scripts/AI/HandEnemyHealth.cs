using UnityEngine;
using UnityEngine.Events;

public class HandEnemyHealth : MonoBehaviour
{
    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;
    public GameObject HandEnemy;  // Reference to the hand enemy
    public Collider2D handCollider;  // Collider of the hand enemy

    [SerializeField] private int currentHealth = 20, maxHealth = 20;
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
        if (isDead)
        {
            return;
        }

        // Only allow damage if the enemy is not underground (use emerging state check or position check)
        if (handEnemyAI != null && !handEnemyAI.isEmerging) // Check if the enemy is emerging
        {
            return;
        }

        if (sender.layer == gameObject.layer)
        {
            return;
        }

        if (handEnemyAI.isEmerging)
        {
            currentHealth -= amount;
            ApplyRecoil(sender);
            


            if (currentHealth > 0)
            {
                OnHitWithReference?.Invoke(sender);
            }
            else
            {
                OnDeathWithReference?.Invoke(sender);
                isDead = true;

                handEnemyAI.enabled = false;
                handCollider.isTrigger = false;

                Invoke(nameof(DestroyHand), destroyDelay);
            }
        
        }
    }


    private void DestroyHand()
    {
        Destroy(HandEnemy);
    }

    //private void DisableAI()
    //{
    //    handEnemyAI.enabled = false;
    //}
    

    private void ApplyRecoil(GameObject sender)
    {
        handEnemyAI.enabled = false;
        Vector2 hitDirection = (HandEnemy.transform.position - sender.transform.position).normalized;
          
        rb.AddForce(hitDirection * hitForce * 2, ForceMode2D.Impulse);
        
        Debug.Log("Recoil Applied");
        handEnemyAI.enabled = true;
    }
}
