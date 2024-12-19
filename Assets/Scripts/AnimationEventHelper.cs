using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    public AudioManager manager;
    public WeaponAttack weaponAttack;
    public UnityEvent OnAnimationTriggered, OnAttackPerformed;

    public void TriggerEvent()
    {
        OnAnimationTriggered?.Invoke();
    }

    public void TriggerAttack()
    {
        OnAttackPerformed?.Invoke();
    }

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
       
    }
    private void CloseAttack()
    {
        manager.PlaySFX(manager.closeAttackSound);
    }

    private void RangeAttack()
    {
        manager.PlaySFX(manager.rangeAttackSound);
    }
}
