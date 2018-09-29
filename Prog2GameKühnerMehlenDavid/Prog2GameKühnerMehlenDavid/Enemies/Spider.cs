using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Reggie.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Enemies
{
    class Spider : Enemy
    {
        private AnimationManagerEnemy animationManager;
        public List<Projectile> projectileList;
        private bool alreadyShot;
        Dictionary<string, Texture2D> EnemySpriteSheetsDic;
        public Spider(Texture2D enemyTexture, Vector2 enemySize, Vector2 enemyPosition, int gameObjectID, Dictionary<string, Texture2D> EnemySpriteSheetsDic) : base(enemyTexture, enemySize, enemyPosition, gameObjectID, EnemySpriteSheetsDic)
        {
            enemyHP = 1;
            movementSpeed = 6f;
            knockBackValue = 30f;
            attackDamage = 0.07f;
            attackRange = 400f;
            animationManager = new AnimationManagerEnemy(EnemySpriteSheetsDic);
            projectileList = new List<Projectile>();
            alreadyShot = false;
            this.EnemySpriteSheetsDic = EnemySpriteSheetsDic;
        }


        public override void EnemyAnimationUpdate(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!facingDirectionRight && !attackAction)
                animationManager.nextAnimation = Enums.EnemyAnimations.SPIDER_MOVE_LEFT;
            else if (facingDirectionRight && !attackAction)
                animationManager.nextAnimation = Enums.EnemyAnimations.SPIDER_MOVE_RIGHT;
            else if (!facingDirectionRight && attackAction)
                animationManager.nextAnimation = Enums.EnemyAnimations.SPIDER_ATTACK_LEFT;
            else if (facingDirectionRight && attackAction)
                animationManager.nextAnimation = Enums.EnemyAnimations.SPIDER_ATTACK_RIGHT;
            animationManager.Animation(gameTime, this, spriteBatch);
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjectList)
        {
            //ResizeEnemyAggroArea(gameObjectList);
            if (!attackAction && attackCooldown == 0)
            {
                if (DetectPlayer() && !knockedBack )
                    EnemyMovement();
                //if (meleeAttack)
                    MeleePlayer();

                EnemyNeutralBehaviour(gameObjectList);
            }
            if (attackAction)
                EnemyAttack(gameTime, gameObjectList);
            //CalculationChargingVector();
            if (projectileList.Count() != 0)
                foreach (var projectile in projectileList.ToList())
                {
                    projectile.TracedPlayerLocation();
                    projectile.Update(gameTime, gameObjectList);
                    if (!projectile.ProjectileState())
                        projectileList.RemoveAt(projectileList.IndexOf(projectile));
                }
            if (attackExecuted)
                CalculationCooldown(gameTime);
            //if (meleeAttack)
            //    MeleePlayer();
            EnemyCheckCollision(gameTime, gameObjectList);
            EnemyPositionCalculation(gameTime);


            if (invincibilityFrames)
                InvincibleFrameState(gameTime);

        }

        private void CalculationCooldown(GameTime gameTime)
        {
            attackCooldown += (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
            if (attackCooldown > 1f)
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
                if (facingDirectionRight && !alreadyShot && !meleeAttack)
                {
                    projectileList.Add(new Projectile(null, new Vector2(100, 50), new Vector2(collisionBoxPosition.X + collisionBoxSize.X, collisionBoxPosition.Y - 10), (int)Enums.ObjectsID.SPIDER));
                    projectileList.Last().SetPlayer(worm);
                    velocity.X = 0f;
                    alreadyShot = true;
                }
                else if (!facingDirectionRight && !alreadyShot && !meleeAttack)
                {
                    projectileList.Add(new Projectile(null, new Vector2(100, 50), new Vector2(collisionBoxPosition.X - collisionBoxSize.X / 2, collisionBoxPosition.Y - 10), (int)Enums.ObjectsID.SPIDER));
                    projectileList.Last().SetPlayer(worm);
                    velocity.X = 0f;
                    alreadyShot = true;
                }
                else if (attackTimer < 2f)
                {
                    if (!worm.playerSlowed && facingDirectionRight)
                        velocity.X = 1;
                    else if (!worm.playerSlowed && !facingDirectionRight)
                        velocity.X = -1;
                    if (worm.playerSlowed)
                    {
                        CalculationChargingVector();
                        if (chargingVector.X >= 0)
                            velocity.X = 3f;
                        else
                            velocity.X = -3f;
                    }
                    //if (meleeAttack)
                        MeleePlayer();

                    if (HitPlayer() && !worm.invincibilityFrames && meleeAttack)
                    {
                        worm.invincibilityFrames = true;
                        worm.ReducePlayerHP(attackDamage);
                        meleeAttack = false;
                    }
                    gravityActive = true;
                    isStanding = false;
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
        public override void DrawProjectile(SpriteBatch spriteBatch, Color color)
        {
            foreach (var projectile in projectileList)
            {
                spriteBatch.Draw(EnemySpriteSheetsDic["spiderWebProjectile"], new Vector2(projectile.collisionRectangle.Left, projectile.collisionRectangle.Top), color);
            }
        }
        public override void drawHealthBar(SpriteBatch spriteBatch, Dictionary<string, Texture2D> texturesDictionary)
        {
            if (enemyHP < 3)
            {
                spriteBatch.Draw(texturesDictionary["enemyHBBorder"], this.gameObjectPosition + new Vector2(32, -50), Color.White);
                spriteBatch.Draw(texturesDictionary["enemyHBFill"], this.gameObjectPosition + new Vector2(32, -50), null, Color.White, 0, Vector2.Zero, new Vector2(enemyHP / 3, 1), SpriteEffects.None, 0);
            }
            base.drawHealthBar(spriteBatch, texturesDictionary);
        }
    }
}
