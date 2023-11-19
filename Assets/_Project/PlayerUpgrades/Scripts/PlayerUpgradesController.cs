using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

		upgradesView.ShowUpgrades(new Upgrade[] { upgrades[index1], upgrades[index2] });
    }
}

[Serializable]
public class Upgrade
{
	[SerializeField] private string upgradeName;
	[SerializeField] private int maxLevel;
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
		return currentLevel < maxLevel;
	}

	public void UpgradeLevel()
	{
		if (!CanUpgrade()) return;

		currentLevel++;
		OnUpgrade?.Invoke(currentLevel);
	}
}
