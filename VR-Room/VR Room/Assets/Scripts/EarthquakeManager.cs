using UnityEngine;

public class EarthquakeManager : MonoBehaviour
{
    public ShakeBuilding shakeEffect;

    [Header("Quake Values")]
    public float[] quakeIntensities = { 0.1f, 0.2f, 0.3f }; // Deprem þiddeti aþamalarý
    public float[] quakeFrequencies = { 5f, 8f, 10f };      // Deprem frekansý aþamalarý
    public float[] quakeDurations = { 12f, 13f, 6f };        // Deprem süreleri (her aþama için farklý)
    public float earthquakeInterval = 12f;                  // Ýki deprem arasýndaki süre

    private int currentStage = 0; // Mevcut deprem aþamasý
    private float timer = 0f;     // Deprem aþamalarý arasýndaki süreyi takip eder
    public static bool canActive = false;

    [Header("Audio Sources")]
    public AudioSource[] soundStages; // Her aþama için farklý ses

    void Update()
    {
        if (!canActive) return;

        timer += Time.deltaTime;

        if (timer >= earthquakeInterval && currentStage < quakeIntensities.Length)
        {
            timer = 0f;

            // Her aþamanýn deðerlerini al
            float intensity = quakeIntensities[currentStage];
            float frequency = quakeFrequencies[currentStage];
            float duration = quakeDurations[currentStage];

            // ShakeEffect'i baþlat
            shakeEffect.StartShake(intensity, frequency, duration);

            // Ýlgili sesi çal
            if (currentStage < soundStages.Length)
            {
                soundStages[currentStage].Play();
            }

            Debug.Log($"Deprem Aþamasý {currentStage + 1} baþladý! Þiddet: {intensity}, Frekans: {frequency}, Süre: {duration}");

            // Bir sonraki aþamaya geç
            currentStage++;
        }
    }

    public void ResetEarthquake()
    {
        currentStage = 0;
        timer = 0f;

        // Tüm sesleri durdur
        foreach (var sound in soundStages)
        {
            sound.Stop();
        }
    }
}
