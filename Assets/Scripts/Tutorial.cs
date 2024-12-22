using UnityEngine;
using UnityEngine.UI;

public class ShowImageOnTrigger : MonoBehaviour
{
    [SerializeField] private Image tutorialImage; // Assign this in the Inspector

    private void Start()
    {
        if (tutorialImage == null)
        {
            Debug.LogError("Tutorial image is not assigned!");
            return;
        }

        // Make sure the image is hidden initially
        tutorialImage.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorialImage.enabled = true; // Show the image when the player enters
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorialImage.enabled = false; // Hide the image when the player exits
        }
    }
}
