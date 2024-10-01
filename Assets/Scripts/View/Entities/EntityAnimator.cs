using System;
using Model.Entities;
using Model.Entities.Components;
using UnityEngine;
using R3;

public class EntityAnimator : MonoBehaviour
{
    private const string _speedParameterName = "Speed";
    private const string _directionXParameterName = "DirectionX";
    private const string _directionYParameterName = "DirectionY";
    
    private static readonly int _speed = Animator.StringToHash(_speedParameterName);
    private static readonly int _directionX = Animator.StringToHash(_directionXParameterName);
    private static readonly int _directionY = Animator.StringToHash(_directionYParameterName);


    [SerializeField] private Entity _entity;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private IDisposable _disposable;
    private void OnEnable()
    {
        var movementComponent = _entity.GetComponent<MovementComponent>();

        _disposable = movementComponent.Velocity.Subscribe(OnMove);
    }
    
    private void OnDisable()
    {
        _disposable?.Dispose();
    }

    private void OnMove(Vector2 value)
    {
        if (value.x != 0)
        {
            _spriteRenderer.flipX = value.x > 0f;
        }
        
        _animator.SetFloat(_speed, value.magnitude);
        _animator.SetFloat(_directionX, value.x);
        _animator.SetFloat(_directionY, value.y); 
    }
}
