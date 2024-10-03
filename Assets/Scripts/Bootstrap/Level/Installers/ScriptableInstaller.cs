using Model.Entities.Settings;
using UnityEngine;
using Zenject;

namespace Bootstrap.Level.Installers
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(ScriptableInstaller))]
    public class ScriptableInstaller : ScriptableObjectInstaller<ScriptableInstaller>
    {
        [SerializeField] private GameSettings _gameSettings;
        
        public override void InstallBindings()
        {
            Container.Bind<GameSettings>().FromInstance(_gameSettings).AsSingle();
        }
    }
}