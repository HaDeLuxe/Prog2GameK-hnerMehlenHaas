using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Menus
{
    public class SplashScreen
    {
        //ContentManager Content;
        Texture2D splashScreenBackground;
        bool loadTextures = true;

        //KeyboardState keyboardState;

        public void RenderSplashScreen(ContentManager content, SpriteBatch spriteBatch)
        {
            if (loadTextures)
                LoadSplashScreenTextures(content);

           // spriteBatch.Draw(splashScreenBackground, new Vector2(0, 0), null ,null , Vector2.Zero,0f, new Vector2( 1800f/(float) splashScreenBackground.Width, 1000/(float)splashScreenBackground.Height), Color.White, SpriteEffects.None, 0f);
            spriteBatch.Draw(splashScreenBackground, new Rectangle(0, 0,1800,1000), Color.White);
        }

        public void ClickedButton()
        {
            //keyboardState = Keyboard.GetState().GetPressedKeys();

            if (Keyboard.GetState().GetPressedKeys().Count() > 0)
                Game1.currentGameState = Game1.GameState.MAINMENU;
           
        }
       
        private void LoadSplashScreenTextures(ContentManager content)
        {
            splashScreenBackground = content.Load<Texture2D>("Images\\pressAValidButtonSplashScreen");
            loadTextures = false;
        }

        //unload function --> wäre schön wenn man diesen State verlässt(buttonclick in menü oder so), dass alle Texturen die nichtmehr gebraucht werden unloaded werden... aber geht nur mit allem auf einmal
    }
}
