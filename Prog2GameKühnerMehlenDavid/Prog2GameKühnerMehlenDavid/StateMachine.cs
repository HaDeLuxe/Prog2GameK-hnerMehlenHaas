using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class StateMachine {

        KeyboardState PreviousState;

        public void ManageGamestates() {
            
            if (Keyboard.GetState().IsKeyDown(Keys.L) && !PreviousState.IsKeyDown(Keys.L))
            {
                if (Game1.CurrentGameState == Game1.GameState.GAMELOOP)
                {
                    Game1.CurrentGameState = Game1.GameState.LEVELEDITOR;
                }
                else if (Game1.CurrentGameState == Game1.GameState.LEVELEDITOR)
                {
                    Game1.CurrentGameState = Game1.GameState.GAMELOOP;
                }
            }
            PreviousState = Keyboard.GetState();

        }
    }
}
