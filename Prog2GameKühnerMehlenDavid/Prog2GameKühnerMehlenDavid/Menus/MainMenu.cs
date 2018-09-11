using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Menus {
    class MainMenu {

        

        Enums.MainMenuButtons currentButton;
        Enums.MainMenuButtons currentControlButton;

        bool buttonPressed;
        bool controlButtonPressed;
        bool controlWindowOpen;

        public MainMenu() { 
            currentButton = Enums.MainMenuButtons.START;
            currentControlButton = Enums.MainMenuButtons.CONTROLNO;
            buttonPressed = true;
            controlWindowOpen = false;
            controlButtonPressed = true;
            
        }

        public void RenderMainMenu(Dictionary<string, Texture2D> texturesDictionary, SpriteBatch SpriteBatch, SpriteFont Font) {

            if (!controlWindowOpen)
            {
                switch (currentButton)
                {
                    case Enums.MainMenuButtons.START:
                        SpriteBatch.Draw(texturesDictionary["MainMenu1"], new Rectangle(0, 0, 1920, 1080), Color.White);
                        break;
                    case Enums.MainMenuButtons.RESUME:
                        SpriteBatch.Draw(texturesDictionary["MainMenu2"], new Rectangle(0, 0, 1920, 1080), Color.White);
                        break;
                    case Enums.MainMenuButtons.OPTIONS:
                        SpriteBatch.Draw(texturesDictionary["MainMenu3"], new Rectangle(0, 0, 1920, 1080), Color.White);
                        break;
                    case Enums.MainMenuButtons.CREDITS:
                        SpriteBatch.Draw(texturesDictionary["MainMenu4"], new Rectangle(0, 0, 1920, 1080), Color.White);
                        break;
                    case Enums.MainMenuButtons.EXIT:
                        SpriteBatch.Draw(texturesDictionary["MainMenu5"], new Rectangle(0, 0, 1920, 1080), Color.White);

                        break;
                }

                SpriteBatch.DrawString(Font, "Neues Spiel", new Vector2(1400, 200), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                SpriteBatch.DrawString(Font, "Fortfahren", new Vector2(1400, 360), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                SpriteBatch.DrawString(Font, "Optionen", new Vector2(1400, 510), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                SpriteBatch.DrawString(Font, "Credits", new Vector2(1400, 670), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                SpriteBatch.DrawString(Font, "Verlassen", new Vector2(1400, 820), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            }
            

            if (controlWindowOpen)
            {
                newGameControlScreen(Font, SpriteBatch, texturesDictionary);
            }
        }

        public void Update(Game1 Game) {
            
            if ((Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)) && !buttonPressed && !controlWindowOpen)
            {
                currentButton--;
                if (currentButton < 0) currentButton = Enums.MainMenuButtons.EXIT;
                buttonPressed = true;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) && !buttonPressed && !controlWindowOpen)
            {
                currentButton++;
                if (currentButton > Enums.MainMenuButtons.EXIT) currentButton = Enums.MainMenuButtons.START;
                buttonPressed = true;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Enter) && !buttonPressed)
            {
                buttonPressed = true;
                switch (currentButton)
                {
                    case Enums.MainMenuButtons.START:

                        controlWindowOpen = true;
                        break;
                    case Enums.MainMenuButtons.RESUME:
                        Game1.currentGameState = Game1.GameState.GAMELOOP; 
                        break;
                    case Enums.MainMenuButtons.OPTIONS:
                        break;
                    case Enums.MainMenuButtons.CREDITS:
                        break;
                    case Enums.MainMenuButtons.EXIT:
                        Game.Exit();
                        break;
                }
            }
            if (Keyboard.GetState().GetPressedKeys().Count() == 0)
            {
                buttonPressed = false;
            }
        }
        
        


        private void LoadNewGame() {
            ItemUIManager.armorPickedUp = false;
            ItemUIManager.snailShellPickedUp = false;
            ItemUIManager.shovelPickedUp = false;
            ItemUIManager.scissorsPickedUp = false;
            ItemUIManager.goldenUmbrellaPickedUp = false;
            ItemUIManager.healthPickedUp = false;
            ItemUIManager.powerPickedUp = false;
            ItemUIManager.jumpPickedUp = false;
            Game1.wormPlayer.gameObjectPosition = new Vector2(13444, 1700);
        }

        void newGameControlScreen( SpriteFont Font, SpriteBatch SpriteBatch, Dictionary<string, Texture2D> texturesDictionary) {
            controlWindowOpen = true;

            if ((Keyboard.GetState().IsKeyDown(Keys.W)|| Keyboard.GetState().IsKeyDown(Keys.Up)) && !controlButtonPressed && controlWindowOpen)
            {
                currentControlButton--;
                if (currentControlButton < Enums.MainMenuButtons.CONTROLYES) currentControlButton = Enums.MainMenuButtons.CONTROLNO;
                controlButtonPressed = true;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) && !controlButtonPressed && controlWindowOpen)
            {
                currentControlButton++;
                if (currentControlButton > Enums.MainMenuButtons.CONTROLNO) currentControlButton = Enums.MainMenuButtons.CONTROLYES;
                controlButtonPressed = true;
            }
            switch (currentControlButton)
            {
                case Enums.MainMenuButtons.CONTROLYES:
                    SpriteBatch.Draw(texturesDictionary["MainMenu1"], new Rectangle(0, 0, 1920, 1080), Color.White);
                    break;
                case Enums.MainMenuButtons.CONTROLNO:
                    SpriteBatch.Draw(texturesDictionary["MainMenu2"], new Rectangle(0, 0, 1920, 1080), Color.White);
                    break;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !controlButtonPressed)
            {
                switch (currentControlButton)
                {
                    case Enums.MainMenuButtons.CONTROLYES:
                        LoadNewGame();
                        Game1.currentGameState = Game1.GameState.GAMELOOP;
                        controlWindowOpen = false;
                        break;
                    case Enums.MainMenuButtons.CONTROLNO:
                        controlWindowOpen = false;
                        break;
                  
                }
                controlButtonPressed = true;
            }


            if (Keyboard.GetState().GetPressedKeys().Count() == 0)
            {
                controlButtonPressed = false;

            }

            SpriteBatch.DrawString(Font, "Ja", new Vector2(1400, 200), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            SpriteBatch.DrawString(Font, "Nein", new Vector2(1400, 360), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            SpriteBatch.DrawString(Font, "Echt jetzt?", new Vector2(1400, 50), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);

        }


       
    }
}
