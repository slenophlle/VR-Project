using UnityEngine;

public class ShakeBuilding : MonoBehaviour
{
    public float intensity = 0.2f; // Deprem �iddeti
    public float frequency = 2f;  // Deprem h�z�
    public float duration = 10f;  // Deprem s�resi

    private Vector3 originalPosition; // Orijinal pozisyon
    private float timer = 0f;
    private bool isShaking = false;

    private float initialIntensity;
    private float initialFrequency;

    void Start()
    {
        originalPosition = transform.position;

        // Ba�lang�� de�erlerini kaydet
        initialIntensity = intensity;
        initialFrequency = frequency;
    }

    void Update()
    {
        if (isShaking)
        {
            if (timer < duration)
            {
                timer += Time.deltaTime;

                // Sallanma hareketi
                float randomX = (Mathf.PerlinNoise(Time.time * frequency, 0) - 0.5f) * 2 * intensity;
                float randomY = (Mathf.PerlinNoise(0, Time.time * frequency) - 0.5f) * 2 * intensity;

                transform.position = originalPosition + new Vector3(randomX, randomY, 0);

                // Sallant� �iddetini azalt
                float progress = timer / duration;
                intensity = Mathf.Lerp(initialIntensity, 0, progress);
                frequency = Mathf.Lerp(initialFrequency, 0, progress);
            }
            else
            {
                StopShake();
            }
        }
    }

    public void StartShake(float quakeIntensity, float quakeFrequency, float quakeDuration)
    {
        intensity = quakeIntensity;
        frequency = quakeFrequency;
        duration = quakeDuration;

        // De�erleri s�f�rlay�n
        initialIntensity = quakeIntensity;
        initialFrequency = quakeFrequency;
        timer = 0f;
        isShaking = true;
    }

    public void StopShake()
    {
        isShaking = false;
        transform.position = originalPosition;

        // De�erleri eski haline getir
        intensity = initialIntensity;
        frequency = initialFrequency;
    }
}
