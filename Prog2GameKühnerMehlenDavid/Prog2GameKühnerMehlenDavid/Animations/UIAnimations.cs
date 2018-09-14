using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Animations
{
    class UIAnimations
    {
        public uiAnimations currentAnimation = uiAnimations.None;
        public uiAnimations nextAnimation = uiAnimations.None;
        private uiAnimations previousAnimation = uiAnimations.None;

        public enum uiAnimations
        {
            Save,
            Reggie_Move_Left,
            Reggie_Move_Right,
            Reggie_Jump,
            None
        }

        Dictionary<string, Animation> divAnimationDestRectanglesDic;

        Animation save_Animation = null;
        Animation walk_Animation_Left = null;
        Animation walk_Animation_Right = null;
        Animation jump_Animation_Left = null;

        public UIAnimations(Dictionary<string, Texture2D> texturesDictionary, Dictionary<string, Texture2D> playerSpriteSheet)
        {
            divAnimationDestRectanglesDic = new Dictionary<string, Animation>();
            save_Animation = new Animation(false, SpriteEffects.None, 141, 141, texturesDictionary["SaveAnimationSpriteSheet"], 25f);
            walk_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Move_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Move_Y"] / 5, playerSpriteSheet["playerMoveSpriteSheet"], 25f);
            walk_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Move_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Move_Y"] / 5, playerSpriteSheet["playerMoveSpriteSheet"], 25f);
            jump_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally,54, 132, playerSpriteSheet["playerJumpSpriteSheet"], 25f);
        }

        public void Animation(GameTime gameTime, SpriteBatch spriteBatch, Matrix transformationMatrix)
        {
            if (currentAnimation == uiAnimations.None ||
                currentAnimation == uiAnimations.Reggie_Move_Left||
                currentAnimation == uiAnimations.Reggie_Move_Right ||
                currentAnimation == uiAnimations.Reggie_Jump)
            {
                currentAnimation = nextAnimation;
            }
            if (currentAnimation == uiAnimations.Save)
            {
                if (save_Animation.getPlayedOnce())
                {
                    if(nextAnimation == uiAnimations.Save)

                    nextAnimation = previousAnimation;
                    currentAnimation = nextAnimation;

                    save_Animation.resetPlayedOnce();
                }
            }

            Rectangle tempRec;

            switch (currentAnimation)
            {
                case uiAnimations.Save:
                    Console.WriteLine("Save Animation");
                    tempRec = divAnimationDestRectanglesDic["SaveAnimation"].Update(gameTime);
                    spriteBatch.Draw(save_Animation.texture, Vector2.Transform(new Vector2(1750, 0), Matrix.Invert(transformationMatrix)), tempRec, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    break;
                case uiAnimations.Reggie_Move_Right:
                    tempRec = walk_Animation_Right.Update(gameTime);
                    spriteBatch.Draw(walk_Animation_Right.texture, new Vector2(13340,1600), tempRec, Color.White, 0, Vector2.Zero, new Vector2(.6f,.6f), SpriteEffects.None, 0);
                    break;
                case uiAnimations.Reggie_Move_Left:
                    tempRec = walk_Animation_Left.Update(gameTime);
                    spriteBatch.Draw(walk_Animation_Left.texture, new Vector2(13070, 1600), tempRec, Color.White, 0, Vector2.Zero, new Vector2(.6f, .6f), SpriteEffects.FlipHorizontally, 0);
                    break;
                case uiAnimations.Reggie_Jump:
                    tempRec = jump_Animation_Left.Update(gameTime);
                    spriteBatch.Draw(jump_Animation_Left.texture, new Vector2(13230, 1510), tempRec, Color.White, 0, Vector2.Zero, new Vector2(.6f,.6f), SpriteEffects.FlipHorizontally, 0);
                    break;
            }
        }


    }

    

}
