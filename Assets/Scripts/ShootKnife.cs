using UnityEngine;

public class ShootKnife : MonoBehaviour
{
    private Vector3 mousePos;
    private Rigidbody2D rb;
    public float force;
    private Animator anim;
    private BoxCollider2D boxCollider;
    //private Health health;
    public Transform circleOrigin;
    [SerializeField] private float radius = 1.59f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        // Get the current mouse position when the knife is instantiated
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Ensure the z-coordinate is 0 for 2D

        // Calculate the direction vector from the knife to the mouse position
        Vector3 direction = (mousePos - transform.position).normalized;

        // Apply force to the knife in the calculated direction
        rb.linearVelocity = direction * force;

        // Rotate the knife to face the direction of movement
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }

    void Update()
    {
        Destroy(gameObject, 5);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        { 
            Destroy(gameObject);
        }
    }
}
