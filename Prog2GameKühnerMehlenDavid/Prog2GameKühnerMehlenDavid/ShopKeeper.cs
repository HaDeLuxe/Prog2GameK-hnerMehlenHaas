using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Reggie.Animations;

namespace Reggie
{
    public class ShopKeeper : GameObject
    {
        UIAnimations uIAnimations;

        public ShopKeeper(Texture2D gameObjectTexture, Vector2 gameObejctSize, Vector2 position, int gameObjectID, Dictionary<string, Texture2D> texturesDictionary) : base(gameObjectTexture, gameObejctSize, position, gameObjectID)
        {
            //uIAnimations = new UIAnimations()
        }

        public void drawShopKeeper(SpriteBatch spriteBatch, GameTime gameTime, Dictionary<string, Texture2D>texturesDictionary)
        {

        }
    }
}
