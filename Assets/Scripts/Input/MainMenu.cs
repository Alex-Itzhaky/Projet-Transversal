using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject optionPanel;

    [SerializeField] private AudioClip _mainMenuMusic;

    public UnityEvent OnPlay;
    public UnityEvent OnRestart;
    public UnityEvent OnLeave;

    private static bool _isMainMenu = true;

    private void Start()
    {
        if (_isMainMenu)
            SoundFXManager.Instance.PlayMusicClip(_mainMenuMusic, Camera.main.transform);
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene("EI-Main scene");
        OnPlay.Invoke();
        _isMainMenu = false;
    }
    
    
    public void RestartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        OnRestart.Invoke();
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

    public void QuitGame()
    {
        //Debug.Log temporaire pour vérifier que le bouton fonctionne
        Debug.Log("Quit");
        Application.Quit();
    }
}
