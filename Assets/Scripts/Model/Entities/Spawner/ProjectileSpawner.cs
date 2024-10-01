using Model.Entities.Components;
using Model.Entities.Projectiles;
using Utils.Pool;

namespace Model.Entities.Spawner
{
    public class ProjectileSpawner
    {
        private readonly EntityPool _entityPool;
        
        public ProjectileSpawner(GameObjectFactory gameObjectFactory)
        {
            _entityPool = new EntityPool(gameObjectFactory);
        }

        public void Spawn(EntitySpawnData spawnData, ProjectileData projectileData)
        {
            var projectile = _entityPool.Get(spawnData);
            projectile.GetComponent<MovementComponent>().SetVelocity(projectileData.Direction * projectileData.ShootingSpeed);

            var contactDamageComponent = projectile.GetComponent<ContactDamageComponent>();
            contactDamageComponent.SetContactDamage(projectileData.Damage);
            contactDamageComponent.OnHit += () => _entityPool.Return(projectile);
        }
    }
}