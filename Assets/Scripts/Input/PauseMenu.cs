using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject optionPanel;
    public bool isGamePaused = false;

    public void Update()
    {
        if (InputManager.Instance.IsEscapePressed)
        {
            if (isGamePaused)
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
        menuPanel.SetActive(true);
        isGamePaused = true;
    }
    
    public void ResumeGame()
    {
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
    }
    
    public void LeaveGame()
    {
    }
}
