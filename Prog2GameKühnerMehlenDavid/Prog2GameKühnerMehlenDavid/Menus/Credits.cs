using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Menus
{
    class Credits
    {
        bool buttonPressed { get; set; } = true;

        public void drawCredits(SpriteBatch spriteBatch, Dictionary<string, Texture2D> texturesDictionary, SpriteFont font)
        {
            spriteBatch.Draw(texturesDictionary["red"], new Vector2(0, 0), null, Color.LightBlue, 0, Vector2.Zero, new Vector2(1920, 1080), SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Created by: ", new Vector2(300, 100), Color.Black, 0, Vector2.Zero, 0.9f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Haas Pascal", new Vector2(300, 200), Color.Black, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Kuehner Markus", new Vector2(300, 300), Color.Black, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Mehlen David", new Vector2(300, 400), Color.Black, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Font used: ", new Vector2(300, 500), Color.Black, 0, Vector2.Zero, 0.9f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Luckiest Guy Font (Free for commercial use, License added in Content folder)", new Vector2(300, 600), Color.Black, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Third hand Images used: ", new Vector2(300, 700), Color.Black,0, Vector2.Zero, 0.9f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "\"Abstract Platformer\" (kenney.nl, License: CC0 1.0 Universal)", new Vector2(300, 800), Color.Black, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "\"Game Icons\" (kenney.nl, License: CC0 1.0 Universal)", new Vector2(300, 900), Color.Black, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0);

            spriteBatch.DrawString(font,"Press B or Enter To Return To MainMenu", new Vector2(1270, 970), Color.Black, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
        }

        public void UpdateCredits()
        {
            if ((GamePad.GetState(0).IsButtonDown(Buttons.B) || Keyboard.GetState().IsKeyDown(Keys.Enter)) && !buttonPressed)
            {
                buttonPressed = true;
                Game1.currentGameState = Game1.GameState.MAINMENU;
            }
            if (GamePad.GetState(0).IsButtonUp(Buttons.B) && Keyboard.GetState().IsKeyUp(Keys.Enter))
                buttonPressed = false;


        }
    }
}
