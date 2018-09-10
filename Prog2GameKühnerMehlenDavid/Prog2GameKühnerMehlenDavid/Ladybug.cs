using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Reggie
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
            if (facingLeft)
                animationManager.nextAnimation = Enums.EnemyAnimations.LADYBUG_FLY_LEFT;
            else if (!facingLeft)
                animationManager.nextAnimation = Enums.EnemyAnimations.LADYBUG_FLY_RIGHT;
            animationManager.Animation(gameTime, this, spriteBatch);
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjectList)
        {
            ResizeEnemyAggroArea(gameObjectList);
            if(attackAction && attackCooldown == 0)
                EnemyAttack(gameTime);
            if (attackExecuted)
                CalculationCooldown(gameTime);
            EnemyPositionCalculation(gameTime, gameObjectList);
            if (DetectPlayer() && !knockedBack)
                EnemyMovement();

        }

        private void CalculationCooldown(GameTime gameTime)
        {
            attackCooldown += (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
            if(attackCooldown >5f)
            {
                attackCooldown = 0;
                attackExecuted = false;
            }
        }

        public override void EnemyAttack(GameTime gameTime)
        {
            attackTimer += (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
            //if(!calculateCharge)
            //    CalculationChargingVector();
            if(attackTimer <4f)
            {
                velocity.Y = -10f;
            }
            //else if(attackTimer>4f && attackTimer<10f)
            //{
            //    if (HitPlayer() && !worm.invincibilityFrames)
            //    {
            //        worm.invincibilityFrames = true;
            //        worm.ReducePlayerHP();
            //    }  
            //}
            else
            {
                attackTimer = 0;
                attackAction = false;
                calculateCharge = false;
                velocity = Vector2.Zero;
                attackExecuted = true;
            }
        }

        private void CalculationChargingVector()
        {
            velocity.X = worm.collisionRectangle.X / 2 - collisionRectangle.X / 2;
            velocity.Y = worm.collisionRectangle.Y / 2 - collisionRectangle.Y / 2;
            calculateCharge = true;
        }
    }
}
