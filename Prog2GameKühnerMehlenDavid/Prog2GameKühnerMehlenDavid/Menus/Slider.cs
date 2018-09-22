using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Reggie.Menus
{
    class Slider
    {
        
        //position between 0-100
        int state = 0;
        Vector2 position = Vector2.Zero;
        Vector2 mousePosition;

        public Slider(int state, Vector2 position)
        {
            this.state = state;
            this.position = position;
        }


        /// <summary>
        /// Draws the Slider
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="position"></param>
        public void drawSlider(SpriteBatch spriteBatch, Dictionary<string, Texture2D> texturesDictionary, Matrix transformationMatrix)
        {
            spriteBatch.Draw(texturesDictionary["sliderbar"], position, Color.White);
            spriteBatch.Draw(texturesDictionary["sliderknob"], position + new Vector2(state, -5), Color.White);
        }

        public void moveSlider()
        {
            MouseState mouseState = Mouse.GetState();
            mousePosition.X = mouseState.X - position.X;
            mousePosition.Y = mouseState.Y;

            if (ButtonState.Pressed == mouseState.LeftButton && mousePosition.Y > position.Y-50 && mousePosition.Y < position.Y +50)
            {
                state = (int)mousePosition.X;
            }
            if (state > 370) state = 370;
            if (state < 0) state = 0;
        }

        public float getCurrentState()
        {
            float temp = state / 370f;
            return temp;
        }

        public void changeCurrentState(int change)
        {
            state += change;
            if (state > 370) state = 370;
            if (state < 0) state = 0;
        }

        
    }
}
