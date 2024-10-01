using Model.Entities.Player;
using Model.Entities.Spawner;
using Model.Leveling;
using UnityEngine;
using Zenject;

namespace Bootstrap.Level
{
    public class LevelBootstrap: MonoBehaviour
    {
        [Inject] private readonly Player _player;
        [Inject] private readonly EntityPoolManager _playerPoolManager;
        
        [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
        [SerializeField] private ExperienceConfig _expConfig;
        
        private void Awake()
        {
            ConfigureSpawner();
            ConfigureManagers();
        }

        private void ConfigureSpawner()
        {
            var enemySpawner = new EnemySpawner(_enemySpawnerConfig, _player, _playerPoolManager);
            enemySpawner.Start();
            
            var projectileSpawner = new ProjectileSpawner(_playerPoolManager);
        }

        private void ConfigureManagers()
        {
            var experienceManager = new ExperienceManager(); ;
        }
    }
}