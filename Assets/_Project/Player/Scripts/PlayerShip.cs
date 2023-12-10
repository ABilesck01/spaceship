using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/New Ship")]
public class PlayerShip : ScriptableObject
{
    public string shipName;
    public Transform ship;
    public Transform shipPrefab;
}
