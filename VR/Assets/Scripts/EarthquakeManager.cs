using UnityEngine;
using System.Collections;
using TMPro;

public class EarthquakeManager : MonoBehaviour
{
    public ShakeBuilding shakeEffect;

    [Header("Quake Values")]
    public float[] quakeIntensities = { 0.1f, 0.2f, 0.3f };
    public float[] quakeFrequencies = { 3f, 3f, 3f };
    public float[] quakeDurations = { 12f, 13f, 6f };
    public Vector2 earthquakeIntervalRange = new Vector2(15f, 25f); // Bekleme s�resi aral���

    private float earthquakeInterval;
    private int currentStage = 0;
    private float timer = 0f;
    public static bool canActive = false;

    [Header("Audio Sources")]
    public AudioSource[] soundStages;

    [Header("UI Elements")]
    public GameObject endPanel;
    public GameObject restartBTN;
    public TMP_Text countdownText;
    public TMP_Text RestartText;
    void Start()
    {
        // �lk deprem aral���n� rastgele belirle
        earthquakeInterval = Random.Range(earthquakeIntervalRange.x, earthquakeIntervalRange.y);
    }

    void Update()
    {
        if (!canActive) return;

        timer += Time.deltaTime;

        if (timer >= earthquakeInterval && currentStage < quakeIntensities.Length)
        {
            timer = 0f;

            // Yeni deprem aral���n� rastgele belirle
            earthquakeInterval = Random.Range(earthquakeIntervalRange.x, earthquakeIntervalRange.y);

            float intensity = quakeIntensities[currentStage];
            float frequency = quakeFrequencies[currentStage];
            float duration = quakeDurations[currentStage];

            shakeEffect.StartShake(intensity, frequency, duration);

            if (currentStage < soundStages.Length)
            {
                soundStages[currentStage].Play();
            }

            Debug.Log($"Deprem A�amas� {currentStage + 1} ba�lad�! �iddet: {intensity}, Frekans: {frequency}, S�re: {duration}, Bekleme S�resi: {earthquakeInterval} saniye");
            currentStage++;

            // Deprem tamamland�ktan sonra bir sonraki a�amaya ge�mek i�in bekleme ba�lat
            StartCoroutine(StartNextStageAfterDelay(duration));
        }

        if (currentStage >= quakeIntensities.Length && !endPanel.activeSelf)
        {
            EndEarthquake();
        }
    }

    void EndEarthquake()
    {
        Debug.Log("Deprem sona erdi!");  // Bu sat�rla do�ru yerden �a�r�ld���n� kontrol et
        canActive = false;
        endPanel.SetActive(true);  // Paneli aktif et

        // Debug ile endPanel'in aktif olup olmad���n� kontrol et
        if (endPanel.activeSelf)
        {
            Debug.Log("End Panel aktif!");
        }
        else
        {
            Debug.Log("End Panel aktif de�il!");
        }
    }

    public void RestartEarthquake()
    {
        restartBTN.SetActive(false);
        countdownText.gameObject.SetActive(true);
        RestartText.gameObject.SetActive(false);
        StartCoroutine(RestartAfterCountdown(5f)); // 5 saniyelik geri say�m ba�lat
    }

    private IEnumerator RestartAfterCountdown(float countdown)
    {
        Debug.Log("Geri say�m ba�lad�...");
        endPanel.SetActive(true);

        while (countdown > 0)
        {
            countdownText.text = $"{countdown} saniye sonra deprem ba�layacak!";
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countdownText.text = "Deprem ba�l�yor!";
        yield return new WaitForSeconds(1f);

        Debug.Log("Deprem tekrar ba�lad�!");
        currentStage = 0;
        timer = 0f;

        // Yeni deprem aral���n� rastgele belirle
        earthquakeInterval = Random.Range(earthquakeIntervalRange.x, earthquakeIntervalRange.y);

        canActive = true;
        endPanel.SetActive(false); // Paneli tekrar kapat
    }

    private IEnumerator StartNextStageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Deprem s�resi kadar bekle
        timer = 0f; // Bekleme s�resi bu noktadan itibaren ba�las�n
    }
}
