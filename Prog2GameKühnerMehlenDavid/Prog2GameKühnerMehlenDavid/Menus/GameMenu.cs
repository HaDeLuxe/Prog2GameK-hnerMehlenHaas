using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Menus {
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
                case Enums.GameMenuButtons.SAVE:
                    SpriteBatch.Draw(texturesDictionary["MainMenu2"], new Rectangle(0, 0, 1920, 1080), Color.White);
                    break;
                case Enums.GameMenuButtons.OPTIONS:
                    SpriteBatch.Draw(texturesDictionary["MainMenu3"], new Rectangle(0, 0, 1920, 1080), Color.White);
                    break;
                case Enums.GameMenuButtons.MAINMENU:
                    SpriteBatch.Draw(texturesDictionary["MainMenu4"], new Rectangle(0, 0, 1920, 1080), Color.White);
                    break;
            }

            SpriteBatch.DrawString(Font, "Weiter", new Vector2(1400, 200), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            SpriteBatch.DrawString(Font, "Speichern", new Vector2(1400, 360), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            SpriteBatch.DrawString(Font, "Optionen", new Vector2(1400, 510), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            SpriteBatch.DrawString(Font, "Hauptmenu", new Vector2(1400, 670), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);

        }

        public void Update(Game1 Game, LoadAndSave loadAndSave)
        {
            if ((Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)) && !buttonPressed)
            {
                currentButton--;
                if (currentButton < 0) currentButton = Enums.GameMenuButtons.MAINMENU;
                buttonPressed = true;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) && !buttonPressed)
            {
                currentButton++;
                if (currentButton > Enums.GameMenuButtons.MAINMENU) currentButton = Enums.GameMenuButtons.RESUME;
                buttonPressed = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !buttonPressed)
            {
                buttonPressed = true;
                switch (currentButton)
                {
                    case Enums.GameMenuButtons.RESUME:
                        Game1.currentGameState = Game1.GameState.GAMELOOP;
                        break;
                    case Enums.GameMenuButtons.SAVE:
                        loadAndSave.Save();
                        break;
                    case Enums.GameMenuButtons.OPTIONS:
                        break;
                    case Enums.GameMenuButtons.MAINMENU:
                        Game1.currentGameState = Game1.GameState.MAINMENU;
                        break;
                }
            }
            if (Keyboard.GetState().GetPressedKeys().Count() == 0)
            {
                buttonPressed = false;
            }
        }
    }
}
