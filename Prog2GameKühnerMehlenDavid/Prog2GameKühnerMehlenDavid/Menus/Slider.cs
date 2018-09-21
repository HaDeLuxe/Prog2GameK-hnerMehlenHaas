using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Reggie.Menus
{
    class Slider
    {
        
        //position between 0-100
        int state = 200;

        public Slider(int state)
        {
            this.state = state;
        }


        /// <summary>
        /// Draws the Slider
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="position"></param>
        public void drawSlider(SpriteBatch spriteBatch, Vector2 position, Dictionary<string, Texture2D> texturesDictionary)
        {
            spriteBatch.Draw(texturesDictionary["sliderbar"], position, Color.White);
            spriteBatch.Draw(texturesDictionary["sliderknob"], position + new Vector2(state, 0), Color.White);
        }
    }
}
