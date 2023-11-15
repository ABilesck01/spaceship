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

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if(ctx.started && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = 1 / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(projectilePrefab, shootPosition.position, shootPosition.rotation);
    }
}
