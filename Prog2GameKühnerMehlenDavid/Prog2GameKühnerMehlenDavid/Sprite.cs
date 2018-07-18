using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog2GameKühnerMehlenDavid {
    public abstract class Sprite {
        protected Texture2D SpriteTexture;
        public Vector2 Position;
        public Vector2 Velocity;
        public Color color = Color.Black;
        public Rectangle SpriteSize;

        public Rectangle SpriteRectangle {
            get { return new Rectangle((int)Position.X, (int)Position.Y, SpriteSize.Width, SpriteSize.Height); }
        }

      
        public Sprite(Texture2D SpriteTexture, Rectangle _SpriteSize) {
            this.SpriteTexture = SpriteTexture;
            SpriteSize = _SpriteSize;
        }

        public virtual void Update(GameTime gameTime, List<Sprite> spriteList) { }

        public virtual void DrawSpriteBatch(SpriteBatch spriteBatch) {
            spriteBatch.Draw(SpriteTexture, Position, color);
        }

        public virtual void DrawSpriteBatch(SpriteBatch spriteBatch,Rectangle sourceRectangle) {

            spriteBatch.Draw(SpriteTexture, Position, sourceRectangle, Color.White);
            
        }

        

        #region SpriteCollision
        protected bool IsTouchingLeftSide(Sprite sprite) {
            return SpriteRectangle.Right + Velocity.X > sprite.SpriteRectangle.Left
                && SpriteRectangle.Left < sprite.SpriteRectangle.Left
                && SpriteRectangle.Bottom > sprite.SpriteRectangle.Top
                && SpriteRectangle.Top < sprite.SpriteRectangle.Bottom;
        }

        protected bool IsTouchingRightSide(Sprite sprite) {
            return SpriteRectangle.Left + Velocity.X < sprite.SpriteRectangle.Right
                && SpriteRectangle.Right > sprite.SpriteRectangle.Right
                && SpriteRectangle.Bottom > sprite.SpriteRectangle.Top
                && SpriteRectangle.Top < sprite.SpriteRectangle.Bottom;
        }

        protected bool IsTouchingTopSide(Sprite sprite, Vector2 Gravity) {
            return SpriteRectangle.Bottom + Velocity.Y + Gravity.Y > sprite.SpriteRectangle.Top
                && SpriteRectangle.Top < sprite.SpriteRectangle.Top
                && SpriteRectangle.Right > sprite.SpriteRectangle.Left
                && SpriteRectangle.Left < sprite.SpriteRectangle.Right;
        }

        protected bool IsTouchingBottomSide(Sprite sprite) {
            return SpriteRectangle.Top + Velocity.Y < sprite.SpriteRectangle.Bottom
                && SpriteRectangle.Bottom > sprite.SpriteRectangle.Bottom
                && SpriteRectangle.Right > sprite.SpriteRectangle.Left
                && SpriteRectangle.Left < sprite.SpriteRectangle.Right;
        }
        #endregion
    }
}
