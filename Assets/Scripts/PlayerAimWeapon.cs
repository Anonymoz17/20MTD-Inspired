using System;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;

    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }

    [SerializeField] private float recoilDuration = 0.1f;
    [SerializeField] private float recoilAngle = 25f;

    private float recoilTimer = 0f;

    private Transform aimTransform;        // Object holding the gun
    private Transform recoilTransform;     // The actual gun sprite (rotated for recoil)
    private Transform aimGunEndPointTransform;
    private SpriteRenderer gunSprite;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        recoilTransform = aimTransform.Find("Gun");
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
        gunSprite = recoilTransform.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HandleAiming();
        HandleGunRecoil();
        HandleShooting();
    }

    private void HandleAiming()
    {
        // Get screen position of the gun (aimTransform)
        Vector3 gunScreenPos = Camera.main.WorldToScreenPoint(aimTransform.position);

        // Direction vector from gun to mouse in screen space
        Vector2 screenDirection = (Vector2)(Input.mousePosition - gunScreenPos);

        // Calculate angle in degrees
        float angle = Mathf.Atan2(screenDirection.y, screenDirection.x) * Mathf.Rad2Deg;

        // Rotate the gun around the Z axis (local)
        aimTransform.localRotation = Quaternion.Euler(0, 0, angle);

        // Flip the gun sprite if aiming backwards
        gunSprite.flipY = (angle > 90 || angle < -90);
    }


    private void HandleShooting()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = MousePos();
            OnShoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mouseWorldPos
            });
        }
    }

    private Vector3 MousePos()
    {
        // Raycast mouse position onto same plane as player/gun (XZ)
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, aimTransform.position);

        if (plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }

        return aimTransform.position;
    }

    private void HandleGunRecoil()
    {
        if (Input.GetMouseButtonDown(0))
        {
            recoilTimer = recoilDuration;
        }

        if (recoilTimer > 0)
        {
            recoilTimer -= Time.deltaTime;
            float appliedRecoilAngle = gunSprite.flipY ? -recoilAngle : recoilAngle;

            // Slight kickback in local X axis
            recoilTransform.localRotation = Quaternion.Euler(appliedRecoilAngle, 0f, 0f);
        }
        else
        {
            recoilTransform.localRotation = Quaternion.identity;
        }
    }
}
