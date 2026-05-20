using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool isLoadedFromMainMenu = false;

    public bool isGameOverPlaying { get; private set; } = false;
    public UnityEvent GameOver;

    [SerializeField] private GameObject _gameOverScreen;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        PauseManager.Instance.UnpauseGame();
        if (scene.name == "ScenePrototype" && !isLoadedFromMainMenu)
        {
            Debug.LogWarning("Le jeu n'a pas �t� lanc� depuis le menu principal. Redirection forc�e vers la sc�ne MainMenu...");
            SceneManager.LoadScene("MainMenu");
        }
        isGameOverPlaying = false;
    }

    private void OnSceneUnloaded(Scene scene)
    {

    }


    public void TriggerPlayerDeath()
    {
        Debug.Log("TriggerPlayerDeath");
        StartCoroutine(PlayerDeathCoroutine());
    }

    private IEnumerator PlayerDeathCoroutine()
    {
        isGameOverPlaying = true;

        yield return new WaitForSecondsRealtime(2);
        PauseManager.Instance.PauseGame();
        _gameOverScreen.SetActive(true);

    }
}
