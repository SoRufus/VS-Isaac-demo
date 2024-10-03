using System.Threading;
using Cysharp.Threading.Tasks;
using Model.Entities.Enemies;
using Model.Entities.Waves;
using Model.Settings;
using UnityEngine;
using Zenject;

namespace Model.Entities.Spawner
{
    public class EnemySpawner
    {
        private readonly Player.Player _player;
        private readonly EnemySpawnerConfig _config;
        private readonly EntityPoolManager _entityPoolManager;
        private readonly WavesManager _wavesManager;

        private CancellationTokenSource _cancellationTokenSource;

        [Inject]
        public EnemySpawner(Player.Player player, EntityPoolManager entityPoolManager, WavesManager wavesManager,
            GameSettings settings)
        {
            _wavesManager = wavesManager;
            _player = player;
            _entityPoolManager = entityPoolManager;
            _config = settings.EnemySpawnerConfig;
        }   

        public void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            AutoSpawn(_cancellationTokenSource.Token).Forget();
        }

        private async UniTask AutoSpawn(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.WaitForSeconds(_wavesManager.CurrentWaveConfig.Frequency, cancellationToken: cancellationToken);
                Spawn();
            }
        }
        
        private void Spawn()
        {
            for (int i = 0; i < _wavesManager.CurrentWaveConfig.Amount; i++)
            {
                var spawnData = new EntitySpawnData(_wavesManager.CurrentWaveConfig.EntityPrefab, GetRandomPosition());
                _entityPoolManager.Get<Enemy>(spawnData);
            }
        }

        private Vector2 GetRandomPosition()
        {
            var angle = Random.Range(0f, Mathf.PI * 2);
            var distance = Random.Range(_config.MinSpawnRadius, _config.MaxSpawnRadius);
            var spawnDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            var spawnPosition = (Vector2)_player.transform.position + spawnDirection * distance;

            return spawnPosition;
        }
    }
}