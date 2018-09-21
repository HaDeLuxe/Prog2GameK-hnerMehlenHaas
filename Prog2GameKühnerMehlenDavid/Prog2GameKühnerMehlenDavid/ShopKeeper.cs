using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reggie.Animations;

namespace Reggie
{
    public class ShopKeeper : GameObject
    {
        NPC_Animations nPC_Animations;

        public bool shopOpen;

        //ButtonStuff
        Enums.ShopKeeperItemButtons currentButton;
        bool buttonPressed;

        //MUSIC
        AudioManager audioManager;


        public ShopKeeper(Texture2D gameObjectTexture, Vector2 gameObejctSize, Vector2 position, int gameObjectID, Dictionary<string, Texture2D> texturesDictionary) : base(gameObjectTexture, gameObejctSize, position, gameObjectID)
        {
            nPC_Animations = new NPC_Animations(texturesDictionary);
            nPC_Animations.currentAnimation = NPC_Animations.NPCAnimations.IdleShopkeeper;
            nPC_Animations.nextAnimation = NPC_Animations.NPCAnimations.IdleShopkeeper;

            audioManager = AudioManager.AudioManagerInstance();
            
            shopOpen = false;

            currentButton = Enums.ShopKeeperItemButtons.STRENGTHPOTION;
            buttonPressed = false;
            
        }

        public void drawShopKeeper(SpriteBatch spriteBatch, GameTime gameTime, Dictionary<string, Texture2D> texturesDictionary)
        {
            nPC_Animations.Animation(gameTime, spriteBatch, this);
            //if (shopOpen == true)
            //{
            //    drawShopInterface(spriteBatch, texturesDictionary);
            //}
        }

        public void drawShopInterface(SpriteBatch spriteBatch, Dictionary<string, Texture2D> texturesDictionary)
        {

            ////Background
            //spriteBatch.Draw(texturesDictionary["ShopKeeperInterface_Background"], new Rectangle(0, 0, 187, 384), Color.White);

            ////ButtonStuff
            //if (currentButton == Enums.ShopKeeperItemButtons.STRENGTHPOTION)
            //    spriteBatch.Draw(texturesDictionary["ShopKeeperInterface_StrengthPotion_Highlighted"], new Rectangle(0, 0, 187, 384), Color.White);
            //else
            //    spriteBatch.Draw(texturesDictionary["ShopKeeperInterface_StrengthPotion"], new Rectangle(0, 0, 187, 384), Color.White);
            //if (currentButton == Enums.ShopKeeperItemButtons.JUMPPOTION)
            //    spriteBatch.Draw(texturesDictionary["ShopKeeperInterface_JumpPotion_Highlighted"], new Rectangle(0, 0, 187, 384), Color.White);
            //else
            //    spriteBatch.Draw(texturesDictionary["ShopKeeperInterface_JumpPotion"], new Rectangle(0, 0, 187, 384), Color.White);
            //if (currentButton == Enums.ShopKeeperItemButtons.HEALTHPOTION)
            //    spriteBatch.Draw(texturesDictionary["ShopKeeperInterface_HealtPotion_Highlighted"], new Rectangle(0, 0, 187, 384), Color.White);
            //else
            //    spriteBatch.Draw(texturesDictionary["ShopKeeperInterface_HealtPotion"], new Rectangle(0, 0, 187, 384), Color.White);

        }

        public void handleShopKeeperEvents()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) || GamePad.GetState(0).ThumbSticks.Left.Y < -0.5f && !buttonPressed)
            {
                currentButton--;
                if (currentButton < 0)
                currentButton = Enums.ShopKeeperItemButtons.STRENGTHPOTION;

                buttonPressed = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) || GamePad.GetState(0).ThumbSticks.Left.Y < +0.5f && !buttonPressed)
            {
                currentButton++;
                if (currentButton > Enums.ShopKeeperItemButtons.STRENGTHPOTION)
                    currentButton = Enums.ShopKeeperItemButtons.JUMPPOTION;

                buttonPressed = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(0).IsButtonDown(Buttons.Y) && !buttonPressed)
            {
                buttonPressed = true;

                if (currentButton == Enums.ShopKeeperItemButtons.STRENGTHPOTION)
                    Buy("StrengthPotion");

                if (currentButton == Enums.ShopKeeperItemButtons.JUMPPOTION)
                    Buy("JumpPotion");

                if (currentButton == Enums.ShopKeeperItemButtons.HEALTHPOTION)
                    Buy("HealthPotion");
            }
            if (Keyboard.GetState().GetPressedKeys().Count() == 0 || GamePad.GetState(0).Equals(0)) //TEST NEEDED EQUALS?!?! 
            {
                buttonPressed = false;
            }
        }

        //MUSS DA NOCH NEN UPDATE GETRIGGERT WERDEN ODER NICHT?
        private void Buy(string s)
        {
            if(s.Equals("StrenghtPotion"))
            {
                if (ItemUIManager.cornnencyQuantity > 5)
                {
                    ItemUIManager.cornnencyQuantity -= 5;
                    ItemUIManager.powerPotionsCount++;
                    audioManager.Play("ReggieBoughtSomething");
                }
            }
            if(s.Equals("JumpPotion"))
            {
                if (ItemUIManager.cornnencyQuantity > 5)
                {
                    ItemUIManager.cornnencyQuantity -= 5;
                    ItemUIManager.jumpPotionsCount++;
                    audioManager.Play("ReggieBoughtSomething");
                }
            }
            if(s.Equals("HealthPotion"))
            {
                if (ItemUIManager.cornnencyQuantity > 5)
                {
                    ItemUIManager.cornnencyQuantity -= 5;
                    ItemUIManager.healthPotionsCount++;
                    audioManager.Play("ReggieBoughtSomething");
                }
            }
        }
    }
}
