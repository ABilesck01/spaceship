using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private float fireRate = 2;

    private float nextTimeToFire = 0;
    private bool hasShoot = false;

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if(ctx.started)
        {
            hasShoot = true;
        }
        else if(ctx.canceled)
        {
            hasShoot = false;
        }
    }

    private void Update()
    {
        if(hasShoot)
        {
            if(Time.time >= nextTimeToFire)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Instantiate(projectilePrefab, shootPosition.position, shootPosition.rotation);
        nextTimeToFire = Time.time + 1 / fireRate;
    }
}
