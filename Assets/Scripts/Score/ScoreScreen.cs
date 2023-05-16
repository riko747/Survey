using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Screen = UI.Screen;

namespace Score
{
    public class ScoreScreen : Screen
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
        }

        private void Start() => mainMenuButton.onClick.AddListener(ShowMainMenu);

        private void OnEnable()
        {
            SetWinOrLoseLabel();
            SetScore();
            SetSpentTime();
        }

        public override Screen GetScreen() => Instance;
        public override void InstantiateScreen() => Instantiate(UISystem.Instance.ScoreScreen, UISystem.Instance.CanvasTransform);
        
        private void ShowMainMenu() => UISystem.Instance.ShowScreen(MainMenuScreen.Instance);
        private void SetSpentTime() => spentTimeLabel.text = TimeSystem.Instance.CurrentTime;
        private void SetScore() => scoreLabel.text = ScoreSystem.Instance.CurrentScore.ToString();
        private void OnDestroy() => mainMenuButton.onClick.RemoveListener(ShowMainMenu);
        
        private void SetWinOrLoseLabel()
        {
            congratulationsLabel.SetActive(ScoreSystem.Instance.PassedTest);
            failLabel.SetActive(!ScoreSystem.Instance.PassedTest);
        }
    }
}
