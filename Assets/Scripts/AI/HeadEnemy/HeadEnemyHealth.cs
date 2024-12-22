using System.Collections;
using UnityEngine;

public class HeadEnemyHealth : EnemyBaseHealth
{
    public Collider2D enemyCollider;
    private bool isKnockedBack = false;
    private float knockbackDuration = 0.2f;// Time during which the enemy is knocked back

   

    public override void OnHit(GameObject sender)
    {
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        base.OnHit(sender);
        Debug.Log("Overriden OnHit called");

        Vector2 knockbackDirection = (transform.position - sender.transform.position).normalized;
        float knockbackForce = 5f;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        Debug.Log("Knockback applied");

        isKnockedBack=true;
        if (TryGetComponent(out HeadEnemyAI ai))
        {
            ai.ApplyKnockback();
        }
        
        StartCoroutine(ResetKnockback());
    }

    private IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false ;        
    }
}

