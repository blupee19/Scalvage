using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMStart : MonoBehaviour
{
    AudioManager manager;
    [SerializeField] private BoxCollider2D trigger;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && SceneManager.GetActiveScene().name == "MainLevel1")
        {
            manager.PlaySFX(manager.Level1BGM);
            trigger.enabled = false;
        }

        if (collision.tag == "Player" && SceneManager.GetActiveScene().name == "MainLevel2")
        {
            manager.PlaySFX(manager.Level2BGM);
            trigger.enabled = false;
        }
        if (collision.tag == "Player" && SceneManager.GetActiveScene().name == "MainLevel3")
        {
            manager.PlaySFX(manager.Level3BGM);
            trigger.enabled = false;
        }
    }
}
