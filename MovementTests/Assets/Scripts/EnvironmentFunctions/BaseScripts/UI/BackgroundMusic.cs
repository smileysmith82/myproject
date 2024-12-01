using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        ApplyMusicSettings();
    }
    public void SetMusicEnabled(bool isEnabled)
    {
        SettingsManager.Instance.SetMusicEnabled(isEnabled);
        ApplyMusicSettings();
    }
    public void SetVolume(float volume)
    {
        SettingsManager.Instance.SetMusicVolume(volume);
        ApplyVolumeSettings();
    }

    public bool IsMusicEnabled()
    {
        return !audioSource.mute;
    }

    public float GetVolume()
    {
        return audioSource.volume;
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
            audioSource.mute = !SettingsManager.Instance.isMusicEnabled;
        }
    }

    public void ApplyVolumeSettings()
    {
        if (SettingsManager.Instance != null)
        {
            audioSource.volume = SettingsManager.Instance.MusicVolume;
        }
    }
    
}
