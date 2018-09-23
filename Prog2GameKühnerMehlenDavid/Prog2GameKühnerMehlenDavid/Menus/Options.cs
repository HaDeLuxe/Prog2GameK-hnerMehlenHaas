using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Menus {
    class Options {

        Slider globalVolumeSlider = null;
        Slider musicVolumeSlider = null;
        Slider soundEffectsVolumeSlider = null;

        enum OptionsStates { GLOBAL, MUSIC, SOUND, BACK }
        OptionsStates currentState = OptionsStates.GLOBAL;

        bool buttonPressed;
        bool musicPlaying;

        AudioManager audioManager;

        public Options()
        {
            globalVolumeSlider = new Slider(370, new Vector2(1300,250));
            musicVolumeSlider = new Slider(370, new Vector2(1300, 410));
            soundEffectsVolumeSlider = new Slider(370, new Vector2(1300, 560));
            buttonPressed = false;
            musicPlaying = false;
            audioManager = AudioManager.AudioManagerInstance();
        }

        public void drawOptions(SpriteBatch spriteBatch, Dictionary<string, Texture2D> texturesDictionary, Matrix transformationMatrix, SpriteFont font)
        {
            switch (currentState)
            {
                case OptionsStates.GLOBAL:
                    spriteBatch.Draw(texturesDictionary["MainMenu1"], new Rectangle(0, 0, 1920, 1080), Color.White);
                    break;
                case OptionsStates.MUSIC:
                    spriteBatch.Draw(texturesDictionary["MainMenu2"], new Rectangle(0, 0, 1920, 1080), Color.White);
                    break;
                case OptionsStates.SOUND:
                    spriteBatch.Draw(texturesDictionary["MainMenu3"], new Rectangle(0, 0, 1920, 1080), Color.White);
                    break;
                case OptionsStates.BACK:
                    spriteBatch.Draw(texturesDictionary["MainMenu4"], new Rectangle(0, 0, 1920, 1080), Color.White);
                    break;
               
            }
            spriteBatch.DrawString(font, "Sounds", new Vector2(1400, 100), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Global", new Vector2(1400, 200), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            globalVolumeSlider.drawSlider(spriteBatch, texturesDictionary, transformationMatrix);
            spriteBatch.DrawString(font, "Music", new Vector2(1400, 360), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            musicVolumeSlider.drawSlider(spriteBatch, texturesDictionary, transformationMatrix);
            spriteBatch.DrawString(font, "Soundeffects", new Vector2(1400, 510), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            soundEffectsVolumeSlider.drawSlider(spriteBatch, texturesDictionary, transformationMatrix);
            spriteBatch.DrawString(font, "Back", new Vector2(1400, 670), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);

        }

        public void Update(MainMenu mainMenu)
        {
            if ((Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up) || GamePad.GetState(0).ThumbSticks.Left.Y > 0.5f) && !buttonPressed)
            {
                currentState--;
                if (currentState < 0) currentState = OptionsStates.BACK;
                buttonPressed = true;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down) || GamePad.GetState(0).ThumbSticks.Left.Y < -0.5f) && !buttonPressed)
            {
                currentState++;
                if (currentState > OptionsStates.BACK) currentState = OptionsStates.GLOBAL;
                buttonPressed = true;
            }

            switch (currentState)
            {
                case OptionsStates.GLOBAL:
                    //audioManager.Break("Reggie_Music_Soundeffect");
                    if (GamePad.GetState(0).ThumbSticks.Left.X < -0.5f)
                    {
                        globalVolumeSlider.changeCurrentState(-5);
                    }
                    if (GamePad.GetState(0).ThumbSticks.Left.X > 0.5f)
                    {
                        globalVolumeSlider.changeCurrentState(5);
                    }
                    break;
                case OptionsStates.MUSIC:
                    audioManager.Play("Reggie_Music_Soundeffect");
                    if (!musicPlaying)
                    {
                        audioManager.Play("Reggie_Music_Soundeffect");
                        musicPlaying = true;
                    }
                    if (GamePad.GetState(0).ThumbSticks.Left.X < -0.5f)
                    {
                        musicVolumeSlider.changeCurrentState(-5);
                        
                    }
                    if (GamePad.GetState(0).ThumbSticks.Left.X > 0.5f)
                    {
                        musicVolumeSlider.changeCurrentState(5);
                    }
                    break;
                case OptionsStates.SOUND:
                    //audioManager.Break("Reggie_Music_Soundeffect");
                    if (GamePad.GetState(0).ThumbSticks.Left.X < -0.5f)
                    {
                        soundEffectsVolumeSlider.changeCurrentState(-5);
                        

                    }
                    if (GamePad.GetState(0).ThumbSticks.Left.X > 0.5f)
                    {
                        soundEffectsVolumeSlider.changeCurrentState(5);
                    }
                    break;
                case OptionsStates.BACK:
                    if ((Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(0).IsButtonDown(Buttons.A)) && !buttonPressed)
                    {
                        mainMenu.getBackToMainMenu();

                    }
                    break;
                    
            }
            if (Keyboard.GetState().GetPressedKeys().Count() == 0 && GamePad.GetState(0).ThumbSticks.Left.Y < 0.5f && GamePad.GetState(0).ThumbSticks.Left.Y > -0.5f)
            {
                buttonPressed = false;
            }

            globalVolumeSlider.moveSlider();
            audioManager.globalVolume = globalVolumeSlider.getCurrentState();
            musicVolumeSlider.moveSlider();
            audioManager.musicVolume = musicVolumeSlider.getCurrentState();
            soundEffectsVolumeSlider.moveSlider();
            audioManager.soundVolume = soundEffectsVolumeSlider.getCurrentState();
        }
    }
}
