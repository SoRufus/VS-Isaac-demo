using Model.Entities.Player;
using Model.Entities.Spawner;
using Model.Leveling;
using Utils.Pool;
using Zenject;

namespace Bootstrap.Level.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Player>().FromInstance((Player)FindObjectOfType(typeof(Player))).AsSingle().NonLazy();

            Container.Bind<GameObjectFactory>().AsSingle().NonLazy();
            Container.Bind<EntityPoolManager>().AsSingle();

            Container.Bind<ExperienceManager>().AsSingle();
            Container.Bind<ProjectileSpawner>().AsSingle();
            Container.Bind<EnemySpawner>().AsSingle();

            Container.BindInterfacesAndSelfTo<Controls>().AsSingle().NonLazy();
        }
    }
}
