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

    public static event EventHandler OnTick;

    private int tick;
    private float tickTimer;

    private void Awake()
    {
        SPEED = speed;
    }

    private void Update()
    {
        tickTimer += Time.deltaTime;
        if(tickTimer > TimeBtwTicks)
        {
            tickTimer = 0;
            tick++;
            OnTick?.Invoke(this, EventArgs.Empty);
        }
    }
}
