using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : SpaceObject, IHealth
{
    [SerializeField] private float health = 3;
    [SerializeField] private float damage = 1;
    [Header("Shoot")]
    [SerializeField] private float TimeBtwShoots = .75f;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private AudioSource shootSound;

    private float timer;

    private void Start()
    {
        damage += GameManager.LEVEL * 0.5f;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0) 
        {
            if (GameManager.Current != null)
                GameManager.Current.Score += (int)score;
            else
                CampaingManager.Current.Score += (int)score;
            Destroy(gameObject);
        }
    }

    public override void OnTriggerEnter(Collider other) 
    {
        if (other.TryGetComponent(out IHealth health))
        {
            health.TakeDamage(1);
        }
    }

    public override void Update()
    {
        base.Update();

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Shoot();
            timer = TimeBtwShoots;
        }
    }

    private void Shoot()
    {
        var projectile = Instantiate(projectilePrefab, shootPosition.position, shootPosition.rotation);
        projectile.Damage = damage;
        shootSound?.Play();
    }
}
