using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Rendering;

public class PlayerHealth : MonoBehaviour
{
    AudioManager audioManager;

    [Header("Health")]
    [SerializeField] private GameObject player;
    [SerializeField] private float startingHealth;
    [SerializeField] private Camera mainCamera;
    private Animator surgeonAnim;
    public bool dead;
    public float currentHealth { get; private set; }

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numOfFlashes;
    private SpriteRenderer surgeon;

    private Respawn respawn;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
        surgeonAnim = GetComponent<Animator>();
        surgeon = GetComponent<SpriteRenderer>();   
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Respawn>();
        GameObject respawnobj = GameObject.FindGameObjectWithTag("Respawn");
        if (respawnobj != null) {            
            respawn = respawnobj.GetComponent<Respawn>();  

            if(respawn == null)
            {
                Debug.LogError("respwan comp not forund");
            }
        }else
        {
            Debug.LogError("not found");
        }
        
    }

    public void Update()
    {
        //...
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            audioManager.PlaySFX(audioManager.playerHurt);
            StartCoroutine(Invulnerability());
            StartCoroutine(Shake(0.2f, 0.3f));

        }
        else
        {
            if (!dead)
            { 
                
                dead = true;
                audioManager.PlaySFX(audioManager.playerDeath);

                //RespawnPlayer();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                
            }
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

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10,11, true);
        for (int i = 0; i < numOfFlashes; i++) 
        {
            surgeon.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
            surgeon.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10,11, false);


    }

    public void RespawnPlayer()
    {
        if (respawn.respawnPoint != null)
        {
            Debug.Log($"Respawning player at checkpoint: {respawn.respawnPoint.name}");
            player.transform.position = respawn.respawnPoint.transform.position;
        }
        else
        {
            Debug.LogWarning("No respawn point set! Ensure the player has triggered a checkpoint.");
        }

        currentHealth = startingHealth; // Reset health after moving to the respawn point
        dead = false; // Mark the player as alive
    }   

}
