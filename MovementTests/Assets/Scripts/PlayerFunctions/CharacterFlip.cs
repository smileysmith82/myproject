using System;
using UnityEngine;

public class CharacterFlip : MonoBehaviour
{
    private Rigidbody2D rb;
    public int facingDirection = 1;
    public float threshold = 0.1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        FlipCharacter();
    }

    private void FlipCharacter()
    {
        if (Mathf.Abs(rb.velocity.x) > threshold)
        {
            if (rb.velocity.x > 0)
            {
                facingDirection = 1;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (rb.velocity.x < 0)
            {
                facingDirection = -1;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

}
