using UnityEngine;

public class AnimationEvent : EnemyBaseHealth
{
    private EnemyBaseHealth enemyBaseHealth;
    void Start()
    {
        enemyBaseHealth = GetComponent<EnemyBaseHealth>();
    }


    public void EnableDamage()
    {
        enemyBaseHealth.canDamage = true;
    }

    public void DisableDamage()
    {
        enemyBaseHealth.canDamage = false;
    }
}
