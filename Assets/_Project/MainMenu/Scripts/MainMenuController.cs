using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject firstSelected;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    private void OnEnable()
    {
        SettingsController.OnClose += SettingsController_OnClose;
    }

    private void OnDisable()
    {
        SettingsController.OnClose -= SettingsController_OnClose;
    }

    private void SettingsController_OnClose(object sender, System.EventArgs e)
    {
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    public void PlayGame() 
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    public void SettingGame() 
    {
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    public void CampaingGame()
    {
        SceneManager.LoadScene(CampaingManager.SceneName);
    }
}
