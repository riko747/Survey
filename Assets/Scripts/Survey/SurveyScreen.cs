using System.Collections.Generic;
using System.Linq;
using Score;
using ScriptableObjects.Questions;
using TMPro;
using UI;
using UnityEngine;
using Random = System.Random;
using Screen = UI.Screen;

namespace Survey
{
    public class SurveyScreen : Screen
    {
        [SerializeField] private TextMeshProUGUI currentQuestion;
        [SerializeField] private TextMeshProUGUI timeLabel;
        [SerializeField] private Transform answersParent;
        [SerializeField] private QuestionData questionData;
        
        private int _currentQuestionIndex;
        private int _incorrectAnswersNumber;
        private int _randomQuestionsSequenceCounter;
        private List<AnswerButton> _answerButtons = new();
        private List<int> _randomQuestionsNumbers = new();

        public static SurveyScreen Instance { get; set; }
        public TextMeshProUGUI TimeLabel => timeLabel;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }

            Destroy(gameObject);
        }

        private void OnEnable()
        {
            RefreshCountersData();
            ShowNewQuestion();
            StartCoroutine(TimeSystem.Instance.TimerCoroutine());
        }

        //Refreshing survey counters, score, creating random questions sequence if user chooses random questions sequence
        private void RefreshCountersData()
        {
            _currentQuestionIndex = 0;
            _randomQuestionsSequenceCounter = 0;
            ScoreSystem.Instance.CurrentScore = 0;
            _incorrectAnswersNumber = 0;
            if (questionData.random)
                CreateRandomQuestionsSequence();
        }

        //Checking that survey screen is existing on scene hierarchy
        public override Screen GetScreen() => Instance;
        //Instantiating survey screen
        public override void InstantiateScreen() => Instantiate(UISystem.Instance.SurveyScreen, UISystem.Instance.CanvasTransform);

        //Showing all question, answers data on screen
        private void ShowNewQuestion()
        {
            if (_currentQuestionIndex >= questionData.questions.Count || _randomQuestionsSequenceCounter >= questionData.questions.Count)
            {
                ScoreSystem.Instance.PassedTest = true;
                UISystem.Instance.ShowScreen(UISystem.Instance.ScoreScreen);
                return;
            }

            RefreshSurveyToNextQuestion();

            if (questionData.random)
            {
                currentQuestion.text = questionData.questions[_randomQuestionsNumbers[_randomQuestionsSequenceCounter]]
                    .question;
            }
            else
                currentQuestion.text = questionData.questions[_currentQuestionIndex].question;

            _answerButtons.Clear();
            _answerButtons = answersParent.GetComponentsInChildren<AnswerButton>()
                .Where(button => button.isActiveAndEnabled).ToList();
            
                for (var i = 0; i < _answerButtons.Count; i++)
                {
                    Answer answerData;
                    answerData.answer = null;
                    answerData.correct = false;
                    var answerButton = _answerButtons[i];
                    if (questionData.random)
                    {
                        answerData = questionData.questions[_randomQuestionsNumbers[_randomQuestionsSequenceCounter]]
                            .answers[i];
                    }
                    else
                    {
                        answerData = questionData.questions[_currentQuestionIndex].answers[i];
                    }
                    
                    answerButton.AnswerText.text = answerData.answer;
                    answerButton.IsValidAnswer = answerData.correct;

                    answerButton.Button.onClick.RemoveAllListeners();
                    answerButton.Button.onClick.AddListener(() =>
                        PrepareToNextQuestion(answerButton, answerButton.IsValidAnswer));
                }
        }

        private void CreateRandomQuestionsSequence()
        {
            _randomQuestionsNumbers.Clear();
            _randomQuestionsNumbers = Enumerable.Range(0, questionData.questions.Count).ToList();
            var random = new Random();

            for (var n = _randomQuestionsNumbers.Count; n > 1; n--)
            {
                var k = random.Next(n);
                (_randomQuestionsNumbers[k], _randomQuestionsNumbers[n - 1]) = (_randomQuestionsNumbers[n - 1], _randomQuestionsNumbers[k]);
            }
        }

        //Checking that user failed or completed test and showing next question depending on the result
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
                    UISystem.Instance.ShowScreen(UISystem.Instance.ScoreScreen);
                }

            }
            _currentQuestionIndex++;
            _randomQuestionsSequenceCounter++;
            ShowNewQuestion();
        }

        //Activating/Deactivating answer buttons depending on answers number
        private void RefreshSurveyToNextQuestion()
        {
            foreach (Transform answerButton in answersParent.transform)
                answerButton.gameObject.SetActive(false);

            for (var i = 0; i != questionData.questions[_currentQuestionIndex].answers.Length; i++)
                answersParent.GetChild(i).gameObject.SetActive(true);
        }
    }
}
