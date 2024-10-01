using System.Threading;
using Cysharp.Threading.Tasks;
using Model.Entities.Enemies;
using UnityEngine;

namespace Model.Entities.Spawner
{
    public class EnemySpawner
    {
        private readonly Player.Player _player;
        private readonly EnemySpawnerConfig _config;

        private CancellationTokenSource _cancellationTokenSource;
        private readonly EntityPoolManager _entityPoolManager;

        public EnemySpawner(EnemySpawnerConfig config, Player.Player player, EntityPoolManager entityPoolManager)
        {
            _config = config;
            _player = player;
            _entityPoolManager = entityPoolManager;
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
                await UniTask.WaitForSeconds(_config.Frequency, cancellationToken: cancellationToken);
                Spawn();
            }
        }
        
        private void Spawn()
        {
            for (int i = 0; i < _config.Amount; i++)
            {
                var spawnData = new EntitySpawnData(_config.EntityPrefab, GetRandomPosition());
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

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}