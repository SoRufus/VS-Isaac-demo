﻿using System;
using Model.Entities.States;
using Model.Entities.Statistics;
using R3;
using UnityEngine;

namespace Model.Entities.Components
{
    public class MovementComponent: EntityComponent
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Statistic _movementSpeedStatistic;

        private readonly ReactiveProperty<Vector2> _velocity = new();
        private StatisticData _movementSpeedData;
        private StateComponent _stateComponent;

        private void OnEnable()
        {
            _movementSpeedData = Entity.GetStatisticData(_movementSpeedStatistic);
            _stateComponent = Entity.GetComponent<StateComponent>();
        }

        public void SetVelocity(Vector2 velocity)
        {
            _velocity.Value = velocity;
        }
        
        public void FixedUpdate()
        {
            if (Time.timeScale == 0) return;
            if (_stateComponent.CurrentState is KnockBackState) return;

            var velocity = _velocity.CurrentValue;
            if (_movementSpeedData != null) velocity *= _movementSpeedData.Value;
            
            _rigidbody.velocity = velocity;
            
        }

        public ReadOnlyReactiveProperty<Vector2> Velocity => _velocity;
    }
}