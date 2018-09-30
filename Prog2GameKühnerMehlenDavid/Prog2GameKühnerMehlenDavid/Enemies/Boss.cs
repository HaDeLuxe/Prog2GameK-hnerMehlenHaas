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
    /// <summary>
    /// Contains logic for the boss fight with Hakume
    /// Contains moveset and attackmoveset
    /// </summary>
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
        private int changeDirection;
        private Vector2 bossLastPosition;
        private int chargeCounter;
        private bool bossRest;
        private float restCooldown;
        private float projectileSpawnCooldown;

        public Boss(Texture2D enemyTexture, Vector2 enemySize, Vector2 enemyPosition, int gameObjectID, Dictionary<string, Texture2D> EnemySpriteSheetsDic) : base(enemyTexture, enemySize, enemyPosition, gameObjectID, EnemySpriteSheetsDic)
        {
            enemyHP = 100;
            
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
            changeDirection = 0;
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
            if(projectileList.Count != 0 && bossPhase == 1)
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
            if (bossPhase == 3 && projectileList.Count() != 0)
            {
                
                foreach (var projectile in projectileList.ToList())
                {
                   //projectile.Launch(true);
                   projectile.TracedPlayerLocation();
                    //projectile.BossOriginLocation(collisionBoxPosition.X + collisionBoxSize.X / 2, collisionBoxPosition.Y + collisionBoxSize.Y / 2);
                    projectile.Update(gameTime, gameObjectList);
                    if (!projectile.ProjectileState())
                        projectileList.RemoveAt(projectileList.IndexOf(projectile));
                }
            }
            
            EnemyPositionCalculation(gameTime);
        }

        private void BossRestLocation()
        {
            if(bossPhase == 1)
            {
                collisionBoxPosition.X = -4477;
                collisionBoxPosition.Y = -10253;
                changeDirection = 0;
            }
            else if (bossPhase == 2)
            {
                collisionBoxPosition.X = -3327;
                collisionBoxPosition.Y = -10941;
            }
            else if(bossPhase == 3)
            {
              
                collisionBoxPosition.X = -3902;
                collisionBoxPosition.Y = -11361;
            }
        }

        public override void EnemyAnimationUpdate(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!facingDirectionRight /*&& !attackAction*/)
                animationManager.nextAnimation = Enums.EnemyAnimations.HAWK_FLY_LEFT;
            else if (facingDirectionRight /*&& !attackAction*/)
                animationManager.nextAnimation = Enums.EnemyAnimations.HAWK_FLY_RIGHT;
            //else if (!facingDirectionRight && calculateCharge)
            //    animationManager.nextAnimation = Enums.EnemyAnimations.HAWK_ATTACK_LEFT;
            //else if (facingDirectionRight && calculateCharge)
            //    animationManager.nextAnimation = Enums.EnemyAnimations.HAWK_ATTACK_RIGHT;
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
                    if (projectileList.Count() <= 50)
                    {
                        projectileList.Add(new Projectile(null, new Vector2(100, 100), new Vector2(collisionBoxPosition.X + collisionBoxSize.X / 2 + 200, collisionBoxPosition.Y + collisionBoxSize.Y / 2), (int)Enums.ObjectsID.BOSS));
                        projectileList.Last().SetPlayer(worm);
                        projectileList.Last().SetBossPhase(bossPhase);
                    }
                  
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
            //if (collisionBoxPosition.X + collisionBoxSize.X / 2 <= worm.collisionBoxPosition.X + worm.collisionBoxSize.X / 2)
            //    ellipseXValue = worm.collisionBoxPosition.X + worm.collisionBoxSize.X / 2 - (collisionBoxPosition.X + collisionBoxSize.X / 2);
            //else
            //    ellipseXValue = collisionBoxPosition.X + collisionBoxSize.X / 2 - (worm.collisionBoxPosition.X + worm.collisionBoxSize.X / 2);
            //ellipseYValue = worm.collisionBoxPosition.Y + worm.collisionBoxSize.Y / 2 - (collisionBoxPosition.Y + collisionBoxSize.Y / 2);
            //ellipseYStartValue = collisionBoxPosition.Y + collisionBoxSize.Y / 2;
            //ellipseXStartValue = collisionBoxPosition.X + collisionBoxSize.X / 2;
        

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
                    chargingVector.X = chargingVector.X / Math.Abs(chargingVector.Y) * 9;
                if ((worm.collisionRectangle.Y / 60 - collisionRectangle.Y / 60) > 0)
                    chargingVector.Y = 9;
                else
                    chargingVector.Y = -9;
            }
            else if (Math.Abs(worm.collisionRectangle.X / 60 - collisionRectangle.X / 60) >= Math.Abs(worm.collisionRectangle.Y / 60 - collisionRectangle.Y / 60) && worm.collisionRectangle.X / 60 - collisionRectangle.X / 60 != 0)
            {
                chargingVector.X = worm.collisionRectangle.X / 60 - collisionRectangle.X / 60;
                chargingVector.Y = worm.collisionRectangle.Y / 60 - collisionRectangle.Y / 60;
                if ((worm.collisionRectangle.X / 60 - collisionRectangle.X / 60) != 0)
                    chargingVector.Y = chargingVector.Y / Math.Abs(chargingVector.X) * 9;
                if ((worm.collisionRectangle.X / 60 - collisionRectangle.X / 60) > 0)
                    chargingVector.X = 9;
                else
                    chargingVector.X = -9;
            }
            velocity = chargingVector;
            calculateCharge = true;
        }

        public override void DrawProjectile(SpriteBatch spriteBatch, Color color)
        {
            if(bossPhase ==1)
            foreach (var projectile in projectileList)
            {
                spriteBatch.Draw(EnemySpriteSheetsDic["egg"], new Vector2(projectile.collisionRectangle.Left, projectile.collisionRectangle.Top), color);
            }
            else if(bossPhase == 3)
            foreach (var projectile in projectileList)
            {
                    spriteBatch.Draw(EnemySpriteSheetsDic["feather"], new Vector2(projectile.collisionRectangle.Left, projectile.collisionRectangle.Top), color);
            }
        }

        private void BossMovement()
        {
            switch (bossPhase)
            {
                case 1:
                    if (phase1Route >= 1600)
                    {
                        facingDirectionRight = false;
                        changeDirection++;

                    }
                    else if (phase1Route <= 100)
                    {
                        facingDirectionRight = true;
                        changeDirection++;
                    }
                    if (changeDirection == 6)
                    {
                        bossRest = true;
                        bossLastPosition = new Vector2(collisionBoxPosition.X, collisionBoxPosition.Y);
                    }
                    if (facingDirectionRight)
                        velocity.X = 30;
                    else if (!facingDirectionRight)
                        velocity.X = -30;
                    phase1Route += velocity.X;
                    //collisionBoxPosition.X += velocity.X;
                    break;
                case 2:
                    if (phase1Route >= 1300)
                        facingDirectionRight = false;
                    else if (phase1Route <= 300)
                        facingDirectionRight = true;
                    if (facingDirectionRight)
                        velocity.X += 10;
                    else if (!facingDirectionRight)
                        velocity.X -= 10;

                    if (!calculateCharge)
                        CalculationChargingVector();
                    if(attackAction && !attackExecuted)
                    {
                        if (ellipseYValue - velocity.Y <= 0)
                            velocity.Y = -velocity.Y;
                        ellipseYValue += velocity.Y;
                        if (ellipseYValue >= 1737)
                            velocity.Y = -velocity.Y;
                        if (ellipseYValue == 0)
                        {
                            attackAction = false;
                            attackExecuted = true;
                            chargeCounter++;
                            calculateCharge = false;
                            if (chargeCounter == 3 && !bossRest)
                            {
                                bossRest = true;
                                attackExecuted = false;
                                bossLastPosition = new Vector2(collisionBoxPosition.X, collisionBoxPosition.Y);
                            }
                        }
                    }
                    phase1Route += velocity.X;
                   collisionBoxPosition += velocity;
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
                    if (attackAction && !attackExecuted)
                    {
                        if (ellipseYValue - velocity.Y <= 0)
                            velocity.Y = -velocity.Y;

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
            if (enemyHP > 75)
                bossPhase = 1;
            else if (enemyHP > 50)
                bossPhase = 2;
            else if (enemyHP > 25)
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
                collisionBoxPosition.X = bossLastPosition.X;
                collisionBoxPosition.Y = bossLastPosition.Y;
            }
        }

        public void drawHealthBar(SpriteBatch spriteBatch, Dictionary<string, Texture2D> texturesDictionary)
        {
            if (enemyHP < 100)
            {
                spriteBatch.Draw(texturesDictionary["enemyHBBorder"], this.gameObjectPosition + new Vector2(32, -50), Color.White);
                spriteBatch.Draw(texturesDictionary["enemyHBFill"], this.gameObjectPosition + new Vector2(32, -50), null, Color.White, 0, Vector2.Zero, new Vector2(enemyHP / 100, 1), SpriteEffects.None, 0);
            }
            base.drawHealthBar(spriteBatch, texturesDictionary);
        }
    }
}
