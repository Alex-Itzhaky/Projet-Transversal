using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;

    [SerializeField] private AudioSource _soundFXObject;
    [SerializeField] private AudioSource _musicObject;
    [SerializeField] private AudioMixer _audioMixer;

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
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume = 1f, float pitchVariance = 0.05f)
    {
        AudioSource audioSource = Instantiate(_soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;

        if (pitchVariance != 0)
        {
            float randomPitch = Random.Range(1f - pitchVariance, 1f + pitchVariance);
            audioSource.pitch = randomPitch;
        }
        
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayMusicClip(AudioClip audioClip, Transform spawnTransform, float volume = 1f, bool looping = true)
    {
        AudioSource audioSource = Instantiate(_musicObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;

        audioSource.Play();
        
        if (!looping)
        {
            float clipLength = audioSource.clip.length;
            Destroy(audioSource.gameObject, clipLength);
        }
    }

    public void FadeMusicOut(float duration)
    {
        _audioMixer.DOSetFloat("musicVolume", -80f, duration).SetEase(Ease.OutQuint).SetUpdate(true);
    }

    public void FadeMusicIn(float duration)
    {
        _audioMixer.DOSetFloat("musicVolume", 0f, duration).SetEase(Ease.OutQuint).SetUpdate(true);
    }

    public void SetMasterVolume(float volume)
    {
        if (volume < 0.0001f || volume > 1f)
        {
            Debug.LogWarning("Le volume doit rester en 0.0001 et 1");
            return;
        }
        _audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20f);
    }

    public void SetSoundFXVolume(float volume)
    {
        if (volume < 0.0001f || volume > 1f)
        {
            Debug.LogWarning("Le volume doit rester en 0.0001 et 1");
            return;
        }
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20f);
    }

    public void SetMusicVolume(float volume)
    {
        if (volume < 0.0001f || volume > 1f)
        {
            Debug.LogWarning("Le volume doit rester en 0.0001 et 1");
            return;
        }
        _audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20f);
    }
}
