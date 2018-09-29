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
    public class Boss : Enemy
    {
        private AnimationManagerEnemy animationManager;
    
        private float diveDamage;
        private List<Projectile> projectileList;
        Dictionary<string, Texture2D> EnemySpriteSheetsDic;

        private int currentBossPhase;
        private int bossPhase;
        private float phase1Route;
        private float ellipseXValue;
        private float ellipseYValue;
        private float ellipseXStartValue;
        private float ellipseYStartValue;

        private Vector2 bossLastPosition;
        private int chargeCounter;
        private bool bossRest;
        private float restCooldown;
        private float projectileSpawnCooldown;

        public Boss(Texture2D enemyTexture, Vector2 enemySize, Vector2 enemyPosition, int gameObjectID, Dictionary<string, Texture2D> EnemySpriteSheetsDic) : base(enemyTexture, enemySize, enemyPosition, gameObjectID, EnemySpriteSheetsDic)
        {
            enemyHP = 20;
            
            diveDamage = 0.1f;
            animationManager = new AnimationManagerEnemy(EnemySpriteSheetsDic);
            projectileList = new List<Projectile>();
            this.EnemySpriteSheetsDic = EnemySpriteSheetsDic;
            bossPhase = 1;
            currentBossPhase = 1;
            phase1Route = 0;
            chargeCounter = 0;
            bossRest = false;
            attackExecuted = true;
            attackAction = false;
            restCooldown = 0;
            projectileSpawnCooldown = 0;
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjectList)
        {
            UpdateBossFightState();
            if (!bossRest)
            {
                BossMovement();
                if (attackExecuted)
                    CalculationCooldown(gameTime);
                if (attackAction)
                    EnemyAttack(gameTime, gameObjectList);
            }
            else
            {
                BossRestLocation();
                BossRestCooldown(gameTime);
            }
            if(projectileList.Count!=0 && bossPhase == 1)
            {
                foreach (var projectile in projectileList.ToList())
                {
                    projectile.TracedPlayerLocation();
                    projectile.BossOriginLocation(collisionBoxPosition.X + collisionBoxSize.X / 2, collisionBoxPosition.Y + collisionBoxSize.Y / 2);
                    projectile.Update(gameTime, gameObjectList);
                    if (!projectile.ProjectileState())
                        projectileList.RemoveAt(projectileList.IndexOf(projectile));
                }
            }
            if (projectileList.Count != 0 && bossPhase == 3)
            {
                if(projectileList.Count ==4)
                foreach (var projectile in projectileList.ToList())
                {
                   projectile.Launch(true);
                   //projectile.TracedPlayerLocation();
                    projectile.BossOriginLocation(collisionBoxPosition.X + collisionBoxSize.X / 2, collisionBoxPosition.Y + collisionBoxSize.Y / 2);
                    projectile.Update(gameTime, gameObjectList);
                    if (!projectile.ProjectileState())
                        projectileList.RemoveAt(projectileList.IndexOf(projectile));
                }
            }
            if(projectileList.Count == 0 && bossPhase == 3)
            {

            }
            EnemyPositionCalculation(gameTime);
        }

        private void BossRestLocation()
        {
            if(bossPhase == 1)
            {
                collisionBoxPosition.X = -4477;
                collisionBoxPosition.Y = -9778;
            }
            else if (bossPhase == 2)
            {
                collisionBoxPosition.X = -3327;
                collisionBoxPosition.Y = -10359;
            }
            else if(bossPhase == 3)
            {
              
                collisionBoxPosition.X = -3902;
                collisionBoxPosition.Y = -10833;
            }
        }

        public override void EnemyAnimationUpdate(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!facingDirectionRight && !attackAction)
                animationManager.nextAnimation = Enums.EnemyAnimations.HAWK_FLY_LEFT;
            else if (facingDirectionRight && !attackAction)
                animationManager.nextAnimation = Enums.EnemyAnimations.HAWK_FLY_RIGHT;
            else if (!facingDirectionRight && attackAction)
                animationManager.nextAnimation = Enums.EnemyAnimations.HAWK_ATTACK_LEFT;
            else if (facingDirectionRight && attackAction)
                animationManager.nextAnimation = Enums.EnemyAnimations.HAWK_ATTACK_RIGHT;
            animationManager.Animation(gameTime, this, spriteBatch);
        }

        public override void EnemyAttack(GameTime gameTime, List<GameObject> gameObjectList)
        {
            switch(bossPhase)
            {
                case 1:
                    if (projectileSpawnCooldown == 0)
                    {
                        projectileList.Add(new Projectile(null, new Vector2(100, 100), new Vector2(collisionBoxPosition.X + collisionBoxSize.X / 2, collisionBoxPosition.Y+ collisionBoxSize.Y), (int)Enums.ObjectsID.BOSS));
                        projectileList.Last().SetPlayer(worm);
                        projectileList.Last().SetBossPhase(bossPhase);
                        ProjectileSpawnCalc(gameTime);
                    }
                    else
                        ProjectileSpawnCalc(gameTime);
                    break;
                case 2:
                    if (HitPlayer() && !worm.invincibilityFrames)
                    {
                        worm.invincibilityFrames = true;
                        worm.ReducePlayerHP(diveDamage);
                    }
                    break;
                case 3:
                    if(projectileSpawnCooldown == 0 && projectileList.Count <= 4)
                    {
                        projectileList.Add(new Projectile(null, new Vector2(100, 100), new Vector2(collisionBoxPosition.X + collisionBoxSize.X / 2+200, collisionBoxPosition.Y + collisionBoxSize.Y/2), (int)Enums.ObjectsID.BOSS));
                        projectileList.Last().SetPlayer(worm);
                        projectileList.Last().SetBossPhase(bossPhase);
                        ProjectileSpawnCalc(gameTime);
                    }
                    else
                        ProjectileSpawnCalc(gameTime);
                    break;
                case 4:
                    break;
            }
        }

        private void ProjectileSpawnCalc(GameTime gameTime)
        {
            projectileSpawnCooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (projectileSpawnCooldown > 2f)
                projectileSpawnCooldown = 0;
        }

        private void CalculationChargingVector()
        {
            if (collisionBoxPosition.X + collisionBoxSize.X / 2 <= worm.collisionBoxPosition.X + worm.collisionBoxSize.X / 2)
                ellipseXValue = worm.collisionBoxPosition.X + worm.collisionBoxSize.X / 2 - (collisionBoxPosition.X + collisionBoxSize.X / 2);
            else
                ellipseXValue = collisionBoxPosition.X + collisionBoxSize.X / 2 - (worm.collisionBoxPosition.X + worm.collisionBoxSize.X / 2);
            ellipseYValue = worm.collisionBoxPosition.Y + worm.collisionBoxSize.Y / 2 - (collisionBoxPosition.Y + collisionBoxSize.Y / 2);
            ellipseYStartValue = collisionBoxPosition.Y + collisionBoxSize.Y / 2;
            ellipseXStartValue = collisionBoxPosition.X + collisionBoxSize.X / 2;
            calculateCharge = true;
        }

        public override void DrawProjectile(SpriteBatch spriteBatch, Color color)
        {
            if(bossPhase ==1)
            foreach (var projectile in projectileList)
            {
                spriteBatch.Draw(EnemySpriteSheetsDic["feather"], new Vector2(projectile.collisionRectangle.Left, projectile.collisionRectangle.Top), color);
            }
            else if(bossPhase == 3)
            foreach (var projectile in projectileList)
            {
                    spriteBatch.Draw(EnemySpriteSheetsDic["egg"], new Vector2(projectile.collisionRectangle.Left, projectile.collisionRectangle.Top), color);
            }
        }

        private void BossMovement()
        {
            switch (bossPhase)
            {
                case 1:
                    if (phase1Route >= 1600)
                        facingDirectionRight = false;
                    else if (phase1Route <= 100)
                        facingDirectionRight = true;
                    if (facingDirectionRight)
                        velocity.X = 10;
                    else if (!facingDirectionRight)
                        velocity.X = -10;
                    phase1Route += velocity.X;
                    //collisionBoxPosition.X += velocity.X;
                    break;
                case 2:
                    if (phase1Route >= 1600)
                        facingDirectionRight = false;
                    else if (phase1Route <= 100)
                        facingDirectionRight = true;
                    if (facingDirectionRight)
                        velocity.X += 10;
                    else if (!facingDirectionRight)
                        velocity.X -= 10;

                    if (!calculateCharge)
                        CalculationChargingVector();
                    if(attackAction && !attackExecuted)
                    {
                        velocity.Y = (1 - ((float)Math.Pow((double)(collisionBoxPosition.X + collisionBoxSize.X - ellipseXStartValue), 2 ) 
                                     / (float)Math.Pow((double)ellipseXValue,2))) * (float)Math.Pow((double)ellipseYValue,2);

                        if (ellipseYStartValue == collisionBoxPosition.Y + collisionBoxSize.Y / 2 && collisionBoxPosition.X + collisionBoxSize.X / 2 == ellipseXStartValue + 2 * ellipseXValue)
                        {
                            attackAction = false;
                            attackExecuted = true;
                            chargeCounter++;
                            if (chargeCounter == 3 && !bossRest)
                            {
                                bossRest = true;
                                attackExecuted = false;
                                bossLastPosition = new Vector2(collisionBoxPosition.X, collisionBoxPosition.Y);
                            }
                        }
                    }
                    phase1Route += velocity.X;
                   // collisionBoxPosition += velocity;
                    break;
                case 3:
                    if (phase1Route >= 1600)
                        facingDirectionRight = false;
                    else if (phase1Route <= 100)
                        facingDirectionRight = true;
                    if (facingDirectionRight)
                        velocity.X = 10;
                    else if (!facingDirectionRight)
                        velocity.X = -10;

                    if (!calculateCharge)
                        CalculationChargingVector();
                    if (attackAction && !attackExecuted && projectileList.Count() ==0)
                    {
                        velocity.Y = (1 - ((float)Math.Pow((double)(collisionBoxPosition.X + collisionBoxSize.X - ellipseXStartValue), 2)
                                     / (float)Math.Pow((double)ellipseXValue, 2))) * (float)Math.Pow((double)ellipseYValue, 2);

                        if (ellipseYStartValue == collisionBoxPosition.Y + collisionBoxSize.Y / 2 && collisionBoxPosition.X + collisionBoxSize.X / 2 == ellipseXStartValue + 2 * ellipseXValue)
                        {
                            attackAction = false;
                            attackExecuted = true;
                            chargeCounter++;
                            if (chargeCounter == 3 && !bossRest)
                            {
                                bossRest = true;
                                attackExecuted = false;
                                bossLastPosition = new Vector2(collisionBoxPosition.X, collisionBoxPosition.Y);
                            }
                        }
                    }
                    phase1Route += velocity.X;
                    break;
                case 4:
                    break;
            }
        }

        private void UpdateBossFightState()
        {
            if (enemyHP > 16)
                bossPhase = 1;
            else if (enemyHP > 12)
                bossPhase = 2;
            else if (enemyHP > 8)
                bossPhase = 3;
          
            //else if (bossPhase > 4)
            //    bossPhase = 4;
        }

        private void CalculationCooldown(GameTime gameTime)
        {
            attackCooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (attackCooldown > 3f)
            {
                attackCooldown = 0;
                attackExecuted = false;
                calculateCharge = false;
                attackAction = true;
            }
        }

        private void BossRestCooldown(GameTime gameTime)
        {
            restCooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (restCooldown > 6f)
            {
                restCooldown = 0;
                bossRest = false;
            }
        }
    }
}
