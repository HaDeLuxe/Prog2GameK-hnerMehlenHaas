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
            EnemyPositionCalculation(gameTime, gameObjectList);
            if (DetectPlayer() && !knockedBack)
                EnemyMovement();

        }
    }
}
