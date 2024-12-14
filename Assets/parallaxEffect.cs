using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layerTransform; // Transform of the layer
        public float parallaxFactor; // Speed factor relative to the camera movement
    }

    public ParallaxLayer[] layers; // Array of layers for the parallax effect
    private Vector3 previousCameraPosition;

    private void Start()
    {
        // Store the initial camera position
        previousCameraPosition = Camera.main.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 cameraMovement = Camera.main.transform.position - previousCameraPosition;

        // Apply parallax effect for each layer
        foreach (var layer in layers)
        {
            if (layer.layerTransform != null)
            {
                Vector3 newLayerPosition = layer.layerTransform.position;
                newLayerPosition += new Vector3(cameraMovement.x * layer.parallaxFactor, cameraMovement.y * layer.parallaxFactor, 0);
                layer.layerTransform.position = newLayerPosition;
            }
        }

        // Update the previous camera position
        previousCameraPosition = Camera.main.transform.position;
    }
}
