using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs 
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }

    private Transform aimGunEndPointTransform;
    private Transform aimTransform;
    private Transform recoilTransform;
    private SpriteRenderer gunSprite;

    private float recoilTimer = 0f;
    private float recoilDuration = 0.1f; // How long the recoil lasts
    [SerializeField]private float recoilAngle = 25f;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        gunSprite = aimTransform.Find("Gun").GetComponent<SpriteRenderer>();
        recoilTransform = aimTransform.Find("Gun");
    }

    void Update()
    {
        HandleAiming();
        HandleGunRecoil();
    }

    // Gun Points to Mouse
    private void HandleAiming()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z - aimTransform.position.z);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = mousePos - aimTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Optional: Clamp to a range (like -90 to +90 degrees) if needed
        aimTransform.rotation = Quaternion.Euler(0, 0, angle);

        // Flip Gun from right to left its left side
        if (angle > 90 || angle < -90)
        {
            gunSprite.flipY = true;
        }
        else
        {
            gunSprite.flipY = false;
        }
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButton(0))
        {
            OnShoot?.Invoke(this, new OnShootEventArgs){
                gunEndPointPosition = aimGunEndPointTransform.position,


            }
        }
    }

    private void HandleGunRecoil()
    {

        // If mouse clicked, start recoil
        if (Input.GetMouseButtonDown(0))
        {
            recoilTimer = recoilDuration;
        }

        // If in recoil state
        if (recoilTimer > 0)
        {
            recoilTimer -= Time.deltaTime;

            float appliedRecoilAngle = gunSprite.flipY ? -recoilAngle : recoilAngle;
            recoilTransform.localRotation = Quaternion.Euler(0, 0, appliedRecoilAngle);
        }
        else
        {
            recoilTransform.localRotation = Quaternion.identity;
        }
    }


}