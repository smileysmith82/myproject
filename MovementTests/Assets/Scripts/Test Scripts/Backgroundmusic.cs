using UnityEngine;
using UnityEngine.UI;
public class Backgroundmusic : MonoBehaviour

{
    private AudioSource audioSource;
    public bool IsMuted => audioSource.mute;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        ApplyMusicSettings();
    }
    public void PlayMusic()
    {
        if (!audioSource.mute)
        {
            audioSource.Play();
        }
    }
    public void PauseMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }
    public void ResumeMusic()
    {
        if (!audioSource.mute && !audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }
    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void ApplyMusicSettings()
    {
        if (SettingsManager.Instance != null)
        {
            audioSource.volume = SettingsManager.Instance.MusicVolume;
        }
    }

    public void ApplyVolumeSettings()
    {
        if (SettingsManager.Instance != null)
        {
            audioSource.volume = SettingsManager.Instance.MusicVolume;
        }
    }

    public bool IsMusicPlaying()
    {
        return audioSource.isPlaying;
    }
}
