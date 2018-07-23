using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {

    class Animation {

        List<Rectangle> spriteSheetDestRectangle;
        int currentFrame = 0;
        bool looping = false;
        SpriteEffects spriteEffects;
        private float timeUntilNextFrame;

        public int currentFrameGetSetter{get{ return currentFrame; }}

        public Animation(bool loop,SpriteEffects spriteEffects, params Rectangle[] spriteSheetDestRectangle) {
            this.spriteSheetDestRectangle = new List<Rectangle>(spriteSheetDestRectangle);
            this.looping = loop;
            this.spriteEffects = spriteEffects;
        }

        
        public void Update(GameTime gameTime, ref Vector2 playerSpritePos) {
            float animationFrameTime = 1f / 25f;

            float gameFrameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeUntilNextFrame -= gameFrameTime;

            if (timeUntilNextFrame <= 0)
            {
                currentFrame++;
                if (currentFrame > 25) currentFrame = 1;
                timeUntilNextFrame += animationFrameTime;
            }
        }
    }



}
