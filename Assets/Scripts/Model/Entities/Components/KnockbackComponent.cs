using System.Threading;
using Cysharp.Threading.Tasks;
using Model.Entities.States;
using UnityEngine;

namespace Model.Entities.Components
{
    public class KnockBackComponent: EntityComponent
    {
        [SerializeField] private Rigidbody2D _rigid;
        
        private CancellationTokenSource _cancellationTokenSource;
        private StateComponent _stateComponent;

        private void OnEnable()
        {
            _stateComponent = Entity.GetComponent<StateComponent>();
        }
        
        private void OnDisable()
        {
            _cancellationTokenSource?.Cancel();
        }

        public void Apply(Vector2 direction, float strength, float duration)
        {
            _stateComponent.ApplyState(new KnockBackState());
            var velocity = direction * strength;

            _rigid.velocity = velocity;
            
            _cancellationTokenSource = new CancellationTokenSource();
            DisableKnockBack(duration, _cancellationTokenSource.Token).Forget();
        }
        
        private async UniTask DisableKnockBack(float time, CancellationToken cancellationToken)
        {
            await UniTask.WaitForSeconds(time, cancellationToken: cancellationToken);
            _stateComponent.ClearState();
        }
    }
}