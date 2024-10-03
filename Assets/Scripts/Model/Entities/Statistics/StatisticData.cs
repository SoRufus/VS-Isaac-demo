using System;
using R3;
using UnityEngine;

namespace Model.Entities.Statistics
{
    [Serializable]
    public class StatisticData
    {
        [field: SerializeField] public Statistic Statistic { get; private set; }

        [SerializeField] private float _baseValue = 1;
        [SerializeField] private float _currentValue = 1;
        
        private readonly ReactiveProperty<float> _reactiveValue = new(1);

        public void SetValue(float value)
        {
            _reactiveValue.Value = value;
            _currentValue = value;
        }

        public void ModifyValue(float value)
        {
            _reactiveValue.Value += value;
            _currentValue += value;
        }
        
        public void SetToBaseValue()
        {
            SetValue(_baseValue);
        }

        public ReadOnlyReactiveProperty<float> ReactiveValue => _reactiveValue;
        public float Value => _currentValue;
        public float BaseValue => _baseValue;
    }
}