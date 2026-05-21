using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject optionPanel;
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _SFXVolumeSlider;
    public bool isGamePaused = false;

    public UnityEvent OnRestart;
    public UnityEvent OnLeave;

    //private IEnumerator Start()
    //{
    //    yield return null;
    //    float masterVolume;
    //    float musicVolume;
    //    float SFXVolume;
    //    SoundFXManager.Instance._audioMixer.GetFloat("masterVolume", out masterVolume);
    //    SoundFXManager.Instance._audioMixer.GetFloat("musicVolume", out musicVolume);
    //    SoundFXManager.Instance._audioMixer.GetFloat("masterVolume", out SFXVolume);

    //    _masterVolumeSlider.value = masterVolume;
    //    _musicVolumeSlider.value = musicVolume;
    //    _SFXVolumeSlider.value = SFXVolume;

    //}

    public void Update()
    {
        if (InputManager.Instance.IsEscapePressed)
        {
            if (!isGamePaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        menuPanel.SetActive(true);
        isGamePaused = true;
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        menuPanel.SetActive(false);
        isGamePaused = false;
    }
    
    public void GoToOption()
    {
        menuPanel.SetActive(false);
        optionPanel.SetActive(true);
    }
    
    public void BackToMenu()
    {
        optionPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void LeaveGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
