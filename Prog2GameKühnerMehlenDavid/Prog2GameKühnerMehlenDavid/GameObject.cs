using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {

    public abstract class GameObject {

        protected Texture2D SpriteTexture;
        protected bool AirDirectionLeft;
        protected bool AirDirectionRight;
        protected bool GravityActive;
        protected int FacingDirection;
        public float MovementSpeed;
        public Vector2 Gravity;
        public Vector2 Position;
        public Vector2 CollisionBoxSize;
        public Vector2 CollisionBoxPosition;
        public Vector2 ChangeCollisionBox;
        public Vector2 SpriteSize;
        public Vector2 Velocity;
        public Color color = Color.Black;
        public Rectangle CollisionRectangle
        {
        get { return new Rectangle((int)CollisionBoxPosition.X, (int)CollisionBoxPosition.Y, (int)CollisionBoxSize.X, (int)CollisionBoxSize.Y); }
        }

        public Rectangle SpriteRectangle {
            get { return new Rectangle((int)Position.X, (int)Position.Y,(int)SpriteSize.X, (int)SpriteSize.Y); }
        }

        public GameObject(Texture2D SpriteTexture, Vector2 _SpriteSize) {
            this.SpriteTexture = SpriteTexture;
            SpriteSize = _SpriteSize;  
        }

        public virtual void Update(GameTime gameTime, List<GameObject> spriteList) { }

        public virtual void DrawSpriteBatch(SpriteBatch spriteBatch) {
            spriteBatch.Draw(SpriteTexture, Position, color);
        }

        public virtual void DrawSpriteBatch(SpriteBatch spriteBatch,Rectangle sourceRectangle) {
            if (FacingDirection == -1)
            {
                spriteBatch.Draw(SpriteTexture, Position, sourceRectangle, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.FlipHorizontally, 0);
            }
            else if(FacingDirection == 1)
                spriteBatch.Draw(SpriteTexture, Position, sourceRectangle, Color.White);
            //spriteBatch.Draw(SpriteTexture, Position, color);
            //Console.WriteLine("Player was drawn!");
        }

        public virtual void DrawAnimation(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle sourceRectangle, Color color) {
            spriteBatch.Draw(texture, position, sourceRectangle, color);
        }

        // SpriteCollision
        protected bool IsTouchingLeftSide(GameObject sprite) {
            return CollisionRectangle.Right + Velocity.X > sprite.SpriteRectangle.Left
                && CollisionRectangle.Left < sprite.SpriteRectangle.Left
                && CollisionRectangle.Bottom > sprite.SpriteRectangle.Top
                && CollisionRectangle.Top < sprite.SpriteRectangle.Bottom;
        }

        protected bool IsTouchingRightSide(GameObject sprite) {
            return CollisionRectangle.Left + Velocity.X < sprite.SpriteRectangle.Right
                && CollisionRectangle.Right > sprite.SpriteRectangle.Right
                && CollisionRectangle.Bottom > sprite.SpriteRectangle.Top
                && CollisionRectangle.Top < sprite.SpriteRectangle.Bottom;
        }

        protected bool IsTouchingTopSide(GameObject sprite, Vector2 Gravity) {
            return CollisionRectangle.Bottom + Velocity.Y + Gravity.Y > sprite.SpriteRectangle.Top
                && CollisionRectangle.Top < sprite.SpriteRectangle.Top
                && CollisionRectangle.Right > sprite.SpriteRectangle.Left
                && CollisionRectangle.Left < sprite.SpriteRectangle.Right;
        }

        protected bool IsTouchingBottomSide(GameObject sprite) {
            return CollisionRectangle.Top + Velocity.Y < sprite.SpriteRectangle.Bottom
                && CollisionRectangle.Bottom > sprite.SpriteRectangle.Bottom
                && CollisionRectangle.Right > sprite.SpriteRectangle.Left
                && CollisionRectangle.Left < sprite.SpriteRectangle.Right;
        }
        
    }
}
