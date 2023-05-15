using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private TextMeshProUGUI scoreValue;

        private void Awake()
        {
            startButton.onClick.AddListener(ShowSurveyScreen);
            exitButton.onClick.AddListener(ExitApp);
        }

        private void Start()
        {
            if (PlayerPrefs.HasKey("bestScore"))
                SetBestScore();
        }

        private void ShowSurveyScreen()
        {
            UISystem.Instance.ShowSurveyScreen();
            gameObject.SetActive(false);
        }

        private void SetBestScore() => scoreValue.text = PlayerPrefs.GetInt("bestScore").ToString();

        private void ExitApp() => Application.Quit();

        private void OnDestroy()
        {
            startButton.onClick.RemoveListener(ShowSurveyScreen);
            exitButton.onClick.RemoveListener(ExitApp);
        }
    }
}
