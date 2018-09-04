using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
   public class AnimationManager {
       
        public enum Animations{
            None,
            Walk_Left,
            Walk_Right,
            Jump_Left,
            Jump_Right,
            Attack_Left,
            Attack_Right,
            Walk_Hat_Left,
            Walk_Hat_Right,
            Jump_Hat_Left,
            Jump_Hat_Right,
            Attack_Hat_Left,
            Attack_Hat_Right,
            Walk_Armor_Left,
            Walk_Armor_Right,
            Walk_Armor_Hat_Left,
            Walk_Armor_Hat_Right,
            Jump_Armor_Left,
            Jump_Armor_Right,
            Jump_Armor_Hat_Left,
            Jump_Armor_Hat_Right,
            Attack_Armor_Left,
            Attack_Armor_Right,
            Attack_Armor_Hat_Left,
            Attack_Armor_Hat_Right,
            Floating_Left,
            Floating_Right};



        public static Animations currentAnimation = Animations.Walk_Right;
        public static Animations nextAnimation = Animations.Walk_Left;

        private Animations previousAnimation = Animations.Walk_Left;

        private Animations secondaryAnimation = Animations.Floating_Left;
        private Animations tertiaryAnimation = Animations.None;
        
        

        //DIVerse ANIMATION DESTination RECTANGLES DICtionnary
        Dictionary<string, Animation> divAnimationDestRectanglesDic;

        Animation Walk_Animation_Left = null;
        Animation Walk_Animation_Right = null;
        Animation Walk_Hat_Animation_Left = null;
        Animation Walk_Hat_Animation_Right = null;
        Animation Walk_Armor_Animation_Left = null;
        Animation Walk_Armor_Animation_Right = null;
        Animation Walk_Armor_Hat_Animation_Left = null;
        Animation Walk_Armor_Hat_Animation_Right = null;

        Animation Jump_Animation_Left = null;
        Animation Jump_Animation_Right = null;
        Animation Jump_Hat_Animation_Left = null;
        Animation Jump_Hat_Animation_Right = null;
        Animation Jump_Armor_Animation_Left = null;
        Animation Jump_Armor_Animation_Right = null;
        Animation Jump_Armor_Hat_Animation_Left = null;
        Animation Jump_Armor_Hat_Animation_Right = null;

        Animation Attack_Animation_Left = null;
        Animation Attack_Animation_Right = null;
        Animation Attack_Hat_Animation_Left = null;
        Animation Attack_Hat_Animation_Right = null;
        Animation Attack_Armor_Animation_Left = null;
        Animation Attack_Armor_Animation_Right = null;
        Animation Attack_Armor_Hat_Animation_Left = null;
        Animation Attack_Armor_Hat_Animation_Right = null;

        Animation floatingAnimation_Right = null;
        Animation floatingAnimation_Left = null;

        Animation walk_Umbrella_Empty_Animation_Left = null;
        Animation walk_Umbrella_Empty_Animation_Right = null;

        Animation Jump_Umbrella_Empty_Animation_Left = null;
        Animation Jump_Umbrella_Empty_Animation_Right = null;

        Animation Attack_Umbrella_Empty_Animation_Left = null;
        Animation Attack_Umbrella_Empty_Animation_Right = null;




        public AnimationManager(Dictionary<string, Texture2D> PlayerSpriteSheet) 
        {
            divAnimationDestRectanglesDic = new Dictionary<string, Animation>();
            //Walk Animations
            Walk_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Move_X"]/5, SpriteSheetSizes.spritesSizes["Reggie_Move_Y"]/5,PlayerSpriteSheet["playerMoveSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Animation_Left",Walk_Animation_Left);
            Walk_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Move_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Move_Y"] / 5, PlayerSpriteSheet["playerMoveSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Animation_Right", Walk_Animation_Right);
            Walk_Hat_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Move_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Move_Hat_Y"] / 5, PlayerSpriteSheet["playerMoveHatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Hat_Animation_Left", Walk_Hat_Animation_Left);
            Walk_Hat_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Move_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Move_Hat_Y"] / 5, PlayerSpriteSheet["playerMoveHatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Hat_Animation_Right", Walk_Hat_Animation_Right);
            Walk_Armor_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, 108, 87, PlayerSpriteSheet["playerMoveArmorSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Armor_Animation_Left", Walk_Armor_Animation_Left);
            Walk_Armor_Animation_Right = new Animation(true, SpriteEffects.None, 108, 87, PlayerSpriteSheet["playerMoveArmorSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Armor_Animation_Right", Walk_Armor_Animation_Right);
            Walk_Armor_Hat_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Move_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Move_Hat_Y"] / 5, PlayerSpriteSheet["playerMoveArmorHatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Armor_Hat_Animation_Left", Walk_Armor_Hat_Animation_Left);
            Walk_Armor_Hat_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Move_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Move_Hat_Y"] / 5, PlayerSpriteSheet["playerMoveArmorHatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Armor_Hat_Animation_Right", Walk_Armor_Hat_Animation_Right);
            //Jump Animations
            Jump_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Jump_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Jump_Y"] / 5, PlayerSpriteSheet["playerJumpSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Animation_Left", Jump_Animation_Left);
            Jump_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Jump_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Jump_Y"] / 5, PlayerSpriteSheet["playerJumpSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Animation_Right", Jump_Animation_Right);
            Jump_Hat_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Jump_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Jump_Hat_Y"] / 5, PlayerSpriteSheet["playerJumpHatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Hat_Animation_Left", Jump_Hat_Animation_Left);
            Jump_Hat_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Jump_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Jump_Hat_Y"] / 5, PlayerSpriteSheet["playerJumpHatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Hat_Animation_Right", Jump_Hat_Animation_Right);
            Jump_Armor_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Jump_Armor_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Jump_Armor_Y"] / 5, PlayerSpriteSheet["playerJumpArmorSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Armor_Animation_Left", Jump_Armor_Animation_Left);
            Jump_Armor_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Jump_Armor_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Jump_Armor_Y"] / 5, PlayerSpriteSheet["playerJumpArmorSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Armor_Animation_Right", Jump_Armor_Animation_Right);
            Jump_Armor_Hat_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Jump_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Jump_Hat_Y"] / 5, PlayerSpriteSheet["playerJumpArmorHatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Armor_Hat_Animation_Left", Jump_Armor_Hat_Animation_Left);
            Jump_Armor_Hat_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Jump_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Jump_Hat_Y"] / 5, PlayerSpriteSheet["playerJumpArmorHatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Armor_Hat_Animation_Right", Jump_Armor_Hat_Animation_Right);
            //Attack Animtions
            Attack_Animation_Left = new Animation(false, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Attack_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Attack_Y"] / 5, PlayerSpriteSheet["playerAttackSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Animation_Left", Attack_Animation_Left);
            Attack_Animation_Right = new Animation(false, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Attack_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Attack_Y"] / 5, PlayerSpriteSheet["playerAttackSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Animation_Right", Attack_Animation_Right);
            Attack_Hat_Animation_Left = new Animation(false, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_Y"] / 5, PlayerSpriteSheet["playerAttackHatSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Hat_Animation_Left", Attack_Hat_Animation_Left);
            Attack_Hat_Animation_Right = new Animation(false, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_Y"] / 5, PlayerSpriteSheet["playerAttackHatSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Hat_Animation_Right", Attack_Hat_Animation_Right);
            Attack_Armor_Animation_Left = new Animation(false, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_Y"] / 5, PlayerSpriteSheet["playerAttackArmorSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Armor_Animation_Left", Attack_Armor_Animation_Left);
            Attack_Armor_Animation_Right = new Animation(false, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_Y"] / 5, PlayerSpriteSheet["playerAttackArmorSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Armor_Animation_Right", Attack_Armor_Animation_Right);
            Attack_Armor_Hat_Animation_Left = new Animation(false, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_Y"] / 5, PlayerSpriteSheet["playerAttackArmorHatSpritesheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Armor_Hat_Animation_Left", Attack_Armor_Hat_Animation_Left);
            Attack_Armor_Hat_Animation_Right = new Animation(false, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_Y"] / 5, PlayerSpriteSheet["playerAttackArmorHatSpritesheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Armor_Hat_Animation_Right", Attack_Armor_Hat_Animation_Right);

            //Umbrella Animations
            Attack_Umbrella_Empty_Animation_Left = new Animation(false, SpriteEffects.FlipHorizontally, 115, 147, PlayerSpriteSheet["playerAttackUmbrellaEmptySpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Umbrella_Empty_Animation_Left", Attack_Umbrella_Empty_Animation_Left);
            Attack_Umbrella_Empty_Animation_Right = new Animation(false, SpriteEffects.None, 115, 147, PlayerSpriteSheet["playerAttackUmbrellaEmptySpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Umbrella_Empty_Animation_Right", Attack_Umbrella_Empty_Animation_Right);

            walk_Umbrella_Empty_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, 75, 106, PlayerSpriteSheet["playerMoveUmbrellaEmptySpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Umbrella_Empty_Animation_Left", walk_Umbrella_Empty_Animation_Left);
            walk_Umbrella_Empty_Animation_Right = new Animation(true, SpriteEffects.None, 75, 106, PlayerSpriteSheet["playerMoveUmbrellaEmptySpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Umbrella_Empty_Animation_Right", walk_Umbrella_Empty_Animation_Right);

            Jump_Umbrella_Empty_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, 48, 95, PlayerSpriteSheet["playerJumpUmbrellaEmptySpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Umbrella_Empty_Animation_Left", Jump_Umbrella_Empty_Animation_Left);
            Jump_Umbrella_Empty_Animation_Right = new Animation(true, SpriteEffects.None, 48, 95, PlayerSpriteSheet["playerJumpUmbrellaEmptySpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Umbrella_Empty_Animation_Right", Jump_Umbrella_Empty_Animation_Right);


            //Floating Animation
            floatingAnimation_Right = new Animation(true, SpriteEffects.None, 79, 93, PlayerSpriteSheet["playerFloatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Float_Animation_Right", floatingAnimation_Right);
            floatingAnimation_Left = new Animation(true, SpriteEffects.FlipHorizontally, 79, 93, PlayerSpriteSheet["playerFloatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Float_Animation_Left", floatingAnimation_Left);





        }

        

        public void animation(GameTime gameTime, ref Player player, SpriteBatch spriteBatch) 
        {
            

            if (currentAnimation == Animations.Jump_Left
                                           || currentAnimation == Animations.Jump_Right
                                           || currentAnimation == Animations.Walk_Left
                                           || currentAnimation == Animations.Walk_Right
                                           || currentAnimation == Animations.Walk_Hat_Left
                                           || currentAnimation == Animations.Walk_Hat_Right
                                           || currentAnimation == Animations.Walk_Armor_Left
                                           || currentAnimation == Animations.Walk_Armor_Right
                                           || currentAnimation == Animations.Walk_Armor_Hat_Left
                                           || currentAnimation == Animations.Walk_Armor_Hat_Right
                                           || currentAnimation == Animations.Jump_Hat_Left
                                           || currentAnimation == Animations.Jump_Hat_Right
                                           || currentAnimation == Animations.Jump_Armor_Left
                                           || currentAnimation == Animations.Jump_Armor_Right
                                           || currentAnimation == Animations.Jump_Armor_Hat_Left
                                           || currentAnimation == Animations.Jump_Armor_Hat_Right)

            {
                currentAnimation = nextAnimation;
            }
            if(currentAnimation == Animations.Attack_Left 
                || currentAnimation == Animations.Attack_Right
                || currentAnimation == Animations.Attack_Hat_Left
                || currentAnimation == Animations.Attack_Hat_Right
                || currentAnimation == Animations.Attack_Armor_Left
                || currentAnimation == Animations.Attack_Armor_Right
                || currentAnimation == Animations.Attack_Armor_Hat_Left
                || currentAnimation == Animations.Attack_Armor_Hat_Right)
            {
                if (Attack_Animation_Right.getPlayedOnce()
                    || Attack_Animation_Left.getPlayedOnce()
                    || Attack_Hat_Animation_Left.getPlayedOnce()
                    || Attack_Hat_Animation_Right.getPlayedOnce()
                    || Attack_Armor_Animation_Left.getPlayedOnce()
                    || Attack_Armor_Animation_Right.getPlayedOnce()
                    || Attack_Armor_Hat_Animation_Left.getPlayedOnce()
                    || Attack_Armor_Hat_Animation_Right.getPlayedOnce())
                {
                    if(nextAnimation == Animations.Attack_Left 
                        || nextAnimation == Animations.Attack_Right
                        || nextAnimation == Animations.Attack_Hat_Left
                        || nextAnimation == Animations.Attack_Hat_Right
                        || nextAnimation == Animations.Attack_Armor_Left
                        || nextAnimation == Animations.Attack_Armor_Right
                        || nextAnimation == Animations.Attack_Armor_Hat_Left
                        || nextAnimation == Animations.Attack_Armor_Hat_Right)


                    nextAnimation = previousAnimation;
                    currentAnimation = nextAnimation;

                    Attack_Animation_Left.resetPlayedOnce();
                    Attack_Animation_Right.resetPlayedOnce();
                    Attack_Hat_Animation_Left.resetPlayedOnce();
                    Attack_Hat_Animation_Right.resetPlayedOnce();
                    Attack_Armor_Animation_Left.resetPlayedOnce();
                    Attack_Armor_Animation_Right.resetPlayedOnce();
                    Attack_Armor_Hat_Animation_Left.resetPlayedOnce();
                    Attack_Armor_Hat_Animation_Right.resetPlayedOnce();
                }
            }


            Rectangle tempRec;
            Rectangle tempRec2;
            switch (currentAnimation)
            {
                case Animations.Walk_Right:
                    player.changeTexture(Walk_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Walk_Animation_Right.getSpriteEffects(),new Vector2(0,0));
                    previousAnimation = Animations.Walk_Right;
                    break;
                case Animations.Walk_Left:
                    player.changeTexture(Walk_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Walk_Animation_Left.getSpriteEffects(),new Vector2(0,0));
                    previousAnimation = Animations.Walk_Left;
                    break;
                case Animations.Walk_Hat_Left:
                    player.changeTexture(Walk_Hat_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Hat_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Walk_Hat_Animation_Left.getSpriteEffects(), new Vector2(0, -20));
                    previousAnimation = Animations.Walk_Hat_Left;
                    break;
                case Animations.Walk_Hat_Right:
                    player.changeTexture(Walk_Hat_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Hat_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Walk_Hat_Animation_Right.getSpriteEffects(), new Vector2(0, -20));
                    previousAnimation = Animations.Walk_Hat_Right;
                    break;
                case Animations.Walk_Armor_Left:
                    player.changeTexture(Walk_Armor_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Armor_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Walk_Armor_Animation_Left.getSpriteEffects(), new Vector2(0, 0));
                    previousAnimation = Animations.Walk_Armor_Left;
                    break;
                case Animations.Walk_Armor_Right:
                    player.changeTexture(Walk_Armor_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Armor_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Walk_Armor_Animation_Right.getSpriteEffects(), new Vector2(0, 0));
                    previousAnimation = Animations.Walk_Armor_Right;
                    break;
                case Animations.Walk_Armor_Hat_Left:
                    player.changeTexture(Walk_Armor_Hat_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Armor_Hat_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Walk_Armor_Hat_Animation_Left.getSpriteEffects(), new Vector2(0, -20));
                    previousAnimation = Animations.Walk_Armor_Hat_Left;
                    break;
                case Animations.Walk_Armor_Hat_Right:
                    player.changeTexture(Walk_Armor_Hat_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Armor_Hat_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Walk_Armor_Hat_Animation_Right.getSpriteEffects(), new Vector2(0, -20));
                    previousAnimation = Animations.Walk_Armor_Hat_Right;
                    break;


                case Animations.Jump_Right:
                    player.changeTexture(Jump_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Jump_Animation_Right.getSpriteEffects(), new Vector2(35,-20));
                    previousAnimation = Animations.Jump_Right;
                    break;
                case Animations.Jump_Left:
                    player.changeTexture(Jump_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Jump_Animation_Left.getSpriteEffects(), new Vector2(15, -20));
                    previousAnimation = Animations.Jump_Left;
                    break;
                case Animations.Jump_Hat_Left:
                    player.changeTexture(Jump_Hat_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Hat_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Jump_Hat_Animation_Left.getSpriteEffects(), new Vector2(15, -35));
                    previousAnimation = Animations.Jump_Hat_Left;
                    break;
                case Animations.Jump_Hat_Right:
                    player.changeTexture(Jump_Hat_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Hat_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Jump_Hat_Animation_Right.getSpriteEffects(), new Vector2(22, -35));
                    previousAnimation = Animations.Jump_Hat_Right;
                    break;
                case Animations.Jump_Armor_Left:
                    player.changeTexture(Jump_Armor_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Armor_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Jump_Armor_Animation_Left.getSpriteEffects(), new Vector2(22, -22));
                    previousAnimation = Animations.Jump_Armor_Left;
                    break;
                case Animations.Jump_Armor_Right:
                    player.changeTexture(Jump_Armor_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Armor_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Jump_Armor_Animation_Right.getSpriteEffects(), new Vector2(34, -22));
                    previousAnimation = Animations.Jump_Armor_Right;
                    break;
                case Animations.Jump_Armor_Hat_Left:
                    player.changeTexture(Jump_Armor_Hat_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Armor_Hat_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Jump_Armor_Hat_Animation_Left.getSpriteEffects(), new Vector2(22, -35));
                    previousAnimation = Animations.Jump_Armor_Hat_Left;
                    break;
                case Animations.Jump_Armor_Hat_Right:
                    player.changeTexture(Jump_Armor_Hat_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Armor_Hat_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Jump_Armor_Hat_Animation_Right.getSpriteEffects(), new Vector2(22, -35));
                    previousAnimation = Animations.Jump_Armor_Hat_Right;
                    break;





                case Animations.Attack_Left:
                    player.changeTexture(Attack_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Attack_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Attack_Animation_Left.getSpriteEffects(), new Vector2(-15, 0));
                    break;
                case Animations.Attack_Right:
                    player.changeTexture(Attack_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Attack_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Attack_Animation_Right.getSpriteEffects(), new Vector2(32, 0));
                    break;
                case Animations.Attack_Hat_Left:
                    player.changeTexture(Attack_Hat_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Attack_Hat_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Attack_Hat_Animation_Left.getSpriteEffects(), new Vector2(-25, 0));
                    break;
                case Animations.Attack_Hat_Right:
                    player.changeTexture(Attack_Hat_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Attack_Hat_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Attack_Hat_Animation_Right.getSpriteEffects(), new Vector2(32, 0));
                    break;
                case Animations.Attack_Armor_Left:
                    player.changeTexture(Attack_Armor_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Attack_Armor_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Attack_Armor_Animation_Left.getSpriteEffects(), new Vector2(-25, 0));
                    break;
                case Animations.Attack_Armor_Right:
                    player.changeTexture(Attack_Armor_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Attack_Armor_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Attack_Armor_Animation_Right.getSpriteEffects(), new Vector2(32, 0));
                    break;
                case Animations.Attack_Armor_Hat_Left:
                    player.changeTexture(Attack_Armor_Hat_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Attack_Armor_Hat_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Attack_Armor_Hat_Animation_Left.getSpriteEffects(), new Vector2(-15, -20));
                    break;
                case Animations.Attack_Armor_Hat_Right:
                    player.changeTexture(Attack_Armor_Hat_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Attack_Armor_Hat_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, Attack_Armor_Hat_Animation_Right.getSpriteEffects(), new Vector2(32, -20));
                    break;

            }

            if(currentAnimation == Animations.Attack_Right || currentAnimation == Animations.Attack_Hat_Right || currentAnimation == Animations.Attack_Armor_Right || currentAnimation == Animations.Attack_Armor_Hat_Right)
            {
                player.changeSecondTexture(Attack_Umbrella_Empty_Animation_Right.texture);
                tempRec2 = divAnimationDestRectanglesDic["Attack_Umbrella_Empty_Animation_Right"].Update(gameTime);
                player.drawSecondTexture(spriteBatch, tempRec2, Attack_Umbrella_Empty_Animation_Right.getSpriteEffects(), new Vector2(64, -33));
            }

            if (currentAnimation == Animations.Attack_Left || currentAnimation == Animations.Attack_Hat_Left || currentAnimation == Animations.Attack_Armor_Left || currentAnimation == Animations.Attack_Armor_Hat_Left)
            {
                player.changeSecondTexture(Attack_Umbrella_Empty_Animation_Left.texture);
                tempRec2 = divAnimationDestRectanglesDic["Attack_Umbrella_Empty_Animation_Left"].Update(gameTime);
                player.drawSecondTexture(spriteBatch, tempRec2, Attack_Umbrella_Empty_Animation_Left.getSpriteEffects(), new Vector2(-71, -33));
            }
            if(currentAnimation == Animations.Walk_Right || currentAnimation == Animations.Walk_Hat_Right || currentAnimation == Animations.Walk_Armor_Right || currentAnimation == Animations.Walk_Armor_Hat_Right)
            {
                player.changeSecondTexture(walk_Umbrella_Empty_Animation_Right.texture);
                tempRec2 = divAnimationDestRectanglesDic["Walk_Umbrella_Empty_Animation_Right"].Update(gameTime);
                player.drawSecondTexture(spriteBatch, tempRec2, walk_Umbrella_Empty_Animation_Right.getSpriteEffects(), new Vector2(78, -22));
            }
            if(currentAnimation == Animations.Walk_Left || currentAnimation == Animations.Walk_Hat_Left || currentAnimation == Animations.Walk_Armor_Left || currentAnimation == Animations.Walk_Armor_Hat_Left)
            {
                player.changeSecondTexture(walk_Umbrella_Empty_Animation_Left.texture);
                tempRec2 = divAnimationDestRectanglesDic["Walk_Umbrella_Empty_Animation_Left"].Update(gameTime);
                player.drawSecondTexture(spriteBatch, tempRec2, walk_Umbrella_Empty_Animation_Left.getSpriteEffects(), new Vector2(-45, -20));
            }
            if (currentAnimation == Animations.Jump_Right || currentAnimation == Animations.Jump_Hat_Right || currentAnimation == Animations.Jump_Armor_Right || currentAnimation == Animations.Jump_Armor_Hat_Right)
            {
                if (!player.isFloating)
                {
                    player.changeSecondTexture(Jump_Umbrella_Empty_Animation_Right.texture);
                    tempRec2 = divAnimationDestRectanglesDic["Jump_Umbrella_Empty_Animation_Right"].Update(gameTime);
                    player.drawSecondTexture(spriteBatch, tempRec2, Jump_Umbrella_Empty_Animation_Right.getSpriteEffects(), new Vector2(58,-63));
                }
                else
                {
                    player.changeSecondTexture(floatingAnimation_Right.texture);
                    tempRec2 = divAnimationDestRectanglesDic["Float_Animation_Right"].Update(gameTime);
                    player.drawSecondTexture(spriteBatch, tempRec2, floatingAnimation_Right.getSpriteEffects(), new Vector2(53, -63));
                }
            }
            if(currentAnimation == Animations.Jump_Left || currentAnimation == Animations.Jump_Hat_Left || currentAnimation == Animations.Jump_Armor_Left || currentAnimation == Animations.Jump_Armor_Hat_Left)
            {
                if (!player.isFloating)
                {
                    player.changeSecondTexture(Jump_Umbrella_Empty_Animation_Left.texture);
                    tempRec2 = divAnimationDestRectanglesDic["Jump_Umbrella_Empty_Animation_Left"].Update(gameTime);
                    player.drawSecondTexture(spriteBatch, tempRec2, Jump_Umbrella_Empty_Animation_Left.getSpriteEffects(), new Vector2(0, -60));
                }
                else
                {
                    player.changeSecondTexture(floatingAnimation_Left.texture);
                    tempRec2 = divAnimationDestRectanglesDic["Float_Animation_Left"].Update(gameTime);
                    player.drawSecondTexture(spriteBatch, tempRec2, floatingAnimation_Left.getSpriteEffects(), new Vector2(-24, -60));
                }
            }

        }

        //switch (secondaryAnimation)
        //{
        //    case Animations.None:
        //        break;
        //    case Animations.Floating_Left:
        //        player.changeSecondTexture(floatingAnimation_Left.texture);
        //        tempRec = divAnimationDestRectanglesDic["Float_Animation_Left"].Update(gameTime);
        //        player.drawSecondTexture(spriteBatch, tempRec, floatingAnimation_Left.getSpriteEffects(), new Vector2(0, 0));
        //        break;
        //    case Animations.Floating_Right:
        //        player.changeSecondTexture(floatingAnimation_Right.texture);
        //        tempRec = divAnimationDestRectanglesDic["Float_Animation_Right"].Update(gameTime);
        //        player.drawSecondTexture(spriteBatch, tempRec, floatingAnimation_Right.getSpriteEffects(), new Vector2(0, 0));
        //        break;
        //}
    }

        
        
    
}
