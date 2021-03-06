﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Reggie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Animations {
    /// <summary>
    /// Contains all animations for player.
    /// Defines them every single animation.
    /// Draws the current player animation.
    /// To call an animation, set next "nextAnimation" to Animatoins Enum.
    /// </summary>
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
            Floating_Right,
            Jump_Shovel,
            Jump_Scissors,
            Jump_Golden,
            Walk_Shovel,
            Walk_Scissors,
            Walk_Golden,
            Attack_Shovel,
            Attack_Scissors,
            Attack_Golden,
            Float_Shovel,
            Float_Scissors,
            Float_Golden2};


        public static Animations currentAnimation = Animations.Walk_Right;
        public static Animations nextAnimation = Animations.Walk_Left;
        protected Color color = Color.White;
        protected Vector2 scale = new Vector2(1,1);
        private Animations previousAnimation = Animations.Walk_Left;

        //DIVerse ANIMATION DESTination RECTANGLES DICtionnary
        Dictionary<string, Animation> divAnimationDestRectanglesDic;

        Animation walk_Animation_Left = null;
        Animation walk_Animation_Right = null;
        Animation walk_Hat_Animation_Left = null;
        Animation walk_Hat_Animation_Right = null;
        Animation walk_Armor_Animation_Left = null;
        Animation walk_Armor_Animation_Right = null;
        Animation walk_Armor_Hat_Animation_Left = null;
        Animation walk_Armor_Hat_Animation_Right = null;

        Animation jump_Animation_Left = null;
        Animation jump_Animation_Right = null;
        Animation jump_Hat_Animation_Left = null;
        Animation jump_Hat_Animation_Right = null;
        Animation jump_Armor_Animation_Left = null;
        Animation jump_Armor_Animation_Right = null;
        Animation jump_Armor_Hat_Animation_Left = null;
        Animation jump_Armor_Hat_Animation_Right = null;

        Animation attack_Animation_Left = null;
        Animation attack_Animation_Right = null;
        Animation attack_Hat_Animation_Left = null;
        Animation attack_Hat_Animation_Right = null;
        Animation attack_Armor_Animation_Left = null;
        Animation attack_Armor_Animation_Right = null;
        Animation attack_Armor_Hat_Animation_Left = null;
        Animation attack_Armor_Hat_Animation_Right = null;

        Animation floatingAnimation_Right = null;
        Animation floatingAnimation_Left = null;
        Animation floatingGoldenAnimation_Right = null;
        Animation floatingGoldenAnimation_Left = null;

        Animation walk_Umbrella_Empty_Animation_Left = null;
        Animation walk_Umbrella_Empty_Animation_Right = null;
        Animation walk_Umbrella_Golden_Animation_Left = null;
        Animation walk_Umbrella_Golden_Animation_Right = null;

        Animation jump_Umbrella_Empty_Animation_Left = null;
        Animation jump_Umbrella_Empty_Animation_Right = null;
        Animation jump_Umbrella_Golden_Animation_Left = null;
        Animation jump_Umbrella_Golden_Animation_Right = null;
       

        Animation attack_Umbrella_Empty_Animation_Left = null;
        Animation attack_Umbrella_Empty_Animation_Right = null;
        Animation attack_Umbrella_Golden_Animation_Left = null;
        Animation attack_Umbrella_Golden_Animation_Right = null;

        Animation walk_Shovel_Animation_Left = null;
        Animation walk_Shovel_Animation_Right = null;
        Animation jump_Shovel_Animation_Left = null;
        Animation jump_Shovel_Animation_Right = null;
        Animation attack_Shovel_Animation_Left = null;
        Animation attack_Shovel_Animation_Right = null;
        Animation float_Shovel_Animation_Left = null;
        Animation float_Shovel_Animation_Right = null;

        Animation walk_Scissors_Animation_Left = null;
        Animation walk_Scissors_Animation_Right = null;
        Animation jump_Scissors_Animation_Left = null;
        Animation jump_Scissors_Animation_Right = null;
        Animation attack_Scissors_Animation_Left = null;
        Animation attack_Scissors_Animation_Right = null;
        Animation float_Scissors_Animation_Left = null;
        Animation float_Scissors_Animation_Right = null;

        //MUSIC
        AudioManager audioManager;


        public AnimationManager(Dictionary<string, Texture2D> playerSpriteSheet) 
        {
            divAnimationDestRectanglesDic = new Dictionary<string, Animation>();
            //Walk Animations
            walk_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Move_X"]/5, SpriteSheetSizes.spritesSizes["Reggie_Move_Y"]/5,playerSpriteSheet["playerMoveSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Animation_Left",walk_Animation_Left);
            walk_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Move_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Move_Y"] / 5, playerSpriteSheet["playerMoveSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Animation_Right", walk_Animation_Right);
            walk_Hat_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Move_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Move_Hat_Y"] / 5, playerSpriteSheet["playerMoveHatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Hat_Animation_Left", walk_Hat_Animation_Left);
            walk_Hat_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Move_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Move_Hat_Y"] / 5, playerSpriteSheet["playerMoveHatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Hat_Animation_Right", walk_Hat_Animation_Right);
            walk_Armor_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, 108, 87, playerSpriteSheet["playerMoveArmorSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Armor_Animation_Left", walk_Armor_Animation_Left);
            walk_Armor_Animation_Right = new Animation(true, SpriteEffects.None, 108, 87, playerSpriteSheet["playerMoveArmorSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Armor_Animation_Right", walk_Armor_Animation_Right);
            walk_Armor_Hat_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Move_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Move_Hat_Y"] / 5, playerSpriteSheet["playerMoveArmorHatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Armor_Hat_Animation_Left", walk_Armor_Hat_Animation_Left);
            walk_Armor_Hat_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Move_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Move_Hat_Y"] / 5, playerSpriteSheet["playerMoveArmorHatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Armor_Hat_Animation_Right", walk_Armor_Hat_Animation_Right);
            //Jump Animations
            jump_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Jump_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Jump_Y"] / 5, playerSpriteSheet["playerJumpSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Animation_Left", jump_Animation_Left);
            jump_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Jump_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Jump_Y"] / 5, playerSpriteSheet["playerJumpSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Animation_Right", jump_Animation_Right);
            jump_Hat_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Jump_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Jump_Hat_Y"] / 5, playerSpriteSheet["playerJumpHatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Hat_Animation_Left", jump_Hat_Animation_Left);
            jump_Hat_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Jump_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Jump_Hat_Y"] / 5, playerSpriteSheet["playerJumpHatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Hat_Animation_Right", jump_Hat_Animation_Right);
            jump_Armor_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Jump_Armor_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Jump_Armor_Y"] / 5, playerSpriteSheet["playerJumpArmorSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Armor_Animation_Left", jump_Armor_Animation_Left);
            jump_Armor_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Jump_Armor_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Jump_Armor_Y"] / 5, playerSpriteSheet["playerJumpArmorSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Armor_Animation_Right", jump_Armor_Animation_Right);
            jump_Armor_Hat_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Jump_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Jump_Hat_Y"] / 5, playerSpriteSheet["playerJumpArmorHatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Armor_Hat_Animation_Left", jump_Armor_Hat_Animation_Left);
            jump_Armor_Hat_Animation_Right = new Animation(true, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Jump_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Jump_Hat_Y"] / 5, playerSpriteSheet["playerJumpArmorHatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Armor_Hat_Animation_Right", jump_Armor_Hat_Animation_Right);
            //Attack Animtions
            attack_Animation_Left = new Animation(false, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Attack_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Attack_Y"] / 5, playerSpriteSheet["playerAttackSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Animation_Left", attack_Animation_Left);
            attack_Animation_Right = new Animation(false, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Attack_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Attack_Y"] / 5, playerSpriteSheet["playerAttackSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Animation_Right", attack_Animation_Right);
            attack_Hat_Animation_Left = new Animation(false, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_Y"] / 5, playerSpriteSheet["playerAttackHatSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Hat_Animation_Left", attack_Hat_Animation_Left);
            attack_Hat_Animation_Right = new Animation(false, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_Y"] / 5, playerSpriteSheet["playerAttackHatSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Hat_Animation_Right", attack_Hat_Animation_Right);
            attack_Armor_Animation_Left = new Animation(false, SpriteEffects.FlipHorizontally, 90, 86, playerSpriteSheet["playerAttackArmorSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Armor_Animation_Left", attack_Armor_Animation_Left);
            attack_Armor_Animation_Right = new Animation(false, SpriteEffects.None, 90,86, playerSpriteSheet["playerAttackArmorSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Armor_Animation_Right", attack_Armor_Animation_Right);
            attack_Armor_Hat_Animation_Left = new Animation(false, SpriteEffects.FlipHorizontally, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_Y"] / 5, playerSpriteSheet["playerAttackArmorHatSpritesheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Armor_Hat_Animation_Left", attack_Armor_Hat_Animation_Left);
            attack_Armor_Hat_Animation_Right = new Animation(false, SpriteEffects.None, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Attack_Hat_Y"] / 5, playerSpriteSheet["playerAttackArmorHatSpritesheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Armor_Hat_Animation_Right", attack_Armor_Hat_Animation_Right);

            //Umbrella Animations
            attack_Umbrella_Empty_Animation_Left = new Animation(false, SpriteEffects.FlipHorizontally, 115, 147, playerSpriteSheet["playerAttackUmbrellaEmptySpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Umbrella_Empty_Animation_Left", attack_Umbrella_Empty_Animation_Left);
            attack_Umbrella_Empty_Animation_Right = new Animation(false, SpriteEffects.None, 115, 147, playerSpriteSheet["playerAttackUmbrellaEmptySpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Umbrella_Empty_Animation_Right", attack_Umbrella_Empty_Animation_Right);
            attack_Umbrella_Golden_Animation_Left = new Animation(false, SpriteEffects.FlipHorizontally, 115, 148, playerSpriteSheet["playerAttackGoldenSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Umbrella_Golden_Animation_Left", attack_Umbrella_Golden_Animation_Left);
            attack_Umbrella_Golden_Animation_Right = new Animation(false, SpriteEffects.None, 115, 148, playerSpriteSheet["playerAttackGoldenSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("Attack_Umbrella_Golden_Animation_Right", attack_Umbrella_Golden_Animation_Right);

            walk_Umbrella_Empty_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, 75, 106, playerSpriteSheet["playerMoveUmbrellaEmptySpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Umbrella_Empty_Animation_Left", walk_Umbrella_Empty_Animation_Left);
            walk_Umbrella_Empty_Animation_Right = new Animation(true, SpriteEffects.None, 75, 106, playerSpriteSheet["playerMoveUmbrellaEmptySpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Umbrella_Empty_Animation_Right", walk_Umbrella_Empty_Animation_Right);
            walk_Umbrella_Golden_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, 75, 106, playerSpriteSheet["playerWalkGoldenSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Umbrella_Golden_Animation_Left", walk_Umbrella_Golden_Animation_Left);
            walk_Umbrella_Golden_Animation_Right = new Animation(true, SpriteEffects.None, 75, 106, playerSpriteSheet["playerWalkGoldenSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Walk_Umbrella_Golden_Animation_Right", walk_Umbrella_Golden_Animation_Right);

            jump_Umbrella_Empty_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, 48, 95, playerSpriteSheet["playerJumpUmbrellaEmptySpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Umbrella_Empty_Animation_Left", jump_Umbrella_Empty_Animation_Left);
            jump_Umbrella_Empty_Animation_Right = new Animation(true, SpriteEffects.None, 48, 95, playerSpriteSheet["playerJumpUmbrellaEmptySpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Umbrella_Empty_Animation_Right", jump_Umbrella_Empty_Animation_Right);
            jump_Umbrella_Golden_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, 48, 95, playerSpriteSheet["playerJumpGoldenSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Umbrella_Golden_Animation_Left", jump_Umbrella_Golden_Animation_Left);
            jump_Umbrella_Golden_Animation_Right = new Animation(true, SpriteEffects.None, 48, 95, playerSpriteSheet["playerJumpGoldenSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Jump_Umbrella_Golden_Animation_Right", jump_Umbrella_Golden_Animation_Right);


            //Item Animations
            walk_Shovel_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, 35, 50, playerSpriteSheet["playerWalkShovelSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("walk_Shovel_Animation_Left", walk_Shovel_Animation_Left);
            walk_Shovel_Animation_Right = new Animation(true, SpriteEffects.None, 35, 50, playerSpriteSheet["playerWalkShovelSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("walk_Shovel_Animation_Right", walk_Shovel_Animation_Right);
            jump_Shovel_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, 25, 32, playerSpriteSheet["playerJumpShovelSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("jump_Shovel_Animation_Left", jump_Shovel_Animation_Left);
            jump_Shovel_Animation_Right = new Animation(true, SpriteEffects.None, 25, 32, playerSpriteSheet["playerJumpShovelSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("jump_Shovel_Animation_Right", jump_Shovel_Animation_Right);
            attack_Shovel_Animation_Left = new Animation(false, SpriteEffects.FlipHorizontally, 67, 76, playerSpriteSheet["playerAttackShovelSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("attack_Shovel_Animation_Left", attack_Shovel_Animation_Left);
            attack_Shovel_Animation_Right = new Animation(false, SpriteEffects.None, 67, 76, playerSpriteSheet["playerAttackShovelSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("attack_Shovel_Animation_Right", attack_Shovel_Animation_Right);
            float_Shovel_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, 22, 30, playerSpriteSheet["playerFloatShovelSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("float_Shovel_Animation_Left", float_Shovel_Animation_Left);
            float_Shovel_Animation_Right = new Animation(true, SpriteEffects.None, 22, 30, playerSpriteSheet["playerFloatShovelSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("float_Shovel_Animation_Right", float_Shovel_Animation_Right);


            walk_Scissors_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, 45, 53, playerSpriteSheet["playerWalkScissorsSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("walk_Scissors_Animation_Left", walk_Scissors_Animation_Left);
            walk_Scissors_Animation_Right = new Animation(true, SpriteEffects.None, 45, 53, playerSpriteSheet["playerWalkScissorsSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("walk_Scissors_Animation_Right", walk_Scissors_Animation_Right);
            jump_Scissors_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, 27, 39, playerSpriteSheet["playerJumpScissorsSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("jump_Scissors_Animation_Left", jump_Scissors_Animation_Left);
            jump_Scissors_Animation_Right = new Animation(true, SpriteEffects.None, 27, 39, playerSpriteSheet["playerJumpScissorsSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("jump_Scissors_Animation_Right", jump_Scissors_Animation_Right);
            attack_Scissors_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, 68, 73, playerSpriteSheet["playerAttackScissorsSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("attack_Scissors_Animation_Left", attack_Scissors_Animation_Left);
            attack_Scissors_Animation_Right = new Animation(false, SpriteEffects.None, 68, 73, playerSpriteSheet["playerAttackScissorsSpriteSheet"], 50f);
            divAnimationDestRectanglesDic.Add("attack_Scissors_Animation_Right", attack_Scissors_Animation_Right);
            float_Scissors_Animation_Left = new Animation(true, SpriteEffects.FlipHorizontally, 26, 39, playerSpriteSheet["playerFloatScissorsSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("float_Scissors_Animation_Left", float_Scissors_Animation_Left);
            float_Scissors_Animation_Right = new Animation(true, SpriteEffects.None, 26, 39, playerSpriteSheet["playerFloatScissorsSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("float_Scissors_Animation_Right", float_Scissors_Animation_Right);
            


            //Floating Animation
            floatingAnimation_Right = new Animation(true, SpriteEffects.None, 79, 93, playerSpriteSheet["playerFloatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Float_Animation_Right", floatingAnimation_Right);
            floatingAnimation_Left = new Animation(true, SpriteEffects.FlipHorizontally, 79, 93, playerSpriteSheet["playerFloatSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Float_Animation_Left", floatingAnimation_Left);
            floatingGoldenAnimation_Right = new Animation(true, SpriteEffects.None, 79, 93, playerSpriteSheet["playerFloatGoldenSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Float_Golden_Animation_Right", floatingGoldenAnimation_Right);
            floatingGoldenAnimation_Left = new Animation(true, SpriteEffects.FlipHorizontally, 79, 93, playerSpriteSheet["playerFloatGoldenSpriteSheet"], 25f);
            divAnimationDestRectanglesDic.Add("Float_Golden_Animation_Left", floatingGoldenAnimation_Left);


          
            audioManager = AudioManager.AudioManagerInstance();
        }

        

        public void animation(GameTime gameTime, ref Player player, SpriteBatch spriteBatch) 
        {
            
            if(player.invincibilityFrames)
            {
                if (player.playerSlowed && (int)(player.invincibilityTimer / 0.40) % 2 == 1)
                    color = Color.Blue;
                else if ((int)(player.invincibilityTimer / 0.40) % 2 == 1)
                    color = Color.Red;
                else
                    color = Color.White;
            }
            else
                color = Color.White;
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
                if (attack_Animation_Right.getPlayedOnce()
                    || attack_Animation_Left.getPlayedOnce()
                    || attack_Hat_Animation_Left.getPlayedOnce()
                    || attack_Hat_Animation_Right.getPlayedOnce()
                    || attack_Armor_Animation_Left.getPlayedOnce()
                    || attack_Armor_Animation_Right.getPlayedOnce()
                    || attack_Armor_Hat_Animation_Left.getPlayedOnce()
                    || attack_Armor_Hat_Animation_Right.getPlayedOnce())
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

                    attack_Animation_Left.resetPlayedOnce();
                    attack_Animation_Right.resetPlayedOnce();
                    attack_Hat_Animation_Left.resetPlayedOnce();
                    attack_Hat_Animation_Right.resetPlayedOnce();
                    attack_Armor_Animation_Left.resetPlayedOnce();
                    attack_Armor_Animation_Right.resetPlayedOnce();
                    attack_Armor_Hat_Animation_Left.resetPlayedOnce();
                    attack_Armor_Hat_Animation_Right.resetPlayedOnce();
                }
            }


            Rectangle tempRec;
            Rectangle tempRec2;
            Rectangle tempRec3;
            Rectangle tempRec4;
            switch (currentAnimation)
            {
                case Animations.Walk_Right:
                    player.changeTexture(walk_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, walk_Animation_Right.getSpriteEffects(),new Vector2(0,0),color,scale);
                    previousAnimation = Animations.Walk_Right;
                    break;
                case Animations.Walk_Left:
                    player.changeTexture(walk_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, walk_Animation_Left.getSpriteEffects(),new Vector2(0,0),color,scale);
                    previousAnimation = Animations.Walk_Left;
                    break;
                case Animations.Walk_Hat_Left:
                    player.changeTexture(walk_Hat_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Hat_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, walk_Hat_Animation_Left.getSpriteEffects(), new Vector2(0, -20), color,scale);
                    previousAnimation = Animations.Walk_Hat_Left;
                    break;
                case Animations.Walk_Hat_Right:
                    player.changeTexture(walk_Hat_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Hat_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, walk_Hat_Animation_Right.getSpriteEffects(), new Vector2(0, -20), color,scale);
                    previousAnimation = Animations.Walk_Hat_Right;
                    break;
                case Animations.Walk_Armor_Left:
                    player.changeTexture(walk_Armor_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Armor_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, walk_Armor_Animation_Left.getSpriteEffects(), new Vector2(0, 0), color,scale);
                    previousAnimation = Animations.Walk_Armor_Left;
                    break;
                case Animations.Walk_Armor_Right:
                    player.changeTexture(walk_Armor_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Armor_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, walk_Armor_Animation_Right.getSpriteEffects(), new Vector2(0, 0), color,scale);
                    previousAnimation = Animations.Walk_Armor_Right;
                    break;
                case Animations.Walk_Armor_Hat_Left:
                    player.changeTexture(walk_Armor_Hat_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Armor_Hat_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, walk_Armor_Hat_Animation_Left.getSpriteEffects(), new Vector2(0, -20), color,scale);
                    previousAnimation = Animations.Walk_Armor_Hat_Left;
                    break;
                case Animations.Walk_Armor_Hat_Right:
                    player.changeTexture(walk_Armor_Hat_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Walk_Armor_Hat_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, walk_Armor_Hat_Animation_Right.getSpriteEffects(), new Vector2(0, -20), color,scale);
                    previousAnimation = Animations.Walk_Armor_Hat_Right;
                    break;


                case Animations.Jump_Right:
                    player.changeTexture(jump_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, jump_Animation_Right.getSpriteEffects(), new Vector2(35,-20), color,scale);
                    previousAnimation = Animations.Jump_Right;
                    break;
                case Animations.Jump_Left:
                    player.changeTexture(jump_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, jump_Animation_Left.getSpriteEffects(), new Vector2(15, -20), color,scale);
                    previousAnimation = Animations.Jump_Left;
                    break;
                case Animations.Jump_Hat_Left:
                    player.changeTexture(jump_Hat_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Hat_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, jump_Hat_Animation_Left.getSpriteEffects(), new Vector2(15, -35), color,scale);
                    previousAnimation = Animations.Jump_Hat_Left;
                    break;
                case Animations.Jump_Hat_Right:
                    player.changeTexture(jump_Hat_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Hat_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, jump_Hat_Animation_Right.getSpriteEffects(), new Vector2(22, -35), color,scale);
                    previousAnimation = Animations.Jump_Hat_Right;
                    break;
                case Animations.Jump_Armor_Left:
                    player.changeTexture(jump_Armor_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Armor_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, jump_Armor_Animation_Left.getSpriteEffects(), new Vector2(22, -22), color,scale);
                    previousAnimation = Animations.Jump_Armor_Left;
                    break;
                case Animations.Jump_Armor_Right:
                    player.changeTexture(jump_Armor_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Armor_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, jump_Armor_Animation_Right.getSpriteEffects(), new Vector2(34, -22), color,scale);
                    previousAnimation = Animations.Jump_Armor_Right;
                    break;
                case Animations.Jump_Armor_Hat_Left:
                    player.changeTexture(jump_Armor_Hat_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Armor_Hat_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, jump_Armor_Hat_Animation_Left.getSpriteEffects(), new Vector2(22, -35), color,scale);
                    previousAnimation = Animations.Jump_Armor_Hat_Left;
                    break;
                case Animations.Jump_Armor_Hat_Right:
                    player.changeTexture(jump_Armor_Hat_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Jump_Armor_Hat_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, jump_Armor_Hat_Animation_Right.getSpriteEffects(), new Vector2(22, -35), color,scale);
                    previousAnimation = Animations.Jump_Armor_Hat_Right;
                    break;





                case Animations.Attack_Left:
                    player.changeTexture(attack_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Attack_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, attack_Animation_Left.getSpriteEffects(), new Vector2(-15, 0), color, scale);
                    break;
                case Animations.Attack_Right:
                    player.changeTexture(attack_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Attack_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, attack_Animation_Right.getSpriteEffects(), new Vector2(32, 0), color, scale);
                    break;
                case Animations.Attack_Hat_Left:
                    player.changeTexture(attack_Hat_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Attack_Hat_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, attack_Hat_Animation_Left.getSpriteEffects(), new Vector2(-25, -20), color, scale);
                    break;
                case Animations.Attack_Hat_Right:
                    player.changeTexture(attack_Hat_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Attack_Hat_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, attack_Hat_Animation_Right.getSpriteEffects(), new Vector2(32, -20), color, scale);
                    break;
                case Animations.Attack_Armor_Left:
                    player.changeTexture(attack_Armor_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Attack_Armor_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, attack_Armor_Animation_Left.getSpriteEffects(), new Vector2(-25, 0), color, scale);
                    break;
                case Animations.Attack_Armor_Right:
                    player.changeTexture(attack_Armor_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Attack_Armor_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, attack_Armor_Animation_Right.getSpriteEffects(), new Vector2(32, 0), color, scale);
                    break;
                case Animations.Attack_Armor_Hat_Left:
                    player.changeTexture(attack_Armor_Hat_Animation_Left.texture);
                    tempRec = divAnimationDestRectanglesDic["Attack_Armor_Hat_Animation_Left"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, attack_Armor_Hat_Animation_Left.getSpriteEffects(), new Vector2(-15, -20), color, scale);
                    break;
                case Animations.Attack_Armor_Hat_Right:
                    player.changeTexture(attack_Armor_Hat_Animation_Right.texture);
                    tempRec = divAnimationDestRectanglesDic["Attack_Armor_Hat_Animation_Right"].Update(gameTime);
                    player.DrawSpriteBatch(spriteBatch, tempRec, attack_Armor_Hat_Animation_Right.getSpriteEffects(), new Vector2(32, -20), color, scale);
                    break;

            }
            
            //Manage Umbrella and Objects Animations
            if(currentAnimation == Animations.Attack_Right || currentAnimation == Animations.Attack_Hat_Right || currentAnimation == Animations.Attack_Armor_Right || currentAnimation == Animations.Attack_Armor_Hat_Right)
            {
                if(ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.SHOVEL)
                {
                    player.changeThirdTexture(attack_Shovel_Animation_Right.texture);
                    tempRec3 = divAnimationDestRectanglesDic["attack_Shovel_Animation_Right"].ReturnRectFromFrameNumber(divAnimationDestRectanglesDic["Attack_Umbrella_Empty_Animation_Right"].currentFrameGetSetter);
                    player.drawThirdTexture(spriteBatch, tempRec3, attack_Shovel_Animation_Right.getSpriteEffects(), new Vector2(75, 18), color);
                }
                if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.SCISSORS)
                {
                    player.changeThirdTexture(attack_Scissors_Animation_Right.texture);
                    tempRec4 = divAnimationDestRectanglesDic["attack_Scissors_Animation_Right"].ReturnRectFromFrameNumber(divAnimationDestRectanglesDic["Attack_Umbrella_Empty_Animation_Right"].currentFrameGetSetter);
                    player.drawThirdTexture(spriteBatch, tempRec4, attack_Scissors_Animation_Right.getSpriteEffects(), new Vector2(70, 18), color);
                }
                if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.GOLDENUMBRELLA)
                {
                    player.changeSecondTexture(attack_Umbrella_Golden_Animation_Right.texture);
                    tempRec2 = divAnimationDestRectanglesDic["Attack_Umbrella_Golden_Animation_Right"].Update(gameTime);
                    player.drawSecondTexture(spriteBatch, tempRec2, attack_Umbrella_Golden_Animation_Right.getSpriteEffects(), new Vector2(64, -33), color);
                }
                else
                {
                    player.changeSecondTexture(attack_Umbrella_Empty_Animation_Right.texture);
                    tempRec2 = divAnimationDestRectanglesDic["Attack_Umbrella_Empty_Animation_Right"].Update(gameTime);
                    player.drawSecondTexture(spriteBatch, tempRec2, attack_Umbrella_Empty_Animation_Right.getSpriteEffects(), new Vector2(64, -33), color);
                }

            }

            if (currentAnimation == Animations.Attack_Left || currentAnimation == Animations.Attack_Hat_Left || currentAnimation == Animations.Attack_Armor_Left || currentAnimation == Animations.Attack_Armor_Hat_Left)
            {
                if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.SHOVEL)
                {
                    player.changeThirdTexture(attack_Shovel_Animation_Left.texture);
                    tempRec3 = divAnimationDestRectanglesDic["attack_Shovel_Animation_Left"].ReturnRectFromFrameNumber(divAnimationDestRectanglesDic["Attack_Umbrella_Empty_Animation_Left"].currentFrameGetSetter);
                    player.drawThirdTexture(spriteBatch, tempRec3, attack_Shovel_Animation_Left.getSpriteEffects(), new Vector2(-35, 23), color);
                }
                if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.SCISSORS)
                {
                    player.changeThirdTexture(attack_Scissors_Animation_Left.texture);
                    tempRec4 = divAnimationDestRectanglesDic["attack_Scissors_Animation_Left"].ReturnRectFromFrameNumber(divAnimationDestRectanglesDic["Attack_Umbrella_Empty_Animation_Left"].currentFrameGetSetter);
                    player.drawThirdTexture(spriteBatch, tempRec4, attack_Scissors_Animation_Left.getSpriteEffects(), new Vector2(-27, 20), color);
                }
                if(ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.GOLDENUMBRELLA)
                {
                    player.changeSecondTexture(attack_Umbrella_Golden_Animation_Left.texture);
                    tempRec2 = divAnimationDestRectanglesDic["Attack_Umbrella_Golden_Animation_Left"].Update(gameTime);
                    player.drawSecondTexture(spriteBatch, tempRec2, attack_Umbrella_Golden_Animation_Left.getSpriteEffects(), new Vector2(-71, -33), color);
                }
                else
                {
                    player.changeSecondTexture(attack_Umbrella_Empty_Animation_Left.texture);
                    tempRec2 = divAnimationDestRectanglesDic["Attack_Umbrella_Empty_Animation_Left"].Update(gameTime);
                    player.drawSecondTexture(spriteBatch, tempRec2, attack_Umbrella_Empty_Animation_Left.getSpriteEffects(), new Vector2(-71, -33), color);
                }
            }

            if(currentAnimation == Animations.Walk_Right || currentAnimation == Animations.Walk_Hat_Right || currentAnimation == Animations.Walk_Armor_Right || currentAnimation == Animations.Walk_Armor_Hat_Right)
            {
               
                if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.SHOVEL)
                {
                    player.changeThirdTexture(walk_Shovel_Animation_Right.texture);
                    tempRec3 = divAnimationDestRectanglesDic["walk_Shovel_Animation_Right"].ReturnRectFromFrameNumber(divAnimationDestRectanglesDic["Walk_Umbrella_Empty_Animation_Right"].currentFrameGetSetter);
                    player.drawThirdTexture(spriteBatch, tempRec3, walk_Shovel_Animation_Right.getSpriteEffects(), new Vector2(95, 28), color);
                    
                }
                if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.SCISSORS)
                {
                    player.changeThirdTexture(walk_Scissors_Animation_Right.texture);
                    tempRec4 = divAnimationDestRectanglesDic["walk_Scissors_Animation_Right"].ReturnRectFromFrameNumber(divAnimationDestRectanglesDic["Walk_Umbrella_Empty_Animation_Right"].currentFrameGetSetter);
                    player.drawThirdTexture(spriteBatch, tempRec4, walk_Scissors_Animation_Right.getSpriteEffects(), new Vector2(83, 25), color);
                }

                if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.GOLDENUMBRELLA)
                {
                    player.changeSecondTexture(walk_Umbrella_Golden_Animation_Right.texture);
                    tempRec2 = divAnimationDestRectanglesDic["Walk_Umbrella_Golden_Animation_Right"].Update(gameTime);
                    player.drawSecondTexture(spriteBatch, tempRec2, walk_Umbrella_Golden_Animation_Right.getSpriteEffects(), new Vector2(78, -22), color);

                }
                else
                {
                    player.changeSecondTexture(walk_Umbrella_Empty_Animation_Right.texture);
                    tempRec2 = divAnimationDestRectanglesDic["Walk_Umbrella_Empty_Animation_Right"].Update(gameTime);
                    player.drawSecondTexture(spriteBatch, tempRec2, walk_Umbrella_Empty_Animation_Right.getSpriteEffects(), new Vector2(78, -22), color);
                }
            }

            if(currentAnimation == Animations.Walk_Left || currentAnimation == Animations.Walk_Hat_Left || currentAnimation == Animations.Walk_Armor_Left || currentAnimation == Animations.Walk_Armor_Hat_Left)
            {
                if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.SHOVEL)
                {
                    player.changeThirdTexture(walk_Shovel_Animation_Left.texture);
                    tempRec3 = divAnimationDestRectanglesDic["walk_Shovel_Animation_Left"].ReturnRectFromFrameNumber(divAnimationDestRectanglesDic["Walk_Umbrella_Empty_Animation_Left"].currentFrameGetSetter);
                    player.drawThirdTexture(spriteBatch, tempRec3, walk_Shovel_Animation_Left.getSpriteEffects(), new Vector2(-20, 25), color);

                }
                if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.SCISSORS)
                {
                    player.changeThirdTexture(walk_Scissors_Animation_Left.texture);
                    tempRec4 = divAnimationDestRectanglesDic["walk_Scissors_Animation_Left"].ReturnRectFromFrameNumber(divAnimationDestRectanglesDic["Walk_Umbrella_Empty_Animation_Left"].currentFrameGetSetter);
                    player.drawThirdTexture(spriteBatch, tempRec4, walk_Scissors_Animation_Left.getSpriteEffects(), new Vector2(-20, 26), color);
                }
                if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.GOLDENUMBRELLA)
                {
                    player.changeSecondTexture(walk_Umbrella_Golden_Animation_Left.texture);
                    tempRec2 = divAnimationDestRectanglesDic["Walk_Umbrella_Golden_Animation_Left"].Update(gameTime);
                    player.drawSecondTexture(spriteBatch, tempRec2, walk_Umbrella_Golden_Animation_Left.getSpriteEffects(), new Vector2(-45, -20), color);
                }
                else if(ItemUIManager.currentItemEquipped.objectID != (int)Enums.ObjectsID.GOLDENUMBRELLA)
                {
                    player.changeSecondTexture(walk_Umbrella_Empty_Animation_Left.texture);
                    tempRec2 = divAnimationDestRectanglesDic["Walk_Umbrella_Empty_Animation_Left"].Update(gameTime);
                    player.drawSecondTexture(spriteBatch, tempRec2, walk_Umbrella_Empty_Animation_Left.getSpriteEffects(), new Vector2(-45, -20), color);
                }
            }

            if (currentAnimation == Animations.Jump_Right || currentAnimation == Animations.Jump_Hat_Right || currentAnimation == Animations.Jump_Armor_Right || currentAnimation == Animations.Jump_Armor_Hat_Right)
            {
                if (!player.isFloating)
                {
                    if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.SHOVEL)
                    {
                        player.changeThirdTexture(jump_Shovel_Animation_Right.texture);
                        tempRec3 = divAnimationDestRectanglesDic["jump_Shovel_Animation_Right"].ReturnRectFromFrameNumber(divAnimationDestRectanglesDic["Jump_Umbrella_Empty_Animation_Right"].currentFrameGetSetter);
                        player.drawThirdTexture(spriteBatch, tempRec3, jump_Shovel_Animation_Right.getSpriteEffects(), new Vector2(79, -10), color);

                    }
                    if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.SCISSORS)
                    {
                        player.changeThirdTexture(jump_Scissors_Animation_Right.texture);
                        tempRec4 = divAnimationDestRectanglesDic["jump_Scissors_Animation_Right"].ReturnRectFromFrameNumber(divAnimationDestRectanglesDic["Jump_Umbrella_Empty_Animation_Right"].currentFrameGetSetter);
                        player.drawThirdTexture(spriteBatch, tempRec4, jump_Scissors_Animation_Right.getSpriteEffects(), new Vector2(70, -14), color);
                    }
                    if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.GOLDENUMBRELLA)
                    {
                        player.changeSecondTexture(jump_Umbrella_Golden_Animation_Right.texture);
                        tempRec2 = divAnimationDestRectanglesDic["Jump_Umbrella_Golden_Animation_Right"].Update(gameTime);
                        player.drawSecondTexture(spriteBatch, tempRec2, jump_Umbrella_Golden_Animation_Right.getSpriteEffects(), new Vector2(58, -63), color);
                    }
                    else
                    {
                        player.changeSecondTexture(jump_Umbrella_Empty_Animation_Right.texture);
                        tempRec2 = divAnimationDestRectanglesDic["Jump_Umbrella_Empty_Animation_Right"].Update(gameTime);
                        player.drawSecondTexture(spriteBatch, tempRec2, jump_Umbrella_Empty_Animation_Right.getSpriteEffects(), new Vector2(58, -63), color);
                    }
                }
                   

              
            
                else
                {
                    if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.SHOVEL)
                    {
                        player.changeThirdTexture(float_Shovel_Animation_Right.texture);
                        tempRec3 = divAnimationDestRectanglesDic["float_Shovel_Animation_Right"].ReturnRectFromFrameNumber(divAnimationDestRectanglesDic["Float_Animation_Right"].currentFrameGetSetter);
                        player.drawThirdTexture(spriteBatch, tempRec3, float_Shovel_Animation_Right.getSpriteEffects(), new Vector2(87, -20), color);
                    }
                    if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.SCISSORS)
                    {
                        player.changeThirdTexture(float_Scissors_Animation_Right.texture);
                        tempRec4 = divAnimationDestRectanglesDic["float_Scissors_Animation_Right"].ReturnRectFromFrameNumber(divAnimationDestRectanglesDic["Float_Animation_Right"].currentFrameGetSetter);
                        player.drawThirdTexture(spriteBatch, tempRec4, float_Scissors_Animation_Right.getSpriteEffects(), new Vector2(80, -20), color);
                    }
                    if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.GOLDENUMBRELLA)
                    {
                        player.changeSecondTexture(floatingGoldenAnimation_Right.texture);
                        tempRec2 = divAnimationDestRectanglesDic["Float_Golden_Animation_Right"].Update(gameTime);
                        player.drawSecondTexture(spriteBatch, tempRec2, floatingGoldenAnimation_Right.getSpriteEffects(), new Vector2(53, -63), color);
                    }
                    else
                    {
                        player.changeSecondTexture(floatingAnimation_Right.texture);
                        tempRec2 = divAnimationDestRectanglesDic["Float_Animation_Right"].Update(gameTime);
                        player.drawSecondTexture(spriteBatch, tempRec2, floatingAnimation_Right.getSpriteEffects(), new Vector2(53, -63), color);
                    }
            
                }
            }

            if (currentAnimation == Animations.Jump_Left || currentAnimation == Animations.Jump_Hat_Left || currentAnimation == Animations.Jump_Armor_Left || currentAnimation == Animations.Jump_Armor_Hat_Left)
            {
                if (!player.isFloating)
                {
                    if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.SHOVEL)
                    {
                        player.changeThirdTexture(jump_Shovel_Animation_Left.texture);
                        tempRec3 = divAnimationDestRectanglesDic["jump_Shovel_Animation_Left"].ReturnRectFromFrameNumber(divAnimationDestRectanglesDic["Jump_Umbrella_Empty_Animation_Left"].currentFrameGetSetter);
                        player.drawThirdTexture(spriteBatch, tempRec3, jump_Shovel_Animation_Left.getSpriteEffects(), new Vector2(2, -10), color);

                    }
                    if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.SCISSORS)
                    {
                        player.changeThirdTexture(jump_Scissors_Animation_Left.texture);
                        tempRec4 = divAnimationDestRectanglesDic["jump_Scissors_Animation_Left"].ReturnRectFromFrameNumber(divAnimationDestRectanglesDic["Jump_Umbrella_Empty_Animation_Left"].currentFrameGetSetter);
                        player.drawThirdTexture(spriteBatch, tempRec4, jump_Scissors_Animation_Left.getSpriteEffects(), new Vector2(10, -14), color);
                    }
                    if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.GOLDENUMBRELLA)
                    {
                        player.changeSecondTexture(jump_Umbrella_Golden_Animation_Left.texture);
                        tempRec2 = divAnimationDestRectanglesDic["Jump_Umbrella_Golden_Animation_Left"].Update(gameTime);
                        player.drawSecondTexture(spriteBatch, tempRec2, jump_Umbrella_Golden_Animation_Left.getSpriteEffects(), new Vector2(0, -63), color);
                    }
                    else
                    {
                        player.changeSecondTexture(jump_Umbrella_Empty_Animation_Left.texture);
                        tempRec2 = divAnimationDestRectanglesDic["Jump_Umbrella_Empty_Animation_Left"].Update(gameTime);
                        player.drawSecondTexture(spriteBatch, tempRec2, jump_Umbrella_Empty_Animation_Left.getSpriteEffects(), new Vector2(0, -63), color);
                    }
                }
                else
                {
                    if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.SHOVEL)
                    {
                        player.changeThirdTexture(float_Shovel_Animation_Left.texture);
                        tempRec3 = divAnimationDestRectanglesDic["float_Shovel_Animation_Left"].ReturnRectFromFrameNumber(divAnimationDestRectanglesDic["Float_Animation_Left"].currentFrameGetSetter);
                        player.drawThirdTexture(spriteBatch, tempRec3, float_Shovel_Animation_Left.getSpriteEffects(), new Vector2(0, -20), color);

                    }
                    if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.SCISSORS)
                    {
                        player.changeThirdTexture(float_Scissors_Animation_Left.texture);
                        tempRec4 = divAnimationDestRectanglesDic["float_Scissors_Animation_Left"].ReturnRectFromFrameNumber(divAnimationDestRectanglesDic["Float_Animation_Left"].currentFrameGetSetter);
                        player.drawThirdTexture(spriteBatch, tempRec4, float_Scissors_Animation_Left.getSpriteEffects(), new Vector2(5, -20), color);
                    }
                    if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.GOLDENUMBRELLA)
                    {
                        player.changeSecondTexture(floatingGoldenAnimation_Left.texture);
                        tempRec2 = divAnimationDestRectanglesDic["Float_Golden_Animation_Left"].Update(gameTime);
                        player.drawSecondTexture(spriteBatch, tempRec2, floatingGoldenAnimation_Left.getSpriteEffects(), new Vector2(-23, -63), color);
                    }
                    else
                    {
                        player.changeSecondTexture(floatingAnimation_Left.texture);
                        tempRec2 = divAnimationDestRectanglesDic["Float_Animation_Left"].Update(gameTime);
                        player.drawSecondTexture(spriteBatch, tempRec2, floatingAnimation_Left.getSpriteEffects(), new Vector2(-23, -63), color);
                    }
                }
            }
        }

        public void changeScale(Vector2 scale)
        {
            this.scale = scale;
        }
    }

        
        
    
}
