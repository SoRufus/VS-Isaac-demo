using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Model.Entities.Statistics;
using R3;
using UnityEngine;

namespace Model.Entities.Components
{
    public class InvincibilityComponent: EntityComponent
    {
        [SerializeField] private Statistic _invincibilityTimeStatistic;

        private readonly ReactiveProperty<bool> _isInvincible = new(false);
        
        private StatisticData _invincibilityData;
        private CancellationTokenSource _cancellationTokenSource;

        
        private void OnEnable()
        {
            _invincibilityData = Entity.GetStatisticData(_invincibilityTimeStatistic);
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            _cancellationTokenSource?.Cancel();
        }

        public void Apply(float timeInSeconds)
        {
            _isInvincible.Value = true;
            
            _cancellationTokenSource = new CancellationTokenSource();
            DisableInvincibilityAfterTime(timeInSeconds, _cancellationTokenSource.Token).Forget();
        }

        public void Apply()
        {
            _isInvincible.Value = true;
            
            _cancellationTokenSource = new CancellationTokenSource();
            DisableInvincibilityAfterTime(_invincibilityData.Value, _cancellationTokenSource.Token).Forget();
        }
        
        private async UniTask DisableInvincibilityAfterTime(float time, CancellationToken cancellationToken)
        {
            await UniTask.WaitForSeconds(time, cancellationToken: cancellationToken);
            _isInvincible.Value = false;
        }

        public ReadOnlyReactiveProperty<bool> IsInvincible => _isInvincible;
    }
}