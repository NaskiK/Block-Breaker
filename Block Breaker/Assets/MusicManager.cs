using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;
    private float originalVolume;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        originalVolume = audioSource.volume;
    }

    public void PauseMusic()
    {
        audioSource.volume = originalVolume * 0.2f; // Lower volume on pause
    }

    public void ResumeMusic()
    {
        audioSource.volume = originalVolume;
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void RestartMusic()
    {
        audioSource.Stop();
        audioSource.Play();
    }
}
