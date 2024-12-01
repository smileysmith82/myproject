using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    [Header("If Bag Has 4 or less coins")]
    public AudioClip lowSingleCoinSound;
    public AudioClip lowMediumCoinSound;
    public AudioClip lowHighCoinSound;
    [Header("If Bag Has between 5 and 49 Coins")]
    public AudioClip mediumSingleCoinSound;
    public AudioClip mediumMediumCoinSound;
    public AudioClip mediumHighCoinSound;
    [Header("If Bag Has more than 50 Coins")]
    public AudioClip highSingleCoinSound;
    public AudioClip highMediumCoinSound;
    public AudioClip highHighCoinSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    public void PlayCoinSound(int coinValue, float currentCoinsInBag)
    {
        AudioClip selectedSound = null;
        if (currentCoinsInBag < 5)
        {
            if (coinValue >= 10)
            {
                selectedSound = lowHighCoinSound;
            }
            else if (coinValue >= 3)
            {
                selectedSound = lowMediumCoinSound;
            }
            else
            {
                selectedSound = lowSingleCoinSound;
            }
        }
        else if (currentCoinsInBag >= 5 && currentCoinsInBag < 50)
        {
            if (coinValue >= 10)
            {
                selectedSound = mediumHighCoinSound;
            }
            else if (coinValue >= 3)
            {
                selectedSound = mediumMediumCoinSound;
            }
            else
            {
                selectedSound = mediumSingleCoinSound;
            }
        }
        else
        {
            if (coinValue >= 50)
            {
                selectedSound = highHighCoinSound;
            }
            else if (coinValue >= 3)
            {
                selectedSound = highMediumCoinSound;
            }
            else
            {
                selectedSound = highSingleCoinSound;
            }
        }
        audioSource.clip = selectedSound;
        audioSource.Play();
    }
}
