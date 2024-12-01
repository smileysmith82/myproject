using UnityEngine;
using UnityEngine.UI;

public class MusicToggleSlider : MonoBehaviour
{
    public Slider slider;
    public AudioSource audioSource;
    public SimpleFloatData musicToggle;
    
    private void Start()
    {
        float savedValue = PlayerPrefs.GetFloat("MusicToggle", 100);
        slider.value = savedValue;
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        UpdateMusicState();
    }

    private void OnSliderValueChanged(float value)
    {
        slider.value = value > 51 ? 100: 0;
        musicToggle.SetValue(slider.value);
        PlayerPrefs.SetFloat("MusicToggle", slider.value);
        PlayerPrefs.Save();
        UpdateMusicState();
    }

    private void UpdateMusicState()
    {
        if (musicToggle.value == 100)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
