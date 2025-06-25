using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    
    public Transform target;  // Assign your sprite (e.g., player)
    public float displacementMultiplier = 0.2f;
    private float zPosition = -10;

    public Vector3 offset = new Vector3(0, 10f, -10f); // Keeps the camera behind
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        Camera.main.orthographic = true;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 cameraDisplacement = (mousePosition - target.position) * displacementMultiplier;
        cameraDisplacement = Vector3.ClampMagnitude(cameraDisplacement, 3f);

    
        Vector3 desiredPosition = target.position + offset + cameraDisplacement;
        desiredPosition.z = zPosition;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

    }
}
