using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private Vector3 originalPos;
    private float shakeTime = 0;
    private float shakeMag = 0;

    void Awake()
    {
        Instance = this;
        originalPos = transform.position;
    }

    void Update()
    {
        if (shakeTime > 0)
        {
            transform.position = originalPos + (Vector3)Random.insideUnitCircle * shakeMag;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            transform.position = originalPos;
        }
    }

    public void Shake(float duration, float magnitude)
    {
        shakeTime = duration;
        shakeMag = magnitude;
    }
}
