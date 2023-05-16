using UI;
using UnityEngine;

namespace Other
{
    public class EntryPoint : MonoBehaviour
    {
        private void Start() => UISystem.Instance.ShowScreen(UISystem.Instance.MainMenuScreen);
    }
}
