using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionSelectionItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtTimer;
    [SerializeField] private GameObject choiceTag;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Fill(MissionData missionData, bool isUnlocked, float timer, bool isChoice = false)
    {
        txtName.text = missionData.missionName;
        button.interactable = isUnlocked;
        choiceTag.SetActive(isChoice && isUnlocked);
        int minutes = (int)(timer / 60);
        int seconds = (int)(timer % 60);
        txtTimer.text = $"{minutes:00}:{seconds:00}";
        button.onClick.AddListener(() =>
        { 
            CampaingManager.MissionData = missionData;
            SceneManager.LoadScene("Mission");
            
        });

    }
}
