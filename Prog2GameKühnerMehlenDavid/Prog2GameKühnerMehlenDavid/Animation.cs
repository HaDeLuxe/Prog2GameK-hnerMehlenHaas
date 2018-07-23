using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog2GameKühnerMehlenDavid {

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

        


        public void Update(GameTime gameTime) {
            float animationFrameTime = 1f / 25f;

            float gameFrameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeUntilNextFrame -= gameFrameTime;
        }
    }



}
