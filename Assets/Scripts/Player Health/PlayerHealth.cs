using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private Camera mainCamera;
    public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //hurt
        }
        else
        {
            //die
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1f);
            StartCoroutine(Shake(0.2f,0.2f));

            Debug.Log("Hit!!!");
        }
    }


    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = mainCamera.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            mainCamera.transform.localPosition = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.localPosition = originalPosition;
    }
}
