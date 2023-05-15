using System;
using UI;
using UnityEngine;

namespace Other
{
    public class EntryPoint : MonoBehaviour
    {
        private void Start()
        {
            UISystem.Instance.ShowMainMenu();
        }
    }
}
