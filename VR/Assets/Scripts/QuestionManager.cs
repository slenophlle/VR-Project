using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    [Header("Soru ve Cevaplar")]
    public Question[] questions;
    private int currentQuestionIndex = 0;

    [Header("UI ��eleri")]
    public TMP_Text questionText;
    public TMP_Text[] answerTexts;
    public Button[] answerButtons;
    public GameObject quitApp;
    public Button quitButton; // QuitApp butonu referans�

    void Start()
    {
        quitApp.SetActive(false); // QuitApp ba�lang��ta kapal�
        quitButton.onClick.AddListener(StartEarthquake); // QuitApp butonuna event ekleniyor
        LoadQuestion();
    }

    void LoadQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            Question currentQuestion = questions[currentQuestionIndex];
            questionText.text = currentQuestion.questionText;

            // Cevap metinlerini doldur
            for (int i = 0; i < answerTexts.Length; i++)
            {
                if (i < currentQuestion.answers.Length)
                {
                    answerTexts[i].gameObject.SetActive(true);
                    answerTexts[i].text = currentQuestion.answers[i];
                }
            }

            // Butonlar� g�ncelle
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
        }
        else
        {
            Debug.Log("Yanl�� Cevap.");
        }

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

        quitApp.SetActive(true); // QuitApp butonunu aktif et
    }

    // QuitApp butonuna bas�ld���nda deprem sim�lasyonunu ba�lat�r
    void StartEarthquake()
    {
        EarthquakeManager.canActive = true;
        Debug.Log("Deprem sim�lasyonu ba�lat�ld�!");
    }
}
