using Model.Entities.States;

namespace Model.Entities.Components
{
    public class StateComponent : EntityComponent
    {
        //wip
        private readonly State _defaultState = new IdleState();
    
        private State _currentState;

        private void OnEnable()
        {
            ApplyState(_defaultState);
        }

        public void ApplyState(State state)
        {
            _currentState?.OnDisposed();
            _currentState = state;
            InitState();
        }

        public void ClearState()
        {
            _currentState?.OnDisposed();
            _currentState = _defaultState;
            InitState();
        }

        private void InitState()
        {
            _currentState?.OnApplied();
        }

        public State CurrentState => _currentState;
    }
}
