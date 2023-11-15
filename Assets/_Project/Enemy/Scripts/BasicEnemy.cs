using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : SpaceObject
{
    [SerializeField] private float TimeBtwShoots = .75f;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPosition;

    private float timer;

    public override void Update()
    {
        base.Update();

        timer += Time.deltaTime;
        if(timer > TimeBtwShoots)
        {
            timer = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(projectilePrefab, shootPosition.position, shootPosition.rotation);
    }
}
