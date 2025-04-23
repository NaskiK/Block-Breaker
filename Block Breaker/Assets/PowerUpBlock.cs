using UnityEngine;

public class PowerUpBlock : MonoBehaviour
{
    public GameObject ballPrefab; // Assign this in the Inspector

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Spawn 2 extra balls at the position of the original ball
            SpawnExtraBalls(collision.gameObject.transform.position);
            Destroy(gameObject); // Destroy the power-up block
        }
    }

    void SpawnExtraBalls(Vector2 origin)
    {
        for (int i = 0; i < 2; i++)
        {
            // Instantiate new balls at the position of the main ball
            GameObject newBall = Instantiate(ballPrefab, origin, Quaternion.identity);

            Rigidbody2D rb = newBall.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Apply a random direction to the newly spawned ball
                Vector2 direction = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
                rb.linearVelocity = direction * 5f; // Adjust the velocity if necessary
            }

            // Ensure the new ball is tagged as "Ball"
            newBall.tag = "Ball";
        }
    }
}
