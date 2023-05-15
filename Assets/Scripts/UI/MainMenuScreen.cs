using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuScreen : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private TextMeshProUGUI scoreValue;
        
        public static MainMenuScreen Instance { get; set; }

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
            if (PlayerPrefs.HasKey("bestScore"))
                SetBestScore();
            startButton.onClick.AddListener(ShowSurveyScreen);
            exitButton.onClick.AddListener(ExitApp);
        }

        private void ShowSurveyScreen()
        {
            UISystem.Instance.InstantiateSurveyScreen();
            Instance.gameObject.SetActive(false);
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
