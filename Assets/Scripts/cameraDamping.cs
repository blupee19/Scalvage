using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player's transform
    public Vector3 offset = new Vector3(0, 0, -10); // Offset to keep the camera at a distance
    public float smoothTime = 0.3f; // Time for damping effect

    private Vector3 velocity = Vector3.zero; // Internal variable for smooth damping

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the target position
            Vector3 targetPosition = target.position + offset;

            // Smoothly move the camera towards the target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}