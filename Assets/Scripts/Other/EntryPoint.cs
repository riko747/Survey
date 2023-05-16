using UI;
using UnityEngine;

namespace Other
{
    public class EntryPoint : MonoBehaviour
    {
        //Entry point to start the game and show main menu
        private void Start() => UISystem.Instance.ShowScreen(UISystem.Instance.MainMenuScreen);
    }
}
