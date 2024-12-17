using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HandEnemyHealth : EnemyBaseHealth
{
    public override void AnimationCalls()
    {
        base.AnimationCalls();
        GameObject player = GameObject.FindWithTag("Player");
        Animator animator = GetComponentInChildren<Animator>();

        if (Vector2.Distance(new Vector2(transform.position.x, 0f), new Vector2(player.transform.position.x, 0f)) <= 8f)
        {
            canDamage = true;
            animator.SetBool("isNearPlayer", true);
        }
        else
        {
            canDamage= false;
            animator.SetBool("isNearPlayer", false);
        }
    }
}
