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
        bool playedOnce = false;
        float speed = 25;
        SpriteEffects spriteEffects;
        private float timeUntilNextFrame;
        public Texture2D texture = null;

        
        public int currentFrameGetSetter{get{ return currentFrame; }}

        public SpriteEffects getSpriteEffects() 
        {
            return spriteEffects;
        }

        public Animation(bool loop,SpriteEffects spriteEffects, int singleSpriteXSize, int singleSpriteYSize, Texture2D texture, float Speed) {
           // this.spriteSheetDestRectangle = new List<Rectangle>(spriteSheetDestRectangle);
            this.looping = loop;
            this.spriteEffects = spriteEffects;
            this.texture = texture;
            this.speed = Speed;
            for (int i = 0; i < 5; i++)
            {
                for (int m = 0; m < 5; m++)
                {
                    Rectangle tempRec = new Rectangle(singleSpriteXSize * m, singleSpriteYSize * i, singleSpriteXSize, singleSpriteYSize);
                    spriteSheetDestRectangle.Add(tempRec);
                }
            }
        }

        public bool getPlayedOnce() { return playedOnce; }
        public void resetPlayedOnce(){ playedOnce = false; }
        
        public Rectangle Update(GameTime gameTime) {
            float animationFrameTime = 1f / speed;

            float gameFrameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeUntilNextFrame -= gameFrameTime;

            if (timeUntilNextFrame <= 0)
            {
                currentFrame++;
                if (currentFrame >= 25){
                    currentFrame = 0;
                    playedOnce = true;
                }
                timeUntilNextFrame += animationFrameTime;
            }
            return spriteSheetDestRectangle[currentFrame];

        }
    }



}
