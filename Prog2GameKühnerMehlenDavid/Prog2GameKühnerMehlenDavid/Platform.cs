using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog2GameKühnerMehlenDavid {
    public class Platform : GameObject {
        public Platform(Texture2D SpriteTexture, Vector2 SpriteSize) : base(SpriteTexture,SpriteSize) { }
        public override void Update(GameTime gameTime, List<GameObject> spriteList) { }
    }
}
