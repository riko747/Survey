using SurveyScreen;
using UnityEngine;

namespace UI
{
    internal interface IUiSystem
    {
        public void ShowMainMenu();
        public void ShowSurveyScreen();
    }
    
    public class UISystem : MonoBehaviour, IUiSystem
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private MainMenu mainMenu;
        [SerializeField] private Survey survey;

        public MainMenu MainMenu { get; set; }

        public void ShowMainMenu() => Instantiate(mainMenu, canvas.transform);

        public void ShowSurveyScreen()
        {
            Instantiate(survey, canvas.transform);
            mainMenu.gameObject.SetActive(false);
        }
    }
}
