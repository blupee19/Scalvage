using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float offset = 2f;

    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + offset);

    }

}
