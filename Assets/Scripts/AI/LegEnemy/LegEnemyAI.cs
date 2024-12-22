using UnityEngine;

public class LegEnemyAI : AIEnemyBase
{
    public Transform LegGFX;
    public GameObject legEnemy;
    public override void Flip()
    {
        if (target.position.x > transform.position.x)
        {
            legEnemy.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            legEnemy.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}

