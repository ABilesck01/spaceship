using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PlayerUpgradesController : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerShoot playerShoot;
	[Space]
	[SerializeField] private PlayerUpgradesView upgradesView;
	[Space]
	[SerializeField] private Upgrade[] upgrades;
	
	private Upgrade[] selectedUpgrades;

	private bool canUpgrade;

	public static event EventHandler OnSelectUpgrade;

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

			playerHealth.ResetHealth();
            canUpgrade = false;
            selectedUpgrades = null;
            upgradesView.Hide();

            OnSelectUpgrade?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Upgrade(int index)
	{
		selectedUpgrades[index].UpgradeLevel();
		canUpgrade = false;
		selectedUpgrades = null;
		upgradesView.Hide();

		OnSelectUpgrade?.Invoke(this, EventArgs.Empty);

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
}

[Serializable]
public class Upgrade
{
	[SerializeField] private string upgradeName;
	[SerializeField, TextArea(2,10)]private string upgradeDescription;
	[SerializeField] private Sprite upgradeIcon;
	[SerializeField, Tooltip("Zero for infinity")] private int maxLevel;
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
