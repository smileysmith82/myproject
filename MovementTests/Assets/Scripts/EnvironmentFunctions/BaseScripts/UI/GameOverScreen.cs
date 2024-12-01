using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text maxLevelText;
    public void Setup(float maxLevel)
    {
        gameObject.SetActive(true);
        maxLevelText.text = "You reached level\n" + maxLevel.ToString();
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("Level1.1");
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
