using UI;
using UnityEngine;
using Zenject;

namespace Other
{
    public class EntryPoint : MonoBehaviour
    {
        [Inject] private IUiSystem _uiSystem;

        private void Start()
        {
            PlayerPrefs.SetInt("bestScore", 0);
            _uiSystem.ShowMainMenu();
        }
    }
}
