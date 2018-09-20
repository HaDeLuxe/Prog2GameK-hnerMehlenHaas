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
        NPC_Animations nPC_Animations;

        public ShopKeeper(Texture2D gameObjectTexture, Vector2 gameObejctSize, Vector2 position, int gameObjectID, Dictionary<string, Texture2D> texturesDictionary) : base(gameObjectTexture, gameObejctSize, position, gameObjectID)
        {
            nPC_Animations = new NPC_Animations(texturesDictionary);
            nPC_Animations.currentAnimation = NPC_Animations.NPCAnimations.IdleShopkeeper;
            nPC_Animations.nextAnimation = NPC_Animations.NPCAnimations.IdleShopkeeper;
        }

        public void drawShopKeeper(SpriteBatch spriteBatch, GameTime gameTime)
        {
            nPC_Animations.Animation(gameTime, spriteBatch, this);
        }
    }
}
