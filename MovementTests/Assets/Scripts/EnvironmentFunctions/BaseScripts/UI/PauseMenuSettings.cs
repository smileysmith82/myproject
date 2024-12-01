using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuSettings : MonoBehaviour
{
    public Slider musicToggle;
    public Slider volumeSlider;
    public TextMeshProUGUI volumeText;
    public SimpleIntBehaviour musicToggleData;
    void Start()
    {
        if (SettingsManager.Instance == null)
        {
            Debug.LogError("The SettingsManager is not initialized");
            return;
        }
        
        volumeSlider.value = SettingsManager.Instance.MusicVolume;
        UpdateVolumePercentageText(volumeSlider.value);

        musicToggle.value = musicToggleData.value > 51 ? 100 : 0;

        musicToggle.onValueChanged.AddListener(delegate { ToggleMusic((int)musicToggle.value); });
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }
    private void ToggleMusic(int value)
    {
        musicToggleData.SetValue(value > 51 ? 100 : 0);
        SettingsManager.Instance.SetMusicEnabled(value > 51);
        FindObjectOfType<BackgroundMusic>().ApplyMusicSettings();
    }
    private void SetVolume(float volume)
    {
        SettingsManager.Instance.MusicVolume = volume;
        FindObjectOfType<BackgroundMusic>().ApplyVolumeSettings();
        UpdateVolumePercentageText(volume);
    }

    private void UpdateVolumePercentageText(float volume)
    {
        int percentage = Mathf.RoundToInt(volume * 100);
        volumeText.text = $"Volume:        {percentage}%";
    }
    
}
