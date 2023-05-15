using System;
using SurveyScreen;
using UnityEngine;

namespace UI
{
    public class UISystem : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private MainMenu mainMenu;
        [SerializeField] private Survey survey;

        public static UISystem Instance { get; set; }
        
        public MainMenu MainMenu { get; set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }
            Destroy(gameObject);
            gameObject.SetActive(true);
        }

        public void ShowMainMenu() => Instantiate(mainMenu, canvas.transform);

        public void ShowSurveyScreen()
        {
            Instantiate(survey, canvas.transform);
            //mainMenu.gameObject.SetActive(false);
        }
    }
}
