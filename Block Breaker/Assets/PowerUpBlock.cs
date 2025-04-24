using UnityEngine;

public class PowerUpBlock : MonoBehaviour
{
    public GameObject explosionEffect;
    public float explosionRadius = 1.5f;
    public AudioClip explosionSound; // Assign in Inspector

    private bool isExploding = false;  // Track if the explosion is currently being processed

    void Explode()
    {
        if (isExploding) return;  // Prevent re-triggering explosion during chain reaction

        isExploding = true;  // Mark as exploding to avoid multiple triggers

        // Play explosion sound
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        // Show explosion particles
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Destroy nearby blocks
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D col in colliders)
        {
            if (col != null && col.gameObject != gameObject)
            {
                // Trigger chain reaction if it's another power-up block
                PowerUpBlock other = col.GetComponent<PowerUpBlock>();
                if (other != null && !other.isExploding)
                {
                    other.Explode();  // Chain reaction!
                }
                else if (col.CompareTag("Block"))
                {
                    Destroy(col.gameObject);  // Destroy regular block
                }
            }
        }

        // Destroy the current block
        Destroy(gameObject);
        isExploding = false;  // Allow future explosions
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Explode();  // Trigger explosion when ball hits
        }
    }
}
