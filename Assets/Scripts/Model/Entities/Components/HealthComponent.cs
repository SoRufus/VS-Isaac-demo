using System;
using Model.Entities.Statistics;
using R3;
using UnityEngine;

namespace Model.Entities.Components
{
    public class HealthComponent: EntityComponent
    {
        [SerializeField] private Statistic _healthStatistic;
        public event Action OnDeath;

        private StatisticData _healthData;
        private IDisposable _disposable;

        private void OnEnable()
        {
            _healthData = Entity.GetStatisticData(_healthStatistic);
            _disposable = _healthData.ReactiveValue.Subscribe(OnHealthModified);
        }

        private void OnDisable()
        {
            _disposable?.Dispose();;
        }

        public void ModifyHealth(float value)
        {
            _healthData.ModifyValue(value);
        }

        private void OnHealthModified(float value)
        {
            if (value <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}