using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject optionPanel;
    public bool isGamePaused = false;

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
