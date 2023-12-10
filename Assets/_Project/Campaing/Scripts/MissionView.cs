using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionView : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;

    private void OnEnable()
    {
        CampaingManager.OnGameWin += CampaingManager_OnGameWin;
    }

    private void OnDisable()
    {
        CampaingManager.OnGameWin -= CampaingManager_OnGameWin;
    }

    private void CampaingManager_OnGameWin(object sender, System.EventArgs e)
    {
        winScreen.SetActive(true);
    }

    public void BackToMissions()
    {
        SceneManager.LoadScene(CampaingManager.SceneName);
    }
}
