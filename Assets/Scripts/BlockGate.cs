using UnityEngine;

public class BlockGate : MonoBehaviour
{
    [SerializeField] private BoxCollider2D trigger;
    [SerializeField] private BoxCollider2D block;
    [SerializeField] private GameObject gate;

    private void Start()
    {
        gate.GetComponent<SpriteRenderer>();
        block.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            block.enabled = true;
            gate.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
