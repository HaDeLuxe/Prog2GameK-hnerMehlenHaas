using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie 
{
    public class Item : GameObject {

        public Item(Texture2D gameObjectTexture, Vector2 gameObjectSize, Vector2 gameObjectPosition, int gameObjectID) : base(gameObjectTexture, gameObjectSize, gameObjectPosition, gameObjectID) {

        }

    }
}
