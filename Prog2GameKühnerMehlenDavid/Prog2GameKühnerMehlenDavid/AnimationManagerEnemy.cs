using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class AnimationManagerEnemy {


        public Enums.EnemyAnimations currentAnimation = Enums.EnemyAnimations.LADYBUG_FLY_LEFT;
        public Enums.EnemyAnimations nextAnimation = Enums.EnemyAnimations.LADYBUG_FLY_LEFT;
        private Enums.EnemyAnimations previousAnimation = Enums.EnemyAnimations.LADYBUG_FLY_LEFT;

        Dictionary<string, Animation> divAnimationDestRectanglesDic;

        Animation Ladybug_Fly_Left = null;
        Animation Ladybug_Fly_Right = null;


        public AnimationManagerEnemy(Dictionary<string, Texture2D> EnemySpriteSheetsDic) 
        {
            divAnimationDestRectanglesDic = new Dictionary<string, Animation>();
            //Ladybug
            Ladybug_Fly_Left = new Animation(true, SpriteEffects.None, 99,65,EnemySpriteSheetsDic["Ladybug_Fly_Spritesheet"],25);
            divAnimationDestRectanglesDic.Add("Ladybug_Fly_Left", Ladybug_Fly_Left);
            Ladybug_Fly_Right = new Animation(true, SpriteEffects.FlipHorizontally, 99, 65, EnemySpriteSheetsDic["Ladybug_Fly_Spritesheet"], 25);
            divAnimationDestRectanglesDic.Add("Ladybug_Fly_Right", Ladybug_Fly_Right);
        }

        public void Animation(GameTime gameTime, Enemy enemy, SpriteBatch spriteBatch) 
        {
            if(currentAnimation == Enums.EnemyAnimations.LADYBUG_FLY_LEFT
                || currentAnimation == Enums.EnemyAnimations.LADYBUG_FLY_RIGHT)
            {
                currentAnimation = nextAnimation;
            }

            Rectangle tempRec;
            switch (currentAnimation)
            {
                case Enums.EnemyAnimations.LADYBUG_FLY_LEFT:
                    enemy.changeTexture(Ladybug_Fly_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Ladybug_Fly_Left"].Update(gameTime);
                    enemy.DrawSpriteBatch(spriteBatch, tempRec, Ladybug_Fly_Left.getSpriteEffects(), new Vector2(0, -30));
                    break;
                case Enums.EnemyAnimations.LADYBUG_FLY_RIGHT:
                    enemy.changeTexture(Ladybug_Fly_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Ladybug_Fly_Right"].Update(gameTime);
                    enemy.DrawSpriteBatch(spriteBatch, tempRec, Ladybug_Fly_Right.getSpriteEffects(), new Vector2(0, -30));
                    break;

            }
        }


        public void NextAnimation(Enums.EnemyAnimations nextAnimation)
        {
            this.nextAnimation = nextAnimation;
        }
    }


}
