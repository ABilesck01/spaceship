using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/New Mission")]
public class MissionData : ScriptableObject
{
    public string missionName;
    [TextArea(3, 10)] public string briefing;
    [TextArea (3,10)] public string ending;
    [Space]
    public int scoreToWin = 1000;
    public int missionIndex = 0;
    public BossController boss;
    public MissionData secondChoice;
}
