using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{
    public int lives = 3;
    public Text LifeCounterText;
    private GameManager gameManager;
    public float deathThreshold = -11.0f;
    public Respawn respawn;
    private Player player;
    private bool hasFallenBelowPlane = false;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        respawn = GetComponent<Respawn>();
        player = GetComponent<Player>();
        UpdateLifeCounter();
    }

    public void Update()
    {
        if (transform.position.y <= deathThreshold)
        {
            LoseLife();
            hasFallenBelowPlane = true;
            
        }
        if (transform.position.y > deathThreshold)
        {
            hasFallenBelowPlane = false;
        }
    }
    public void LoseLife()
    {
        player.TriggerHitAnimation();
        if (lives >= 1)
        {
            lives--;
            respawn.RespawnPlayer();
        }
        if (lives == 0 || lives < 0)
        {
            Debug.Log("Game Over");
            gameManager.PlayerDeath();
            player.canMove = false;
        }
        UpdateLifeCounter();
    }

    private void UpdateLifeCounter()
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
