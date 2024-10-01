using System;
using Model.Entities.Statistics;
using R3;
using UnityEngine;

namespace Model.Entities.Components
{
    public class HealthComponent: EntityComponent
    {
        [SerializeField] private Statistic _healthStatistic;
        [SerializeField] private bool _invincibilityAfterDamage;
        public event Action OnDeath;

        private InvincibilityComponent _invincibilityComponent;
        private KnockBackComponent _knockBackComponent;
        private StatisticData _healthData;
        private IDisposable _disposable;

        private void OnEnable()
        {
            _healthData = Entity.GetStatisticData(_healthStatistic);
            _invincibilityComponent = Entity.GetComponent<InvincibilityComponent>();
            _knockBackComponent = Entity.GetComponent<KnockBackComponent>();
            
            _disposable = _healthData.ReactiveValue.Subscribe(OnHealthModified);
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }

        public void ModifyHealth(float value)
        {
            if (!CanDamage() && value < 0) return;
            
            _healthData.ModifyValue(value);
            TryApplyInvincibility();
            TryApplyKnockBack();
        }

        private void TryApplyInvincibility()
        {
            if (!_invincibilityComponent) return; 
            if (_invincibilityComponent.IsInvincible.CurrentValue) return;
            if (!_invincibilityAfterDamage) return;
            
            _invincibilityComponent.Apply();
        }
        
        private void TryApplyKnockBack()
        {
            if (!_knockBackComponent) return; 
            if (!_invincibilityAfterDamage) return;
            
            _invincibilityComponent.Apply();
        }

        private bool CanDamage()
        {
            if (!_invincibilityComponent) return true;
            return !_invincibilityComponent.IsInvincible.CurrentValue;
        }

        private void OnHealthModified(float value)
        {
            if (value <= 0)
            {
                OnDeath?.Invoke();
                Entity.Dispose();
            }
        }
    }
}