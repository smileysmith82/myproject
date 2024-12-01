using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    public bool isMusicEnabled = true;
    public float MusicVolume = 1.0f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetMusicEnabled(bool isEnabled)
    {
        isMusicEnabled = isEnabled;
    }
    public void SetMusicVolume(float volume)
    {
        MusicVolume = volume;
    }
}
