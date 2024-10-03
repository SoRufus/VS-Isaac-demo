using System;
using System.Collections.Generic;
using Model.Entities.Waves;
using UnityEngine;

namespace Model.Entities.Spawner
{
    [Serializable]
    public class EnemySpawnerConfig
    {
        [field: SerializeField] public float MinSpawnRadius { get; private set; }
        [field: SerializeField] public float MaxSpawnRadius { get; private set; }
        [field: SerializeField] public float WavesChangingFrequency { get; private set; }
        
        [SerializeField] public List<WaveConfig> _waves = new();

        public WaveConfig GetWave(int index)
        {
            return _waves.Count > index ? _waves[index] : _waves[^index];
        }
    }
}