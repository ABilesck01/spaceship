using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float health;
    [SerializeField] private Slider healthBar;
    [Space]
    [SerializeField] private GameObject explosion;
    [SerializeField] private AudioSource explosionAudio;

    public static event EventHandler OnBossDeath;

    private void Awake()
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health;
        if (health <= 0)
        {
            OnBossDeath?.Invoke(this, EventArgs.Empty);
            explosionAudio.Play();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    
}
