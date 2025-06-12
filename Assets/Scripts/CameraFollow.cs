using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;  // Assign your sprite (e.g., player)

    public Vector3 offset = new Vector3(0, 10f, -10f); // Keeps the camera behind
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
