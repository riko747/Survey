using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuScreen : Screen
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
            startButton.onClick.AddListener(() => UISystem.Instance.ShowScreen(UISystem.Instance.SurveyScreen));
            exitButton.onClick.AddListener(ExitApp);
        }
        
        private void OnEnable()
        {
            if (PlayerPrefs.HasKey("bestScore"))
                SetBestScore();
        }
        
        //Setting the highest score in the Main Menu screen when the player enters the game
        private void SetBestScore() => scoreValue.text = PlayerPrefs.GetInt("bestScore").ToString();

        private void ExitApp() => Application.Quit();

        private void OnDestroy()
        {
            startButton.onClick.RemoveListener(() => UISystem.Instance.ShowScreen(UISystem.Instance.SurveyScreen));
            exitButton.onClick.RemoveListener(ExitApp);
        }

        //Instantiating main menu screen
        public override void InstantiateScreen() => Instantiate(UISystem.Instance.MainMenuScreen, UISystem.Instance.CanvasTransform);
        
        //Checking that main menu screen is existing on scene hierarchy
        public override Screen GetScreen() => Instance;
    }
}
