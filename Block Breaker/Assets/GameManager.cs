using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject pausePanel;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void Retry()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
