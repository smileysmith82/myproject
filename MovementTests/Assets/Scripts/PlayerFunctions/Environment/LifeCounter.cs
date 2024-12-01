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
    public int maxLives = 99;
    [Header("Audio")] 
    public AudioSource audioSource;
    public AudioClip LevelUpSound;
    public AudioClip teleportSound;
    public AudioClip ouchSound;
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
            if (lives == 1)
            {
                Debug.Log("Game Over");
                gameManager.PlayerDeath();
                player.canMove = false;
            }
            else
            {
                audioSource.PlayOneShot(teleportSound);
                LoseLife();    
            }
            
        }
    }
    public void LoseLife()
    {
        animatorTest.TriggerHitAnimation();
        if (lives <= 0) return;
        if (lives < 4)
        {
            healthData.UpdateValue(-0.34f);     
        }
        lives--;
        respawn.RespawnPlayer();
        
        if (lives == 0)
        {
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
            if (lives < 4)
            {
                healthData.UpdateValue(0.34f);

                if (healthData.value > 1.0f)
                {
                    healthData.SetValue(1.0f);
                }
            }
            UpdateLifeCounter();
            PlayLevelUpSound();
            Debug.Log("Gained a life! Total Lives: " + lives);
        }
        else
        {
            Debug.Log("Maximum Lives reached!");
        }
    }
    private void PlayLevelUpSound()
    {
        if (audioSource != null && LevelUpSound != null)
        {
            audioSource.PlayOneShot(LevelUpSound);
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
            audioSource.pitch = 1;
            audioSource.PlayOneShot(ouchSound);
            
            LoseLife();
        }
    }
}
