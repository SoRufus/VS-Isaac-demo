using Model.Entities.Player;
using Model.Entities.Spawner;
using Model.Entities.Waves;
using Model.GameState;
using Model.Leveling;
using Model.UI;
using Utils.Pool;
using Zenject;

namespace Bootstrap.Level.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Player>().FromInstance((Player)FindObjectOfType(typeof(Player))).AsSingle().NonLazy();

            Container.Bind<GameObjectFactory>().AsSingle();
            Container.Bind<EntityPoolManager>().AsSingle();
            Container.Bind<ExperienceManager>().AsSingle();
            Container.Bind<ProjectileSpawner>().AsSingle();
            Container.Bind<EnemySpawner>().AsSingle();
            Container.Bind<UIManager>().AsSingle();
            Container.Bind<GameStateManager>().AsSingle();
            Container.Bind<WavesManager>().AsSingle();

            Container.BindInterfacesAndSelfTo<Controls>().AsSingle().NonLazy();
        }
    }
}
