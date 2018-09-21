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
        public Slider(Texture2D gameObjectTexture, Vector2 gameObejctSize, Vector2 position, int gameObjectID) : base(gameObjectTexture, gameObejctSize, position, gameObjectID)
        {
        }
    }
}
