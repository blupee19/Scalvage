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
    public Animator weaponAnimator;
    public float offset = 0f;
    public bool closeAttack {  get; private set; }

    private void Awake()
    {
        closeAttackAction = playerControls.FindActionMap("Player").FindAction("Attack");
        closeAttackAction.performed += context => closeAttack = true;
        closeAttackAction.canceled += context => closeAttack = false;
    }

    private void OnEnable()
    {
        closeAttackAction.Enable();
    }
    private void OnDisable()
    {
        closeAttackAction.Disable();
    }

    private void Start()
    {
        weapon.GetComponent<EdgeCollider2D>().enabled = false;
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
            weaponAnimator.SetBool("closeAttack", true)

        }
        else
        {
            weaponAnimator.SetBool("closeAttack", false);
        }

    }

}
