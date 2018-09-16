using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Reggie
{
    public class ShopKeeper : GameObject
    {
        public ShopKeeper(Texture2D gameObjectTexture, Vector2 gameObejctSize, Vector2 position, int gameObjectID) : base(gameObjectTexture, gameObejctSize, position, gameObjectID)
        {
        }
    }
}
