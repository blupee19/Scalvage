using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameObject.GetComponent<MovingTrap>().enabled = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<MovingTrap>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

        }
    }
}
