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
    public class Snail : Enemy
    {
        private AnimationManagerEnemy animationManager;
        private List<Projectile> projectileList;
        private bool alreadyShot;
        public Snail(Texture2D enemyTexture, Vector2 enemySize, Vector2 enemyPosition, int gameObjectID, Dictionary<string, Texture2D> EnemySpriteSheetsDic) : base(enemyTexture, enemySize, enemyPosition, gameObjectID, EnemySpriteSheetsDic)
        {
            enemyHP = 200;
            movementSpeed = 3f;
            knockBackValue = 10f;
            attackDamage = 0.09f;
            attackRange = 400f;
            animationManager = new AnimationManagerEnemy(EnemySpriteSheetsDic);
            projectileList = new List<Projectile>();
            alreadyShot = false;
            //enemyAggroAreaSize = new Vector4(500, 500, 1100, 1050);
            //changeCollisionBox = new Vector2(0, 0);
            //enemyAggroArea = new Rectangle((int)(enemyPosition.X - enemyAggroAreaSize.X), (int)(enemyPosition.Y - enemyAggroAreaSize.Y), (int)(enemyAggroAreaSize.Z), (int)(enemyAggroAreaSize.W));
            //collisionBoxPosition = new Vector2(enemyPosition.X + changeCollisionBox.X, enemyPosition.Y + changeCollisionBox.Y);
            //collisionBoxSize = new Vector2(enemySize.X, enemySize.Y);
            //movementDirectionGone = 0;
        }


        public override void EnemyAnimationUpdate(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!facingDirectionRight && !attackAction)
                animationManager.nextAnimation = Enums.EnemyAnimations.LADYBUG_FLY_LEFT;
            else if (facingDirectionRight && !attackAction)
                animationManager.nextAnimation = Enums.EnemyAnimations.LADYBUG_FLY_RIGHT;
            else if (!facingDirectionRight && attackAction)
                animationManager.nextAnimation = Enums.EnemyAnimations.LADYBUG_ATTACK_LEFT;
            else if (facingDirectionRight && attackAction)
                animationManager.nextAnimation = Enums.EnemyAnimations.LADYBUG_ATTACK_RIGHT;
            animationManager.Animation(gameTime, this, spriteBatch);
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjectList)
        {
            //ResizeEnemyAggroArea(gameObjectList);
            if (!attackAction && attackCooldown == 0)
            {
                if (DetectPlayer() && !knockedBack)
                    EnemyMovement();
                if (!DetectPlayer())
                    EnemyNeutralBehaviour(gameObjectList);
            }
            if (attackAction)
                EnemyAttack(gameTime, gameObjectList);
            //CalculationChargingVector();
            if(projectileList.Count() !=0)
            foreach (var projectile in projectileList.ToList())
            {
                projectile.TracedPlayerLocation();
                projectile.Update(gameTime, gameObjectList);
                if (!projectile.ProjectileState())
                    projectileList.RemoveAt(projectileList.IndexOf(projectile));
            }
            if (attackExecuted)
                CalculationCooldown(gameTime);
            EnemyCheckCollision(gameTime, gameObjectList);
            EnemyPositionCalculation(gameTime);


            if (invincibilityFrames)
                InvincibleFrameState(gameTime);

        }

        private void CalculationCooldown(GameTime gameTime)
        {
            attackCooldown += (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
            if (attackCooldown > 2f)
            {
                attackCooldown = 0;
                attackExecuted = false;
                alreadyShot = false;
            }
        }

        public override void EnemyAttack(GameTime gameTime, List<GameObject> gameObjectList)
        {
            if (!knockedBack)
            {
                attackTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                //if (!calculateCharge)
                if (attackTimer < 1f)
                    CalculationChargingVector();
                if (chargingVector.X < 0)
                    facingDirectionRight = false;
                else if (chargingVector.X > 0)
                    facingDirectionRight = true;
                if (facingDirectionRight && !alreadyShot)
                {
                    projectileList.Add(new Projectile(null, new Vector2(100, 50), new Vector2(collisionBoxPosition.X + collisionBoxSize.X, collisionBoxPosition.Y), (int)Enums.ObjectsID.SNAIL));
                    projectileList.Last().SetPlayer(worm);
                    velocity.X = 0f;
                    alreadyShot = true;
                }
                else if (!facingDirectionRight && !alreadyShot)
                {
                    projectileList.Add(new Projectile(null, new Vector2(100, 50), new Vector2(collisionBoxPosition.X - collisionBoxSize.X, collisionBoxPosition.Y), (int)Enums.ObjectsID.SNAIL));
                    projectileList.Last().SetPlayer(worm);
                    velocity.X = 0f;
                    alreadyShot = true;
                }
                else if (attackTimer < 2f)
                {
                    //if (!calculateCharge)
                    //CalculationChargingVector();
                    //foreach (var projectile in projectileList.ToList())
                    //{
                    //    projectile.TracedPlayerLocation();
                    //    projectile.Update(gameTime, gameObjectList);
                    //    if (!projectile.ProjectileState())
                    //        projectileList.RemoveAt(projectileList.IndexOf(projectile));
                    //}
                    if (facingDirectionRight)
                        velocity.X = -1f;
                    else
                        velocity.X = 1f;
                }
                else
                {
                    attackTimer = 0;
                    attackAction = false;
                    // calculateCharge = false;
                    velocity = Vector2.Zero;
                    attackExecuted = true;
                    gravityActive = true;
                    isStanding = false;
                    chargingVector = Vector2.Zero;
                }
            }
            else
            {
                attackTimer = 0;
                attackAction = false;
                calculateCharge = false;
                velocity = Vector2.Zero;
                attackExecuted = true;
                gravityActive = true;
                isStanding = false;
                chargingVector = Vector2.Zero;
            }
        }

        private void CalculationChargingVector()
        {
            if (chargingVector == Vector2.Zero)
            {
                if ((worm.collisionRectangle.X / 60 - collisionRectangle.X / 60) == 0)
                    chargingVector.X = 0;
                if ((worm.collisionRectangle.Y / 60 - collisionRectangle.Y / 60) == 0)
                    chargingVector.Y = 0;
            }
            if (Math.Abs(worm.collisionRectangle.X / 60 - collisionRectangle.X / 60) <= Math.Abs(worm.collisionRectangle.Y / 60 - collisionRectangle.Y / 60) && worm.collisionRectangle.Y / 60 - collisionRectangle.Y / 60 != 0)
            {
                chargingVector.X = worm.collisionRectangle.X / 60 - collisionRectangle.X / 60;
                chargingVector.Y = worm.collisionRectangle.Y / 60 - collisionRectangle.Y / 60;
                if ((worm.collisionRectangle.Y / 60 - collisionRectangle.Y / 60) != 0)
                    chargingVector.X = chargingVector.X / Math.Abs(chargingVector.Y) * 8;
                if ((worm.collisionRectangle.Y / 60 - collisionRectangle.Y / 60) > 0)
                    chargingVector.Y = 8;
                else
                    chargingVector.Y = -8;
            }
            else if (Math.Abs(worm.collisionRectangle.X / 60 - collisionRectangle.X / 60) >= Math.Abs(worm.collisionRectangle.Y / 60 - collisionRectangle.Y / 60) && worm.collisionRectangle.X / 60 - collisionRectangle.X / 60 != 0)
            {
                chargingVector.X = worm.collisionRectangle.X / 60 - collisionRectangle.X / 60;
                chargingVector.Y = worm.collisionRectangle.Y / 60 - collisionRectangle.Y / 60;
                if ((worm.collisionRectangle.X / 60 - collisionRectangle.X / 60) != 0)
                    chargingVector.Y = chargingVector.Y / Math.Abs(chargingVector.X) * 8;
                if ((worm.collisionRectangle.X / 60 - collisionRectangle.X / 60) > 0)
                    chargingVector.X = 8;
                else
                    chargingVector.X = -8;
            }
           // calculateCharge = true;
        }
        public override void DrawProjectile(SpriteBatch spriteBatch, Color color, Texture2D enemyTexture)
        {
            foreach(var projectile in projectileList)
            {
                spriteBatch.Draw(enemyTexture,new Vector2(projectile.collisionRectangle.Left,projectile.collisionRectangle.Top), color);
            }
        }
    }
}
