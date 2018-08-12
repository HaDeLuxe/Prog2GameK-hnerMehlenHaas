using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie
{
    public class Enemy : GameObject
    {
        public Rectangle enemyAggroArea;
        public Vector4 enemyAggroAreaSize;
        public Player worm;
        public int enemyHP;
        bool stillAlive;
        bool knockedBack;
        float knockBackValue;
        float fallCooldown;
        public bool fallOutOfMap;
        
        public Enemy(Texture2D enemyTexture, Vector2 enemySize, Vector2 enemyPosition, int gameObjectID) : base(enemyTexture, enemySize, enemyPosition, gameObjectID)
        {
            gravityActive = true;
            isStanding = false;
            stillAlive = true;
            knockedBack = false;
            fallOutOfMap = false;
            enemyHP = 3;
            movementSpeed = 7f;
            knockBackValue = 20f;
           // objectID = (int)Enums.ObjectsID.ENEMY;
            //Position = new Vector2(900, 200);
            changeCollisionBox = new Vector2(0, 0);
            enemyAggroAreaSize = new Vector4(400, 300, 650, 850);
            collisionBoxPosition = new Vector2(enemyPosition.X + changeCollisionBox.X, enemyPosition.Y + changeCollisionBox.Y);
            enemyAggroArea = new Rectangle((int)(enemyPosition.X - enemyAggroAreaSize.X), (int)(enemyPosition.Y - enemyAggroAreaSize.Y), (int)(enemyAggroAreaSize.W), (int)(enemyAggroAreaSize.Z));
            collisionBoxSize = new Vector2(50, 50);
        }
        //public Rectangle EnemyAggroArea
        //{
        //    get { return new Rectangle((int)(Position.X - EnemyAggroAreaSize.X), (int)(Position.Y - EnemyAggroAreaSize.Y), (int)(Position.X + EnemyAggroAreaSize.Z), (int)(Position.Y + EnemyAggroAreaSize.W)); }
        //}
        public override void Update(GameTime gameTime, List<GameObject> gameObjectList) {
            ResizeEnemyAggroArea(gameObjectList);
            EnemyPositionCalculation(gameTime, gameObjectList);
            if (DetectPlayer() && !knockedBack)
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

        public void SetPlayer(Player wormPlayer)
        {
            worm = wormPlayer;
        }

        private void EnemyPositionCalculation(GameTime gameTime, List<GameObject> platformList)
        {
            foreach (var platform in platformList)
            {
                if (velocity.X > 0 && IsTouchingLeftSide(platform) ||
                   (velocity.X < 0 && IsTouchingRightSide(platform)))
                    velocity.X = 0;
                else if (IsTouchingBottomSide(platform,gravity))
                {
                    velocity.Y = 0;
                    collisionBoxPosition.Y = platform.gameObjectPosition.Y + platform.gameObjectRectangle.Height;
                    //Position.Y = CollisionBoxPosition.Y - ChangeCollisionBox.Y;
                    gravityActive = true;
                }
                else if (IsTouchingTopSide(platform, gravity))
                {
                    velocity.Y = 0;
                    gravity = Vector2.Zero;
                    fallCooldown = 0;
                    collisionBoxPosition.Y = platform.gameObjectPosition.Y - collisionBoxSize.Y;
                    //Position.Y = CollisionBoxPosition.Y - ChangeCollisionBox.Y;
                    //EnemyAggroArea.Y = (int)(Position.Y - EnemyAggroAreaSize.Y);

                    gravityActive = false;
                    isStanding = true;
                    knockedBack = false;
                    pressedLeftKey= false;
                    pressedRightKey = false;
                    
                }

                else if (!IsTouchingTopSide(platform, gravity) &&  isStanding == false)
                {
                    
                    gravityActive = true;
                    if (pressedRightKey && knockedBack == false)
                        velocity.X = movementSpeed;
                    else if (pressedLeftKey && knockedBack == false)
                        velocity.X = -movementSpeed;
                    else if (pressedRightKey && knockedBack)
                        velocity.X = knockBackValue;
                    else if (pressedLeftKey && knockedBack)
                        velocity.X = -knockBackValue;
                    if (IsTouchingLeftSide(platform) || IsTouchingRightSide(platform))
                        velocity.X = 0;
                }
            }
            if (gravityActive && isStanding == false)
            {
                fallCooldown += (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
                gravity.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * 15;
                collisionBoxPosition += gravity;
            }
            else
                gravity = Vector2.Zero;
            collisionBoxPosition += velocity;
            velocity = Vector2.Zero;
            if (fallCooldown >= 5)
                    fallOutOfMap = true;
            if (gameObjectPosition != collisionBoxPosition - changeCollisionBox)
            {
                gameObjectPosition = collisionBoxPosition - changeCollisionBox;
                enemyAggroArea.X = (int)(gameObjectPosition.X - enemyAggroAreaSize.X);
                enemyAggroArea.Y = (int)(gameObjectPosition.Y - enemyAggroAreaSize.Y);
                isStanding = false;
            }
            //else
            //{
            //    IsStanding = true;
            //    GravityActive = false;
            //}
            
        }

        private void EnemyMovement()
        {
            if (enemyAggroArea.Left + velocity.X + enemyAggroAreaSize.X > worm.collisionRectangle.Right &&
                enemyAggroArea.Left + velocity.X < worm.collisionRectangle.Right &&
              enemyAggroArea.Right > worm.collisionRectangle.Right &&
              enemyAggroArea.Bottom > worm.collisionRectangle.Top &&
              enemyAggroArea.Top < worm.collisionRectangle.Bottom)
            {
                pressedRightKey = false;
                pressedLeftKey = true;
                velocity.X = -movementSpeed;
            }
            else if (enemyAggroArea.Right + velocity.X - enemyAggroAreaSize.X /*+ SpriteSize.X*/ < worm.collisionRectangle.Left &&
                enemyAggroArea.Right + velocity.X > worm.collisionRectangle.Left &&
                enemyAggroArea.Left < worm.collisionRectangle.Left &&
                enemyAggroArea.Bottom > worm.collisionRectangle.Top &&
                enemyAggroArea.Top < worm.collisionRectangle.Bottom)
            {
                pressedLeftKey = false;
                pressedRightKey = true;
                velocity.X = movementSpeed;
            }
        }

        public virtual void EnemyAttack() { }

        private bool DetectPlayer()
        {
            if (enemyAggroArea.Right + velocity.X > worm.collisionRectangle.Left &&
                enemyAggroArea.Left < worm.collisionRectangle.Left &&
                enemyAggroArea.Bottom > worm.collisionRectangle.Top &&
                enemyAggroArea.Top < worm.collisionRectangle.Bottom)
            {
                //pressedLeftKey = false;
                //pressedRightKey = true;
                return true;
            }
            else if (enemyAggroArea.Left + velocity.X < worm.collisionRectangle.Right &&
              enemyAggroArea.Right > worm.collisionRectangle.Right &&
              enemyAggroArea.Bottom > worm.collisionRectangle.Top &&
              enemyAggroArea.Top < worm.collisionRectangle.Bottom)
            {
                //pressedLeftKey = true;
                //pressedRightKey = false;
                return true;
            }
            else if (enemyAggroArea.Bottom + velocity.Y + gravity.Y > worm.collisionRectangle.Top &&
            enemyAggroArea.Top < worm.collisionRectangle.Top &&
            enemyAggroArea.Right > worm.collisionRectangle.Left &&
            enemyAggroArea.Left < worm.collisionRectangle.Right)
                return true;
            else if (enemyAggroArea.Top + velocity.Y < worm.collisionRectangle.Bottom &&
           enemyAggroArea.Bottom > worm.collisionRectangle.Bottom &&
           enemyAggroArea.Right > worm.collisionRectangle.Left &&
           enemyAggroArea.Left < worm.collisionRectangle.Right)
                return true;
            else
                return false;
        }
        public void ReduceEnemyHP()
        {
            if (stillAlive)
                enemyHP--;
            if(enemyHP < 1)
                stillAlive = false;  
        }

        public bool EnemyAliveState()
        {
            return stillAlive;
        }

        public void KnockBackPosition(bool knockBackDirectionRight)
        {
            knockedBack = true;
            isStanding = false;
            velocity.Y = -knockBackValue;
            if (knockBackDirectionRight)
            {
                velocity.X = knockBackValue;
                pressedRightKey = true;
                pressedLeftKey = false;
            }
            else
            {
                velocity.X = -knockBackValue;
                pressedRightKey = false;
                pressedLeftKey = true;
            }
            ReduceEnemyHP();
        }
    }
}
