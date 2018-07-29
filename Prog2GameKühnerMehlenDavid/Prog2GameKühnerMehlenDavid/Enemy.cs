﻿using Microsoft.Xna.Framework;
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
        public int EnemyHP;
        bool StillAlive;
        bool KnockedBack;
        float KnockBackValue;
        float FallCooldown;
        public bool FallOutOfMap;
        
        public Enemy(Texture2D SpriteTexture, Vector2 SpriteSize, Vector2 Position) : base(SpriteTexture, SpriteSize, Position)
        {
            GravityActive = true;
            IsStanding = false;
            StillAlive = true;
            KnockedBack = false;
            FallOutOfMap = false;
            EnemyHP = 3;
            MovementSpeed = 5f;
            KnockBackValue = 20f; 
            //Position = new Vector2(900, 200);
            ChangeCollisionBox = new Vector2(0, 0);
            EnemyAggroAreaSize = new Vector4(400, 300, 650, 850);
            CollisionBoxPosition = new Vector2(Position.X + ChangeCollisionBox.X, Position.Y + ChangeCollisionBox.Y);
            EnemyAggroArea = new Rectangle((int)(Position.X - EnemyAggroAreaSize.X), (int)(Position.Y - EnemyAggroAreaSize.Y), (int)(EnemyAggroAreaSize.W), (int)(EnemyAggroAreaSize.Z));
            CollisionBoxSize = new Vector2(50, 50);
        }
        //public Rectangle EnemyAggroArea
        //{
        //    get { return new Rectangle((int)(Position.X - EnemyAggroAreaSize.X), (int)(Position.Y - EnemyAggroAreaSize.Y), (int)(Position.X + EnemyAggroAreaSize.Z), (int)(Position.Y + EnemyAggroAreaSize.W)); }
        //}
        public override void Update(GameTime gameTime, List<GameObject> spriteList) {
            ResizeEnemyAggroArea(spriteList);
            EnemyCollision(gameTime, spriteList);
            if (DetectPlayer() && !KnockedBack)
                EnemyMovement();
        }

        private void ResizeEnemyAggroArea(List<GameObject> spriteList)
        {
           // EnemyAggroAreaSize = new Vector4(100, 100, 150, 150);
            //foreach(var sprite in spriteList)
            //{
            //    if (EnemyAggroArea.Right + Velocity.X > sprite.SpriteRectangle.Left &&
            //  EnemyAggroArea.Left < sprite.SpriteRectangle.Left &&
            //  EnemyAggroArea.Bottom > sprite.SpriteRectangle.Top &&
            //  EnemyAggroArea.Top < sprite.SpriteRectangle.Bottom)
            //        EnemyAggroAreaSize.Z -= EnemyAggroArea.Right + Velocity.X - Math.Abs(sprite.SpriteRectangle.Left);
            //    else if (EnemyAggroArea.Left + Velocity.X < sprite.SpriteRectangle.Right &&
            //      EnemyAggroArea.Right > sprite.SpriteRectangle.Right &&
            //      EnemyAggroArea.Bottom > sprite.SpriteRectangle.Top &&
            //      EnemyAggroArea.Top < sprite.SpriteRectangle.Bottom)
            //        EnemyAggroAreaSize.X = Position.X - sprite.SpriteRectangle.Right;
            //    else if (EnemyAggroArea.Bottom + Velocity.Y + Gravity.Y > sprite.SpriteRectangle.Top &&
            //    EnemyAggroArea.Top < sprite.SpriteRectangle.Top &&
            //    EnemyAggroArea.Right > sprite.SpriteRectangle.Left &&
            //    EnemyAggroArea.Left < sprite.SpriteRectangle.Right)
            //        EnemyAggroAreaSize.W = sprite.SpriteRectangle.Top;
            //    else if (EnemyAggroArea.Top + Velocity.Y < sprite.SpriteRectangle.Bottom &&
            //   EnemyAggroArea.Bottom > sprite.SpriteRectangle.Bottom &&
            //   EnemyAggroArea.Right > sprite.SpriteRectangle.Left &&
            //   EnemyAggroArea.Left < sprite.SpriteRectangle.Right)
            //        EnemyAggroAreaSize.Y = Position.Y - sprite.SpriteRectangle.Bottom;

            //    //EnemyAggroArea.Width = (int)(Position.X + EnemyAggroAreaSize.Z);
            //    //EnemyAggroArea.Height = (int)(Position.Y + EnemyAggroAreaSize.W);
            //}
        }

        public void SetPlayer(Player WormPlayer)
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
                else if (IsTouchingBottomSide(sprite,Gravity))
                {
                    Velocity.Y = 0;
                    GravityActive = true;
                   // IsStanding = true;
                    CollisionBoxPosition.Y = sprite.Position.Y + sprite.SpriteRectangle.Height;
                    Position.Y = CollisionBoxPosition.Y - ChangeCollisionBox.Y;
                }
                else if (IsTouchingTopSide(sprite, Gravity))
                {
                    Velocity.Y = 0;
                    GravityActive = false;
                    Gravity = Vector2.Zero;
                    IsStanding = true;
                    CollisionBoxPosition.Y = sprite.Position.Y - CollisionBoxSize.Y;
                    Position.Y = CollisionBoxPosition.Y - ChangeCollisionBox.Y;
                    EnemyAggroArea.Y = (int)(Position.Y - EnemyAggroAreaSize.Y);
                    KnockedBack = false;
                    AirDirectionLeft = false;
                    AirDirectionRight = false;
                    FallCooldown = 0;
                }

                else if (!IsTouchingTopSide(sprite, Gravity) &&  IsStanding == false)
                {
                    FallCooldown += (float)gameTime.ElapsedGameTime.TotalSeconds * 2; 
                    GravityActive = true;
                    if (AirDirectionRight && KnockedBack == false)
                        Velocity.X = MovementSpeed;
                    else if (AirDirectionLeft && KnockedBack == false)
                        Velocity.X = -MovementSpeed;
                    else if (AirDirectionRight && KnockedBack)
                        Velocity.X = KnockBackValue;
                    else if (AirDirectionLeft && KnockedBack)
                        Velocity.X = -KnockBackValue;
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
                if (GravityActive && IsStanding == false)
                {
                    //if (!PreviousState.IsKeyDown(Keys.Space))
                    Gravity.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * 15;
                    //if (Gravity.Y > -JumpSpeed && PreviousState.IsKeyDown(Keys.Space))
                    //    Gravity.Y = 12f;
                    //else
                    //    Gravity.Y = 5;
                    
                }
                else
                    Gravity = Vector2.Zero;
                if (FallCooldown >= 5)
                    FallOutOfMap = true;
            }
            if (IsStanding == false)
                CollisionBoxPosition += Gravity;
            CollisionBoxPosition += Velocity;

            Velocity = Vector2.Zero;
            if (Position != CollisionBoxPosition - ChangeCollisionBox)
            {
                Position = CollisionBoxPosition - ChangeCollisionBox;
                EnemyAggroArea.X = (int)(Position.X - EnemyAggroAreaSize.X);
                EnemyAggroArea.Y = (int)(Position.Y - EnemyAggroAreaSize.Y);
            }
            IsStanding = false;
        }

        private void EnemyMovement()
        {
            if (EnemyAggroArea.Left + Velocity.X + EnemyAggroAreaSize.X > Worm.CollisionRectangle.Right &&
                EnemyAggroArea.Left + Velocity.X < Worm.CollisionRectangle.Right &&
              EnemyAggroArea.Right > Worm.CollisionRectangle.Right &&
              EnemyAggroArea.Bottom > Worm.CollisionRectangle.Top &&
              EnemyAggroArea.Top < Worm.CollisionRectangle.Bottom)
                Velocity.X = -MovementSpeed;
            else if (EnemyAggroArea.Right + Velocity.X - EnemyAggroAreaSize.X /*+ SpriteSize.X*/ < Worm.CollisionRectangle.Left &&
                EnemyAggroArea.Right + Velocity.X > Worm.CollisionRectangle.Left &&
                EnemyAggroArea.Left < Worm.CollisionRectangle.Left &&
                EnemyAggroArea.Bottom > Worm.CollisionRectangle.Top &&
                EnemyAggroArea.Top < Worm.CollisionRectangle.Bottom)
                Velocity.X = MovementSpeed;
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
        public void ReduceEnemyHP()
        {
            if (StillAlive)
                EnemyHP--;
            if(EnemyHP < 1)
                StillAlive = false;
           
               
        }

        public bool EnemyAliveState()
        {
            return StillAlive;
        }
        public void KnockBackPosition(bool KnockBackDirectionRight)
        {
            KnockedBack = true;
            IsStanding = false;
            Velocity.Y = -KnockBackValue;
            if (KnockBackDirectionRight)
            {
                Velocity.X = KnockBackValue;
                AirDirectionRight = true;
                AirDirectionLeft = false;
            }
            else
            {
                Velocity.X = -KnockBackValue;
                AirDirectionRight = false;
                AirDirectionLeft = true;
            }
            ReduceEnemyHP();
        }
    }
}
