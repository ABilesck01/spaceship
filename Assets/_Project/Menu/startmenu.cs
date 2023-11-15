using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startmenu : MonoBehaviour
{
    [SerializeField] private GameObject start;
    private void OnEnable()
    {
        GameManager.OnStartGame += GameManager_OnStartGame;
    }
    private void OnDisable()
    {
        GameManager.OnStartGame -= GameManager_OnStartGame;

    }
    private void GameManager_OnStartGame(object sender, System.EventArgs e)
    {
        start.SetActive(false);
    }
}
