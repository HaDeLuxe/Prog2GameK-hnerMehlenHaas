using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
   public class AnimationManager {
       
        public enum Animations{Walk_Left, Walk_Right, Jump_Left, Jump_Right, Attack_Left, Attack_Right};


        public static Animations currentAnimation = Animations.Walk_Right;
        public static Animations nextAnimation = Animations.Walk_Left;

        private Animations PreviousAnimation = Animations.Walk_Left;
        

        List<Texture2D> playerSpriteSheet = new List<Texture2D>();

        //DIVerse ANIMATION DESTination RECTANGLES DICtionnary
        Dictionary<string, Animation> divAnimationDestRectanglesDic;

        Animation Walk_Animation_Left = null;
        Animation Walk_Animation_Right = null;
        Animation Jump_Animation_Left = null;
        Animation Jump_Animation_Right = null;
        Animation Attack_Animation_Left = null;
        Animation Attack_Animation_Right = null;
        

        public AnimationManager(Dictionary<string, Texture2D> PlayerSpriteSheet) 
        {
            divAnimationDestRectanglesDic = new Dictionary<string, Animation>();
            Walk_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.SpritesSizes["Reggie_Move_X"]/5, SpriteSheetSizes.SpritesSizes["Reggie_Move_Y"]/5,PlayerSpriteSheet["playerMoveSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Animation_Left",Walk_Animation_Left);
            Walk_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.SpritesSizes["Reggie_Move_X"] / 5, SpriteSheetSizes.SpritesSizes["Reggie_Move_Y"] / 5, PlayerSpriteSheet["playerMoveSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Animation_Right", Walk_Animation_Right);
            Jump_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.SpritesSizes["Reggie_Jump_X"] / 5, SpriteSheetSizes.SpritesSizes["Reggie_Jump_Y"] / 5, PlayerSpriteSheet["playerJumpSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Animation_Left", Jump_Animation_Left);
            Jump_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.SpritesSizes["Reggie_Jump_X"] / 5, SpriteSheetSizes.SpritesSizes["Reggie_Jump_Y"] / 5, PlayerSpriteSheet["playerJumpSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Animation_Right", Jump_Animation_Right);
            Attack_Animation_Left = new Animation(false, SpriteEffects.FlipHorizontally, SpriteSheetSizes.SpritesSizes["Reggie_Attack_X"] / 5, SpriteSheetSizes.SpritesSizes["Reggie_Attack_Y"] / 5, PlayerSpriteSheet["playerAttackSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Animation_Left", Attack_Animation_Left);
            Attack_Animation_Right = new Animation(false, SpriteEffects.None, SpriteSheetSizes.SpritesSizes["Reggie_Attack_X"] / 5, SpriteSheetSizes.SpritesSizes["Reggie_Attack_Y"] / 5, PlayerSpriteSheet["playerAttackSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Animation_Right", Attack_Animation_Right);
            

        }

        

        public void animation(GameTime gameTime, ref Player player, SpriteBatch spriteBatch) 
        {
            

            if (currentAnimation == Animations.Jump_Left
                                           || currentAnimation == Animations.Jump_Right
                                           || currentAnimation == Animations.Walk_Left
                                           || currentAnimation == Animations.Walk_Right)
            {
                currentAnimation = nextAnimation;
            }
            if(currentAnimation == Animations.Attack_Left || currentAnimation == Animations.Attack_Right)
            {
                if(Attack_Animation_Right.getPlayedOnce() || Attack_Animation_Left.getPlayedOnce())
                {
                    currentAnimation = nextAnimation;
                    Attack_Animation_Left.resetPlayedOnce();
                    Attack_Animation_Right.resetPlayedOnce();
                }
            }


                Rectangle tempRec;
            switch (currentAnimation)
            {
                case Animations.Walk_Right:
                    player.changeTexture(Walk_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Walk_Animation_Right.getSpriteEffects(),new Vector2(0,0));
                    PreviousAnimation = Animations.Walk_Right;
                    break;
                case Animations.Walk_Left:
                    player.changeTexture(Walk_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Walk_Animation_Left.getSpriteEffects(),new Vector2(0,0));
                    PreviousAnimation = Animations.Walk_Left;
                    break;
                case Animations.Jump_Right:
                    player.changeTexture(Jump_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Jump_Animation_Right.getSpriteEffects(), new Vector2(65,-20));
                    PreviousAnimation = Animations.Jump_Right;
                    break;
                case Animations.Jump_Left:
                    player.changeTexture(Jump_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Jump_Animation_Left.getSpriteEffects(), new Vector2(15, -20));
                    PreviousAnimation = Animations.Jump_Left;
                    break;
                case Animations.Attack_Left:
                    player.changeTexture(Attack_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Attack_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Attack_Animation_Left.getSpriteEffects(), new Vector2(-25, -25));
                    break;
                case Animations.Attack_Right:
                    player.changeTexture(Attack_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Attack_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Attack_Animation_Right.getSpriteEffects(), new Vector2(32, -25));
                    break;
            }
        }

       
        
    }
}
