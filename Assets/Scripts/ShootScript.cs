using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public Transform gunHolder;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - gunHolder.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Optional: Clamp to a range (like -90 to +90 degrees) if needed
        gunHolder.rotation = Quaternion.Euler(0, 0, angle);
    }
}

