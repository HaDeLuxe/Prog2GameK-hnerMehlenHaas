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

        public static bool snailShellPickedUp { get; set; }
        public static bool scissorsPickedUp { get; set; }
        public static bool armorPickedUp { get; set; }
        public static bool shovelPickedUp { get; set; }
        public static bool healthPickedUp { get; set; }
        public static bool jumpPickedUp { get; set; }
        public static bool powerPickedUp { get; set; }
        public static bool goldenUmbrellaPickedUp{ get; set; }
        GameObject scissors;
        GameObject shovel;
        GameObject idle;
        public static GameObject currentlyEquipped;
        GameObject healthPotion;
        GameObject jumpPotion;
        GameObject powerPotion;
        GameObject goldenUmbrella;
        bool triggerPushed = false;


        List<GameObject> ItemsFound;

        public ItemUIManager() 
        {
            snailShellPickedUp = false;
            scissorsPickedUp = false;
            ItemsFound = new List<GameObject>();
            currentlyEquipped = new Item(null, new Vector2(0, 0), new Vector2(0, 0), (int)Enums.ObjectsID.NONE);
        }

        //public int getCurrentEquipped() {
        //    return currentlyEquipped.objectID;
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
                    if (ItemsFound.Count == 0) currentlyEquipped = scissors;
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
                    if (ItemsFound.Count == 0) currentlyEquipped = shovel;
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
                    if(ItemsFound.Count == 0) currentlyEquipped = goldenUmbrella;
                    ItemsFound.Add(goldenUmbrella);
                    DestroyGameItem(Enums.ObjectsID.GOLDENUMBRELLA, ref GameObjectList);
                }
                    
            }
            




            if (GamePad.GetState(0).IsButtonDown(Buttons.LeftShoulder) && !triggerPushed)
            {
                if(ItemsFound.Count() > 0)
                {
                    int currentIndex = 0;
                    for (int i = 0; i < ItemsFound.Count(); i++)
                    {
                        if (ItemsFound[i] == currentlyEquipped) currentIndex = i;
                    }
                    currentIndex--;
                    if (currentIndex < 0) currentIndex = ItemsFound.Count()-1;
                    currentlyEquipped = ItemsFound[currentIndex];
                }
                triggerPushed = true;
            }

            if (GamePad.GetState(0).IsButtonDown(Buttons.RightShoulder) && !triggerPushed)
            {
                if (ItemsFound.Count() > 0)
                {
                    int currentIndex = 0;
                    for (int i = 0; i < ItemsFound.Count(); i++)
                    {
                        if (ItemsFound[i] == currentlyEquipped) currentIndex = i;
                    }
                    currentIndex++;
                    if (currentIndex >= ItemsFound.Count()) currentIndex = 0;
                    currentlyEquipped = ItemsFound[currentIndex];
                }
                triggerPushed = true;
            }
            if (GamePad.GetState(0).IsButtonUp(Buttons.LeftShoulder) && GamePad.GetState(0).IsButtonUp(Buttons.RightShoulder)) triggerPushed = false;


        }

        public void drawUI(Dictionary<string, Texture2D> TexturesDictionnary, SpriteBatch spriteBatch, Matrix transformationMatrix, GraphicsDevice graphics, float playerHPRatio) 
        {
            spriteBatch.Draw(TexturesDictionnary["Healthbar"], Vector2.Transform(new Vector2(10, 150+(1-playerHPRatio)*770), Matrix.Invert(transformationMatrix)), null, Color.White , 0, Vector2.One, new Vector2(1f,playerHPRatio),SpriteEffects.None,0);
            spriteBatch.Draw(TexturesDictionnary["UI"], Vector2.Transform(new Vector2(-751, 90), Matrix.Invert(transformationMatrix)), Color.White);

            if (snailShellPickedUp)
                spriteBatch.Draw(TexturesDictionnary["SnailShell"], Vector2.Transform(new Vector2(100, 685), Matrix.Invert(transformationMatrix)), Color.White);
            else spriteBatch.Draw(TexturesDictionnary["SnailShell"], Vector2.Transform(new Vector2(100, 685), Matrix.Invert(transformationMatrix)), Color.Black);
            if(armorPickedUp)
                spriteBatch.Draw(TexturesDictionnary["Armor_64x64"], Vector2.Transform(new Vector2(95, 495), Matrix.Invert(transformationMatrix)),null, Color.White,0,Vector2.One,new Vector2(1.4f,1.4f),SpriteEffects.None,0);
            else spriteBatch.Draw(TexturesDictionnary["Armor_64x64"], Vector2.Transform(new Vector2(95, 495), Matrix.Invert(transformationMatrix)), null, Color.Black, 0, Vector2.One, new Vector2(1.4f, 1.4f), SpriteEffects.None, 0);

            if (healthPickedUp)
                spriteBatch.Draw(TexturesDictionnary["HealthItem"], Vector2.Transform(new Vector2(355, 880), Matrix.Invert(transformationMatrix)), Color.White);

            if (ItemsFound.Count() > 0)
            {
                if(currentlyEquipped == goldenUmbrella)
                    spriteBatch.Draw(currentlyEquipped.getTexture(), Vector2.Transform(new Vector2(145, 960), Matrix.Invert(transformationMatrix)), null, Color.White, -45, Vector2.One, new Vector2(1.4f, 1.4f), SpriteEffects.None, 0);
                else
                    spriteBatch.Draw(currentlyEquipped.getTexture(), Vector2.Transform(new Vector2(145, 860), Matrix.Invert(transformationMatrix)), null, Color.White, 0, Vector2.One, new Vector2(1.4f, 1.4f), SpriteEffects.None, 0);
            }


            spriteBatch.Draw(TexturesDictionnary["buttonL1"], Vector2.Transform(new Vector2(97, 785), Matrix.Invert(transformationMatrix)), null, Color.Gray, 0, Vector2.One, new Vector2(0.7f, 0.7f), SpriteEffects.None, 0);
            spriteBatch.Draw(TexturesDictionnary["buttonR1"], Vector2.Transform(new Vector2(208, 785), Matrix.Invert(transformationMatrix)), null, Color.Gray, 0, Vector2.One, new Vector2(0.7f, 0.7f), SpriteEffects.None, 0);
            spriteBatch.Draw(TexturesDictionnary["buttonL2"], Vector2.Transform(new Vector2(297, 785), Matrix.Invert(transformationMatrix)), null, Color.Gray, 0, Vector2.One, new Vector2(0.7f, 0.7f), SpriteEffects.None, 0);
            spriteBatch.Draw(TexturesDictionnary["buttonR2"], Vector2.Transform(new Vector2(413, 785), Matrix.Invert(transformationMatrix)), null, Color.Gray, 0, Vector2.One, new Vector2(0.7f, 0.7f), SpriteEffects.None, 0);






        }

    }
}
