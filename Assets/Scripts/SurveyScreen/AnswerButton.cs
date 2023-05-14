using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SurveyScreen
{
    public class AnswerButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI answerText;

        public Button Button => button;
        public TextMeshProUGUI AnswerText { get; set; }
        public bool IsValidAnswer { get; set; }

        private void OnDestroy() => button.onClick.RemoveAllListeners();
    }
}
