using Model.Entities.Player;
using Model.Entities.Spawner;
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

            Container.Bind<EntityPoolManager>().AsSingle().NonLazy();
            Container.Bind<ProjectileSpawner>().AsSingle();

            Container.BindInterfacesAndSelfTo<Controls>().AsSingle().NonLazy();
        }
    }
}
