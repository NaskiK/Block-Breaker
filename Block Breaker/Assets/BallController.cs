using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    public float speed = 8f;
    public Vector2 initialDirection = Vector2.up + Vector2.right;
    private Rigidbody2D rb;
    private bool isLaunched = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Freeze until launch
    }

    void Update()
    {
        // Launch the ball with a mouse click
        if (!isLaunched && Input.GetMouseButtonDown(0))
        {
            LaunchBall();
        }

        // Keep the ball's speed constant
        if (isLaunched)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
        }
    }

    void LaunchBall()
    {
        rb.isKinematic = false;
        rb.linearVelocity = initialDirection.normalized * speed;
        isLaunched = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Calculate hit position relative to paddle center
            Vector3 paddlePos = collision.transform.position;
            float hitFactor = (transform.position.x - paddlePos.x) / collision.collider.bounds.size.x;

            // Create new direction and apply it
            Vector2 newDir = new Vector2(hitFactor, 1).normalized;
            rb.linearVelocity = newDir * speed;
        }

        if (collision.gameObject.CompareTag("Block"))
        {
            Destroy(collision.gameObject);
        }
    }

    public void ResetBall(Vector3 position)
    {
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;
        isLaunched = false;
        transform.position = position;
    }
}
