using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SceneAlex");
    }

    public void QuitGame()
    {
        //Debug.Log temporaire pour vérifier que le bouton fonctionne
        Debug.Log("Quit");
        Application.Quit();
    }
}
