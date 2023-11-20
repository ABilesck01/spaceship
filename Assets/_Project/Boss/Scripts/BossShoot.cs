using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    [SerializeField] private float damage = 1;
    [SerializeField] private float timeBtwShoots = .75f;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private LayerMask player;

    private float timer;

    private void Start()
    {
        damage += GameManager.LEVEL;
        timeBtwShoots -= GameManager.LEVEL * 0.1f;
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Shoot();
            timer = timeBtwShoots;
        }
    }

    private void Shoot()
    {
        try
        {
            Instantiate(projectilePrefab, shootPosition.position, shootPosition.rotation);
        }
        catch (System.Exception)
        {

        }
    }
}
