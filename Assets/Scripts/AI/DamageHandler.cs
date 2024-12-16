using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    private HandEnemyAI handEnemyAI;

    void Start()
    {
        // Get the reference to the GroundEnemyAI script on the parent object (HandEnemy)
        handEnemyAI = transform.parent.GetComponent<HandEnemyAI>();
    }

    // Called when the attack animation begins (using an animation event)
    public void OnAttackAnimationStart()
    {
        if (handEnemyAI != null)
        {
            handEnemyAI.EnableDamage();
            Debug.Log("Damage enabled");// Enable damage to the player
        }
    }

    // Called when the attack animation ends (using an animation event)
    public void OnAttackAnimationEnd()
    {
        if (handEnemyAI != null)
        {
            handEnemyAI.DisableDamage();
            Debug.Log("Damage Disable"); // Disable damage to the player
        }
    }
}
