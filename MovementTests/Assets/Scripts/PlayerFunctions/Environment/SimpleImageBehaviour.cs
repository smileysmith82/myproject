using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SimpleImageBehaviour : MonoBehaviour
{
    private Image imageObj;
    public SimpleFloatData healthDataObj;
    public SimpleFloatData coinDataObj;
    public int lives = 3;
    public LifeCounter lifecounter;
    private const int maxCoinsforLife = 100;
    
    public Text coinCountText;
    private SoundTrigger soundTrigger;
    
    private void Start()
    {
        imageObj = GetComponent<Image>();
        if (lifecounter == null)
        {
            lifecounter = FindObjectOfType<LifeCounter>();
        }

        coinDataObj.value = 0.0f;
        UpdateWithFloatData();
        UpdateCoinUI();
        
        soundTrigger = FindObjectOfType<SoundTrigger>();
    }
    public void Update()
    {
        UpdateWithFloatData();
    }
    public void UpdateWithFloatData()
    {
        if (healthDataObj != null)
        {
            float clampedHealthValue = Mathf.Clamp01(healthDataObj.value);
            imageObj.fillAmount = clampedHealthValue;
        }

        if (coinDataObj != null)
        {
            float fillAmount = Mathf.Clamp01(coinDataObj.value / maxCoinsforLife);
            imageObj.fillAmount = fillAmount;
            
            if (coinDataObj.value >= maxCoinsforLife)
            {
                coinDataObj.value -= maxCoinsforLife;
                lifecounter.GainLife();
                lifecounter.UpdateLifeCounter();
            }
            UpdateCoinUI();
        }
    }

    public void CollectCoin(int coinValue)
    {
        if (coinDataObj != null)
        {
            coinDataObj.UpdateValue(coinValue);
            UpdateCoinUI();
            soundTrigger.PlayCoinSound(coinValue, coinDataObj.value);
        }
    }

    private void UpdateCoinUI()
    {
        if (coinCountText != null)
        {
            coinCountText.text = "Coins: " + Mathf.FloorToInt(coinDataObj.value);
        }
    }
}
