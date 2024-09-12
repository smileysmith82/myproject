using UnityEngine;

public class Forces : MonoBehaviour
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
        rb.AddForce(Vector2.right * 10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with " + collision.gameObject.name);
    }
}
