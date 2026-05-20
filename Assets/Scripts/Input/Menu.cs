using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject optionPanel;
    
    public void PlayGame()
    {
        SceneManager.LoadScene("EI-Main scene");
    }

    public void ResumeGame()
    {
        menuPanel.SetActive(false);
    }

    public void PauseGame()
    {
        menuPanel.SetActive(true);
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    public void LeaveGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        //Debug.Log temporaire pour vérifier que le bouton fonctionne
        Debug.Log("Quit");
        Application.Quit();
    }
}
