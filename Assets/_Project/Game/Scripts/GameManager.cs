using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    public static float SPEED;
    public static int LEVEL = 0;

    [SerializeField] private float TimeBtwTicks = 0.2f;
    [SerializeField] private float speed;
    [SerializeField] private VisualEffect warp;
    [SerializeField] private PlayerInputManager playerInputManager;
    [Space]
    [SerializeField] private TextMeshProUGUI txtScore;

    private int score;
    private float scoreToBoss;
    private bool canTick = false;
    private bool hasStarted = false;
    private bool isOnBossFight = false;

    public static event EventHandler OnTick;
    public static event EventHandler OnPlayerEnter;
    public static event EventHandler OnStartGame;
    public static event EventHandler OnBossFight;
    public static event EventHandler OnGameOver;

    private int tick;
    private float tickTimer;

    private int playersAmount = 0;
    private int playersDead = 0;
    private int playersUpgrade = 0;
    private int playerReady = 0;

    public static GameManager Current;

    public int Score
    {
        get => score;

        set
        {
            score = value;
            scoreToBoss = value - LEVEL * 1000;
            txtScore.text = score.ToString("###,###,#00");

            if(scoreToBoss >= 1000 && score != 0 && !isOnBossFight)
            {
                isOnBossFight = true;
                scoreToBoss = 0;
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
        playerReady = 0;
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += PlayerHealth_OnPlayerDeath;
        PlayerCustomization.OnPlayerReady += PlayerCustomization_OnPlayerReady;
        BossHealth.OnBossDeath += BossHealth_OnBossDeath;
        PlayerUpgradesController.OnSelectUpgrade += PlayerUpgradesController_OnSelectUpgrade;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= PlayerHealth_OnPlayerDeath;
        PlayerCustomization.OnPlayerReady -= PlayerCustomization_OnPlayerReady;
        BossHealth.OnBossDeath -= BossHealth_OnBossDeath;
        PlayerUpgradesController.OnSelectUpgrade -= PlayerUpgradesController_OnSelectUpgrade;

        Current = null;
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

    private void PlayerCustomization_OnPlayerReady(object sender, EventArgs e)
    {
        playerReady++;
        if(playerReady >= playersAmount)
        {
            playerInputManager.DisableJoining();
            SwitchTick(true);
            OnStartGame?.Invoke(this, EventArgs.Empty);
            Score = 0;
        }
    }

    private void BossHealth_OnBossDeath(object sender, EventArgs e)
    {
        scoreToBoss = 0;
        playersUpgrade = 0;
        LEVEL++;
        isOnBossFight = false;
    }

    private void PlayerUpgradesController_OnSelectUpgrade(object sender, Upgrade e)
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

    public void StartGame()
    {
        playersAmount++;

        if(hasStarted) return;

        //SwitchTick(true);
        OnPlayerEnter?.Invoke(this, EventArgs.Empty);
        //Score = 0;
    }

    public void SwitchTick(bool tick)
    {
        canTick = tick;
        float warpSpeed = tick ? 1 : 0;
        warp.SetFloat("WarpAmount", warpSpeed);
    }
}
