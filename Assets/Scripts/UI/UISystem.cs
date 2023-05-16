using Score;
using Survey;
using UnityEngine;

namespace UI
{
    public abstract class Screen : MonoBehaviour
    {
        public abstract void InstantiateScreen();
        public abstract Screen GetGameObject();
    }
    
    public class UISystem : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private MainMenuScreen mainMenuScreen;
        [SerializeField] private SurveyScreen surveyScreen;
        [SerializeField] private ScoreScreen scoreScreen;

        public static UISystem Instance { get; set; }
        public Transform CanvasTransform => canvas.transform;
        public MainMenuScreen MainMenuScreen => mainMenuScreen;
        public SurveyScreen SurveyScreen => surveyScreen;
        public ScoreScreen ScoreScreen => scoreScreen;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }

            Destroy(gameObject);
        }

        public void ShowScreen(Screen screen)
        {
            DeactivateAllScreens();
            if (!screen.GetGameObject())
                screen.InstantiateScreen();
            else
                screen.GetGameObject().gameObject.SetActive(true);
        }
        
        private void DeactivateAllScreens()
        {
            if (SurveyScreen.Instance)
                SurveyScreen.Instance.gameObject.SetActive(false);
            if (MainMenuScreen.Instance)
                MainMenuScreen.Instance.gameObject.SetActive(false);
            if (ScoreScreen.Instance)
                ScoreScreen.Instance.gameObject.SetActive(false);

        }
    }
}
