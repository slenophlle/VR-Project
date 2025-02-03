using UnityEngine;

public class ShakeBuilding : MonoBehaviour
{
    public float intensity = 0.2f; // Deprem þiddeti
    public float frequency = 1f;   // Deprem titreþim sýklýðý
    public float duration = 10f;   // Deprem süresi

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float timer = 0f;
    private bool isShaking = false;

    private float initialIntensity;
    private float initialFrequency;

    public Rigidbody buildingRb;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        initialIntensity = intensity;
        initialFrequency = frequency;
    }

    void FixedUpdate()
    {
        if (isShaking)
        {
            if (timer < duration)
            {
                timer += Time.fixedDeltaTime;

                float timeOffset = Time.time * frequency;

                // Daha doðal hareket için 3 farklý Perlin Noise deðeri hesaplayalým
                float randomX = (Mathf.PerlinNoise(timeOffset, 0.5f) - 0.5f) * intensity;
                float randomZ = (Mathf.PerlinNoise(0.5f, timeOffset) - 0.5f) * intensity;
                float randomY = (Mathf.PerlinNoise(timeOffset * 0.5f, 0.2f) - 0.5f) * intensity * 0.3f;


                Vector3 shakeOffset = new Vector3(randomX, randomY, randomZ);
                buildingRb.MovePosition(originalPosition + shakeOffset);

                // Hafif dönme hareketi ekleyelim
                float randomRotZ = (Mathf.PerlinNoise(timeOffset, 0.3f) - 0.5f) * intensity * 2;
                float randomRotX = (Mathf.PerlinNoise(0.3f, timeOffset) - 0.5f) * intensity * 2;

                Quaternion shakeRotation = Quaternion.Euler(randomRotX, 0, randomRotZ);
                buildingRb.MoveRotation(originalRotation * shakeRotation);

                // Deprem þiddetini zamanla azalt
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

        initialIntensity = quakeIntensity;
        initialFrequency = quakeFrequency;
        timer = 0f;
        isShaking = true;
    }

    public void StopShake()
    {
        isShaking = false;
        buildingRb.MovePosition(originalPosition);
        buildingRb.MoveRotation(originalRotation);

        intensity = initialIntensity;
        frequency = initialFrequency;
    }
}
