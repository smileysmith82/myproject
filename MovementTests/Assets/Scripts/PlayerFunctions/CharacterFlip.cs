using UnityEngine;

public class CharacterFlip : MonoBehaviour
{
    public KeyCode key1 = KeyCode.RightArrow, key2 = KeyCode.LeftArrow;
    public float direction1 = 0, direction2 = 180;
    public int facingDirection = 1;
    

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(key1))
        {
            facingDirection = 1;
            transform.rotation = Quaternion.Euler(0, direction1, 0);
        }

        if (Input.GetKeyDown(key2))
        {
            facingDirection = -1;
            transform.rotation = Quaternion.Euler(0, direction2, 0);
        }
        
    }
/*
 private Rigidbody2D rb;
     private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        flipCharacter();
    }
    public void flipCharacter()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {if (rb.velocity.x >= 0)
            {
                facingDirection = 1;
                transform.rotation = Quaternion.Euler(0, right, 0);
            }
            else if (rb.velocity.x <= 0)
            {
                facingDirection = -1;
                transform.rotation = Quaternion.Euler(0, left, 0);
            }
        }
    }
 */

}
