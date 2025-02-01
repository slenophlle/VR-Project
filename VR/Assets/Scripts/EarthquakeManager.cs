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
    public Vector2 earthquakeIntervalRange = new Vector2(15f, 25f); // Bekleme süresi aralýðý

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
        // Ýlk deprem aralýðýný rastgele belirle
        earthquakeInterval = Random.Range(earthquakeIntervalRange.x, earthquakeIntervalRange.y);
    }

    void Update()
    {
        if (!canActive) return;

        timer += Time.deltaTime;

        if (timer >= earthquakeInterval && currentStage < quakeIntensities.Length)
        {
            timer = 0f;

            // Yeni deprem aralýðýný rastgele belirle
            earthquakeInterval = Random.Range(earthquakeIntervalRange.x, earthquakeIntervalRange.y);

            float intensity = quakeIntensities[currentStage];
            float frequency = quakeFrequencies[currentStage];
            float duration = quakeDurations[currentStage];

            shakeEffect.StartShake(intensity, frequency, duration);

            if (currentStage < soundStages.Length)
            {
                soundStages[currentStage].Play();
            }

            Debug.Log($"Deprem Aþamasý {currentStage + 1} baþladý! Þiddet: {intensity}, Frekans: {frequency}, Süre: {duration}, Bekleme Süresi: {earthquakeInterval} saniye");
            currentStage++;

            // Deprem tamamlandýktan sonra bir sonraki aþamaya geçmek için bekleme baþlat
            StartCoroutine(StartNextStageAfterDelay(duration));
        }

        if (currentStage >= quakeIntensities.Length && !endPanel.activeSelf)
        {
            EndEarthquake();
        }
    }

    void EndEarthquake()
    {
        Debug.Log("Deprem sona erdi!");  // Bu satýrla doðru yerden çaðrýldýðýný kontrol et
        canActive = false;
        endPanel.SetActive(true);  // Paneli aktif et

        // Debug ile endPanel'in aktif olup olmadýðýný kontrol et
        if (endPanel.activeSelf)
        {
            Debug.Log("End Panel aktif!");
        }
        else
        {
            Debug.Log("End Panel aktif deðil!");
        }
    }

    public void RestartEarthquake()
    {
        restartBTN.SetActive(false);
        countdownText.gameObject.SetActive(true);
        RestartText.gameObject.SetActive(false);
        StartCoroutine(RestartAfterCountdown(5f)); // 5 saniyelik geri sayým baþlat
    }

    private IEnumerator RestartAfterCountdown(float countdown)
    {
        Debug.Log("Geri sayým baþladý...");
        endPanel.SetActive(true);

        while (countdown > 0)
        {
            countdownText.text = $"{countdown} saniye sonra deprem baþlayacak!";
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countdownText.text = "Deprem baþlýyor!";
        yield return new WaitForSeconds(1f);

        Debug.Log("Deprem tekrar baþladý!");
        currentStage = 0;
        timer = 0f;

        // Yeni deprem aralýðýný rastgele belirle
        earthquakeInterval = Random.Range(earthquakeIntervalRange.x, earthquakeIntervalRange.y);

        canActive = true;
        endPanel.SetActive(false); // Paneli tekrar kapat
    }

    private IEnumerator StartNextStageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Deprem süresi kadar bekle
        timer = 0f; // Bekleme süresi bu noktadan itibaren baþlasýn
    }
}
