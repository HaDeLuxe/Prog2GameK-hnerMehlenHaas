using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class GameManager {

        public static bool SnailShellPickedUp { get; set; }
        public static bool ScissorsPickedUp { get; set; }
        public static bool ArmorPickedUp { get; set; }
        public static bool ShovelPickedUp { get; set; }
        GameObject SnailShell;
        GameObject Scissors;
        GameObject Armor;
        GameObject Shovel;
        GameObject currentlyEquipped;
        bool TriggerPushed = false;


        List<GameObject> ItemsFound;

        public GameManager() 
        {
            SnailShellPickedUp = false;
            ScissorsPickedUp = false;
            ItemsFound = new List<GameObject>();
        }

        public int getCurrentEquipped() {
            return currentlyEquipped.objectID;
        }


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
            if (SnailShell == null)
            {
                for (int i = 0; i < GameObjectList.Count(); i++)
                {
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.SNAILSHELL)
                        SnailShell = GameObjectList[i];
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.SCISSORS) Scissors = GameObjectList[i];
                }
            }
            if (SnailShell != null)
            {
                if (SnailShellPickedUp)
                {
                    DestroyGameItem(Enums.ObjectsID.SNAILSHELL, ref GameObjectList);
                }
            }

            if (Scissors == null)
            {
                for (int i = 0; i < GameObjectList.Count(); i++)
                {
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.SCISSORS)
                        Scissors = GameObjectList[i];
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.SCISSORS) Scissors = GameObjectList[i];
                }
            }
            if (Scissors != null)
            {
                if (ScissorsPickedUp)
                {
                    if (ItemsFound.Count == 0) currentlyEquipped = Scissors;
                    ItemsFound.Add(Scissors);
                    DestroyGameItem(Enums.ObjectsID.SCISSORS, ref GameObjectList);
                }
            }

            if (Armor == null)
            {
                for (int i = 0; i < GameObjectList.Count(); i++)
                {
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.ARMOR)
                        Armor = GameObjectList[i];
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.ARMOR) Armor = GameObjectList[i];
                }
            }
            if (Armor != null)
            {
                if (ArmorPickedUp)
                    DestroyGameItem(Enums.ObjectsID.ARMOR, ref GameObjectList);
            }

            if (Shovel == null)
            {
                for (int i = 0; i < GameObjectList.Count(); i++)
                {
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.SHOVEL)
                        Shovel = GameObjectList[i];
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.ARMOR) Shovel = GameObjectList[i];
                }
            }
            if (Shovel != null)
            {
                if (ShovelPickedUp)
                {
                    if (ItemsFound.Count == 0) currentlyEquipped = Shovel;
                    ItemsFound.Add(Shovel);
                    DestroyGameItem(Enums.ObjectsID.SHOVEL, ref GameObjectList);
                }
            }

            if (GamePad.GetState(0).IsButtonDown(Buttons.LeftShoulder) && !TriggerPushed)
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
                TriggerPushed = true;
            }
            if (GamePad.GetState(0).IsButtonDown(Buttons.RightShoulder) && !TriggerPushed)
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
                TriggerPushed = true;
            }
            if (GamePad.GetState(0).IsButtonUp(Buttons.LeftShoulder) && GamePad.GetState(0).IsButtonUp(Buttons.RightShoulder)) TriggerPushed = false;


        }

        public void drawUI(Dictionary<string, Texture2D> TexturesDictionnary, SpriteBatch spriteBatch, Matrix transformationMatrix, GraphicsDevice graphics) 
        {
            spriteBatch.Draw(TexturesDictionnary["UI"], Vector2.Transform(new Vector2(-797, 90), Matrix.Invert(transformationMatrix)), Color.White);
            if (SnailShellPickedUp)
                spriteBatch.Draw(TexturesDictionnary["SnailShell"], Vector2.Transform(new Vector2(105, 685), Matrix.Invert(transformationMatrix)), Color.White);
            else spriteBatch.Draw(TexturesDictionnary["SnailShell"], Vector2.Transform(new Vector2(105, 685), Matrix.Invert(transformationMatrix)), Color.Black);
            if(ArmorPickedUp)
                spriteBatch.Draw(TexturesDictionnary["Armor_64x64"], Vector2.Transform(new Vector2(95, 495), Matrix.Invert(transformationMatrix)),null, Color.White,0,Vector2.One,new Vector2(1.4f,1.4f),SpriteEffects.None,0);
            else spriteBatch.Draw(TexturesDictionnary["Armor_64x64"], Vector2.Transform(new Vector2(95, 495), Matrix.Invert(transformationMatrix)), null, Color.Black, 0, Vector2.One, new Vector2(1.4f, 1.4f), SpriteEffects.None, 0);



            if (ItemsFound.Count() > 0)
            {
                //currentlyEquipped = ItemsFound[0];
                spriteBatch.Draw(currentlyEquipped.getTexture(), Vector2.Transform(new Vector2(145, 860), Matrix.Invert(transformationMatrix)), null, Color.White, 0, Vector2.One, new Vector2(1.4f, 1.4f), SpriteEffects.None, 0);
            }


            spriteBatch.Draw(TexturesDictionnary["buttonL1"], Vector2.Transform(new Vector2(88, 785), Matrix.Invert(transformationMatrix)), null, Color.Gray, 0, Vector2.One, new Vector2(0.7f, 0.7f), SpriteEffects.None, 0);
            spriteBatch.Draw(TexturesDictionnary["buttonR1"], Vector2.Transform(new Vector2(208, 785), Matrix.Invert(transformationMatrix)), null, Color.Gray, 0, Vector2.One, new Vector2(0.7f, 0.7f), SpriteEffects.None, 0);
            spriteBatch.Draw(TexturesDictionnary["buttonL2"], Vector2.Transform(new Vector2(288, 785), Matrix.Invert(transformationMatrix)), null, Color.Gray, 0, Vector2.One, new Vector2(0.7f, 0.7f), SpriteEffects.None, 0);
            spriteBatch.Draw(TexturesDictionnary["buttonR2"], Vector2.Transform(new Vector2(413, 785), Matrix.Invert(transformationMatrix)), null, Color.Gray, 0, Vector2.One, new Vector2(0.7f, 0.7f), SpriteEffects.None, 0);






        }

    }
}
