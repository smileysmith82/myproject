using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public float walkOffSpeed = 3f;
    public float transitionDelay = 2f;
    private bool isTransitioning = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTransitioning)
        {
            isTransitioning = true;
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                //player.StartLevelEndTransition(walkOffSpeed);
            }
            Invoke("LoadNextLevel", transitionDelay);
        }
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
