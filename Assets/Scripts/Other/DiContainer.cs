using Score;
using UI;
using UnityEngine;
using Zenject;

namespace Other
{
    public class DiContainer : MonoInstaller
    {
        [SerializeField] private UISystem uiSystem;
        [SerializeField] private ScoreSystem scoreSystem;
        public override void InstallBindings()
        {
            Container.Bind<IUiSystem>().To<UISystem>().FromComponentInHierarchy(uiSystem).AsSingle();
            Container.Bind<IScoreSystem>().To<ScoreSystem>().FromComponentInHierarchy(scoreSystem).AsSingle();
        }
    }
}
