using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TriggerEventBehaviour : MonoBehaviour
{
    public UnityEvent triggerEvent;
    public Text coinCountText;
    private Player player;
    private LifeCounter lifeCounter;
    private AnimatorTest animatortest;
    private SoundTrigger soundTrigger;
    private SimpleImageBehaviour simpleImageBehaviour;
    
    void Start()
    {
        player = FindObjectOfType<Player>();
        animatortest = FindObjectOfType<AnimatorTest>();
        lifeCounter = FindObjectOfType<LifeCounter>();
        soundTrigger = FindObjectOfType<SoundTrigger>();
        simpleImageBehaviour = FindObjectOfType<SimpleImageBehaviour>();
        UpdateCoinUI();
    }
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Coin coin = other.GetComponent<Coin>();
            if (coin != null)
            {
                int coinValue = coin.GetValue();
                FindObjectOfType<SimpleImageBehaviour>().CollectCoin(coinValue);
                soundTrigger.PlayCoinSound(coinValue, simpleImageBehaviour.coinDataObj.value);
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("Trap"))
        {
            if (player != null)
            {
                animatortest.TriggerHitAnimation();
                lifeCounter.LoseLife();
            }
        }
        
    }

    private void UpdateCoinUI()
    {
        if (coinCountText != null)
        {
            coinCountText.text = "Coins: " + Mathf.FloorToInt(FindObjectOfType<SimpleImageBehaviour>().coinDataObj.value*100);
        }
    }
}
