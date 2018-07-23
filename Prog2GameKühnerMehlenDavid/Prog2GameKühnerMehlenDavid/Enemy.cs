using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    public class Enemy : GameObject
    {
        public Rectangle EnemyAggroArea;
        public Vector4 EnemyAggroAreaSize;
        public Player Worm;
        
        public Enemy(Texture2D SpriteTexture, Vector2 SpriteSize) : base(SpriteTexture, SpriteSize)
        {
            GravityActive = true;
            MovementSpeed = 2f;
            Position = new Vector2(1500, 200);
            ChangeCollisionBox = new Vector2(0, 0);
            EnemyAggroAreaSize = new Vector4(100, 100, 150, 150);
            CollisionBoxPosition = new Vector2(Position.X + ChangeCollisionBox.X, Position.Y + ChangeCollisionBox.Y);
            EnemyAggroArea = new Rectangle((int)(Position.X - EnemyAggroAreaSize.X), (int)(Position.Y - EnemyAggroAreaSize.Y), (int)(Position.X + EnemyAggroAreaSize.Z), (int)(Position.Y+ EnemyAggroAreaSize.W));
            CollisionBoxSize = new Vector2(50, 50);
        }
        public override void Update(GameTime gameTime, List<GameObject> spriteList) {
            ResizeEnemyAggroArea(spriteList);
            EnemyCollision(gameTime, spriteList);
            if (DetectPlayer())
                EnemyMovement();
        }

        private void ResizeEnemyAggroArea(List<GameObject> spriteList)
        {
            EnemyAggroAreaSize = new Vector4(100, 100, 150, 150);
            foreach(var sprite in spriteList)
            {
                if (EnemyAggroArea.Right + Velocity.X > sprite.SpriteRectangle.Left &&
              EnemyAggroArea.Left < sprite.SpriteRectangle.Left &&
              EnemyAggroArea.Bottom > sprite.SpriteRectangle.Top &&
              EnemyAggroArea.Top < sprite.SpriteRectangle.Bottom)
                    EnemyAggroAreaSize.Z = sprite.SpriteRectangle.Left - Position.X;
                else if (EnemyAggroArea.Left + Velocity.X < sprite.SpriteRectangle.Right &&
                  EnemyAggroArea.Right > sprite.SpriteRectangle.Right &&
                  EnemyAggroArea.Bottom > sprite.SpriteRectangle.Top &&
                  EnemyAggroArea.Top < sprite.SpriteRectangle.Bottom)
                    EnemyAggroAreaSize.X = Position.X - sprite.SpriteRectangle.Right;
                else if (EnemyAggroArea.Bottom + Velocity.Y + Gravity.Y > sprite.SpriteRectangle.Top &&
                EnemyAggroArea.Top < sprite.SpriteRectangle.Top &&
                EnemyAggroArea.Right > sprite.SpriteRectangle.Left &&
                EnemyAggroArea.Left < sprite.SpriteRectangle.Right)
                    EnemyAggroAreaSize.W = sprite.SpriteRectangle.Top - Position.Y;
                else if (EnemyAggroArea.Top + Velocity.Y < sprite.SpriteRectangle.Bottom &&
               EnemyAggroArea.Bottom > sprite.SpriteRectangle.Bottom &&
               EnemyAggroArea.Right > sprite.SpriteRectangle.Left &&
               EnemyAggroArea.Left < sprite.SpriteRectangle.Right)
                    EnemyAggroAreaSize.Y = Position.Y - sprite.SpriteRectangle.Bottom;
            }
        }

        public void setPlayer(Player WormPlayer)
        {
            Worm = WormPlayer;
        }

        private void EnemyCollision(GameTime gameTime, List<GameObject> spriteList)
        {
            foreach (var sprite in spriteList)
            {
                if (Velocity.X > 0 && IsTouchingLeftSide(sprite) ||
                   (Velocity.X < 0 && IsTouchingRightSide(sprite)))
                    Velocity.X = 0;
                else if (IsTouchingBottomSide(sprite))
                {
                    Velocity.Y = 0;
                    GravityActive = true;
                    CollisionBoxPosition.Y = sprite.Position.Y + sprite.SpriteRectangle.Height;
                    Position.Y = CollisionBoxPosition.Y - ChangeCollisionBox.Y;
                }
                else if (IsTouchingTopSide(sprite, Gravity))
                {
                    Velocity.Y = 0;
                    GravityActive = false;
                    Gravity = Vector2.Zero;
                    CollisionBoxPosition.Y = sprite.Position.Y - CollisionBoxSize.Y;
                    Position.Y = CollisionBoxPosition.Y - ChangeCollisionBox.Y;
                }

                if (!IsTouchingTopSide(sprite, Gravity))
                {
                    GravityActive = true;
                    //if (AirDirectionRight)
                    //    Velocity.X = MovementSpeed;
                    //else if (AirDirectionLeft)
                    //    Velocity.X = -MovementSpeed;
                    if (IsTouchingLeftSide(sprite) || IsTouchingRightSide(sprite))
                        Velocity.X = 0;
                    //if (PreviousState.IsKeyDown(Keys.Space) && JumpCounter < 3)
                    //{
                    //    JumpSpeed = -10f;
                    //    if (JumpCounter != PreviousJumpCounter)
                    //        Gravity = Vector2.Zero;
                    //    if (JumpCounter == 1)
                    //        FirstJump = true;
                    //    else if (JumpCounter == 2)
                    //        SecondJump = true;
                    //}
                    //PreviousJumpCounter = JumpCounter;
                }
                if (GravityActive)
                {
                    //if (!PreviousState.IsKeyDown(Keys.Space))
                    Gravity.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * 15;
                    //if (Gravity.Y > -JumpSpeed && PreviousState.IsKeyDown(Keys.Space))
                    //    Gravity.Y = 12f;
                    //else
                    //    Gravity.Y = 5;
                    CollisionBoxPosition += Gravity;
                }
                else
                    Gravity = Vector2.Zero;
                CollisionBoxPosition += Velocity;
                Position = CollisionBoxPosition - ChangeCollisionBox;
                Velocity = Vector2.Zero;
            }
        }

        private void EnemyMovement()
        {
          
        }

        private bool DetectPlayer()
        {
            if (EnemyAggroArea.Right + Velocity.X > Worm.CollisionRectangle.Left &&
                EnemyAggroArea.Left < Worm.CollisionRectangle.Left &&
                EnemyAggroArea.Bottom > Worm.CollisionRectangle.Top &&
                EnemyAggroArea.Top < Worm.CollisionRectangle.Bottom)
                return true;
            else if (EnemyAggroArea.Left + Velocity.X < Worm.CollisionRectangle.Right &&
              EnemyAggroArea.Right > Worm.CollisionRectangle.Right &&
              EnemyAggroArea.Bottom > Worm.CollisionRectangle.Top &&
              EnemyAggroArea.Top < Worm.CollisionRectangle.Bottom)
                return true;
            else if (EnemyAggroArea.Bottom + Velocity.Y + Gravity.Y > Worm.CollisionRectangle.Top &&
            EnemyAggroArea.Top < Worm.CollisionRectangle.Top &&
            EnemyAggroArea.Right > Worm.CollisionRectangle.Left &&
            EnemyAggroArea.Left < Worm.CollisionRectangle.Right)
                return true;
            else if (EnemyAggroArea.Top + Velocity.Y < Worm.CollisionRectangle.Bottom &&
           EnemyAggroArea.Bottom > Worm.CollisionRectangle.Bottom &&
           EnemyAggroArea.Right > Worm.CollisionRectangle.Left &&
           EnemyAggroArea.Left < Worm.CollisionRectangle.Right)
                return true;
            else
                return false;
        }
    }
}
