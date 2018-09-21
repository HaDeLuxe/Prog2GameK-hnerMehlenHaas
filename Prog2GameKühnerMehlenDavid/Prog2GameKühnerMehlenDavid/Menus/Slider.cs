using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Reggie.Menus
{
    class Slider : GameObject
    {

        Texture2D sliderKnobTexture = null;
        Texture2D sliderBarTexture = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameObjectTexture"></param>
        /// <param name="gameObejctSize"></param>
        /// <param name="position"></param>
        /// <param name="gameObjectID"></param>
        /// <param name=""></param>
        public Slider(Texture2D gameObjectTexture, Vector2 gameObejctSize, Vector2 position, int gameObjectID, Texture2D sliderKnobTexture ) : base(gameObjectTexture, gameObejctSize, position, gameObjectID)
        {
            this.sliderBarTexture = gameObjectTexture;
            this.sliderKnobTexture = sliderKnobTexture;
        }

        //public drawSlider()
        //{

        //}
    }
}
