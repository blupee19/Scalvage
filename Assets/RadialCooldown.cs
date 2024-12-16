using UnityEngine;
using UnityEngine.UI;

public class RadialCooldown : MonoBehaviour
{
    [SerializeField] private WeaponAttack weaponAttack;
    public Image attackCooldownTimer;
    public Image attackCooldownTimerBlack;
    private float cooldownTimer = 2f;
    private float cooldownDuration = 2f;  // Cooldown duration in seconds

    void Start()
    {
        attackCooldownTimer.fillAmount = 1f;
    }

    void Update()
    {
        // If the attack is thrown and cooldown hasn't started yet
        if (weaponAttack.throwAttack && cooldownTimer >= cooldownDuration)
        {
            // Start the cooldown and reset the timer
            cooldownTimer = 0f;
            attackCooldownTimer.fillAmount = 0f;
        }

        // Increment the cooldown timer
        cooldownTimer += Time.deltaTime;

        // Update the fillAmount based on the cooldown time
        if (cooldownTimer <= cooldownDuration)
        {
            // Gradually fill the timer based on the cooldown duration
            attackCooldownTimer.fillAmount = cooldownTimer / cooldownDuration;
        }
    }
}
