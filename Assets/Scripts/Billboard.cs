using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main == null) return;

        Vector3 lookDirection = Camera.main.transform.forward;
        transform.forward = lookDirection;
    }
}
