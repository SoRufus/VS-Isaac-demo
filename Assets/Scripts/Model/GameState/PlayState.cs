using Core;
using UnityEngine;

namespace Model.GameState
{
    public class PlayState: IGameState
    {
        public void Enter()
        {
            Time.timeScale = 1;
        }

        public void Exit()
        {
            
        }
    }
}