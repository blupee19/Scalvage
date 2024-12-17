using System.Runtime.CompilerServices;
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
            animator.SetBool("isNearPlayer", true);
        }
        else
        {
            animator.SetBool("isNearPlayer", false);
        }
    }    
}
