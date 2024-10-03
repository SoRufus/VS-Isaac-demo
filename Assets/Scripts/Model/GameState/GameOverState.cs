using Core;
using UnityEngine;

namespace Model.GameState
{
    public class GameOverState: IGameState
    {
        public void Enter()
        {
            Time.timeScale = 0;
        }

        public void Exit()
        {
            
        }
    }
}