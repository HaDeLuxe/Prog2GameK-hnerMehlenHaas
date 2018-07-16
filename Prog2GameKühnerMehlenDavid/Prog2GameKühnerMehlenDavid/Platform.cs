using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog2GameKühnerMehlenDavid {
    public class Platform : Sprite {
        public Platform(Texture2D SpriteTexture, Rectangle SpriteSize) : base(SpriteTexture,SpriteSize) { }
        public override void Update(GameTime gameTime, List<Sprite> spriteList) { }
    }
}
