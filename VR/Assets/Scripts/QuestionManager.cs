using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    [Header("Soru ve Cevaplar")]
    public Question[] questions;
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0; // Do�ru cevaplanan soru say�s�

    [Header("UI ��eleri")]
    public TMP_Text questionText;
    public TMP_Text[] answerTexts;
    public Button[] answerButtons;
    public GameObject quitApp;
    public Button quitButton;
    public TMP_Text scoreText; // Do�ru cevap say�s�n� g�sterecek UI ��esi

    void Start()
    {
        quitApp.SetActive(false);
        UpdateScoreUI(); // Skor UI'yi ba�lat
        LoadQuestion();
    }

    void LoadQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            Question currentQuestion = questions[currentQuestionIndex];
            questionText.text = currentQuestion.questionText;

            for (int i = 0; i < answerTexts.Length; i++)
            {
                if (i < currentQuestion.answers.Length)
                {
                    answerTexts[i].gameObject.SetActive(true);
                    answerTexts[i].text = currentQuestion.answers[i];
                }
            }

            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (i < currentQuestion.answers.Length)
                {
                    answerButtons[i].gameObject.SetActive(true);
                    answerButtons[i].onClick.RemoveAllListeners();
                    int answerIndex = i;
                    answerButtons[i].onClick.AddListener(() => CheckAnswer(answerIndex));
                }
            }
        }
        else
        {
            EndQuiz();
        }
    }

    public void CheckAnswer(int selectedAnswerIndex)
    {
        if (selectedAnswerIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            Debug.Log("Do�ru Cevap!");
            correctAnswers++; // Do�ru cevap say�s�n� art�r
        }
        else
        {
            Debug.Log("Yanl�� Cevap.");
        }

        UpdateScoreUI(); // Skor ekran�n� g�ncelle
        currentQuestionIndex++;
        LoadQuestion();
    }

    void EndQuiz()
    {
        questionText.text = "Tebrikler! T�m sorular� tamamlad�n�z.";
        foreach (var button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
        foreach (var tmptext in answerTexts)
        {
            tmptext.gameObject.SetActive(false);
        }

        quitApp.SetActive(true);

        // Butona sadece quiz tamamland�ktan sonra Listener ekleniyor
        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(StartEarthquake);
    }

    void StartEarthquake()
    {
        EarthquakeManager.canActive = true;
        Debug.Log("Deprem sim�lasyonu ba�lat�ld�!");
    }

    void UpdateScoreUI()
    {
        scoreText.text = "D: " + correctAnswers;
    }
}
