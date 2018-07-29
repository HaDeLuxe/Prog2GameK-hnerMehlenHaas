using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    public class Platform : GameObject {
        public Platform(Texture2D SpriteTexture, Vector2 SpriteSize, Vector2 Position) : base(SpriteTexture,SpriteSize, Position) { }
        public override void Update(GameTime gameTime, List<GameObject> spriteList) { }
    }
}
