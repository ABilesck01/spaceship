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
    [Header("Charged shield")]
    [SerializeField] private float shieldDecreaseRate;
    [SerializeField] private Slider shieldBar;
    [Space]
    [SerializeField] private float healAmount = 3;
    [SerializeField] private int medkitAmount = 3;
    [SerializeField] private TextMeshProUGUI txtMedkit;
    [Space]
    [SerializeField] private GameObject explosion;
    [SerializeField] private AudioSource explosionAudio;

    private float currentHealth;
    private float currentShield;

    public float CurrentShield 
    { 
        get => currentShield; 
        set
        {
            currentShield = value;
            shieldBar.value = currentShield;
        } 
    }

    public static event EventHandler OnPlayerDeath;

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
        txtMedkit.text = medkitAmount.ToString();
    }

    private void Awake()
    {
        if (healthBar == null || shieldBar == null) return;

        currentHealth = MaxHeath;
        healthBar.maxValue = currentHealth;
        healthBar.value = currentHealth;
        shieldBar.maxValue = MaxHeath;
        shieldBar.value = 0;
        txtMedkit.text = medkitAmount.ToString();
    }

    private void Update()
    {
        DecreaseShield();
    }

    public void TakeDamage(float damage)
    {
        if(CurrentShield > 0)
        {
            CurrentShield -= damage;
            return;
        }

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

    public void Heal()
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

    public void ResetHealth()
    {
        currentHealth = MaxHeath;
        healthBar.value = currentHealth;
    }

    public void ChargedShield()
    {
        CurrentShield = MaxHeath;
    }

    private void DecreaseShield()
    {
        if(CurrentShield <= 0) return;

        CurrentShield -= Time.deltaTime * shieldDecreaseRate;
    }

    public void SetView(Slider health, Slider shield, TextMeshProUGUI medkit, AudioSource kaboom)
    {
        healthBar = health;
        shieldBar = shield;
        txtMedkit = medkit;
        explosionAudio = kaboom;

        currentHealth = MaxHeath;
        healthBar.maxValue = currentHealth;
        healthBar.value = currentHealth;
        shieldBar.maxValue = MaxHeath;
        shieldBar.value = 0;
        txtMedkit.text = medkitAmount.ToString();
    }
}
