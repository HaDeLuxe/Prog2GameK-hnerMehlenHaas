using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
   public class AnimationManager {
       
        public enum Animations{Walk_Left, Walk_Right, Jump_Left, Jump_Right};


        public static Animations currentAnimation = Animations.Walk_Right;

        List<Texture2D> playerSpriteSheet = new List<Texture2D>();

        //DIVerse ANIMATION DESTination RECTANGLES DICtionnary
        Dictionary<string, Animation> divAnimationDestRectanglesDic;

        Animation Walk_Animation_Left = null;
        Animation Walk_Animation_Right = null;
        Animation Jump_Animation_Left = null;
        Animation Jump_Animation_Right = null;

        public AnimationManager(Dictionary<string, Texture2D> PlayerSpriteSheet) 
        {
            divAnimationDestRectanglesDic = new Dictionary<string, Animation>();
            Walk_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.SpritesSizes["Reggie_Move_X"]/5, SpriteSheetSizes.SpritesSizes["Reggie_Move_Y"]/5,PlayerSpriteSheet["playerMoveSpriteSheet"]);
            divAnimationDestRectanglesDic.Add("Walk_Animation_Left",Walk_Animation_Left);
            Walk_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.SpritesSizes["Reggie_Move_X"] / 5, SpriteSheetSizes.SpritesSizes["Reggie_Move_Y"] / 5, PlayerSpriteSheet["playerMoveSpriteSheet"]);
            divAnimationDestRectanglesDic.Add("Walk_Animation_Right", Walk_Animation_Right);
            Jump_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.SpritesSizes["Reggie_Jump_X"] / 5, SpriteSheetSizes.SpritesSizes["Reggie_Jump_Y"] / 5, PlayerSpriteSheet["playerJumpSpriteSheet"]);
            divAnimationDestRectanglesDic.Add("Jump_Animation_Left", Jump_Animation_Left);
            Jump_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.SpritesSizes["Reggie_Jump_X"] / 5, SpriteSheetSizes.SpritesSizes["Reggie_Jump_Y"] / 5, PlayerSpriteSheet["playerJumpSpriteSheet"]);
            divAnimationDestRectanglesDic.Add("Jump_Animation_Right", Jump_Animation_Right);
            
        }


        public void animation(GameTime gameTime, ref Player player, SpriteBatch spriteBatch) 
        {
            Rectangle tempRec;
            switch (currentAnimation)
            {
                case Animations.Walk_Right:
                    player.changeTexture(Walk_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Walk_Animation_Right.getSpriteEffects());
                    break;
                case Animations.Walk_Left:
                    player.changeTexture(Walk_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Walk_Animation_Left.getSpriteEffects());
                    break;
                case Animations.Jump_Right:
                    player.changeTexture(Jump_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Jump_Animation_Right.getSpriteEffects());
                    break;
                case Animations.Jump_Left:
                    player.changeTexture(Jump_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Jump_Animation_Left.getSpriteEffects());
                    break;
            }
            //Rectangle tempRec = divAnimationDestRectanglesDic["Walk_Animation_Right"].Update(gameTime);
            //player.changeTexture()
            //player.DrawSpriteBatch(spriteBatch, tempRec);
        }

       
        
    }
}
