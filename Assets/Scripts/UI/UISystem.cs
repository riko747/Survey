using Score;
using Survey;
using UnityEngine;

namespace UI
{
    public abstract class Screen : MonoBehaviour
    {
        public abstract void InstantiateScreen();
        public abstract Screen GetScreen();
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

        //Logic of display and instantiation of game screens.
        public void ShowScreen(Screen screen)
        {
            DeactivateAllScreens();
            if (!screen.GetScreen())
                screen.InstantiateScreen();
            else
                screen.GetScreen().gameObject.SetActive(true);
        }
        
        //Logic of deactivating all game screens, before showing only one
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
