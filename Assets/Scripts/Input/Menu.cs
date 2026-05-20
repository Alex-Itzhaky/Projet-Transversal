using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("EI-Main scene");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        //Debug.Log temporaire pour vérifier que le bouton fonctionne
        Debug.Log("Quit");
        Application.Quit();
    }
}
