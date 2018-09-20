using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Animations
{

    class NPC_Animations
    {
        public NPCAnimations currentAnimation = NPCAnimations.None;
        public NPCAnimations nextAnimation = NPCAnimations.None;
        private NPCAnimations previousAnimation = NPCAnimations.None;

        public enum NPCAnimations
        {
            IdleShopkeeper,
            WavingShopkeeper,
            None
        }

        Animation idleShopkeeper = null;
        Animation wavingShopkeeper = null;

        public NPC_Animations(Dictionary<string, Texture2D> texturesDictionary)
        {
            idleShopkeeper = new Animation(true, SpriteEffects.None, 334, 407, texturesDictionary["idleShop"], 25f);
            wavingShopkeeper = new Animation(false, SpriteEffects.None, 114, 407, texturesDictionary["wavingShop"], 25f);
        }

        public void Animation(GameTime gameTime, SpriteBatch spriteBatch, GameObject gameObject)
        {
            if(currentAnimation == NPCAnimations.IdleShopkeeper)
            {
                currentAnimation = nextAnimation;
            }
            if(currentAnimation == NPCAnimations.WavingShopkeeper)
            {
                if (wavingShopkeeper.getPlayedOnce())
                {
                    if (nextAnimation == NPCAnimations.WavingShopkeeper)
                        nextAnimation = previousAnimation;
                    currentAnimation = nextAnimation;

                    wavingShopkeeper.resetPlayedOnce();
                }
            }

            Rectangle tempRec;

            switch (currentAnimation)
            {
                case NPCAnimations.IdleShopkeeper:
                    tempRec = idleShopkeeper.Update(gameTime);
                    spriteBatch.Draw(idleShopkeeper.texture, gameObject.gameObjectPosition, tempRec,Color.White, 0,Vector2.Zero, new Vector2(1,1), SpriteEffects.None, 0);
                    break;
                case NPCAnimations.WavingShopkeeper:
                    tempRec = wavingShopkeeper.Update(gameTime);
                    spriteBatch.Draw(wavingShopkeeper.texture, gameObject.gameObjectPosition, tempRec, Color.White, 0, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0);
                    break;
            }

        }

    }
}
