using UnityEngine;

public class EarthquakeManager : MonoBehaviour
{
    public ShakeBuilding shakeEffect;

    [Header("Quake Values")]
    public float[] quakeIntensities = { 0.1f, 0.2f, 0.3f }; // Deprem �iddeti a�amalar�
    public float[] quakeFrequencies = { 5f, 8f, 10f };      // Deprem frekans� a�amalar�
    public float[] quakeDurations = { 12f, 13f, 6f };        // Deprem s�releri (her a�ama i�in farkl�)
    public float earthquakeInterval = 12f;                  // �ki deprem aras�ndaki s�re

    private int currentStage = 0; // Mevcut deprem a�amas�
    private float timer = 0f;     // Deprem a�amalar� aras�ndaki s�reyi takip eder
    public static bool canActive = false;

    [Header("Audio Sources")]
    public AudioSource[] soundStages; // Her a�ama i�in farkl� ses

    void Update()
    {
        if (!canActive) return;

        timer += Time.deltaTime;

        if (timer >= earthquakeInterval && currentStage < quakeIntensities.Length)
        {
            timer = 0f;

            // Her a�aman�n de�erlerini al
            float intensity = quakeIntensities[currentStage];
            float frequency = quakeFrequencies[currentStage];
            float duration = quakeDurations[currentStage];

            // ShakeEffect'i ba�lat
            shakeEffect.StartShake(intensity, frequency, duration);

            // �lgili sesi �al
            if (currentStage < soundStages.Length)
            {
                soundStages[currentStage].Play();
            }

            Debug.Log($"Deprem A�amas� {currentStage + 1} ba�lad�! �iddet: {intensity}, Frekans: {frequency}, S�re: {duration}");

            // Bir sonraki a�amaya ge�
            currentStage++;
        }
    }

    public void ResetEarthquake()
    {
        currentStage = 0;
        timer = 0f;

        // T�m sesleri durdur
        foreach (var sound in soundStages)
        {
            sound.Stop();
        }
    }
}
