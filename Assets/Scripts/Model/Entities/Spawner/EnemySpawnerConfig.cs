using System;
using UnityEngine;

namespace Model.Entities.Spawner
{
    [Serializable]
    public class EnemySpawnerConfig
    {
        [field: SerializeField] public Entity EntityPrefab { get; private set; }
        [field: SerializeField] public float MinSpawnRadius { get; private set; }
        [field: SerializeField] public float MaxSpawnRadius { get; private set; }
        [field: SerializeField] public float Amount { get; private set; }
        [field: SerializeField] public float Frequency { get; private set; }
    }
}