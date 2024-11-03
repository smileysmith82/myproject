using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{
    [Header("Retrieving Variables")]
    private GameManager gameManager;
    public Respawn respawn;
    private Player player;
    private AnimatorTest animatorTest;
    public SimpleFloatData healthData;
    public Text LifeCounterText;
    
    [Header("In Game Variables")]
    public float deathThreshold = -11.0f;
    public int startingLives = 3;
    public int lives;
    public int maxLives = 100;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        respawn = GetComponent<Respawn>();
        player = GetComponent<Player>();
        animatorTest = GetComponent<AnimatorTest>();
        lives = startingLives;
        UpdateLifeCounter();
        if (healthData != null)
        {
            healthData.SetValue(1.0f);
        }
    }

    public void Update()
    {
        if (transform.position.y <= deathThreshold)
        {
            LoseLife();
        }
    }
    public void LoseLife()
    {
        animatorTest.TriggerHitAnimation();
        if (lives <= 0) return;
        
        if (lives > 1)
        {
            float healthLostPercentage = 1f/lives;
            healthData.UpdateValue(-healthLostPercentage);
            lives--;
            respawn.RespawnPlayer();
        }
        else if (lives == 1)
        {
            
            float healthLostPercentage = 1f/lives;
            healthData?.UpdateValue(-healthLostPercentage);
            lives--;
            UpdateLifeCounter();
            Debug.Log("Game Over");
            gameManager.PlayerDeath();
            player.canMove = false;
        }
        
        UpdateLifeCounter();
    }
    
    public void GainLife()
    {
        if (lives < maxLives)
        {
            lives++;
            UpdateLifeCounter();
            Debug.Log("Gained a life! Total Lives: " + lives);
        }
        else
        {
            Debug.Log("Maximum Lives reached!");
        }
    }
    public void UpdateLifeCounter()
    {
        if (LifeCounterText != null)
        {
            LifeCounterText.text = "Lives: " + lives;
        }
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")  || collision.gameObject.CompareTag("Trap"))
        {
            LoseLife();
        }
    }
}
