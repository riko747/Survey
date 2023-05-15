using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Score
{
    public class ScoreScreen : MonoBehaviour
    {
        [SerializeField] private GameObject congratulationsLabel;
        [SerializeField] private GameObject failLabel;
        [SerializeField] private TextMeshProUGUI spentTimeLabel;
        [SerializeField] private TextMeshProUGUI scoreLabel;
        [SerializeField] private Button mainMenuButton;

        public static ScoreScreen Instance { get; set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }

            Destroy(gameObject);
            
            SetWinOrLoseLabel();
            SetScore();
        }

        private void Start()
        {
            mainMenuButton.onClick.AddListener(ShowMainMenu);
        }

        private void SetWinOrLoseLabel()
        {
            if (ScoreSystem.Instance.PassedTest)
                congratulationsLabel.SetActive(true);
            else
                failLabel.SetActive(true);
        }

        private void ShowMainMenu() => UISystem.Instance.ShowScreen(MainMenuScreen.Instance.gameObject);

        private void SetSpentTime()
        {
            
        }

        private void SetScore() => scoreLabel.text = ScoreSystem.Instance.CurrentScore.ToString();

        private void OnDestroy() => mainMenuButton.onClick.RemoveListener(ShowMainMenu);
    }
}
