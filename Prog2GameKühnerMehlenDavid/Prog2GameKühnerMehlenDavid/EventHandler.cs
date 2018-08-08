using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie
{
    class EventHandler
    {

        KeyboardState previousState;

        public void ManageGameStates()
        {
            //SPLASHSCREEN
            if (Game1.currentGameState == Game1.GameState.SPLASHSCREEN)
            {
                //gibt enums zurück welche states
               Game1.currentGameState = splashScreen.clickedButton();

            }
            //MAINMENU
            if (Game1.currentGameState == Game1.GameState.MAINMENU)
            {

            }

            //INGAME
            if (Game1.currentGameState == Game1.GameState.GAMELOOP)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.L) && !previousState.IsKeyDown(Keys.L))
                    Game1.currentGameState = Game1.GameState.LEVELEDITOR;
                previousState = Keyboard.GetState();
            }

            //LEVELEDITORSTATE
            if (Game1.currentGameState == Game1.GameState.LEVELEDITOR)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.L) && !previousState.IsKeyDown(Keys.L))
                    Game1.currentGameState = Game1.GameState.GAMELOOP;
                previousState = Keyboard.GetState();
            }
        }
    }
}
