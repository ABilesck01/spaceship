using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float SPEED;
    public static int LEVEL = 0;

    [SerializeField] private float TimeBtwTicks = 0.2f;
    [SerializeField] private float speed;
    [Space]
    [SerializeField] private TextMeshProUGUI txtScore;

    private float score;
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
            txtScore.text = score.ToString("###,###,#00");

            if(score % 1000 == 0 && score != 0)
            {
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
    }
}
