using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vitante.GameEngine
{
    class GameStateManager
    {
        static GameState currentState;

        public static GameState GetState()
        {
            return currentState;
        }

        public static void SetState(GameState newState)
        {
            currentState = newState;
        }
    }
}
