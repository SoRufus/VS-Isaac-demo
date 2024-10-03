using Model.Entities.Player;
using Model.Entities.Spawner;
using Model.Leveling;
using UnityEngine;
using Zenject;

namespace Bootstrap.Level
{
    public class LevelBootstrap: MonoBehaviour
    {
        [Inject] private readonly EnemySpawner _enemySpawner;
        
        private void Awake()
        {
            ConfigureSpawner();
        }

        private void ConfigureSpawner()
        {
            _enemySpawner.Start();
        }
    }
}