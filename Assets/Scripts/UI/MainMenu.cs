using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [Inject] private IUiSystem _uiSystem;
        
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private TextMeshProUGUI scoreValue;

        private void Awake()
        {
            startButton.onClick.AddListener(ShowSurveyScreen);
            exitButton.onClick.AddListener(ExitApp);
        }

        private void ShowSurveyScreen()
        {
            _uiSystem.ShowSurveyScreen();
        }

        public void SetBestScore()
        {
            scoreValue.text = PlayerPrefs.GetInt("bestScore").ToString();
        }

        private void ExitApp() => Application.Quit();

        private void OnDestroy()
        {
            startButton.onClick.RemoveListener(ShowSurveyScreen);
            exitButton.onClick.RemoveListener(ExitApp);
        }
    }
}
