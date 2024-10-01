using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Model.Entities.Components;
using Model.Entities.Spawner;
using Model.Entities.Statistics;
using UnityEngine;

namespace Model.Entities
{
    public class Entity: MonoBehaviour, IEntity
    {
        [field: SerializeField] public List<StatisticData> EntityStatistics { get; private set; }

        private void OnEnable()
        {
            foreach (var statistic in EntityStatistics)
            {
                statistic.SetToBaseValue();
            }
        }

        public virtual void OnSpawned(EntitySpawnData spawnData)
        {
            transform.position = spawnData.Position;
            transform.SetParent(spawnData.Parent);
        }

        public virtual void OnDespawned()
        {
            
        }
        
        public bool TryGetStatisticData(Statistic statistic, out StatisticData data)
        {
            data = EntityStatistics.FirstOrDefault(x => x.Statistic == statistic);

            return data != null;
        }

        public StatisticData GetStatisticData(Statistic statistic)
        {
            return EntityStatistics.FirstOrDefault(x => x.Statistic == statistic);
        }
        
    }
}