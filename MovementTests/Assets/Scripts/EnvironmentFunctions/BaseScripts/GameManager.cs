using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public LifeCounter lifeCounter;
    public SimpleFloatData healthData;
    public SimpleFloatData coins;
    public GameOverScreen gameOverScreen;
    public BackgroundMusic backgroundMusic;
    private float maxLevel = 1.1f;
    public void GameOver()
    {
        if (gameOverScreen != null)
        {
            gameOverUI.SetActive(true);
            gameOverScreen.Setup(maxLevel);    
        }
    }
    public void PlayerDeath()
    {

        GameOver();
        backgroundMusic.StopMusic();
        Debug.Log("Player Death");

        if (healthData != null)
        {
            healthData.SetValue(1.0f);
        }
        if (coins!= null)
            coins.SetValue(0.0f);

        if (lifeCounter != null)
        {
            lifeCounter.lives = 3;
            lifeCounter.UpdateLifeCounter();
        }
        
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) Destroy(player);
        
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void OnLevelComplete()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels! Congratulations! You will now be taken to the Main menu");
            SceneManager.LoadScene("MainMenu");
        }
    }
    void Start()
    {
        if (healthData != null)
            healthData.SetValue(1.0f);
    }
    
}
