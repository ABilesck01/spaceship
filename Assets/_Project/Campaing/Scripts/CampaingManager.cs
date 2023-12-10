using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class CampaingManager : MonoBehaviour
{
    public static readonly string SceneName = "MissionSelection";

    [SerializeField] private MissionData missionData;
    public static MissionData MissionData;
    [SerializeField] private float TimeBtwTicks = 0.2f;
    [SerializeField] private VisualEffect warp;
    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private float speed;
    [SerializeField] private Transform bossPosition;

    private bool canTick = false;
    private bool canTime = false;
    private int tick;
    private float tickTimer;
    private int score;

    private bool isOnBossFight = false;

    public static event EventHandler OnTick;
    public static event EventHandler OnBossFight;
    public static event EventHandler OnPlayerEnter;
    public static event EventHandler OnGameWin;
    public static event EventHandler OnGameOver;

    public static CampaingManager Current;

    private float time = 0;

    public int Score
    {
        get => score;

        set
        {
            score = value;
            txtScore.text = score.ToString("###,###,#00");

            if (score >= missionData.scoreToWin && score != 0 && !isOnBossFight)
            {
                isOnBossFight = true;
                Debug.Log("Boss fight");
                OnBossFight?.Invoke(this, EventArgs.Empty);
                SwitchTick(false);
                BossController boss = Instantiate(missionData.boss, bossPosition.position, Quaternion.identity);
            }
        }
    }

    private void Awake()
    {
        GameManager.LEVEL = missionData.missionIndex;
    }

    private void OnEnable()
    {
        PlayerCustomization.OnPlayerReady += PlayerCustomization_OnPlayerReady;
        PlayerUpgradesController.OnSelectUpgrade += PlayerUpgradesController_OnSelectUpgrade;
        BossHealth.OnBossDeath += BossHealth_OnBossDeath;
        PlayerHealth.OnPlayerDeath += PlayerHealth_OnPlayerDeath;
    }

    private void OnDisable()
    {
        PlayerCustomization.OnPlayerReady -= PlayerCustomization_OnPlayerReady;
        PlayerUpgradesController.OnSelectUpgrade -= PlayerUpgradesController_OnSelectUpgrade;
        BossHealth.OnBossDeath -= BossHealth_OnBossDeath;
        PlayerHealth.OnPlayerDeath -= PlayerHealth_OnPlayerDeath;
    }

    private void BossHealth_OnBossDeath(object sender, EventArgs e)
    {
        isOnBossFight = false;
        canTime = false;
    }

    private void PlayerHealth_OnPlayerDeath(object sender, EventArgs e)
    {
        SwitchTick(false);
        OnGameOver?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerUpgradesController_OnSelectUpgrade(object sender, Upgrade e)
    {
        MissionProgression missionProgression = new MissionProgression();
        
        missionProgression.GetData();
        if(missionProgression.currentMissionIndex <= missionData.missionIndex)
            missionProgression.currentMissionIndex++;

        missionProgression.AddUpgrade(e);
        missionProgression.SetTimer(time, missionData.missionIndex);
        missionProgression.SaveData();
        
        OnGameWin?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerCustomization_OnPlayerReady(object sender, EventArgs e)
    {
        SwitchTick(true);
        canTime = true;
        Score = 0;
    }

    private void Start()
    {
        Current = this;

        GameManager.SPEED = speed;

        if (MissionData != null)
        {
            missionData = MissionData;
        }
    }

    private void Update()
    {
        MissionTimer();
        Tick();
    }

    private void MissionTimer()
    {
        if (!canTime) return;
        time += Time.deltaTime;
    }

    private void Tick()
    {
        if (!canTick) return;

        tickTimer += Time.deltaTime;
        if (tickTimer > TimeBtwTicks)
        {
            tickTimer = 0;
            tick++;
            OnTick?.Invoke(this, EventArgs.Empty);
        }
    }

    public void StartGame()
    {
        Score = 0;
        OnPlayerEnter?.Invoke(this, EventArgs.Empty);
        playerInputManager.DisableJoining();
    }

    public void SwitchTick(bool tick)
    {
        canTick = tick;
        float warpSpeed = tick ? 1 : 0;
        warp.SetFloat("WarpAmount", warpSpeed);
    }
}
