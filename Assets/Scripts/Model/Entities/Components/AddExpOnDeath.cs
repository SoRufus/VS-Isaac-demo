using System;
using Model.Entities.Statistics;
using Model.Leveling;
using UnityEngine;
using Zenject;

namespace Model.Entities.Components
{
    public class AddExpOnDeath: EntityComponent
    {
        [Inject] private readonly ExperienceManager _experienceManager;
        
        [SerializeField] private Statistic _expAmountStatistic;

        private StatisticData _expAmountData;
        private HealthComponent _healthComponent;

        private IDisposable _disposable;

        private void OnEnable()
        {
            _expAmountData = Entity.GetStatisticData(_expAmountStatistic);
            _healthComponent = Entity.GetComponent<HealthComponent>();

            _healthComponent.OnDeath += OnEntityDeath;
        }

        private void OnDisable()
        {
            _healthComponent.OnDeath -= OnEntityDeath;
        }

        private void OnEntityDeath()
        {
            _experienceManager.ModifyExperience((int)_expAmountData.Value);
        }
    }
}