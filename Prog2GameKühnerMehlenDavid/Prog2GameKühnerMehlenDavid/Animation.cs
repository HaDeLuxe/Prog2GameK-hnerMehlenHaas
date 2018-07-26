using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {

    class Animation {

        List<Rectangle> spriteSheetDestRectangle = new List<Rectangle>();
        
        int currentFrame = 0;
        bool looping = false;
        SpriteEffects spriteEffects;
        private float timeUntilNextFrame;



        //public void AddRectanglesToList(int singleSpriteXSize, int singleSpriteYSize) {
        //    for (int i = 0; i < 5; i++)
        //    {
        //        for (int m = 0; m < 5; m++)
        //        {
        //            Rectangle tempRec = new Rectangle(singleSpriteXSize * m, singleSpriteYSize * i, singleSpriteXSize, singleSpriteYSize);
        //        }
        //    }
        //}

        public int currentFrameGetSetter{get{ return currentFrame; }}

        public Animation(bool loop,SpriteEffects spriteEffects, int singleSpriteXSize, int singleSpriteYSize) {
           // this.spriteSheetDestRectangle = new List<Rectangle>(spriteSheetDestRectangle);
            this.looping = loop;
            this.spriteEffects = spriteEffects;

            for (int i = 0; i < 5; i++)
            {
                for (int m = 0; m < 5; m++)
                {
                    Rectangle tempRec = new Rectangle(singleSpriteXSize * m, singleSpriteYSize * i, singleSpriteXSize, singleSpriteYSize);
                    spriteSheetDestRectangle.Add(tempRec);
                }
            }
        }

        
        public Rectangle Update(GameTime gameTime) {
            float animationFrameTime = 1f / 25f;

            float gameFrameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeUntilNextFrame -= gameFrameTime;

            if (timeUntilNextFrame <= 0)
            {
                currentFrame++;
                if (currentFrame >= 25) currentFrame = 0;
                timeUntilNextFrame += animationFrameTime;
            }
            return spriteSheetDestRectangle[currentFrame];

        }
    }



}
