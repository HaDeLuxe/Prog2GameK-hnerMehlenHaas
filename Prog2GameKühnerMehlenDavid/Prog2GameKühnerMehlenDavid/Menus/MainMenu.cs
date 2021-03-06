﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Menus {
    /// <summary>
    /// Logic for the MainMenu and takes action after pushing a button.
    /// </summary>
    class MainMenu {


        Enums.MainMenuStates currentState;
        Enums.MainMenuButtons currentButton;
        Enums.MainMenuButtons currentControlButton;
        Options options = null;

        bool buttonPressed;
        bool controlButtonPressed;
        bool controlWindowOpen;

        public MainMenu() {
            currentState = Enums.MainMenuStates.MAIN;
            currentButton = Enums.MainMenuButtons.START;
            currentControlButton = Enums.MainMenuButtons.CONTROLNO;
            buttonPressed = true;
            controlWindowOpen = false;
            controlButtonPressed = true;
            options = new Options();
            
        }

        public void RenderMainMenu(Dictionary<string, Texture2D> texturesDictionary, SpriteBatch spriteBatch, SpriteFont Font, Levels levelManager, LoadAndSave loadAndSave, ref List<GameObject> allGameObjects, ref Player player, Game1 game, Matrix transformationMatrix) {
            switch (currentState)
            {
                case Enums.MainMenuStates.MAIN:
                    if (!controlWindowOpen)
                    {
                        switch (currentButton)
                        {
                            case Enums.MainMenuButtons.START:
                                spriteBatch.Draw(texturesDictionary["MainMenu1"], new Rectangle(0, 0, 1920, 1080), Color.White);
                                break;
                            case Enums.MainMenuButtons.RESUME:
                                spriteBatch.Draw(texturesDictionary["MainMenu2"], new Rectangle(0, 0, 1920, 1080), Color.White);
                                break;
                            case Enums.MainMenuButtons.OPTIONS:
                                spriteBatch.Draw(texturesDictionary["MainMenu3"], new Rectangle(0, 0, 1920, 1080), Color.White);
                                break;
                            case Enums.MainMenuButtons.CREDITS:
                                spriteBatch.Draw(texturesDictionary["MainMenu4"], new Rectangle(0, 0, 1920, 1080), Color.White);
                                break;
                            case Enums.MainMenuButtons.EXIT:
                                spriteBatch.Draw(texturesDictionary["MainMenu5"], new Rectangle(0, 0, 1920, 1080), Color.White);

                                break;
                        }

                        spriteBatch.DrawString(Font, "Neues Spiel", new Vector2(1400, 200), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        spriteBatch.DrawString(Font, "Fortfahren", new Vector2(1400, 360), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        spriteBatch.DrawString(Font, "Optionen", new Vector2(1400, 510), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        spriteBatch.DrawString(Font, "Credits", new Vector2(1400, 670), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                        spriteBatch.DrawString(Font, "Verlassen", new Vector2(1400, 820), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                    }


                    if (controlWindowOpen)
                    {
                        newGameControlScreen(Font, spriteBatch, texturesDictionary, loadAndSave, ref allGameObjects, ref player, game);
                    }
                    break;
                case Enums.MainMenuStates.OPTION:
                    options.drawOptions(spriteBatch, texturesDictionary, transformationMatrix, Font);
                    break;
            }
            
        }

        public void Update(Game1 Game)
        {
            switch (currentState)
            {
                case Enums.MainMenuStates.MAIN:
                    if ((Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up) || GamePad.GetState(0).IsButtonDown(Buttons.DPadUp) || GamePad.GetState(0).ThumbSticks.Left.Y > 0.5f) && !buttonPressed && !controlWindowOpen)
                    {
                        currentButton--;
                        if (currentButton < 0) currentButton = Enums.MainMenuButtons.EXIT;
                        buttonPressed = true;
                    }
                    if ((Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down) || GamePad.GetState(0).IsButtonDown(Buttons.DPadDown) || GamePad.GetState(0).ThumbSticks.Left.Y < -0.5f) && !buttonPressed && !controlWindowOpen)
                    {
                        currentButton++;
                        if (currentButton > Enums.MainMenuButtons.EXIT) currentButton = Enums.MainMenuButtons.START;
                        buttonPressed = true;
                    }
                    if ((Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(0).IsButtonDown(Buttons.A)) && !buttonPressed)
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
                                currentState = Enums.MainMenuStates.OPTION;
                                break;
                            case Enums.MainMenuButtons.CREDITS:
                                Game1.currentGameState = Game1.GameState.CREDITS;
                                break;
                            case Enums.MainMenuButtons.EXIT:
                                Game.Exit();
                                break;
                        }
                    }
                    if (Keyboard.GetState().GetPressedKeys().Count() == 0 && 
                        GamePad.GetState(0).IsButtonUp(Buttons.DPadUp) && 
                        GamePad.GetState(0).IsButtonUp(Buttons.A) && 
                        GamePad.GetState(0).IsButtonUp(Buttons.DPadDown) && 
                        GamePad.GetState(0).ThumbSticks.Left.Y < 0.5f && 
                        GamePad.GetState(0).ThumbSticks.Left.Y > -0.5f)
                    {
                        buttonPressed = false;
                    }
                    break;
                case Enums.MainMenuStates.OPTION:
                    options.Update(this);
                break;
            }
            
        }


        public void getBackToMainMenu()
        {
            currentState = Enums.MainMenuStates.MAIN;
        }

        private void LoadNewGame(LoadAndSave loadAndSave) {
            ItemUIManager.armorPickedUp = false;
            ItemUIManager.snailShellPickedUp = false;
            ItemUIManager.shovelPickedUp = false;
            ItemUIManager.scissorsPickedUp = false;
            ItemUIManager.goldenUmbrellaPickedUp = false;
            ItemUIManager.healthPickedUp = false;
            ItemUIManager.powerPickedUp = false;
            ItemUIManager.jumpPickedUp = false;
            ItemUIManager.cornnencyQuantity = 0;
            Game1.wormPlayer.gameObjectPosition = new Vector2(13444, 1700);
        }

        void newGameControlScreen(SpriteFont Font, SpriteBatch SpriteBatch, Dictionary<string, Texture2D> texturesDictionary, LoadAndSave loadAndSave, ref List<GameObject> allGameObjects, ref Player player, Game1 game1) {
            controlWindowOpen = true;

            if ((Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up) || GamePad.GetState(0).IsButtonDown(Buttons.DPadUp) || GamePad.GetState(0).ThumbSticks.Left.Y > 0.5f) && !controlButtonPressed && controlWindowOpen)
            {
                currentControlButton--;
                if (currentControlButton < Enums.MainMenuButtons.CONTROLYES) currentControlButton = Enums.MainMenuButtons.CONTROLNO;
                controlButtonPressed = true;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down) || GamePad.GetState(0).IsButtonDown(Buttons.DPadDown) || GamePad.GetState(0).ThumbSticks.Left.Y < -0.5f) && !controlButtonPressed && controlWindowOpen)
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

            if ((Keyboard.GetState().IsKeyDown(Keys.Enter)|| GamePad.GetState(0).IsButtonDown(Buttons.A)) && !controlButtonPressed)
            {
                switch (currentControlButton)
                {
                    case Enums.MainMenuButtons.CONTROLYES:
                        game1.NewGame();
                        LoadNewGame(loadAndSave);
                        Game1.currentGameState = Game1.GameState.GAMELOOP;
                        controlWindowOpen = false;
                        break;
                    case Enums.MainMenuButtons.CONTROLNO:
                        controlWindowOpen = false;
                        break;
                  
                }
                controlButtonPressed = true;
            }


            if (Keyboard.GetState().GetPressedKeys().Count() == 0 && GamePad.GetState(0).IsButtonUp(Buttons.DPadUp) && GamePad.GetState(0).IsButtonUp(Buttons.A) && GamePad.GetState(0).IsButtonUp(Buttons.DPadDown) &&GamePad.GetState(0).ThumbSticks.Left.Y < 0.5f && GamePad.GetState(0).ThumbSticks.Left.Y > -0.5f)
            {
                controlButtonPressed = false;

            }

            SpriteBatch.DrawString(Font, "Ja", new Vector2(1400, 200), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            SpriteBatch.DrawString(Font, "Nein", new Vector2(1400, 360), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            SpriteBatch.DrawString(Font, "Echt jetzt?", new Vector2(1400, 50), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);

        }


       
    }
}
