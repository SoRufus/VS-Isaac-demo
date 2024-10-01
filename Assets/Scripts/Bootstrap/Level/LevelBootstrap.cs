using Model.Entities.Enemies;
using Model.Entities.Player;
using Model.Entities.Spawner;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils.Pool;
using Zenject;

namespace Bootstrap.Level
{
    public class LevelBootstrap: MonoBehaviour
    {
        [Inject] private readonly Player _player;
        [Inject] private readonly GameObjectFactory _gameObjectFactory;
        [Inject] private readonly EntityPoolManager _playerPoolManager;
        
        [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
        private void Awake()
        {
            ConfigureSpawner();
        }

        private void ConfigureSpawner()
        {
            var enemySpawner = new EnemySpawner(_enemySpawnerConfig, _player, _playerPoolManager);
            enemySpawner.Start();
            
            var projectileSpawner = new ProjectileSpawner(_playerPoolManager);
        }
    }
}