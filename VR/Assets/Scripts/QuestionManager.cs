using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    [Header("Soru ve Cevaplar")]
    public Question[] questions;
    private int currentQuestionIndex = 0;

    [Header("UI Öðeleri")]
    public TMP_Text questionText;
    public TMP_Text[] answerTexts;
    public Button[] answerButtons;
    public GameObject quitApp;
    public Button quitButton; // QuitApp butonu referansý

    void Start()
    {
        quitApp.SetActive(false); // QuitApp baþlangýçta kapalý
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

            // Butonlarý güncelle
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
            Debug.Log("Doðru Cevap!");
        }
        else
        {
            Debug.Log("Yanlýþ Cevap.");
        }

        currentQuestionIndex++;
        LoadQuestion();
    }

    void EndQuiz()
    {
        questionText.text = "Tebrikler! Tüm sorularý tamamladýnýz.";
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

    // QuitApp butonuna basýldýðýnda deprem simülasyonunu baþlatýr
    void StartEarthquake()
    {
        EarthquakeManager.canActive = true;
        Debug.Log("Deprem simülasyonu baþlatýldý!");
    }
}
