using UnityEngine;

public class KnifeProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private Animator anim;
    private bool hit;
    private Collider2D knifeCollider;
    private void Awake()
    {
        knifeCollider = GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        knifeCollider.enabled = false;
        anim.SetTrigger("Explode");
    }

    public void SetDirection(float _direction)
    {
        hit = false;
    }
}
