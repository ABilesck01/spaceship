using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    [SerializeField] private float TimeBtwShoots = .75f;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPosition;

    private float timer;

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Shoot();
            timer = TimeBtwShoots;
        }
    }

    private void Shoot()
    {
        Instantiate(projectilePrefab, shootPosition.position, shootPosition.rotation);
    }
}