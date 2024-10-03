using Model.Entities.Components;
using Model.Entities.Projectiles;
using Zenject;

namespace Model.Entities.Spawner
{
    public class ProjectileSpawner
    {
        private readonly EntityPoolManager _entityPoolManager;
        
        [Inject]
        public ProjectileSpawner(EntityPoolManager entityPoolManager)
        {
            _entityPoolManager = entityPoolManager;
        }

        public void Spawn(EntitySpawnData spawnData, ProjectileData projectileData)
        {
            var projectile = _entityPoolManager.Get<Projectile>(spawnData);
            projectile.GetComponent<MovementComponent>().SetVelocity(projectileData.Direction * projectileData.ShootingSpeed);

            var contactDamageComponent = projectile.GetComponent<ContactDamageComponent>();
            contactDamageComponent.SetContactDamage(projectileData.Damage);
            contactDamageComponent.OnHit += (entity) => OnHit(projectile, entity, projectileData);
            
            projectile.DisposeAfterTime(projectileData.LifeTime / projectileData.ShootingSpeed);
        }

        private void OnHit(Entity projectile, Entity target, ProjectileData data)
        {
            if (target.TryGetComponent(out KnockBackComponent knockBackComponent))
            {
                knockBackComponent.Apply(data.Direction, data.KnockBackStrength, data.KnockBackDuration);
            }
            
            projectile.Dispose();
        }
    }
}