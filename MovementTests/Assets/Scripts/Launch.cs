using UnityEngine;

public class Launch : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * 100);
    }
    private void Update()
    {

    rb = GetComponent<Rigidbody2D>();
    rb.AddForce(Vector2.right * 20);
    }
}
