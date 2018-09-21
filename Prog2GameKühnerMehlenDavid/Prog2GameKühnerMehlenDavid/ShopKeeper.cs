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
        Enums.ShopKeeperItemButtons lastButton;

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

            lastButton = Enums.ShopKeeperItemButtons.STRENGTHPOTION;
            currentButton = Enums.ShopKeeperItemButtons.STRENGTHPOTION;
           
            buttonPressed = false;
            
        }

        public void drawShopKeeper(SpriteBatch spriteBatch, GameTime gameTime, Dictionary<string, Texture2D> texturesDictionary, Matrix transformationMatrix)
        {
            nPC_Animations.Animation(gameTime, spriteBatch, this);
            if (shopOpen == true)
            {
                drawShopInterface(spriteBatch, texturesDictionary, transformationMatrix);
            }
        }

        public void drawShopInterface(SpriteBatch spriteBatch, Dictionary<string, Texture2D> texturesDictionary, Matrix transformationMatrix)
        {

            //Background
            spriteBatch.Draw(texturesDictionary["ShopKeeperInterface_Background"], Vector2.Transform(new Vector2(500, 0), Matrix.Invert(transformationMatrix)), Color.White);
            spriteBatch.Draw(texturesDictionary["ShopKeeperInterface_AllPotions"], Vector2.Transform(new Vector2(500, 0), Matrix.Invert(transformationMatrix)), Color.White);

            //ButtonStuff
            if (lastButton == Enums.ShopKeeperItemButtons.STRENGTHPOTION)
                spriteBatch.Draw(texturesDictionary["ShopKeeperInterface_StrengthPotion_Highlighted"], Vector2.Transform(new Vector2(500,0),Matrix.Invert(transformationMatrix)), Color.White);

            if (lastButton == Enums.ShopKeeperItemButtons.JUMPPOTION)
                spriteBatch.Draw(texturesDictionary["ShopKeeperInterface_JumpPotion_Highlighted"], Vector2.Transform(new Vector2(500, 0), Matrix.Invert(transformationMatrix)), Color.White);
            
            if (lastButton == Enums.ShopKeeperItemButtons.HEALTHPOTION)
                spriteBatch.Draw(texturesDictionary["ShopKeeperInterface_HealtPotion_Highlighted"], Vector2.Transform(new Vector2(500, 0), Matrix.Invert(transformationMatrix)), Color.White);


        }

        public void handleShopKeeperEvents()
        {
            lastButton = currentButton;

            if ((Keyboard.GetState().IsKeyDown(Keys.W) || GamePad.GetState(0).ThumbSticks.Left.Y < -0.5f) && !buttonPressed)
            {
                currentButton--;
                if (currentButton < 0)
                currentButton = Enums.ShopKeeperItemButtons.STRENGTHPOTION;

                buttonPressed = true;
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.S) || GamePad.GetState(0).ThumbSticks.Left.Y > +0.5f) && !buttonPressed)
            {
                currentButton++;
                if (currentButton <= Enums.ShopKeeperItemButtons.STRENGTHPOTION)
                    currentButton = Enums.ShopKeeperItemButtons.JUMPPOTION;
                if (currentButton > Enums.ShopKeeperItemButtons.JUMPPOTION)
                    currentButton = Enums.ShopKeeperItemButtons.HEALTHPOTION;


                buttonPressed = true;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(0).IsButtonDown(Buttons.A)) && !buttonPressed)
            {
                buttonPressed = true;

                if (lastButton == Enums.ShopKeeperItemButtons.STRENGTHPOTION)
                    Buy("StrengthPotion");

                if (lastButton == Enums.ShopKeeperItemButtons.JUMPPOTION)
                    Buy("JumpPotion");

                if (lastButton == Enums.ShopKeeperItemButtons.HEALTHPOTION)
                    Buy("HealthPotion");
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Escape) || GamePad.GetState(0).IsButtonDown(Buttons.B)) && !buttonPressed)
            {
                shopOpen = false;
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
                if (ItemUIManager.cornnencyQuantity >= 5)
                {
                    ItemUIManager.cornnencyQuantity -= 5;
                    ItemUIManager.powerPotionsCount++;
                    audioManager.Play("ReggieBoughtSomething");
                }
            }
            if(s.Equals("JumpPotion"))
            {
                if (ItemUIManager.cornnencyQuantity >= 5)
                {
                    ItemUIManager.cornnencyQuantity -= 5;
                    ItemUIManager.jumpPotionsCount++;
                    audioManager.Play("ReggieBoughtSomething");
                }
            }
            if(s.Equals("HealthPotion"))
            {
                if (ItemUIManager.cornnencyQuantity >= 5)
                {
                    ItemUIManager.cornnencyQuantity -= 5;
                    ItemUIManager.healthPotionsCount++;
                    audioManager.Play("ReggieBoughtSomething");
                }
            }
        }
    }
}
