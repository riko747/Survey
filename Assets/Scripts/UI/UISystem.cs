using Score;
using Survey;
using UnityEngine;

namespace UI
{
    public class UISystem : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private MainMenuScreen mainMenuScreen;
        [SerializeField] private SurveyScreen surveyScreen;
        [SerializeField] private ScoreScreen scoreScreen;

        public static UISystem Instance { get; set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }

            Destroy(gameObject);
        }

        public void InstantiateMainMenu() => Instantiate(mainMenuScreen, canvas.transform);
        public void InstantiateSurveyScreen() => Instantiate(surveyScreen, canvas.transform);
        private void InstantiateScoreScreen() => Instantiate(scoreScreen, canvas.transform);

        public void ShowScoreScreen()
        {
            if (FindObjectOfType<ScoreScreen>() == null)
                InstantiateScoreScreen();
            else
                ShowScreen(ScoreScreen.Instance.gameObject);
        }

        public void ShowScreen(GameObject screen)
        {
            DeactivateAllScreens();
            screen.SetActive(true);
        }

        private void DeactivateAllScreens()
        {
            if (mainMenuScreen)
                MainMenuScreen.Instance.gameObject.SetActive(false);
            if (surveyScreen)
                SurveyScreen.Instance.gameObject.SetActive(false);
            if (scoreScreen)
                ScoreScreen.Instance.gameObject.SetActive(false);
        }
    }
}
