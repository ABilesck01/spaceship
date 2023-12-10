using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MissionProgression
{
    public static readonly string Key = "progression";
    public static readonly int MaxMissions = 5;
    public int currentMissionIndex = 0;
    public List<string> upgrades;
    public float[] missionTimes;

    public void SaveData()
    {
        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(Key, json);

    }

    public void IncrementMissionIndex()
    {
        currentMissionIndex++;
        if(currentMissionIndex >= MaxMissions)
        {
            currentMissionIndex = MaxMissions;
        }

    }

    public void AddUpgrade(Upgrade upgrade)
    {
        if (upgrade == null) return;

        if (!upgrade.IsSaveble) return;

        upgrades.Add(upgrade.UpgradeName);

    }

    public MissionProgression()
    {
        upgrades = new List<string>();
        missionTimes = new float[]
        {
            0, 0, 0, 0, 0
        };
    }

    public void SetTimer(float timer, int index)
    {
        if(timer < missionTimes[index] || missionTimes[index] == 0)
        {
            missionTimes[index] = timer;
        }
    }

    public void GetData()
    {
        if (!PlayerPrefs.HasKey(Key))
        {
            this.currentMissionIndex = 0;
            return;

        }

        string json = PlayerPrefs.GetString(Key);
        MissionProgression obj = JsonUtility.FromJson<MissionProgression>(json);
        this.currentMissionIndex = obj.currentMissionIndex;
        this.upgrades = obj.upgrades;
        this.missionTimes = obj.missionTimes;
    }
}
