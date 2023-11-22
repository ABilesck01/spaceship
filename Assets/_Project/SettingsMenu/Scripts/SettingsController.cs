using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private GameObject firstSelected;
    [Space]
    [SerializeField] private AudioMixer audioMixer;

    public static event EventHandler OnClose;

    private void Start()
    {
        EventSystem.current.firstSelectedGameObject = firstSelected;
    }

    public void ToggleQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void ToggleMaxFps(int fps)
    {
        Application.targetFrameRate = fps;
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void Close()
    {
        SceneManager.UnloadSceneAsync("Settings");
        OnClose?.Invoke(this, EventArgs.Empty);
    }
}
