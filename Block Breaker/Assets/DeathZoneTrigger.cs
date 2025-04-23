using UnityEngine;

public class DeathZoneTrigger : MonoBehaviour
{
    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            gm.GameOver();
        }
    }
}
