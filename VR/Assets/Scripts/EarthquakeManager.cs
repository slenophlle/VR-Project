using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class EarthquakeManager : MonoBehaviour
{
    public ShakeBuilding shakeEffect;

    [Header("Quake Values")]
    public float[] quakeIntensities = { 0.1f, 0.2f, 0.3f };
    public float[] quakeFrequencies = { 3f, 3f, 3f };
    public float[] quakeDurations = { 12f, 13f, 6f };
    public Vector2 earthquakeIntervalRange = new Vector2(15f, 25f);

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

    [Header("Objects to Change")]
    public GameObject[] solidObjects;  // Sa�lam objeler (normal bina)
    public GameObject[] brokenObjects; // K�r�k objeler (hasarl� bina)

    private Vector3[] solidObjectPositions;  // Sa�lam objelerin ba�lang�� konumlar�
    private GameObject[] instantiatedBrokenObjects;  // K�r�k objeler i�in instantiate edilmi� nesneler

    [System.Serializable]
    public class ObjectPair
    {
        public GameObject solidObject;
        public GameObject brokenObject;
    }

    public ObjectPair[] objectPairs;  // Sa�lam ve k�r�k objelerin e�le�tirilmi� listesi

    void Start()
    {
        earthquakeInterval = Random.Range(earthquakeIntervalRange.x, earthquakeIntervalRange.y);

        // Sa�lam objelerin ba�lang�� konumlar�n� kaydet
        solidObjectPositions = new Vector3[solidObjects.Length];
        for (int i = 0; i < solidObjects.Length; i++)
        {
            if (solidObjects[i] != null)
                solidObjectPositions[i] = solidObjects[i].transform.position;
        }

        // K�r�k objeleri tutacak diziyi ba�lat
        instantiatedBrokenObjects = new GameObject[objectPairs.Length];
    }


    void Update()
    {
        if (!canActive) return;

        timer += Time.deltaTime;

        if (timer >= earthquakeInterval && currentStage < quakeIntensities.Length)
        {
            timer = 0f;
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

            // Depremin ikinci a�amas�nda binalar� k�r
            if (currentStage == 1)
            {
                ChangeBuildings();
            }

            currentStage++;
            StartCoroutine(StartNextStageAfterDelay(duration));
        }

        if (currentStage >= quakeIntensities.Length && !endPanel.activeSelf)
        {
            EndEarthquake();
        }
    }

    void ChangeBuildings()
    {
        Debug.Log("Binalar k�r�ld�!");
        for (int i = 0; i < objectPairs.Length; i++)
        {
            if (objectPairs[i].solidObject != null)
            {
                objectPairs[i].solidObject.SetActive(false);  // Sa�lam objeyi devre d��� b�rak
            }

            if (objectPairs[i].brokenObject != null)
            {
                // K�r�k objeyi instantiate et ve sahneye yerle�tir
                instantiatedBrokenObjects[i] = Instantiate(objectPairs[i].brokenObject, solidObjectPositions[i], Quaternion.identity);

                // Objeyi aktif yap
                instantiatedBrokenObjects[i].SetActive(true);  // Bu sat�r, objeyi sahnede aktif yapacak
            }
        }
    }


    void EndEarthquake()
    {
        Debug.Log("Deprem sona erdi!");
        canActive = false;
        endPanel.SetActive(true);
    }

    public void RestartEarthquake()
    {
        restartBTN.SetActive(false);
        countdownText.gameObject.SetActive(true);
        RestartText.gameObject.SetActive(false);
        StartCoroutine(RestartAfterCountdown(5f));
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

        Debug.Log("Sahne yeniden ba�lat�l�yor...");
        ResetBuildings();
    }

    void ResetBuildings()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator StartNextStageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        timer = 0f;
    }
}
