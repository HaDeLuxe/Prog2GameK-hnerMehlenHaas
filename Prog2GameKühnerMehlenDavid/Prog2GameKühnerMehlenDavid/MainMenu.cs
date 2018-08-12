using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class MainMenu {

        Texture2D MainMenu1;
        Texture2D MainMenu2;
        Texture2D MainMenu3;
        Texture2D MainMenu4;
        Texture2D MainMenu5;

        Enums Enums;

        Enums.MainMenuButtons CurrentButton;

        bool TexturesLoaded;
        bool buttonPressed;

        public MainMenu() {
            Enums = new Enums();
            CurrentButton = Enums.MainMenuButtons.START;
            TexturesLoaded = false;
            buttonPressed = false;
        }

        public void RenderMainMenu(ContentManager ContentManager, SpriteBatch SpriteBatch, SpriteFont Font) {
            if (!TexturesLoaded)
            {
                LoadMainMenuTextures(ContentManager);
            }

            switch(CurrentButton)
            {
                case Enums.MainMenuButtons.START:
                    SpriteBatch.Draw(MainMenu1, new Rectangle(0, 0, 2560, 1440), Color.White);
                    break;
                case Enums.MainMenuButtons.RESUME:
                    SpriteBatch.Draw(MainMenu2, new Rectangle(0, 0, 1920, 1080), Color.White);
                    break;
                case Enums.MainMenuButtons.OPTIONS:
                    SpriteBatch.Draw(MainMenu3, new Rectangle(0, 0, 1920, 1080), Color.White);
                    break;
                case Enums.MainMenuButtons.CREDITS:
                    SpriteBatch.Draw(MainMenu4, new Rectangle(0, 0, 1920, 1080), Color.White);
                    break;
                case Enums.MainMenuButtons.EXIT:
                    SpriteBatch.Draw(MainMenu5, new Rectangle(0, 0, 1920, 1080), Color.White);
                    
                    break;
            }

            SpriteBatch.DrawString(Font, "Neues Spiel", new Vector2(1400, 200), Color.Black, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
            SpriteBatch.DrawString(Font, "Fortfahren", new Vector2(1400, 360), Color.Black, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
            SpriteBatch.DrawString(Font, "Optionen", new Vector2(1400, 510), Color.Black, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
            SpriteBatch.DrawString(Font, "Credits", new Vector2(1400, 670), Color.Black, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
            SpriteBatch.DrawString(Font, "Verlassen", new Vector2(1400, 820), Color.Black, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
        }

        public void Update(Game1 Game) {
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !buttonPressed)
            {
                CurrentButton--;
                if (CurrentButton < 0) CurrentButton = Enums.MainMenuButtons.EXIT;
                buttonPressed = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && !buttonPressed)
            {
                CurrentButton++;
                if (CurrentButton > Enums.MainMenuButtons.EXIT) CurrentButton = Enums.MainMenuButtons.START;
                buttonPressed = true;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Enter) && !buttonPressed)
            {
                switch (CurrentButton)
                {
                    case Enums.MainMenuButtons.START:
                        Game1.currentGameState = Game1.GameState.GAMELOOP;
                        break;
                    case Enums.MainMenuButtons.RESUME:
                        break;
                    case Enums.MainMenuButtons.OPTIONS:
                        break;
                    case Enums.MainMenuButtons.CREDITS:
                        break;
                    case Enums.MainMenuButtons.EXIT:
                        Game.Exit();
                        break;
                }
                buttonPressed = true;
            }
            if (Keyboard.GetState().GetPressedKeys().Count() == 0)
            {
                buttonPressed = false;
            }

            
        }


        private void LoadMainMenuTextures(ContentManager ContentManager) {
            MainMenu1 = ContentManager.Load<Texture2D>("Images\\MainMenu\\HauptMenu-1");
            MainMenu2 = ContentManager.Load<Texture2D>("Images\\MainMenu\\HauptMenu-2");
            MainMenu3 = ContentManager.Load<Texture2D>("Images\\MainMenu\\HauptMenu-3");
            MainMenu4 = ContentManager.Load<Texture2D>("Images\\MainMenu\\HauptMenu-4");
            MainMenu5 = ContentManager.Load<Texture2D>("Images\\MainMenu\\HauptMenu-5");
            TexturesLoaded = true;
        }
    }
}
