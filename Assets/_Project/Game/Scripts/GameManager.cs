using System;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    public static float SPEED;
    public static int LEVEL = 0;

    [SerializeField] private float TimeBtwTicks = 0.2f;
    [SerializeField] private float speed;
    [SerializeField] private VisualEffect warp;
    [Space]
    [SerializeField] private TextMeshProUGUI txtScore;

    private float score;
    private float scoreToBoss;
    private bool canTick = false;
    private bool hasStarted = false;

    public static event EventHandler OnTick;
    public static event EventHandler OnStartGame;
    public static event EventHandler OnBossFight;
    public static event EventHandler OnGameOver;

    private int tick;
    private float tickTimer;

    private int playersAmount = 0;
    private int playersDead = 0;
    private int playersUpgrade = 0;

    public static GameManager Current;

    public float Score
    {
        get => score;

        set
        {
            score = value;
            scoreToBoss = score;
            txtScore.text = score.ToString("###,###,#00");

            if(scoreToBoss >= 1000 && score != 0)
            {
                Debug.Log("Boss fight");
                OnBossFight?.Invoke(this, EventArgs.Empty);
                SwitchTick(false);
            }
        }
    }


    private void Awake()
    {
        SPEED = speed;
        LEVEL = 0;
        Current = this;
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += PlayerHealth_OnPlayerDeath;
        BossHealth.OnBossDeath += BossHealth_OnBossDeath;
        PlayerUpgradesController.OnSelectUpgrade += PlayerUpgradesController_OnSelectUpgrade;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= PlayerHealth_OnPlayerDeath;
        BossHealth.OnBossDeath -= BossHealth_OnBossDeath;
        PlayerUpgradesController.OnSelectUpgrade -= PlayerUpgradesController_OnSelectUpgrade;
    }

    private void PlayerHealth_OnPlayerDeath(object sender, EventArgs e)
    {
        playersDead++;
        if(playersDead >= playersAmount)
        {
            SwitchTick(false);
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }
    }

    private void BossHealth_OnBossDeath(object sender, EventArgs e)
    {
        scoreToBoss = 0;
        playersUpgrade = 0;
        LEVEL++;
    }

    private void PlayerUpgradesController_OnSelectUpgrade(object sender, EventArgs e)
    {
        playersUpgrade++;
        if (playersUpgrade >= playersAmount)
        {
            SwitchTick(true);
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
        Score = 0;
    }
    public void SwitchTick(bool tick)
    {
        canTick = tick;
        float warpSpeed = tick ? 1 : 0;
        warp.SetFloat("WarpAmount", warpSpeed);
    }
}
