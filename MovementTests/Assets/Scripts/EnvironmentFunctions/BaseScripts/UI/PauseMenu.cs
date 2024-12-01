using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject SettingsMenuUI;
    public BackgroundMusic backgroundMusic;

    private void Start()
    {
        backgroundMusic = GameObject.Find("BackgroundMusic").GetComponent<BackgroundMusic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        backgroundMusic.ResumeMusic();
        GameIsPaused = false;
    }

    void Pause()
    {
        
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        backgroundMusic.PauseMusic();
        GameIsPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    
}
