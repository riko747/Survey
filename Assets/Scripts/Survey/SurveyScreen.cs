using System;
using System.Collections.Generic;
using System.Linq;
using Score;
using ScriptableObjects.Questions;
using TMPro;
using UI;
using UnityEngine;

namespace Survey
{
    public class SurveyScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentQuestion;
        [SerializeField] private Transform answersParent;
        [SerializeField] private QuestionData questionData;
        
        private int _currentQuestionIndex;
        private int _incorrectAnswersNumber;
        private List<AnswerButton> _answerButtons = new();
        
        public static SurveyScreen Instance { get; set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }

            Destroy(gameObject);
        }

        private void Start()
        {
            ShowNewQuestion();
        }

        private void ShowNewQuestion()
        {
            if (_currentQuestionIndex >= questionData.questions.Count)
            {
                ScoreSystem.Instance.PassedTest = true;
                UISystem.Instance.ShowScoreScreen();
                return;
            }

            RefreshSurveyToNextQuestion();

            currentQuestion.text = questionData.questions[_currentQuestionIndex].question;

            _answerButtons.Clear();
            _answerButtons = answersParent.GetComponentsInChildren<AnswerButton>()
                .Where(button => button.isActiveAndEnabled).ToList();

            for (var i = 0; i < _answerButtons.Count; i++)
            {
                var answerButton = _answerButtons[i];
                var answerData = questionData.questions[_currentQuestionIndex].answers[i];

                answerButton.AnswerText.text = answerData.answer;
                answerButton.IsValidAnswer = answerData.correct;

                answerButton.Button.onClick.RemoveAllListeners();
                answerButton.Button.onClick.AddListener(() => PrepareToNextQuestion(answerButton, answerButton.IsValidAnswer));
            }
        }

        private void PrepareToNextQuestion(AnswerButton currentButton, bool valid)
        {
            currentButton.IsValidAnswer = false;

            if (valid)
                ScoreSystem.Instance.UpdateScore();
            else
            {
                _incorrectAnswersNumber++;
                if (_incorrectAnswersNumber == 3)
                {
                    ScoreSystem.Instance.PassedTest = false;
                    UISystem.Instance.ShowScoreScreen();
                }

            }

            Debug.Log("Score: " + ScoreSystem.Instance.CurrentScore);
            _currentQuestionIndex++;
            ShowNewQuestion();
        }

        private void RefreshSurveyToNextQuestion()
        {
            foreach (Transform answerButton in answersParent.transform)
                answerButton.gameObject.SetActive(false);

            for (var i = 0; i != questionData.questions[_currentQuestionIndex].answers.Length; i++)
                answersParent.GetChild(i).gameObject.SetActive(true);
        }
    }
}
