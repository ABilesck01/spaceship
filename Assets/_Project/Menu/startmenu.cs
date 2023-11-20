using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class startmenu : MonoBehaviour
{
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;
    [SerializeField] private GameObject endFirstSelected;
    private void OnEnable()
    {
        GameManager.OnStartGame += GameManager_OnStartGame;
        GameManager.OnGameOver += GameManager_OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.OnStartGame -= GameManager_OnStartGame;
        GameManager.OnGameOver -= GameManager_OnGameOver;
    }

    private void GameManager_OnStartGame(object sender, System.EventArgs e)
    {
        start.SetActive(false);
    }

    private void GameManager_OnGameOver(object sender, System.EventArgs e)
    {
        end.SetActive(true);
        EventSystem.current.SetSelectedGameObject(endFirstSelected);
    }

    public void BtnRestartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
