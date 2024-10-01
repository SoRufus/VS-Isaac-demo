using System;
using Model.Entities.Components;
using Model.Entities.Player;
using Model.Entities.States;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Bootstrap
{
    public class PlayerInputHandler: MonoBehaviour, IDisposable
    {
        [Inject] private readonly Controls _controls;
        [Inject] private readonly Player _player;

        private StateComponent _stateComponent;
        private MovementComponent _movementComponent;
        private ShootingComponent _shootingComponent;
        
        private IDisposable _disposable;
        
        private void Awake()
        {
            _controls.Enable();
            
            _stateComponent = _player.GetComponent<StateComponent>();
            _movementComponent = _player.GetComponent<MovementComponent>();
            _shootingComponent = _player.GetComponent<ShootingComponent>();

            var disposableBuilder = Disposable.CreateBuilder();
            
            Observable.FromEvent<InputAction.CallbackContext>(
                    h => _controls.Player.Movement.performed += h,
                    h => _controls.Player.Movement.performed -= h)
                .Subscribe(ctx => OnMove(ctx.ReadValue<Vector2>()))
                .AddTo(ref disposableBuilder);
            
            Observable.FromEvent<InputAction.CallbackContext>(
                    h => _controls.Player.Movement.canceled += h,
                    h => _controls.Player.Movement.canceled -= h)
                .Subscribe(ctx => OnMove(ctx.ReadValue<Vector2>()))
                .AddTo(ref disposableBuilder);
            
            Observable.FromEvent<InputAction.CallbackContext>(
                    h => _controls.Player.Shoot.performed += h,
                    h => _controls.Player.Shoot.performed -= h)
                .Subscribe(ctx => OnShoot(ctx.ReadValue<Vector2>()))
                .AddTo(ref disposableBuilder);
            
            Observable.FromEvent<InputAction.CallbackContext>(
                    h => _controls.Player.Shoot.canceled += h,
                    h => _controls.Player.Shoot.canceled -= h)
                .Subscribe(ctx => OnShoot(ctx.ReadValue<Vector2>()))
                .AddTo(ref disposableBuilder);

            _disposable = disposableBuilder.Build();
        }

        private void OnMove(Vector2 movement)
        {
            _movementComponent.SetVelocity(movement);
            
            if (_stateComponent.CurrentState is ShootingState) return;

            if (movement != Vector2.zero)
            {
                _stateComponent.ApplyState(new MovementState());
            }
            else
            {
                _stateComponent.ApplyState(new IdleState());
            }
        }

        private void OnShoot(Vector2 direction)
        {
            _shootingComponent.SetShootingDirection(direction);

            
            if (direction != Vector2.zero)
            {
                _stateComponent.ApplyState(new ShootingState());
            }
            else
            {
                _stateComponent.ClearState();
            }
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}