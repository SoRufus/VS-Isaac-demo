using Model.Entities.Player;
using Model.Entities.Spawner;
using UnityEngine;
using Utils.Pool;
using Zenject;

namespace Bootstrap.Level
{
    public class LevelBootstrap: MonoBehaviour
    {
        [Inject] private readonly Player _player;
        [Inject] private readonly GameObjectFactory _gameObjectFactory;
        
        [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
        private void Awake()
        {
            ConfigureSpawner();
        }

        private void ConfigureSpawner()
        {
            var enemySpawner = new EnemySpawner(_enemySpawnerConfig, _player, _gameObjectFactory);
            enemySpawner.Start();
            
            var projectileSpawner = new ProjectileSpawner(_gameObjectFactory);
        }
    }
}