using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private BossController[] bosses;
    [SerializeField] private Transform bossPosition;

    private void OnEnable()
    {
        GameManager.OnBossFight += GameManager_OnBossFight;
    }

    private void OnDisable()
    {
        GameManager.OnBossFight -= GameManager_OnBossFight;
    }

    private void GameManager_OnBossFight(object sender, System.EventArgs e)
    {
        int rand = Random.Range(0, bosses.Length);
        Instantiate(bosses[rand], bossPosition.position, Quaternion.identity);
    }
}
