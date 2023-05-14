using System.Linq;
using Score;
using ScriptableObjects.Questions;
using TMPro;
using UnityEngine;
using Zenject;

namespace SurveyScreen
{
    public class Survey : MonoBehaviour
    {
        [Inject] private IScoreSystem _scoreSystem;
        
        [SerializeField] private TextMeshProUGUI currentQuestion;
        [SerializeField] private Transform answersParent;
        [SerializeField] private QuestionData questionData;

        private int _numberOfQuestions;
        private int _currentQuestionIndex = 0;
        private void Start()
        {
            _numberOfQuestions = questionData.questions.Count;
            ShowNewQuestion();
        }

        private void ShowNewQuestion()
        {
            RefreshSurveyToNextQuestion();

            currentQuestion.text = questionData.questions[_currentQuestionIndex].question;

            var answerButtons = answersParent.GetComponentsInChildren<AnswerButton>()
                .Where(button => button.isActiveAndEnabled).ToList();
            for (var i = 0; i != answerButtons.Count; i++)
            {
                answerButtons[i].AnswerText.text = questionData.questions[_currentQuestionIndex].answers[i].answer;
                if (questionData.questions[_currentQuestionIndex].answers[i].correct)
                    answerButtons[i].IsValidAnswer = true;
                answerButtons[i].Button.onClick.AddListener(() => PrepareToNextQuestion(answerButtons[i].IsValidAnswer));
            }
        }

        private void PrepareToNextQuestion(bool valid)
        {
            if (valid)
                _scoreSystem.UpdateScore();
            _currentQuestionIndex++;
            ShowNewQuestion();
        }

        private void RefreshSurveyToNextQuestion()
        {
            foreach (GameObject answerButton in answersParent)
                answerButton.SetActive(false);

            for (var i = 0; i != questionData.questions[_currentQuestionIndex].answers.Length; i++)
                answersParent.GetChild(i).gameObject.SetActive(true);
        }
    }
}
