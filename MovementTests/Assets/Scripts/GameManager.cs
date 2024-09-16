using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public void PlayerDeath()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        Debug.Log("Player Death");
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) Destroy(player);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
