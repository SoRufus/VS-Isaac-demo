using Core;

namespace Model.GameState
{
    public class GameStateManager
    {
        private IGameState _currentState;
        
        public GameStateManager()
        {
            _currentState = new PlayState();
        }   

        public void ChangeState(IGameState state)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }

            _currentState = state;
            _currentState.Enter();  
        }
    }
}