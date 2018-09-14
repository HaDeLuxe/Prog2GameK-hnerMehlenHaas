using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Reggie.Animations;

namespace Reggie.Enemies
{
    public class Ladybug : Enemy
    {

        private AnimationManagerEnemy animationManager;
        public Ladybug(Texture2D enemyTexture, Vector2 enemySize, Vector2 enemyPosition, int gameObjectID, Dictionary<string, Texture2D> EnemySpriteSheetsDic) : base(enemyTexture, enemySize, enemyPosition, gameObjectID, EnemySpriteSheetsDic)
        {
            enemyHP = 3;
            movementSpeed = 5f;
            knockBackValue = 20f;
            attackRange = 10f;
            animationManager = new AnimationManagerEnemy(EnemySpriteSheetsDic);
        }


        public override void EnemyAnimationUpdate(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!facingDirectionRight)
                animationManager.nextAnimation = Enums.EnemyAnimations.LADYBUG_FLY_LEFT;
            else if (facingDirectionRight)
                animationManager.nextAnimation = Enums.EnemyAnimations.LADYBUG_FLY_RIGHT;
            animationManager.Animation(gameTime, this, spriteBatch);
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjectList)
        {
            ResizeEnemyAggroArea(gameObjectList);
            if (!attackAction && attackCooldown == 0)
            {
                if (DetectPlayer() && !knockedBack)
                    EnemyMovement();
            }
            if (attackAction)
                EnemyAttack(gameTime);
            if (attackExecuted)
                CalculationCooldown(gameTime);
            EnemyCheckCollision(gameTime, gameObjectList);
            EnemyPositionCalculation(gameTime);
           

        }

        private void CalculationCooldown(GameTime gameTime)
        {
            attackCooldown += (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
            if(attackCooldown >1f)
            {
                attackCooldown = 0;
                attackExecuted = false;
            }
        }

        public override void EnemyAttack(GameTime gameTime)
        {
            attackTimer += (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
            //if (!calculateCharge)
            if(attackTimer<1f)
                CalculationChargingVector();
            if (chargingVector.X < 0)
                facingDirectionRight = false;
            else if (chargingVector.X > 0)
                facingDirectionRight = true;
            if (attackTimer <1f)
            {
                velocity.Y = -6f;
                velocity.X = 0f;
                isStanding = false;
            }
            else if (attackTimer > 1f && attackTimer < 3f)
            {
                if (!calculateCharge)
                    CalculationChargingVector();
                velocity =  chargingVector * 3f;
                if (HitPlayer() && !worm.invincibilityFrames)
                {
                    worm.invincibilityFrames = true;
                    worm.ReducePlayerHP();
                    //worm.KnockBackPosition(facingDirectionRight, 35);
                }
            }
            else
            {
                attackTimer = 0;
                attackAction = false;
                calculateCharge = false;
                velocity = Vector2.Zero;
                attackExecuted = true;
                chargingVector = Vector2.Zero;
            }
        }

        private void CalculationChargingVector()
        {
            chargingVector.X = worm.collisionRectangle.X / 60 - collisionRectangle.X / 60;
            chargingVector.Y = worm.collisionRectangle.Y / 60 - collisionRectangle.Y / 60;
            
            calculateCharge = true;
        }
    }
}
