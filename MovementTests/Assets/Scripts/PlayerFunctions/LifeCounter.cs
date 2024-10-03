using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{
    public int lives = 3;
    public Text LifeCounterText;
    private GameManager gameManager;
    public float deathThreshold = -11.0f;
    public Respawn respawn;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        respawn = GetComponent<Respawn>();
        UpdateLifeCounter();
    }

    public void Update()
    {
        if (transform.position.y < deathThreshold)
        {
            LoseLife();
        }
    }
    public void LoseLife()
    {
        lives--;
        UpdateLifeCounter();

        if (lives <= 0)
        {
            Debug.Log("Game Over");
            gameManager.PlayerDeath();
        }
        else
        {
            respawn.RespawnPlayer();
        }
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            LoseLife();
        }
    }
}
