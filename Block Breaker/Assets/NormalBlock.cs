using UnityEngine;

public class NormalBlock : MonoBehaviour
{
    public AudioClip destroySound; // Sound to play when the block is destroyed
    public GameObject destroyEffect; // Particle effect when destroyed

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Play destroy sound if available
            if (destroySound != null)
            {
                AudioSource.PlayClipAtPoint(destroySound, transform.position);
            }

            // Trigger particle effect when block is destroyed
            if (destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
            }

            // Inform the GameManager that a block has been destroyed
            FindObjectOfType<GameManager>().BlockDestroyed();

            // Destroy the block immediately
            Destroy(gameObject);
        }
    }
}
