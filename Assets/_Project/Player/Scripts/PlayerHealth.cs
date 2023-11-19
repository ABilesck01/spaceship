using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float MaxHeath;
    [SerializeField] private Slider healthBar;
    [Space]
    [SerializeField] private float healAmount = 3;
    [SerializeField] private int medkitAmount = 3;
    [SerializeField] private TextMeshProUGUI txtMedkit;
    [Space]
    [SerializeField] private GameObject explosion;
    [SerializeField] private AudioSource explosionAudio;

    private float currentHealth;

    public static event EventHandler OnPlayerDeath;

    public void OnHealInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started) Heal();
    }

    private void OnEnable()
    {
        BossHealth.OnBossDeath += BossHealth_OnBossDeath;
    }

    private void OnDisable()
    {
        BossHealth.OnBossDeath -= BossHealth_OnBossDeath;
    }

    private void BossHealth_OnBossDeath(object sender, System.EventArgs e)
    {
        medkitAmount = 3;
    }

    private void Awake()
    {
        currentHealth = MaxHeath;
        healthBar.maxValue = currentHealth;
        healthBar.value = currentHealth;
        txtMedkit.text = medkitAmount.ToString();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
        if(currentHealth <= 0)
        {
            explosionAudio.Play();

            GetComponentInParent<PlayerMovement>().SwitchMove(false);
            GetComponentInParent<PlayerShoot>().SwithDead(false);
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            Instantiate(explosion, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }

    }

    private void Heal()
    {
        if (currentHealth >= MaxHeath) return;
        if (medkitAmount <= 0) return;

        medkitAmount--;
        txtMedkit.text = medkitAmount.ToString();
        currentHealth += healAmount;
        healthBar.value = currentHealth;
        if (currentHealth > MaxHeath)
            currentHealth = MaxHeath;
    }
}
