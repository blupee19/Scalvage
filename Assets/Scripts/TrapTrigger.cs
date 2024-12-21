using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    AudioManager manager;
    [SerializeField] private BoxCollider2D trigger;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameObject.GetComponent<MovingTrap>().enabled = false;
        manager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<MovingTrap>().enabled = true;
            trigger.enabled = false;
            manager.PlaySFX(manager.trapTrigger);
            Debug.Log("Trap Triggered");

        }
    }
}
