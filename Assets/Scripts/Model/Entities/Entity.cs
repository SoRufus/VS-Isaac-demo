using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Entities;
using Cysharp.Threading.Tasks;
using Model.Entities.Enemies;
using Model.Entities.Spawner;
using Model.Entities.Statistics;
using UnityEngine;
using Zenject;

namespace Model.Entities
{
    public class Entity: MonoBehaviour, IEntity
    {
        [Inject] private readonly EntityPoolManager _entityPoolManager;
        
        [field: SerializeField] public List<StatisticData> EntityStatistics { get; private set; }
        
        [field: SerializeField] public string Name { get; private set; }
        
        public event Action OnSpawned;
        public event Action OnDisposed;
        
        private CancellationTokenSource _cancellationTokenSource;

        private void OnEnable()
        {
            foreach (var statistic in EntityStatistics)
            {
                statistic.SetToBaseValue();
            }
        }

        public virtual void Spawn(EntitySpawnData spawnData)
        {
            OnSpawned?.Invoke();
            transform.position = spawnData.Position;
            transform.SetParent(spawnData.Parent);
        }
        
        public virtual void Dispose()
        {
            OnDisposed?.Invoke();
            _cancellationTokenSource?.Cancel();
            
            _entityPoolManager.Return(this);
        }

        public void DisposeAfterTime(float time)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            DisposeAfterTime(time, _cancellationTokenSource.Token).Forget();
        }
        
        private async UniTask DisposeAfterTime(float time, CancellationToken cancellationToken)
        {
            await UniTask.WaitForSeconds(time, cancellationToken: cancellationToken);
            Dispose();
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