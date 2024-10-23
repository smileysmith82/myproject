using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{
    public int lives = 3;
    public Text LifeCounterText;
    public SimpleFloatData healthData;
    private GameManager gameManager;
    public float deathThreshold = -11.0f;
    public Respawn respawn;
    private Player player;
    private AnimatorTest animatorTest;
    public SimpleFloatData dataObj;
    private bool hasFallenBelowPlane = false;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        respawn = GetComponent<Respawn>();
        player = GetComponent<Player>();
        animatorTest = GetComponent<AnimatorTest>();
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
            hasFallenBelowPlane = true;
        }
        if (transform.position.y > deathThreshold)
        {
            hasFallenBelowPlane = false;
        }
    }
    public void LoseLife()
    {
        animatorTest.TriggerHitAnimation();
        if (dataObj != null)
        {
            dataObj.UpdateValue(-0.34f);
        }
        if (lives >=1)
        {
            lives--;
            respawn.RespawnPlayer();
            healthData.UpdateValue(-0.34f);
        }
        else if (lives ==0 || lives < 0)
        {
            Debug.Log("Game Over");
            gameManager.PlayerDeath();
            player.canMove = false;
        }
        
        UpdateLifeCounter();
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
