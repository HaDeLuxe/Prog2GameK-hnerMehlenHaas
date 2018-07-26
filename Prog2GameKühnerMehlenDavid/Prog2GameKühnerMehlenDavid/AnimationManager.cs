using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
   public class AnimationManager {
       

        private float timeUntilNextFrame;
        private int frameIndex;

        Dictionary<string, Animation> divAnimationDestRectanglesDic;


        public AnimationManager(int x, int y) 
        {
            divAnimationDestRectanglesDic = new Dictionary<string, Animation>();
            timeUntilNextFrame = 0f;
            frameIndex = 1;
            Animation Walk_Animation_Left = new Animation(true, SpriteEffects.None, SpriteSheetSizes.SpritesSizes["Reggie_Move_X"]/5, SpriteSheetSizes.SpritesSizes["Reggie_Move_Y"]/5);
            divAnimationDestRectanglesDic.Add("Walk_Animation_Left",Walk_Animation_Left);
            
            //divAnimationDestRectanglesDic.Add("Walk_Animation_Right", new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.SpritesSizes["Reggie_Move_X"], SpriteSheetSizes.SpritesSizes["Reggie_Move_Y"]));
        }


        public void animation(GameTime gameTime, ref Player player, SpriteBatch spriteBatch) 
        {
            Rectangle tempRec = divAnimationDestRectanglesDic["Walk_Animation_Left"].Update(gameTime);
            player.DrawSpriteBatch(spriteBatch, tempRec);
        }

        //public void animation( GameTime gameTime, ref Vector2 playerSpritePos) 
        //{
        //    float animationFrameTime = 1f / 25f;

        //    float gameFrameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        //    timeUntilNextFrame -= gameFrameTime;
        //    if (timeUntilNextFrame <= 0)
        //    {
        //        frameIndex++;
        //        if (frameIndex > 25) frameIndex = 1;
        //        timeUntilNextFrame += animationFrameTime;
        //    }

        //    if (frameIndex >= 1 && frameIndex <= 5)
        //    {
        //        playerSpritePos.Y = 0;
        //        playerSpritePos.X = frameIndex-1;
        //    }
        //    else if (frameIndex > 5 && frameIndex <= 10)
        //    {
        //        playerSpritePos.Y = 1;
        //        playerSpritePos.X = frameIndex - 6;
        //    }
        //    else if (frameIndex > 10 && frameIndex <= 15)
        //    {
        //        playerSpritePos.Y = 2;
        //        playerSpritePos.X = frameIndex - 11;
        //    }
        //    else if (frameIndex > 15 && frameIndex <= 20)
        //    {
        //        playerSpritePos.Y = 3;
        //        playerSpritePos.X = frameIndex - 16;
        //    }
        //    else if (frameIndex > 20 && frameIndex <= 25)
        //    {
        //        playerSpritePos.Y = 4;
        //        playerSpritePos.X = frameIndex - 21;
        //    }
        //    //Console.WriteLine(frameIndex);
        //} //Returns x and y data, to which part of the PlayerMoveAnimSheet to show
        
    }
}
