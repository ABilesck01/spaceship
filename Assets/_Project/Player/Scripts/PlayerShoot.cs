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
    [SerializeField] private float damage = 1;
    [Space]
    [SerializeField] private AudioSource shootSound;

    [SerializeField] private int projectileAmount = 1;

    private Transform shipPosition;

    private float nextTimeToFire = 0;
    private bool hasShoot = false;

    private bool isDead = false;

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

    private void Start()
    {
        shipPosition = shootPosition.parent;
    }

    private void Update()
    {
        if (isDead) return;

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
        for (int i = 0; i < projectileAmount; i++)
        {
            float angle = shipPosition.eulerAngles.z;
            float xOffset = i - (projectileAmount - 1) / 2.0f;

            Vector3 offset = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * xOffset, Mathf.Sin(Mathf.Deg2Rad * angle) * xOffset, 0);
            Vector3 spawnPosition = shootPosition.position + offset;

            var projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.Euler(0, 0, angle));
            projectile.Damage = damage;
        }

        shootSound.Play();
        nextTimeToFire = Time.time + 1 / fireRate;
    }

    public void SwithDead(bool value)
    {
        isDead = value;
    }

    public void UpgradeCannon(int level)
    {
        projectileAmount = level;
    }

    public void UpgradeFireRate(int level)
    {
        fireRate += (level * 0.2f);
    }

    public void UpgradeDamage(int level)
    {
        damage = level;
    }
}
