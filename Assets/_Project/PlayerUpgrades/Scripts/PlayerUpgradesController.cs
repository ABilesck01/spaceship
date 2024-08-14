using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class PlayerUpgradesController : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerShoot playerShoot;
	[Space]
	[SerializeField] private PlayerUpgradesView upgradesView;
	[Space]
	[SerializeField] private Upgrade[] upgrades;

	[SerializeField] private Button upgrade01;
	[SerializeField] private Button upgrade02;
	[SerializeField] private Button upgrade03;
	
	private Upgrade[] selectedUpgrades;

	private bool canUpgrade;

	public static event EventHandler<Upgrade> OnSelectUpgrade;

    public void SelectFirstUpgrade(InputAction.CallbackContext ctx)
	{
		if(ctx.started)
		{
			if (!canUpgrade) return;

			Upgrade(0);
		}
	}

    public void SelectSecondUpgrade(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (!canUpgrade) return;

            Upgrade(1);
        }
    }

    public void SelectThirdUpgrade(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (!canUpgrade) return;

            RestoreShip();
        }
    }

    private void RestoreShip()
    {
        playerHealth.ResetHealth();
        canUpgrade = false;
        selectedUpgrades = null;
        upgradesView.Hide();

        OnSelectUpgrade?.Invoke(this, null);
    }

    private void Upgrade(int index)
	{
        if (!canUpgrade) return;

        selectedUpgrades[index].UpgradeLevel();
		OnSelectUpgrade?.Invoke(this, selectedUpgrades[index]);
		canUpgrade = false;
		upgradesView.Hide();

		selectedUpgrades = null;

    }

    private void GetData()
    {
        MissionProgression mp = new MissionProgression();
        mp.GetData();
        Debug.Log(mp.upgrades.Count);
        foreach (var item in mp.upgrades)
        {
            Debug.Log(item);
            for (int i = 0; i < upgrades.Length; i++)
            {
                if (this.upgrades[i].UpgradeName.Equals(item))
                {
                    this.upgrades[i].UpgradeLevel();
                }
            }
        }
    }

    private void Start()
    {
        upgrade01.onClick.AddListener(() => Upgrade(0));
        upgrade02.onClick.AddListener(() => Upgrade(1));
        upgrade03.onClick.AddListener(RestoreShip);
    }

    private void OnEnable()
    {
        BossHealth.OnBossDeath += BossHealth_OnBossDeath;
        
    }

    
    private void OnDisable()
    {
        BossHealth.OnBossDeath -= BossHealth_OnBossDeath;
    }

    private void BossHealth_OnBossDeath(object sender, EventArgs e)
    {
        int index1 = Random.Range(0, upgrades.Length);
        int index2;
        do
        {
            index2 = Random.Range(0, upgrades.Length);
        } 
		while (index2 == index1);

		selectedUpgrades = new Upgrade[] { upgrades[index1], upgrades[index2] };

		canUpgrade = true;
        upgradesView.ShowUpgrades(selectedUpgrades);
    }

    public void OnPlayerReady(Transform playerTransform)
    {
        GetData();
		playerHealth = playerTransform.GetComponent<PlayerHealth>();
    }
}

[Serializable]
public class Upgrade
{
	[SerializeField] private string upgradeName;
	[SerializeField, TextArea(2,10)]private string upgradeDescription;
	[SerializeField] private Sprite upgradeIcon;
	[SerializeField, Tooltip("Zero for infinity")] private int maxLevel;
    [SerializeField] private bool isSaveble;

    public bool IsSaveble
    {
        get { return isSaveble; }
        set { isSaveble = value; }
    }

    public UnityEvent<int> OnUpgrade;

	private int currentLevel = 1;

    public Upgrade()
    {
		currentLevel = 1;
    }

    public string UpgradeName
    {
        get { return upgradeName; }
        set { upgradeName = value; }
    }

    public string UpgradeDescription
    {
        get { return upgradeDescription; }
        set { upgradeDescription = value; }
    }


	public Sprite UpgradeIcon
	{
		get { return upgradeIcon; }
		set { upgradeIcon = value; }
	}


	public int MaxLevel
	{
		get { return maxLevel; }
	}

	public int CurrentLevel
	{
		get { return currentLevel; }
		set 
		{
			currentLevel = value; 
			if(currentLevel > maxLevel)
				currentLevel = maxLevel;
		}
	}

	public bool CanUpgrade()
	{
		if(maxLevel == 0) return true;

		return currentLevel < maxLevel;
	}

	public void UpgradeLevel()
	{
		if (!CanUpgrade()) return;

		currentLevel++;
		OnUpgrade?.Invoke(currentLevel);
	}
}
