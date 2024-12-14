using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject weapon;
    public PlayerController controller;
    public InputActionAsset playerControls;
    private InputAction closeAttackAction;
    private InputAction throwAttackAction;
    public Animator weaponAnimator;
    public float offset = 0f;
    public Transform circleOrigin;
    public float radius;
    public bool closeAttack {  get; private set; }
    public bool throwAttack { get; private set; }

    private void Awake()
    {
        closeAttackAction = playerControls.FindActionMap("Player").FindAction("Attack");
        throwAttackAction = playerControls.FindActionMap("Player").FindAction("Throw");
        
        closeAttackAction.performed += context => closeAttack = true;
        closeAttackAction.canceled += context => closeAttack = false;

        throwAttackAction.performed += context => throwAttack = false;
        throwAttackAction.canceled += context => throwAttack = true;
    }

    private void OnEnable()
    {
        closeAttackAction.Enable();
        throwAttackAction.Enable();
    }
    private void OnDisable()
    {
        closeAttackAction.Disable();
        throwAttackAction.Disable();
    }

    private void Start()
    {
        //...
    }

    private void Update()
    {
        Quaternion weaponInitRot = weapon.transform.rotation;
        Quaternion weaponRotation = new Quaternion(weaponInitRot.x, weaponInitRot.y, weaponInitRot.z + 360, weaponInitRot.w);
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + offset);

        if (!controller.facingRight)
        {
            weapon.GetComponent<SpriteRenderer>().flipY = true;
        }
        else 
        {
            weapon.GetComponent<SpriteRenderer>().flipY = false;
        }

        if (closeAttack)
        {
            weaponAnimator.SetBool("closeAttack", true);

        }
        else
        {
            weaponAnimator.SetBool("closeAttack", false);
        }

        if (throwAttack)
        {
            ThrowWeapon();
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            //Debug.Log(collider.name);
            Health health;
            if(health = collider.GetComponent<Health>())
            {
                health.GetHit(1, transform.parent.gameObject);
            }
        }
    }

    private void ThrowWeapon()
    {
      //...
    }

}
