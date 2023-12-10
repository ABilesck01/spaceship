using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionSelection : MonoBehaviour
{
    [SerializeField] private MissionSelectionItem template;
    [SerializeField] private Transform container;
    [SerializeField] private MissionData[] missionDatas;

    private void Start()
    {
        MissionProgression missionProgression = new MissionProgression();
        missionProgression.GetData();
        
        for (int i = 0; i < missionDatas.Length; i++)
        {
            var item = Instantiate(template, container);
            item.Fill(missionDatas[i], i <= missionProgression.currentMissionIndex, missionProgression.missionTimes[i], missionDatas[i].secondChoice != null);
            if (missionDatas[i].secondChoice != null)
            {
                var secondItem = Instantiate(template, container);
                secondItem.Fill(missionDatas[i].secondChoice, i <= missionProgression.currentMissionIndex, missionProgression.missionTimes[i], missionDatas[i].secondChoice != null);
            }
        }
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("StartMenu");
    }

}
