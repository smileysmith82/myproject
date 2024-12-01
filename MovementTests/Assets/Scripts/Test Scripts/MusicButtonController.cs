using UnityEngine;
using UnityEngine.UI;

public class MusicButtonController : MonoBehaviour
{
    public Backgroundmusic backgroundMusic;
    public GameObject cancelSymbol;
    void Start()
    {
        if (backgroundMusic == null)
        {
            GameObject gameManager = GameObject.Find("GameManager");
            if (gameManager != null)
            {
                backgroundMusic = gameManager.GetComponent<Backgroundmusic>();
            }

            if (backgroundMusic == null)
            {
                Debug.LogError("Background music not found on Game Manager");
            }
        }
    }

    public void OnButtonClick()
    {
        if (backgroundMusic == null)
        {
            Debug.LogError("Background Music reference is missing");
            return;
        }
        if (backgroundMusic.IsMusicPlaying())
        {
            backgroundMusic.PauseMusic();
            cancelSymbol.SetActive(true);
        }
        else
        {
            backgroundMusic.ResumeMusic();
            cancelSymbol.SetActive(false);
        }
    }
}