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

        KeyboardState PreviousState;

        public void ManageGameStates()
        {
            switch (Game1.CurrentGameState)
            {
                case Game1.GameState.SPLASHSCREEN:
                    //gibt enums zurück welche states
                    //Game1.CurrentGameState = splashScreen.clickedButton();
                    break;
                case Game1.GameState.MAINMENU:
                    break;
                case Game1.GamState.GAMELOOP:
                    if (Keyboard.GetState().IsKeyDown(Keys.L) && !PreviousState.IsKeyDown(Keys.L))
                        Game1.CurrentGameState = Game1.GameState.LEVELEDITOR;
                    PreviousState = Keyboard.GetState();
                    break;
                case Game1.GameState.LEVELEDITOR:

                    if (Keyboard.GetState().IsKeyDown(Keys.L) && !PreviousState.IsKeyDown(Keys.L))
                        Game1.CurrentGameState = Game1.GameState.GAMELOOP;
                    PreviousState = Keyboard.GetState();
                    break;
            }
           
        }
    }
}
