using System;
using System.Collections.Generic;
using Model.Entities.Player;
using Model.Entities.Statistics;
using UnityEngine;
using Zenject;
using R3;

namespace View.UI
{
    public class StatisticIconsView: MonoBehaviour
    {
        [Inject] private readonly Player _player;

        [SerializeField] private Statistic _statistic;
        [SerializeField] private List<GameObject> _emptyContainers = new();
        [SerializeField] private List<GameObject> _fullContainers = new();
        [SerializeField] private bool _halfContainers;

        private StatisticData _statisticData;
        private IDisposable _disposable;
        
        private void OnEnable()
        {
            _statisticData = _player.GetStatisticData(_statistic);
            _disposable = _statisticData.ReactiveValue.Subscribe(OnStatisticValueChanged);

            InitIconContainers();
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }

        private void InitIconContainers()
        {
            for (int i = 0; i < _emptyContainers.Count; i++)
            {
                var value = _statisticData.BaseValue;
                value /= _halfContainers ? 2 : 1;
                
                _emptyContainers[i].SetActive(i >= value);
            }
        }

        private void OnStatisticValueChanged(float value)
        {
            for (int i = 0; i < _fullContainers.Count; i++)
            {
                _fullContainers[i].SetActive(i >= value);
            }
        }
    }
}