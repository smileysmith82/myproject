using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Toggle musicToggle;
    public Slider volumeSlider;
    private BackgroundMusic backgroundMusic;
    void Start()
    {
        backgroundMusic = GameObject.Find("BackgroundMusic").GetComponent<BackgroundMusic>();
        musicToggle.isOn = backgroundMusic.IsMusicEnabled();
        volumeSlider.value = backgroundMusic.GetVolume();
        
        musicToggle.onValueChanged.AddListener(ToggleMusic);
        volumeSlider.onValueChanged.AddListener(AdjustVolume);
    }
    private void ToggleMusic(bool isEnabled)
    {
        SettingsManager.Instance.SetMusicEnabled(isEnabled);
        backgroundMusic.SetMusicEnabled(isEnabled);
    }
    private void AdjustVolume(float volume)
    {
        SettingsManager.Instance.SetMusicVolume(volume);
        backgroundMusic.SetVolume(volume);
    }
}
