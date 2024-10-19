using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class TriggerEventBehaviour : MonoBehaviour
{
    public UnityEvent triggerEvent;
    public int coinCount = 0;
    public Text coinCountText;
    private Player player;
    private LifeCounter lifeCounter;

    void Start()
    {
        player = FindObjectOfType<Player>();
        lifeCounter = FindObjectOfType<LifeCounter>();
        UpdateCoinUI();
    }
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCount++;
            Debug.Log("Coin Collected");
            Destroy(other.gameObject);
            UpdateCoinUI();
        }
        else if (other.CompareTag("Trap"))
        {
            if (player != null)
            {
                player.TriggerHitAnimation();
                lifeCounter.LoseLife();
            }
        }
        
    }

    private void UpdateCoinUI()
    {
        if (coinCountText != null)
        {
            coinCountText.text = "Coins: " + coinCount;
        }
    }
}
