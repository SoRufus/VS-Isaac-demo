using System;
using System.Collections.Generic;
using System.Linq;
using Model.Entities.Player;
using Model.Entities.Statistics;
using R3;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace View.UI
{
    public class StatisticIconsView: MonoBehaviour
    {
        [Inject] private readonly Player _player;

        [SerializeField] private Statistic _statistic;
        [SerializeField] private List<GameObject> _emptyContainers = new();
        [SerializeField] private List<GameObject> _halfEmptyContainers = new();
        [SerializeField] private List<GameObject> _fullContainers = new();

        private StatisticData _statisticData;
        private IDisposable _disposable;
        
        private void Awake()
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
                _emptyContainers[i].SetActive(i < _statisticData.BaseValue);
            }
            
            for (int i = 0; i < _halfEmptyContainers.Count; i++)
            {
                _halfEmptyContainers[i].SetActive(i < _statisticData.BaseValue / 2);
            }
        }

        private void OnStatisticValueChanged(float value)
        {
            for (int i = 0; i < _fullContainers.Count; i++)
            {
                _fullContainers[i].SetActive(i < value);
            }
        }
    }
}