using System;
using Model.Entities.Spawner;
using Model.Entities.Waves;
using UnityEngine;
using Zenject;

namespace Bootstrap.Level
{
    public class LevelBootstrap: MonoBehaviour
    {
        [Inject] private readonly EnemySpawner _enemySpawner;
        [Inject] private readonly WavesManager _wavesManager;
        
        private void Awake()
        {
            ConfigureSpawner();
        }

        private void ConfigureSpawner()
        {
            _wavesManager.Start();
            _enemySpawner.Start();
        }
    }
}