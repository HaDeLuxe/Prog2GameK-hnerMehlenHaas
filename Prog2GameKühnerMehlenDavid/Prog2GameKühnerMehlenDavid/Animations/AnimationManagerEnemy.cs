using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Reggie.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Animations {
    class AnimationManagerEnemy {

        protected Color color = Color.White;
        protected Vector2 scale = new Vector2(1, 1);
        public Enums.EnemyAnimations currentAnimation = Enums.EnemyAnimations.LADYBUG_FLY_LEFT;
        public Enums.EnemyAnimations nextAnimation = Enums.EnemyAnimations.LADYBUG_FLY_LEFT;
        public Enums.EnemyAnimations previousAnimation = Enums.EnemyAnimations.LADYBUG_FLY_LEFT;

        Dictionary<string, Animation> divAnimationDestRectanglesDic;

        Animation ladybug_Fly_Left = null;
        Animation ladybug_Fly_Right = null;
        Animation ladybug_Attack_Left = null;
        Animation ladybug_Attack_Right = null;

        Animation ant_Move_Left = null;
        Animation ant_Move_Right = null;
        Animation ant_Attack_Left = null;
        Animation ant_Attack_Right = null;

        Animation hawk_Fly_Left = null;
        Animation hawk_Fly_Right = null;
        Animation hawk_Attack_Left = null;
        Animation hawk_Attack_Right = null;

        Animation spider_Move_Left = null;
        Animation spider_Move_Right = null;
        Animation spider_Attack_Left = null;
        Animation spider_Attack_Right = null;

        Animation snail_Move_Left = null;
        Animation snail_Move_Right = null;
        Animation snail_Attack_Left = null;
        Animation snail_Attack_Right = null;
        Animation snail_Transf_Left = null;
        Animation snail_Transf_Right = null;
        Animation snail_Aggro_Left = null;
        Animation snail_Aggro_Right = null;


        public AnimationManagerEnemy(Dictionary<string, Texture2D> EnemySpriteSheetsDic) 
        {
            divAnimationDestRectanglesDic = new Dictionary<string, Animation>();
            ladybug_Fly_Left = new Animation(true, SpriteEffects.None, 99,65,EnemySpriteSheetsDic["Ladybug_Fly_Spritesheet"],25);
            divAnimationDestRectanglesDic.Add("Ladybug_Fly_Left", ladybug_Fly_Left);
            ladybug_Fly_Right = new Animation(true, SpriteEffects.FlipHorizontally, 99, 65, EnemySpriteSheetsDic["Ladybug_Fly_Spritesheet"], 25);
            divAnimationDestRectanglesDic.Add("Ladybug_Fly_Right", ladybug_Fly_Right);
            ladybug_Attack_Left = new Animation(false, SpriteEffects.None, 99, 65, EnemySpriteSheetsDic["Ladybug_Attack_Spritesheet"], 50);
            ladybug_Attack_Right = new Animation(false, SpriteEffects.FlipHorizontally, 99, 65, EnemySpriteSheetsDic["Ladybug_Attack_Spritesheet"], 50);
            ant_Move_Left = new Animation(true, SpriteEffects.None, 37, 64, EnemySpriteSheetsDic["antMovingSpriteSheet"], 25);
            ant_Move_Right = new Animation(true, SpriteEffects.FlipHorizontally, 37, 64, EnemySpriteSheetsDic["antMovingSpriteSheet"], 25);
            ant_Attack_Left = new Animation(false, SpriteEffects.None, 44, 64, EnemySpriteSheetsDic["antAttackSpriteSheet"], 50f);
            ant_Attack_Right = new Animation(false, SpriteEffects.FlipHorizontally, 44, 64, EnemySpriteSheetsDic["antAttackSpriteSheet"], 50f);
            hawk_Fly_Left = new Animation(true, SpriteEffects.None, 400, 392, EnemySpriteSheetsDic["hawkFlightSpriteSheet"], 25f);
            hawk_Fly_Right = new Animation(true, SpriteEffects.FlipHorizontally, 400, 392, EnemySpriteSheetsDic["hawkFlightSpriteSheet"], 25f);
            hawk_Attack_Left = new Animation(false, SpriteEffects.None, 400, 422, EnemySpriteSheetsDic["hawkAttackSpriteSheet"], 50f);
            hawk_Attack_Right = new Animation(false, SpriteEffects.FlipHorizontally, 400, 422, EnemySpriteSheetsDic["hawkAttackSpriteSheet"], 50f);
            spider_Move_Left = new Animation(true, SpriteEffects.None, 100, 50, EnemySpriteSheetsDic["spiderMovingSpriteSheet"], 25f);
            spider_Move_Right = new Animation(true, SpriteEffects.FlipHorizontally, 100, 50, EnemySpriteSheetsDic["spiderMovingSpriteSheet"], 25f);
            spider_Attack_Left = new Animation(false, SpriteEffects.None, 100, 64, EnemySpriteSheetsDic["spiderAttackSpriteSheet"], 50f);
            spider_Attack_Right = new Animation(false, SpriteEffects.FlipHorizontally, 100, 64, EnemySpriteSheetsDic["spiderAttackSpriteSheet"], 50f);
            snail_Move_Left = new Animation(true, SpriteEffects.None, 123, 75, EnemySpriteSheetsDic["snailMoveSpriteSheet"], 25f);
            snail_Move_Right = new Animation(true, SpriteEffects.FlipHorizontally, 123, 75, EnemySpriteSheetsDic["snailMoveSpriteSheet"], 25f);
            snail_Aggro_Left = new Animation(true, SpriteEffects.None, 123, 79, EnemySpriteSheetsDic["snailAggressiveSpriteSheet"], 25f);
            snail_Aggro_Right = new Animation(true, SpriteEffects.FlipHorizontally, 123, 75, EnemySpriteSheetsDic["snailAggressiveSpriteSheet"], 25f);
            snail_Attack_Left = new Animation(false, SpriteEffects.None, 123, 75, EnemySpriteSheetsDic["snailAttackSpriteSheet"], 50f);
            snail_Attack_Right = new Animation(false, SpriteEffects.FlipHorizontally, 123, 75, EnemySpriteSheetsDic["snailAttackSpriteSheet"], 50f);
            snail_Transf_Left = new Animation(false, SpriteEffects.None, 123, 75, EnemySpriteSheetsDic["snailTransfSpriteSheet"], 25f);
            snail_Transf_Right = new Animation(false, SpriteEffects.FlipHorizontally, 123, 75, EnemySpriteSheetsDic["snailTransfSpriteSheet"], 25f);
        }

        

        public void Animation(GameTime gameTime, Enemy enemy, SpriteBatch spriteBatch) 
        {
            if (enemy.invincibilityFrames)
            {
                if ((int)(enemy.invincibilityTimer / 0.40) % 2 == 1)
                    color = Color.Blue;
                else if ((int)(enemy.invincibilityTimer / 0.40) % 2 == 1)
                    color = Color.Red;
                else
                    color = Color.White;
            }
            else
                color = Color.White;
            if (currentAnimation == Enums.EnemyAnimations.LADYBUG_FLY_LEFT
                || currentAnimation == Enums.EnemyAnimations.LADYBUG_FLY_RIGHT
                || currentAnimation == Enums.EnemyAnimations.ANT_MOVE_LEFT
                || currentAnimation == Enums.EnemyAnimations.ANT_MOVE_RIGHT
                ||currentAnimation == Enums.EnemyAnimations.HAWK_FLY_LEFT
                || currentAnimation == Enums.EnemyAnimations.HAWK_FLY_RIGHT
                || currentAnimation == Enums.EnemyAnimations.SPIDER_MOVE_LEFT
                || currentAnimation == Enums.EnemyAnimations.SPIDER_MOVE_RIGHT
                || currentAnimation == Enums.EnemyAnimations.SNAIL_MOVE_LEFT
                || currentAnimation == Enums.EnemyAnimations.SNAIL_MOVE_RIGHT
                || currentAnimation == Enums.EnemyAnimations.SNAIL_AGGRO_LEFT
                || currentAnimation == Enums.EnemyAnimations.SNAIL_AGGRO_RIGHT)
            {
                currentAnimation = nextAnimation;
            }

            if(currentAnimation == Enums.EnemyAnimations.LADYBUG_ATTACK_LEFT
                || currentAnimation == Enums.EnemyAnimations.LADYBUG_ATTACK_RIGHT
                || currentAnimation == Enums.EnemyAnimations.ANT_ATTACK_LEFT
                || currentAnimation ==Enums.EnemyAnimations.ANT_ATTACK_RIGHT
                || currentAnimation == Enums.EnemyAnimations.HAWK_ATTACK_LEFT
                || currentAnimation == Enums.EnemyAnimations.HAWK_ATTACK_RIGHT
                || currentAnimation == Enums.EnemyAnimations.SPIDER_ATTACK_LEFT
                || currentAnimation == Enums.EnemyAnimations.SPIDER_ATTACK_RIGHT
                || currentAnimation == Enums.EnemyAnimations.SNAIL_TRANSF_LEFT
                || currentAnimation == Enums.EnemyAnimations.SNAIL_TRANSF_RIGHT
                || currentAnimation == Enums.EnemyAnimations.SNAIL_ATTACK_LEFT
                || currentAnimation == Enums.EnemyAnimations.SNAIL_ATTACK_RIGHT)
            {
                if(ladybug_Attack_Left.getPlayedOnce()
                    || ladybug_Attack_Right.getPlayedOnce()
                    || ant_Attack_Left.getPlayedOnce()
                    || ant_Attack_Right.getPlayedOnce()
                    || hawk_Attack_Left.getPlayedOnce()
                    || hawk_Attack_Right.getPlayedOnce()
                    || spider_Attack_Left.getPlayedOnce()
                    || spider_Attack_Right.getPlayedOnce()
                    || snail_Transf_Left.getPlayedOnce()
                    || snail_Transf_Right.getPlayedOnce()
                    || snail_Attack_Left.getPlayedOnce()
                    || snail_Attack_Right.getPlayedOnce())
                {
                    if(nextAnimation == Enums.EnemyAnimations.LADYBUG_ATTACK_LEFT
                        || nextAnimation == Enums.EnemyAnimations.LADYBUG_ATTACK_RIGHT)
                    
                        nextAnimation = previousAnimation;
                        currentAnimation = nextAnimation;

                    ladybug_Attack_Left.resetPlayedOnce();
                    ladybug_Attack_Right.resetPlayedOnce();
                    ant_Attack_Left.resetPlayedOnce();
                    ant_Attack_Right.resetPlayedOnce();
                    hawk_Attack_Left.resetPlayedOnce();
                    hawk_Attack_Right.resetPlayedOnce();
                    spider_Attack_Left.resetPlayedOnce();
                    spider_Attack_Right.resetPlayedOnce();
                    snail_Transf_Left.resetPlayedOnce();
                    snail_Transf_Right.resetPlayedOnce();
                    snail_Attack_Left.resetPlayedOnce();
                    snail_Attack_Right.resetPlayedOnce();                    
                }
            }

            Rectangle tempRec;
            switch (currentAnimation)
            {
                case Enums.EnemyAnimations.LADYBUG_FLY_LEFT:
                    enemy.changeTexture(ladybug_Fly_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Ladybug_Fly_Left"].Update(gameTime);
                    enemy.DrawSpriteBatch(spriteBatch, tempRec, ladybug_Fly_Left.getSpriteEffects(), new Vector2(0, -30),color, scale);
                    previousAnimation = Enums.EnemyAnimations.LADYBUG_FLY_LEFT;
                    break;
                case Enums.EnemyAnimations.LADYBUG_FLY_RIGHT:
                    enemy.changeTexture(ladybug_Fly_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Ladybug_Fly_Right"].Update(gameTime);
                    enemy.DrawSpriteBatch(spriteBatch, tempRec, ladybug_Fly_Right.getSpriteEffects(), new Vector2(0, -30), color,scale);
                    previousAnimation = Enums.EnemyAnimations.LADYBUG_FLY_RIGHT;
                    break;
                case Enums.EnemyAnimations.LADYBUG_ATTACK_LEFT:
                    enemy.changeTexture(ladybug_Attack_Left.texture);
                    tempRec = ladybug_Attack_Left.Update(gameTime);
                    enemy.DrawSpriteBatch(spriteBatch, tempRec, ladybug_Attack_Left.getSpriteEffects(), new Vector2(0, -15), color,scale);
                    break;
                case Enums.EnemyAnimations.LADYBUG_ATTACK_RIGHT:
                    enemy.changeTexture(ladybug_Attack_Right.texture);
                    tempRec = ladybug_Attack_Right.Update(gameTime);
                    enemy.DrawSpriteBatch(spriteBatch, tempRec, ladybug_Attack_Right.getSpriteEffects(), new Vector2(0, -15), color,scale);
                    break;
                case Enums.EnemyAnimations.SNAIL_ATTACK_RIGHT:
                    enemy.changeTexture(snail_Attack_Right.texture);
                    tempRec = snail_Attack_Right.Update(gameTime);
                    enemy.DrawSpriteBatch(spriteBatch, tempRec, snail_Attack_Right.getSpriteEffects(), new Vector2(0, -15), color, scale);
                    break;
                case Enums.EnemyAnimations.SNAIL_ATTACK_LEFT:
                    enemy.changeTexture(snail_Attack_Left.texture);
                    tempRec = snail_Attack_Left.Update(gameTime);
                    enemy.DrawSpriteBatch(spriteBatch, tempRec, snail_Attack_Left.getSpriteEffects(), new Vector2(0, -15), color, scale);
                    break;
                case Enums.EnemyAnimations.SNAIL_MOVE_LEFT:
                    enemy.changeTexture(snail_Move_Left.texture);
                    tempRec = snail_Move_Left.Update(gameTime);
                    enemy.DrawSpriteBatch(spriteBatch, tempRec, snail_Move_Left.getSpriteEffects(), new Vector2(0, -15), color, scale);
                    break;
                case Enums.EnemyAnimations.SNAIL_MOVE_RIGHT:
                    enemy.changeTexture(snail_Move_Right.texture);
                    tempRec = snail_Move_Right.Update(gameTime);
                    enemy.DrawSpriteBatch(spriteBatch, tempRec, snail_Move_Right.getSpriteEffects(), new Vector2(0, -15), color, scale);
                    break;
                case Enums.EnemyAnimations.SPIDER_ATTACK_RIGHT:
                    enemy.changeTexture(spider_Attack_Right.texture);
                    tempRec = spider_Attack_Right.Update(gameTime);
                    enemy.DrawSpriteBatch(spriteBatch, tempRec, spider_Attack_Right.getSpriteEffects(), new Vector2(0, -15), color, scale);
                    break;
                case Enums.EnemyAnimations.SPIDER_ATTACK_LEFT:
                    enemy.changeTexture(spider_Attack_Left.texture);
                    tempRec = spider_Attack_Left.Update(gameTime);
                    enemy.DrawSpriteBatch(spriteBatch, tempRec, spider_Attack_Left.getSpriteEffects(), new Vector2(0, -15), color, scale);
                    break;
                case Enums.EnemyAnimations.SPIDER_MOVE_LEFT:
                    enemy.changeTexture(spider_Move_Left.texture);
                    tempRec = spider_Move_Left.Update(gameTime);
                    enemy.DrawSpriteBatch(spriteBatch, tempRec, spider_Move_Left.getSpriteEffects(), new Vector2(0, -15), color, scale);
                    break;
                case Enums.EnemyAnimations.SPIDER_MOVE_RIGHT:
                    enemy.changeTexture(spider_Move_Right.texture);
                    tempRec = spider_Move_Right.Update(gameTime);
                    enemy.DrawSpriteBatch(spriteBatch, tempRec, spider_Move_Right.getSpriteEffects(), new Vector2(0, -15), color, scale);
                    break;
            }
        }


        public void NextAnimation(Enums.EnemyAnimations nextAnimation)
        {
            this.nextAnimation = nextAnimation;
        }
    }


}
