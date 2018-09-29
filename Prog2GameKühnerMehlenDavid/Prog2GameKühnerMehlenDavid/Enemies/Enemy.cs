using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Enemies
{
    public class Enemy : GameObject
    {
        #region Field
        public Rectangle enemyAggroArea;
        public Vector4 enemyAggroAreaSize;
        protected float attackRange;
        protected Player worm;
        public float enemyHP;
        protected bool stillAlive;
        protected bool knockedBack;
        protected float knockBackValue;
        protected bool attackAction;
        protected float fallCooldown;
        public bool fallOutOfMap { get; set; }
        protected float attackTimer;
        protected Vector2 chargingVector;
        protected bool calculateCharge;
        protected bool attackExecuted;
        protected float attackCooldown;
        public bool invincibilityFrames;
        public float invincibilityTimer;
        public float ratioCharge;
        protected float movementDirectionGone;
        protected bool leftFootAir;
        protected bool rightFootAir;
        protected bool meleeAttack;

        //MUSIC
        AudioManager audioManager;
        


        protected float attackDamage;
        #endregion

        protected Rectangle leftFootRect
        {
            get { return new Rectangle((int)collisionBoxPosition.X, (int)(collisionBoxPosition.Y + collisionBoxSize.Y), 1, 1); }
        }
        protected Rectangle rightFootRect
        {
            get { return new Rectangle((int)(collisionBoxPosition.X+collisionBoxSize.X-1), (int)(collisionBoxPosition.Y + collisionBoxSize.Y), 1, 1); }
        }

        public Enemy(Texture2D enemyTexture, Vector2 enemySize, Vector2 enemyPosition, int gameObjectID, Dictionary<string, Texture2D> EnemySpriteSheetsDic) : base(enemyTexture, enemySize, enemyPosition, gameObjectID)
        {
            gravityActive = true;
            isStanding = false;
            stillAlive = true;
            knockedBack = false;
            fallOutOfMap = false;
            attackAction = false;
            calculateCharge = false;
            attackExecuted = false;
            attackCooldown = 0;
            ratioCharge = 1f;
            invincibilityFrames = false;
            invincibilityTimer = 0;
            leftFootAir = false;
            rightFootAir = false;
            meleeAttack = false;
            // objectID = (int)Enums.ObjectsID.ENEMY;
            //Position = new Vector2(900, 200);
            changeCollisionBox = new Vector2(0, 0);
            enemyAggroAreaSize = new Vector4(500, 500, 1100, 1050);
            collisionBoxPosition = new Vector2(enemyPosition.X + changeCollisionBox.X, enemyPosition.Y + changeCollisionBox.Y);
            enemyAggroArea = new Rectangle((int)(enemyPosition.X - enemyAggroAreaSize.X), (int)(enemyPosition.Y - enemyAggroAreaSize.Y), (int)(enemyAggroAreaSize.Z), (int)(enemyAggroAreaSize.W));
            collisionBoxSize = new Vector2(enemySize.X, enemySize.Y);
            movementDirectionGone = 0;

            //MUSIC
            audioManager = AudioManager.AudioManagerInstance();

        }

        public virtual void EnemyAnimationUpdate(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }

        

        public void changeTexture(Texture2D texture) {
            this.gameObjectTexture = texture;
        }

        public void ResizeEnemyAggroArea(List<GameObject> spriteList)
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

        public void EnemyCheckCollision(GameTime gameTime, List<GameObject> platformList)
        {
            for (int i = 0; i < platformList.Count(); i++)
            {
                if (velocity.X > 0 && IsTouchingLeftSide(platformList[i]))
                {
                    velocity.X = 0;
                    movementDirectionGone = 1000;
                    collisionBoxPosition.X = platformList[i].gameObjectPosition.X - collisionBoxSize.X;
                    pressedLeftKey = false;
                    pressedRightKey = false;
                    if (objectID == (int)Enums.ObjectsID.SPIDER && worm.playerSlowed)
                        velocity.Y = -10f;
                }
                else if (velocity.X < 0 && IsTouchingRightSide(platformList[i]))
                {
                    velocity.X = 0;
                    movementDirectionGone = 0;
                    collisionBoxPosition.X = platformList[i].gameObjectPosition.X + platformList[i].gameObjectRectangle.Width;
                    pressedLeftKey = false;
                    pressedRightKey = false;
                    if (objectID == (int)Enums.ObjectsID.SPIDER && worm.playerSlowed)
                        velocity.Y = -10f;
                }
                else if (IsTouchingBottomSide(platformList[i], gravity))
                {
                    velocity.Y = 0;
                    collisionBoxPosition.Y = platformList[i].gameObjectPosition.Y + platformList[i].gameObjectRectangle.Height;
                    //Position.Y = CollisionBoxPosition.Y - ChangeCollisionBox.Y;
                    gravityActive = true;
                }
                else if (IsTouchingTopSide(platformList[i], gravity))
                {
                    resetBasicValues();
                    fallCooldown = 0;
                    collisionBoxPosition.Y = platformList[i].gameObjectPosition.Y - collisionBoxSize.Y;
                    //Position.Y = CollisionBoxPosition.Y - ChangeCollisionBox.Y;
                    //EnemyAggroArea.Y = (int)(Position.Y - EnemyAggroAreaSize.Y);


                    knockedBack = false;
                    pressedLeftKey = false;
                    pressedRightKey = false;

                }

                else if (!IsTouchingTopSide(platformList[i], gravity) && isStanding == false && !attackAction)
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
                    if (IsTouchingLeftSide(platformList[i]) || IsTouchingRightSide(platformList[i]))
                        velocity.X = 0;
                }
            }
            
        }
        public void EnemyPositionCalculation(GameTime gameTime)
        {
           
            if (gravityActive && isStanding == false /*&& !attackAction*/)
            {
                fallCooldown += (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
                gravity.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * 15;
                collisionBoxPosition += gravity;
            }
            else
                gravity = Vector2.Zero;
            collisionBoxPosition += velocity;
            velocity = Vector2.Zero;
            //if (fallCooldown >= 5)
            //        fallOutOfMap = true;
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

        public void EnemyMovement()
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
                if (Math.Abs(collisionRectangle.Left - worm.collisionRectangle.Right) < worm.collisionBoxSize.X*2)
                {
                    meleeAttack = true;
                }
            }
            else if (enemyAggroArea.Right + velocity.X - enemyAggroAreaSize.X < worm.collisionRectangle.Left &&
                enemyAggroArea.Right + velocity.X > worm.collisionRectangle.Left &&
                enemyAggroArea.Left < worm.collisionRectangle.Left &&
                enemyAggroArea.Bottom > worm.collisionRectangle.Top &&
                enemyAggroArea.Top < worm.collisionRectangle.Bottom)
            {
                pressedLeftKey = false;
                pressedRightKey = true;
                velocity.X = movementSpeed;
                if (Math.Abs(collisionRectangle.Right - worm.collisionRectangle.Left) < worm.collisionBoxSize.X*2)
                    meleeAttack = true;
            }
            else
                velocity.X = 0;

            if (velocity.X < 0)
                facingDirectionRight = false;
            else if (velocity.X > 0)
                facingDirectionRight = true;
        }

        public virtual void EnemyAttack(GameTime gameTime) { }
        public virtual void EnemyAttack(GameTime gameTime, List<GameObject> gameObjectList) { }
        public bool DetectPlayer()
        {
            if (enemyAggroArea.Right + velocity.X > worm.collisionRectangle.Left &&
                enemyAggroArea.Left < worm.collisionRectangle.Left &&
                enemyAggroArea.Bottom > worm.collisionRectangle.Top &&
                enemyAggroArea.Top < worm.collisionRectangle.Bottom)
            {
                if(objectID != (int)Enums.ObjectsID.SPIDER || (objectID == (int)Enums.ObjectsID.SPIDER && !worm.playerSlowed))
                if (Math.Abs(worm.collisionRectangle.Left - collisionRectangle.Right) < attackRange)
                {
                    resetBasicValues();
                    velocity.X = 0;
                    facingDirectionRight = false;
                    attackAction = true;
                }
                return true;
            }
            else if (enemyAggroArea.Left + velocity.X < worm.collisionRectangle.Right &&
              enemyAggroArea.Right > worm.collisionRectangle.Right &&
              enemyAggroArea.Bottom > worm.collisionRectangle.Top &&
              enemyAggroArea.Top < worm.collisionRectangle.Bottom)
            {
                if (objectID != (int)Enums.ObjectsID.SPIDER|| (objectID == (int)Enums.ObjectsID.SPIDER && !worm.playerSlowed))
                    if (Math.Abs(worm.collisionRectangle.Right - collisionRectangle.Left) <  attackRange)
                {
                    resetBasicValues();
                    velocity.X = 0;
                    facingDirectionRight = true;
                    attackAction = true;
                }
                return true;
            }
            else if (enemyAggroArea.Bottom + velocity.Y + gravity.Y > worm.collisionRectangle.Top &&
            enemyAggroArea.Top < worm.collisionRectangle.Top &&
            enemyAggroArea.Right > worm.collisionRectangle.Left &&
            enemyAggroArea.Left < worm.collisionRectangle.Right)
            {
                if (worm.collisionRectangle.Top - collisionRectangle.Bottom < collisionBoxSize.Y)
                {
                    resetBasicValues();
                    velocity.X = 0;
                    attackAction = true;
                }
                return true;
            }
            else if (enemyAggroArea.Top + velocity.Y < worm.collisionRectangle.Bottom &&
           enemyAggroArea.Bottom > worm.collisionRectangle.Bottom &&
           enemyAggroArea.Right > worm.collisionRectangle.Left &&
           enemyAggroArea.Left < worm.collisionRectangle.Right)
            {
                if (collisionRectangle.Top- worm.collisionRectangle.Bottom < collisionBoxSize.Y)
                {
                    resetBasicValues();
                    velocity.X = 0;
                    attackAction = true;
                }
                return true;
            }
            else
                return false;
        }

        public void MeleePlayer()
        {
            if (collisionRectangle.Right + velocity.X +10 > worm.collisionRectangle.Left &&
               collisionRectangle.Left < worm.collisionRectangle.Left &&
               collisionRectangle.Bottom > worm.collisionRectangle.Top &&
               collisionRectangle.Top < worm.collisionRectangle.Bottom)
            {
                resetBasicValues();
                velocity.X = 0;
                facingDirectionRight = true;
                if (collisionRectangle.Right + velocity.X > worm.collisionRectangle.Left)
                    meleeAttack = true;
        
            }
            else if (collisionRectangle.Left + velocity.X +10 < worm.collisionRectangle.Right &&
              collisionRectangle.Right > worm.collisionRectangle.Right &&
              collisionRectangle.Bottom > worm.collisionRectangle.Top &&
              collisionRectangle.Top < worm.collisionRectangle.Bottom)
            {
                resetBasicValues();
                velocity.X = 0;
                facingDirectionRight = false;
                if (collisionRectangle.Left + velocity.X < worm.collisionRectangle.Right)
                    meleeAttack = true;
            }
            
            
            else
                meleeAttack=  false;
        }


        public bool HitPlayer()
        {
            if (collisionRectangle.Right + velocity.X > worm.collisionRectangle.Left &&
                collisionRectangle.Left < worm.collisionRectangle.Left &&
                collisionRectangle.Bottom > worm.collisionRectangle.Top &&
                collisionRectangle.Top < worm.collisionRectangle.Bottom)
                    return true;
            else if (collisionRectangle.Left + velocity.X < worm.collisionRectangle.Right &&
              collisionRectangle.Right > worm.collisionRectangle.Right &&
              collisionRectangle.Bottom > worm.collisionRectangle.Top &&
              collisionRectangle.Top < worm.collisionRectangle.Bottom)
                return true;
            else if (collisionRectangle.Bottom + velocity.Y + gravity.Y > worm.collisionRectangle.Top &&
            collisionRectangle.Top < worm.collisionRectangle.Top &&
            collisionRectangle.Right > worm.collisionRectangle.Left &&
            collisionRectangle.Left < worm.collisionRectangle.Right)
                return true;
            else if (collisionRectangle.Top + velocity.Y < worm.collisionRectangle.Bottom &&
           collisionRectangle.Bottom > worm.collisionRectangle.Bottom &&
           collisionRectangle.Right > worm.collisionRectangle.Left &&
           collisionRectangle.Left < worm.collisionRectangle.Right)
                return true;
            else
                return false;
        }



        public void ReduceEnemyHP(float playerDamage)
        {
            if (stillAlive)
                enemyHP -= playerDamage;
            if(enemyHP < 1)
                stillAlive = false;  
        }

        public bool EnemyAliveState()
        {
            return stillAlive;
        }

        public void KnockBackPosition(bool knockBackDirectionRight, float playerDamage)
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
            audioManager.Play("ReggieAttackHits");
            ReduceEnemyHP(playerDamage);
        }

        public void resetBasicValues()
        {
            velocity.Y = 0;
            gravityActive = false;
            gravity = Vector2.Zero;
            isStanding = true;
        }

        public void InvincibleFrameState(GameTime gameTime)
        {
            invincibilityTimer += (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
            if (invincibilityTimer > 5f)
            {
                invincibilityTimer = 0;
                invincibilityFrames = false;
            }
        }
        protected void EnemyNeutralBehaviour(List<GameObject> gameObjectList)
        {
            if (movementDirectionGone == 1000)
            {
               // velocity.X = -movementSpeed;
                facingDirectionRight = false;
            }
                  if (movementDirectionGone == 0)
            {
               // velocity.X = movementSpeed;
                facingDirectionRight = true;
            }
            if (facingDirectionRight)
                velocity.X = movementSpeed;
            else
                velocity.X = -movementSpeed;
            movementDirectionGone += velocity.X;

            foreach (var platform in gameObjectList)
            {
                //if (!leftFootRect.Intersects(platform.gameObjectRectangle)&& IsTouchingTopSide(platform,gravity) && rightFootRect.Intersects(platform.gameObjectRectangle))
                //    movementDirectionGone = 0;
                //else if (!rightFootRect.Intersects(platform.gameObjectRectangle) && IsTouchingTopSide(platform, gravity) && leftFootRect.Intersects(platform.gameObjectRectangle))
                //    movementDirectionGone = 100;
                if(IsTouchingTopSide(platform, gravity))
                {
                    if (!leftFootRect.Intersects(platform.gameObjectRectangle))
                        leftFootAir = true;
                    if (!rightFootRect.Intersects(platform.gameObjectRectangle))
                        rightFootAir = true;
                }
            }
            if(leftFootAir && rightFootAir)
            {
                leftFootAir = false;
                rightFootAir = false;
            }
            if (leftFootAir)
                movementDirectionGone = 0;
            if (rightFootAir)
                movementDirectionGone = 1000;
        }
        public virtual void DrawProjectile(SpriteBatch spriteBatch, Color color){ }
    }
}
