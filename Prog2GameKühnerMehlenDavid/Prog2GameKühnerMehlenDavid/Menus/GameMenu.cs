using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace Reggie.Menus {
    /// <summary>
    /// Opens the pause menu by pressing P on the keyboard or Start on the GameController
    /// Contains the logic when pressing a button.
    /// </summary>
    class GameMenu {
        Enums.GameMenuButtons currentButton;
        bool buttonPressed;

        public GameMenu() 
        {
            currentButton = Enums.GameMenuButtons.RESUME;
            buttonPressed = false;
        }

        public void DrawGameMenu(Dictionary<string, Texture2D> texturesDictionary, SpriteBatch SpriteBatch, SpriteFont Font) 
        {
            switch (currentButton)
            {
                case Enums.GameMenuButtons.RESUME:
                    SpriteBatch.Draw(texturesDictionary["MainMenu1"], new Rectangle(0, 0, 1920, 1080), Color.White);
                    break;
                case Enums.GameMenuButtons.OPTIONS:
                    SpriteBatch.Draw(texturesDictionary["MainMenu2"], new Rectangle(0, 0, 1920, 1080), Color.White);
                    break;
                case Enums.GameMenuButtons.MAINMENU:
                    SpriteBatch.Draw(texturesDictionary["MainMenu3"], new Rectangle(0, 0, 1920, 1080), Color.White);
                    break;
            }

            SpriteBatch.DrawString(Font, "Weiter", new Vector2(1400, 200), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            SpriteBatch.DrawString(Font, "Optionen", new Vector2(1400, 370), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            SpriteBatch.DrawString(Font, "Hauptmenu", new Vector2(1400, 510), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);

        }

        public void Update(Game1 Game, LoadAndSave loadAndSave)
        {
            if ((Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up) || GamePad.GetState(0).IsButtonDown(Buttons.DPadUp) || GamePad.GetState(0).ThumbSticks.Left.Y > 0.5f) && !buttonPressed)
            {
                currentButton--;
                if (currentButton < 0) currentButton = Enums.GameMenuButtons.MAINMENU;
                buttonPressed = true;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down) || GamePad.GetState(0).IsButtonDown(Buttons.DPadDown) || GamePad.GetState(0).ThumbSticks.Left.Y < -0.5f) && !buttonPressed)
            {
                currentButton++;
                if (currentButton > Enums.GameMenuButtons.MAINMENU) currentButton = Enums.GameMenuButtons.RESUME;
                buttonPressed = true;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(0).IsButtonDown(Buttons.A)) && !buttonPressed)
            {
                buttonPressed = true;
                switch (currentButton)
                {
                    case Enums.GameMenuButtons.RESUME:
                        Game1.currentGameState = Game1.GameState.GAMELOOP;
                        break;
                    case Enums.GameMenuButtons.OPTIONS:
                        break;
                    case Enums.GameMenuButtons.MAINMENU:
                        Game1.currentGameState = Game1.GameState.MAINMENU;
                        break;
                }
            }
            if (Keyboard.GetState().GetPressedKeys().Count() == 0 && GamePad.GetState(0).ThumbSticks.Left.Y < 0.5f && GamePad.GetState(0).ThumbSticks.Left.Y > -0.5f && GamePad.GetState(0).IsButtonUp(Buttons.DPadUp) && GamePad.GetState(0).IsButtonUp(Buttons.DPadDown) && GamePad.GetState(0).IsButtonUp(Buttons.A))
            {
                buttonPressed = false;
            }
        }
    }
}
