using System.Collections;
using Unity.VectorGraphics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private float _sceneFadeDuration;
    [SerializeField] private SceneFade _sceneFade;

    private IEnumerator Start()
    {
        //SoundFXManager.Instance.UseStoredVolumes();
        yield return _sceneFade.FadeInCoroutine(_sceneFadeDuration);
    }
    

    public void LoadScene(string scene)
    {
        StartCoroutine(LoadSceneCoroutine(scene));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
       //SoundFXManager.Instance.StoreVolumes();
        yield return SoundFXManager.Instance.FadeMusicOut(_sceneFadeDuration);
        yield return _sceneFade.FadeOutCoroutine(_sceneFadeDuration);
        yield return SceneManager.LoadSceneAsync(sceneName);
        //PauseManager.Instance.UnpauseGame();
        SoundFXManager.Instance.FadeMusicIn(_sceneFadeDuration);
    }
}
