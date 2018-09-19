using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class ItemUIManager {

        public static bool snailShellPickedUp { get; set; } = false;
        public static bool scissorsPickedUp { get; set; } = false;
        public static bool armorPickedUp { get; set; } = false;
        public static bool shovelPickedUp { get; set; } = false;
        public static bool healthPickedUp { get; set; } = false;
        public static bool jumpPickedUp { get; set; } = false;
        public static bool powerPickedUp { get; set; } = false;
        public static bool goldenUmbrellaPickedUp { get; set; } = false;
        public static int cornnencyQuantity { get; set; }
        GameObject scissors;
        GameObject shovel;
        public static GameObject currentItemEquipped;
        public static GameObject currentPotionEquipped;
        GameObject healthPotion;
        GameObject jumpPotion;
        GameObject powerPotion;
        GameObject goldenUmbrella;
        bool shoulderButtonPushed = false;
        bool triggerButtonPushed = false;
        public static int healthPotionsCount { get; set; } = 0;
        public static int powerPotionsCount { get; set; } = 0;
        public static int jumpPotionsCount { get; set; } = 0;
 

        List<GameObject> ItemsFound;
        List<GameObject> PotionsFound;

        public ItemUIManager() 
        {
            ItemsFound = new List<GameObject>();
            PotionsFound = new List<GameObject>();
            currentItemEquipped = new Item(null, new Vector2(0, 0), new Vector2(0, 0), (int)Enums.ObjectsID.NONE);
            //MUSS EIGENTLICH AUS SAVE GELADEN WERDEN?! UND DELETET WENN
            cornnencyQuantity = 0;
        }

        //public int getCurrentEquipped() {
        //    return currentItemEquipped.objectID;
        //}


        private void DestroyGameItem(Enums.ObjectsID ObjectID, ref List<GameObject> GameObjectList) {
            for(int i = 0; i < GameObjectList.Count(); i++)
            {
                if(GameObjectList[i].objectID == (int)ObjectID)
                {
                    GameObjectList.RemoveAt(i);
                }
            }
        }

        public void ManageItems(ref Player Player, ref List<GameObject> GameObjectList) 
        {
          
            if (snailShellPickedUp)
            {
               
               DestroyGameItem(Enums.ObjectsID.SNAILSHELL, ref GameObjectList);
            }
           

            if (scissors == null)
            {
                for (int i = 0; i < GameObjectList.Count(); i++)
                {
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.SCISSORS)
                        scissors = GameObjectList[i];
                }
            }
            if (scissors != null)
            {
                if (scissorsPickedUp)
                {
                    if (ItemsFound.Count == 0) currentItemEquipped = scissors;
                    ItemsFound.Add(scissors);
                    DestroyGameItem(Enums.ObjectsID.SCISSORS, ref GameObjectList);
                }
            }
            
            if (armorPickedUp)
                DestroyGameItem(Enums.ObjectsID.ARMOR, ref GameObjectList);

            if (shovel == null)
            {
                for (int i = 0; i < GameObjectList.Count(); i++)
                {
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.SHOVEL)
                        shovel = GameObjectList[i];
                }
            }
            if (shovel != null)
            {
                if (shovelPickedUp)
                {
                    if (ItemsFound.Count == 0) currentItemEquipped = shovel;
                    ItemsFound.Add(shovel);
                    DestroyGameItem(Enums.ObjectsID.SHOVEL, ref GameObjectList);
                }
            }
            if (healthPotion == null)
            {
                for (int i = 0; i < GameObjectList.Count(); i++)
                {
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.HEALTHPOTION)
                        healthPotion = GameObjectList[i];
                }
            }
            if(healthPotion != null)
            {
                if (healthPickedUp)
                {
                    if (PotionsFound.Count == 0) currentPotionEquipped = healthPotion;
                    PotionsFound.Add(healthPotion);
                    DestroyGameItem(Enums.ObjectsID.HEALTHPOTION, ref GameObjectList);
                }
            }
            if(jumpPotion == null)
            {
                for (int i = 0; i < GameObjectList.Count(); i++)
                {
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.JUMPPOTION)
                        jumpPotion = GameObjectList[i];
                }
            }
            if(jumpPotion != null)
            {
                if (jumpPickedUp)
                {
                    if (PotionsFound.Count == 0) currentPotionEquipped = jumpPotion;
                    PotionsFound.Add(jumpPotion);
                    DestroyGameItem(Enums.ObjectsID.JUMPPOTION, ref GameObjectList);
                }
            }
            if(powerPotion == null)
            {
                for (int i = 0; i < GameObjectList.Count(); i++)
                {
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.POWERPOTION)
                        powerPotion = GameObjectList[i];
                }
            }
            if (powerPotion != null)
            {
                if(powerPickedUp)
                {
                    if (PotionsFound.Count == 0) currentPotionEquipped = powerPotion;
                    PotionsFound.Add(powerPotion);
                    DestroyGameItem(Enums.ObjectsID.POWERPOTION, ref GameObjectList);
                }
            }
            if(goldenUmbrella == null){
                for (int i = 0; i < GameObjectList.Count(); i++)
                {
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.GOLDENUMBRELLA)
                        goldenUmbrella = GameObjectList[i];
                }
            }
            if(goldenUmbrella != null)
            {
                if (goldenUmbrellaPickedUp)
                {
                    if(ItemsFound.Count == 0) currentItemEquipped = goldenUmbrella;
                    ItemsFound.Add(goldenUmbrella);
                    DestroyGameItem(Enums.ObjectsID.GOLDENUMBRELLA, ref GameObjectList);
                }
                    
            }
            




            if (GamePad.GetState(0).IsButtonDown(Buttons.LeftShoulder) && !shoulderButtonPushed)
            {
                if(ItemsFound.Count() > 0)
                {
                    int currentIndex = 0;
                    for (int i = 0; i < ItemsFound.Count(); i++)
                    {
                        if (ItemsFound[i] == currentItemEquipped) currentIndex = i;
                    }
                    currentIndex--;
                    if (currentIndex < 0) currentIndex = ItemsFound.Count()-1;
                    currentItemEquipped = ItemsFound[currentIndex];
                }
                shoulderButtonPushed = true;
            }

            if (GamePad.GetState(0).IsButtonDown(Buttons.RightShoulder) && !shoulderButtonPushed)
            {
                if (ItemsFound.Count() > 0)
                {
                    int currentIndex = 0;
                    for (int i = 0; i < ItemsFound.Count(); i++)
                    {
                        if (ItemsFound[i] == currentItemEquipped) currentIndex = i;
                    }
                    currentIndex++;
                    if (currentIndex >= ItemsFound.Count()) currentIndex = 0;
                    currentItemEquipped = ItemsFound[currentIndex];
                }
                shoulderButtonPushed = true;
            }
            if (GamePad.GetState(0).IsButtonUp(Buttons.LeftShoulder) && GamePad.GetState(0).IsButtonUp(Buttons.RightShoulder)) shoulderButtonPushed = false;

            if (GamePad.GetState(0).IsButtonDown(Buttons.RightTrigger) && !triggerButtonPushed)
            {
                if (PotionsFound.Count() > 0)
                {
                    int currentIndex = 0;
                    for (int i = 0; i < PotionsFound.Count(); i++)
                    {
                        if (PotionsFound[i] == currentPotionEquipped) currentIndex = i;
                    }
                    currentIndex++;
                    if (currentIndex >= PotionsFound.Count()) currentIndex = 0;
                    currentPotionEquipped = PotionsFound[currentIndex];
                }
                triggerButtonPushed = true;
            }
            if (GamePad.GetState(0).IsButtonDown(Buttons.LeftTrigger) && !triggerButtonPushed)
            {
                if (PotionsFound.Count() > 0)
                {
                    int currentIndex = 0;
                    for (int i = 0; i < PotionsFound.Count(); i++)
                    {
                        if (PotionsFound[i] == currentPotionEquipped) currentIndex = i;
                    }
                    currentIndex--;
                    if (currentIndex < 0) currentIndex = PotionsFound.Count() - 1;
                    currentPotionEquipped = PotionsFound[currentIndex];
                }
                triggerButtonPushed = true;
            }
            if (GamePad.GetState(0).IsButtonUp(Buttons.LeftTrigger) && GamePad.GetState(0).IsButtonUp(Buttons.RightTrigger)) triggerButtonPushed = false;


        }

        public void drawUI(Dictionary<string, Texture2D> TexturesDictionnary, SpriteBatch spriteBatch, Matrix transformationMatrix, GraphicsDevice graphics, float playerHPRatio, SpriteFont font) 
        {
            spriteBatch.Draw(TexturesDictionnary["Healthbar"], Vector2.Transform(new Vector2(10, 150+(1-playerHPRatio)*770), Matrix.Invert(transformationMatrix)), null, Color.White , 0, Vector2.One, new Vector2(1f,playerHPRatio),SpriteEffects.None,0);
            spriteBatch.Draw(TexturesDictionnary["UI"], Vector2.Transform(new Vector2(-751, 90), Matrix.Invert(transformationMatrix)), Color.White);

            if (snailShellPickedUp)
                spriteBatch.Draw(TexturesDictionnary["SnailShell"], Vector2.Transform(new Vector2(100, 685), Matrix.Invert(transformationMatrix)), Color.White);
            else spriteBatch.Draw(TexturesDictionnary["SnailShell"], Vector2.Transform(new Vector2(100, 685), Matrix.Invert(transformationMatrix)), Color.Black);
            if(armorPickedUp)
                spriteBatch.Draw(TexturesDictionnary["Armor_64x64"], Vector2.Transform(new Vector2(95, 495), Matrix.Invert(transformationMatrix)),null, Color.White,0,Vector2.One,new Vector2(1.4f,1.4f),SpriteEffects.None,0);
            else spriteBatch.Draw(TexturesDictionnary["Armor_64x64"], Vector2.Transform(new Vector2(95, 495), Matrix.Invert(transformationMatrix)), null, Color.Black, 0, Vector2.One, new Vector2(1.4f, 1.4f), SpriteEffects.None, 0);

            
            if (ItemsFound.Count() > 0)
            {
                if(currentItemEquipped == goldenUmbrella)
                    spriteBatch.Draw(currentItemEquipped.getTexture(), Vector2.Transform(new Vector2(145, 960), Matrix.Invert(transformationMatrix)), null, Color.White, -45, Vector2.One, new Vector2(1.4f, 1.4f), SpriteEffects.None, 0);
                else
                    spriteBatch.Draw(currentItemEquipped.getTexture(), Vector2.Transform(new Vector2(145, 860), Matrix.Invert(transformationMatrix)), null, Color.White, 0, Vector2.One, new Vector2(1.4f, 1.4f), SpriteEffects.None, 0);
                if(currentItemEquipped.objectID == (int)Enums.ObjectsID.GOLDENUMBRELLA)
                {
                    spriteBatch.DrawString(font, "Gold Umbrella", Vector2.Transform(new Vector2(112, 990), Matrix.Invert(transformationMatrix)), Color.Black, 0, Vector2.Zero, 0.4f, SpriteEffects.None, 0);
                }
                if(currentItemEquipped.objectID == (int)Enums.ObjectsID.SHOVEL)
                    spriteBatch.DrawString(font, "Shovel", Vector2.Transform(new Vector2(150, 990), Matrix.Invert(transformationMatrix)), Color.Black, 0, Vector2.Zero, 0.4f, SpriteEffects.None, 0);
                if(currentItemEquipped.objectID == (int)Enums.ObjectsID.SCISSORS)
                    spriteBatch.DrawString(font, "Scissors", Vector2.Transform(new Vector2(145, 990), Matrix.Invert(transformationMatrix)), Color.Black, 0, Vector2.Zero, 0.4f, SpriteEffects.None, 0);

            }

            if (PotionsFound.Count() > 0)
            {
                spriteBatch.Draw(currentPotionEquipped.getTexture(), Vector2.Transform(new Vector2(345, 865), Matrix.Invert(transformationMatrix)), null, Color.White, 0, Vector2.One, new Vector2(1.4f, 1.4f), SpriteEffects.None, 0);
                if(currentPotionEquipped.objectID == (int)Enums.ObjectsID.HEALTHPOTION)
                {
                    if(healthPotionsCount < 10)
                        spriteBatch.DrawString(font, healthPotionsCount.ToString(), Vector2.Transform(new Vector2(380, 820), Matrix.Invert(transformationMatrix)), Color.DarkRed, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
                    else
                        spriteBatch.DrawString(font, healthPotionsCount.ToString(), Vector2.Transform(new Vector2(365, 820), Matrix.Invert(transformationMatrix)), Color.DarkRed, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(font, "Health Potion", Vector2.Transform(new Vector2(315, 990), Matrix.Invert(transformationMatrix)), Color.Black, 0,Vector2.Zero, 0.4f,SpriteEffects.None, 0);
                }
                if (currentPotionEquipped.objectID == (int)Enums.ObjectsID.JUMPPOTION)
                {
                    if(jumpPotionsCount < 10)
                        spriteBatch.DrawString(font, jumpPotionsCount.ToString(), Vector2.Transform(new Vector2(380, 820), Matrix.Invert(transformationMatrix)), Color.DarkRed, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
                    else
                        spriteBatch.DrawString(font, jumpPotionsCount.ToString(), Vector2.Transform(new Vector2(365, 820), Matrix.Invert(transformationMatrix)), Color.DarkRed, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0);

                    spriteBatch.DrawString(font, "Jump Potion", Vector2.Transform(new Vector2(320, 990), Matrix.Invert(transformationMatrix)), Color.Black, 0, Vector2.Zero, 0.4f, SpriteEffects.None, 0);
                }
                if (currentPotionEquipped.objectID == (int)Enums.ObjectsID.POWERPOTION)
                {
                    if(powerPotionsCount < 10)
                        spriteBatch.DrawString(font, powerPotionsCount.ToString(), Vector2.Transform(new Vector2(380, 820), Matrix.Invert(transformationMatrix)), Color.DarkRed, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
                    else
                        spriteBatch.DrawString(font, powerPotionsCount.ToString(), Vector2.Transform(new Vector2(365, 820), Matrix.Invert(transformationMatrix)), Color.DarkRed, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(font, "Power Potion", Vector2.Transform(new Vector2(315, 990), Matrix.Invert(transformationMatrix)), Color.Black, 0, Vector2.Zero, 0.4f, SpriteEffects.None, 0);
                }
            }


            var temp = string.Format("Coins: {0}", cornnencyQuantity);
            spriteBatch.DrawString(font, temp, Vector2.Transform(new Vector2(500, 900), Matrix.Invert(transformationMatrix)), Color.Black);
           

            spriteBatch.Draw(TexturesDictionnary["buttonL1"], Vector2.Transform(new Vector2(97, 785), Matrix.Invert(transformationMatrix)), null, Color.Gray, 0, Vector2.One, new Vector2(0.7f, 0.7f), SpriteEffects.None, 0);
            spriteBatch.Draw(TexturesDictionnary["buttonR1"], Vector2.Transform(new Vector2(208, 785), Matrix.Invert(transformationMatrix)), null, Color.Gray, 0, Vector2.One, new Vector2(0.7f, 0.7f), SpriteEffects.None, 0);
            spriteBatch.Draw(TexturesDictionnary["buttonL2"], Vector2.Transform(new Vector2(297, 785), Matrix.Invert(transformationMatrix)), null, Color.Gray, 0, Vector2.One, new Vector2(0.7f, 0.7f), SpriteEffects.None, 0);
            spriteBatch.Draw(TexturesDictionnary["buttonR2"], Vector2.Transform(new Vector2(413, 785), Matrix.Invert(transformationMatrix)), null, Color.Gray, 0, Vector2.One, new Vector2(0.7f, 0.7f), SpriteEffects.None, 0);
        }

        

    }
}
