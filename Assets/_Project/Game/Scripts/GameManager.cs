using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float SPEED;

    [SerializeField] private float TimeBtwTicks = 0.2f;
    [SerializeField] private float speed;

    private bool canTick = false;
    private bool hasStarted = false;

    public static event EventHandler OnTick;
    public static event EventHandler OnStartGame;

    private int tick;
    private float tickTimer;

    private int playersAmount = 0;
    private int playersDead = 0;

    private void Awake()
    {
        SPEED = speed;
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += PlayerHealth_OnPlayerDeath;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= PlayerHealth_OnPlayerDeath;
    }

    private void PlayerHealth_OnPlayerDeath(object sender, EventArgs e)
    {
        playersDead++;
        if(playersDead >= playersAmount)
        {
            SwitchTick(false);
        }
    }

    private void Update()
    {
        if (!canTick) return;

        tickTimer += Time.deltaTime;
        if(tickTimer > TimeBtwTicks)
        {
            tickTimer = 0;
            tick++;
            OnTick?.Invoke(this, EventArgs.Empty);
        }
    }

    public void StartGame ()
    {
        playersAmount++;

        if(hasStarted) return;

        SwitchTick(true);
        OnStartGame?.Invoke(this, EventArgs.Empty);
    }
    public void SwitchTick(bool tick)
    {
        canTick = tick;
    }
}
