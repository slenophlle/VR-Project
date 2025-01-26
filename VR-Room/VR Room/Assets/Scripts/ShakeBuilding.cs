using UnityEngine;

public class ShakeBuilding : MonoBehaviour
{
    public float intensity = 0.2f; // Deprem þiddeti
    public float frequency = 2f;  // Deprem hýzý
    public float duration = 10f;  // Deprem süresi

    private Vector3 originalPosition; // Orijinal pozisyon
    private float timer = 0f;
    private bool isShaking = false;

    private float initialIntensity;
    private float initialFrequency;

    void Start()
    {
        originalPosition = transform.position;

        // Baþlangýç deðerlerini kaydet
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

                // Sallantý þiddetini azalt
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

        // Deðerleri sýfýrlayýn
        initialIntensity = quakeIntensity;
        initialFrequency = quakeFrequency;
        timer = 0f;
        isShaking = true;
    }

    public void StopShake()
    {
        isShaking = false;
        transform.position = originalPosition;

        // Deðerleri eski haline getir
        intensity = initialIntensity;
        frequency = initialFrequency;
    }
}
