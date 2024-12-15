using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroy after `lifetime` seconds
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collision (e.g., damage enemy or destroy the bullet)
        Destroy(gameObject);
    }
}