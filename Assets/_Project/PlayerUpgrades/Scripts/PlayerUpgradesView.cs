using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgradesView : MonoBehaviour
{
    [SerializeField] private GameObject view;
    [SerializeField] private PlayerUpgradesViewItem[] items;


    public void ShowUpgrades(Upgrade[] upgrades)
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            items[i].txtName.text = upgrades[i].UpgradeName;
            items[i].txtDescription.text = upgrades[i].UpgradeDescription;
            items[i].icon.sprite = upgrades[i].UpgradeIcon;
        }

        view.SetActive(true);
    }

    public void Hide()
    {
        view.SetActive(false);
    }
}

[System.Serializable]
public class PlayerUpgradesViewItem
{
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtDescription;
    public Image icon;
}
