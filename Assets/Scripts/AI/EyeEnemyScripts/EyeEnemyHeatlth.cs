using UnityEngine;

public class EyeEnemyHeatlth : EnemyBaseHealth
{
    protected static GameObject lastHitSender;
    protected static Collider2D enemyCollider;
   
    public override void OnHit(GameObject sender)
    {
        lastHitSender = sender;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        base.OnHit(sender);
        Debug.Log("EyeEnemyHealth: Flying eye got hit!");
        
        if (rb != null)
        {
            Vector2 knockbackDirection = (transform.position - sender.transform.position).normalized;
            rb.AddForce(knockbackDirection * 4f, ForceMode2D.Impulse);
        }
    }   
}

