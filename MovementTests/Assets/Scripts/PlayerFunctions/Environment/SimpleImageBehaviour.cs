using System;
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
            Debug.Log("Health Value: " + healthDataObj.value + ", Clamped Value: " + clampedHealthValue);
            imageObj.fillAmount = clampedHealthValue;
        }

        if (coinDataObj != null)
        {
            Debug.Log("Coins Value: " + coinDataObj.value);
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
