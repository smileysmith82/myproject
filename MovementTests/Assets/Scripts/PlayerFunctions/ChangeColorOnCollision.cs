
using UnityEngine;

public class ChangeColorOnCollision : MonoBehaviour
{
    public byte red = 212;
    public byte green = 33;
    public byte blue = 49;
    public byte alpha = 255;
    private Rigidbody2D rb;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                red = (byte)Mathf.Clamp(red - 60, 0, 255);
                green = (byte)Mathf.Clamp(green - 9, 0, 255);
                blue = (byte)Mathf.Clamp(blue - 15, 0, 255);
                Color32 newColor = new Color32(red, green, blue, alpha);
                spriteRenderer.color = newColor;
                if (red == 0 && green == 0 && blue == 0)
                {
                    Debug.Log("Object is now Black");
                    gameManager.PlayerDeath();
                }
            }
        }
    }
}
