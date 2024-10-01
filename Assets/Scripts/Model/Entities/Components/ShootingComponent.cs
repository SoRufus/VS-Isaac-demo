using System.Threading;
using Cysharp.Threading.Tasks;
using Model.Entities.Projectiles;
using Model.Entities.Spawner;
using Model.Entities.Statistics;
using UnityEngine;
using Utils.Pool;
using Zenject;

namespace Model.Entities.Components
{
    public class ShootingComponent: EntityComponent
    {
        [Inject] private readonly ProjectileSpawner _projectileSpawner;

        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private Statistic _damageStatistic;
        [SerializeField] private Statistic _attackSpeedStatistic;
        [SerializeField] private Statistic _rangeStatistic;
        [SerializeField] private Statistic _shootingSpeedStatistic;
        [SerializeField] private Statistic _knockBackStrength;
        [SerializeField] private Statistic _knockBackDuration;

        private Vector2 _direction = Vector2.zero;
        private CancellationTokenSource _cancellationTokenSource;
        
        private StatisticData _attackSpeedData;
        private StatisticData _rangeData;
        private StatisticData _damageData;
        private StatisticData _shootingSpeedData;
        private StatisticData _knockBackStrengthData;
        private StatisticData _knockBackDurationData;

        private bool _canShoot = true;
        
        private void OnEnable()
        {
            InitStats();
        }
        
        private void OnDisable()
        {
            _cancellationTokenSource?.Cancel();
        }

        private void InitStats()
        {
            _attackSpeedData = Entity.GetStatisticData(_attackSpeedStatistic);
            _rangeData = Entity.GetStatisticData(_rangeStatistic);
            _shootingSpeedData = Entity.GetStatisticData(_shootingSpeedStatistic);
            _damageData = Entity.GetStatisticData(_damageStatistic);
            _knockBackStrengthData = Entity.GetStatisticData(_knockBackStrength);
            _knockBackDurationData = Entity.GetStatisticData(_knockBackDuration);
        }

        public void SetShootingDirection(Vector2 direction)
        {
            _direction = direction;
            
            if (!CanShoot()) return;
            
            _cancellationTokenSource = new CancellationTokenSource();
            AutoShooting(_cancellationTokenSource.Token).Forget();
        }

        private async UniTask AutoShooting(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (!CanShoot()) break;
                
                _canShoot = false;
                SpawnProjectile();
                
                await UniTask.WaitForSeconds(_attackSpeedData.Value, cancellationToken: cancellationToken);
                _canShoot = true;
            }
        }
        
        private void SpawnProjectile()
        {
            var spawnData = new EntitySpawnData(_weaponConfig.ProjectilePrefab, transform.position);
            var projectileData = new ProjectileData(_shootingSpeedData.Value, _damageData.Value, 
                _rangeData.Value, _direction, _knockBackStrengthData.Value, _knockBackDurationData.Value);
            
            _projectileSpawner.Spawn(spawnData, projectileData);
        }
        
        private bool CanShoot()
        {
            return _direction != Vector2.zero && _canShoot;
        }
    }
}