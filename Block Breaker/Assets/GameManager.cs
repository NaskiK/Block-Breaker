using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject winUI; // Add reference to your "You Win" UI
    public GameObject pausePanel;
    public AudioClip gameOverSound; // Assign this in the Inspector
    public AudioClip winSound; // Sound to play when you win

    private AudioSource audioSource;
    private bool isPaused = false;

    private int totalBlocks;  // The total number of blocks (count only regular blocks)
    private int remainingBlocks;  // The number of blocks left in the game

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InitializeBlocks(); // Initialize block count at the start of the game
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Initialize the count of remaining blocks
    private void InitializeBlocks()
    {
        // Find all blocks that are tagged as "Block" (regular blocks)
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        totalBlocks = blocks.Length;

        // Set the remaining blocks count to the total blocks count
        remainingBlocks = totalBlocks;
    }

    // Call this method when a regular block is destroyed
    public void BlockDestroyed()
    {
        remainingBlocks--; // Reduce the number of remaining blocks
        if (remainingBlocks <= 0)
        {
            YouWin(); // If there are no more blocks, trigger the "You Win" screen
        }
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;

        if (gameOverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }

        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.StopMusic();
        }
    }

    public void YouWin()
    {
        winUI.SetActive(true); // Show "You Win" UI
        Time.timeScale = 0f; // Stop the game

        if (winSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(winSound); // Play "You Win" sound
        }

        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.StopMusic();
        }
    }

    public void Retry()
    {
        Time.timeScale = 1f;

        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.RestartMusic();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;

            if (MusicManager.Instance != null)
            {
                MusicManager.Instance.PauseMusic();
            }
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;

            if (MusicManager.Instance != null)
            {
                MusicManager.Instance.ResumeMusic();
            }
        }
    }
}
