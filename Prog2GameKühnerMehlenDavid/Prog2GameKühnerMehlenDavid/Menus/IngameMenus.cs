using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Reggie.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Menus
{
    /// <summary>
    /// Contains the Tutorial Animations and the Save Animation
    /// </summary>
    public class IngameMenus
    {
        SpriteBatch spriteBatch { get; }
        Dictionary<string, Texture2D> texturesDictionary { get; }

        UIAnimations  saveAnimation;
        UIAnimations  rightTutPlayer;
        UIAnimations leftTutPlayer;
        UIAnimations jumpTutPlayer;
        UIAnimations floatingTutPlayer;
        UIAnimations attackTutPlayer;

       


        public IngameMenus(SpriteBatch spriteBatch, Dictionary<string, Texture2D> texturesDictionary, Dictionary<string, Texture2D> playerSpriteSheets)
        {
            this.spriteBatch = spriteBatch;
            this.texturesDictionary = texturesDictionary;

            saveAnimation = new UIAnimations(texturesDictionary, playerSpriteSheets);
            rightTutPlayer = new UIAnimations(texturesDictionary, playerSpriteSheets);
            rightTutPlayer.nextAnimation = UIAnimations.uiAnimations.Reggie_Move_Right;
            leftTutPlayer = new UIAnimations(texturesDictionary, playerSpriteSheets);
            leftTutPlayer.nextAnimation = UIAnimations.uiAnimations.Reggie_Move_Left;
            jumpTutPlayer = new UIAnimations(texturesDictionary, playerSpriteSheets);
            jumpTutPlayer.nextAnimation = UIAnimations.uiAnimations.Reggie_Jump;
            floatingTutPlayer = new UIAnimations(texturesDictionary, playerSpriteSheets);
            floatingTutPlayer.currentAnimation = UIAnimations.uiAnimations.Reggie_Float;
            attackTutPlayer = new UIAnimations(texturesDictionary, playerSpriteSheets);
            attackTutPlayer.nextAnimation = UIAnimations.uiAnimations.Reggie_Attack;
        }

       public void saveAnimStart()
       {
            saveAnimation.nextAnimation = UIAnimations.uiAnimations.Save;
       }

        public void drawSaveIcon(Vector2 playerPosition)
        {
            spriteBatch.Draw(texturesDictionary["SaveIcon"], new Vector2(playerPosition.X, playerPosition.Y -70), Color.White);
        }

        public void drawUpdate(GameTime gameTime, Matrix transformationMatrix, ref Player player)
        {
            saveAnimation.Animation(gameTime, spriteBatch, transformationMatrix);
            spriteBatch.Draw(texturesDictionary["moveTutorial"], new Vector2(13000, 1200), Color.White);
            rightTutPlayer.Animation(gameTime, spriteBatch, transformationMatrix);
            leftTutPlayer.Animation(gameTime, spriteBatch, transformationMatrix);
            jumpTutPlayer.Animation(gameTime, spriteBatch, transformationMatrix);
            spriteBatch.Draw(texturesDictionary["glideTutorial"], new Vector2(11950, 500), Color.White);
            floatingTutPlayer.Animation(gameTime, spriteBatch, transformationMatrix);
            attackTutPlayer.Animation(gameTime, spriteBatch, transformationMatrix);
            spriteBatch.Draw(texturesDictionary["attackTutorial"], new Vector2(9800, 700), Color.White);



        }





    }
}
