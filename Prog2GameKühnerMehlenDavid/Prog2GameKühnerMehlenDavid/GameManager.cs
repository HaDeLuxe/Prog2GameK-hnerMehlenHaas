using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        GameObject SnailShell;
        GameObject Scissors;
        GameObject Armor;

        List<GameObject> ItemsFound;

        public GameManager() 
        {
            SnailShellPickedUp = false;
            ScissorsPickedUp = false;
            ItemsFound = new List<GameObject>();
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
                {
                    DestroyGameItem(Enums.ObjectsID.ARMOR, ref GameObjectList);
                }
            }


        }

        public void drawUI(Dictionary<string, Texture2D> TexturesDictionnary, SpriteBatch spriteBatch, Matrix transformationMatrix, GraphicsDevice graphics) 
        {
            spriteBatch.Draw(TexturesDictionnary["UI"], Vector2.Transform(new Vector2(-790, 90), Matrix.Invert(transformationMatrix)), Color.White);
            if (SnailShellPickedUp)
                spriteBatch.Draw(TexturesDictionnary["SnailShell"], Vector2.Transform(new Vector2(105, 695), Matrix.Invert(transformationMatrix)), Color.White);
            else spriteBatch.Draw(TexturesDictionnary["SnailShell"], Vector2.Transform(new Vector2(105, 695), Matrix.Invert(transformationMatrix)), Color.Black);
            if(ArmorPickedUp)
                spriteBatch.Draw(TexturesDictionnary["Armor_64x64"], Vector2.Transform(new Vector2(95, 505), Matrix.Invert(transformationMatrix)),null, Color.White,0,Vector2.One,new Vector2(1.4f,1.4f),SpriteEffects.None,0);
            else spriteBatch.Draw(TexturesDictionnary["Armor_64x64"], Vector2.Transform(new Vector2(95, 505), Matrix.Invert(transformationMatrix)), null, Color.Black, 0, Vector2.One, new Vector2(1.4f, 1.4f), SpriteEffects.None, 0);
            GameObject currentlyEquipped;

            if (ItemsFound.Count() > 0)
            {
                currentlyEquipped = ItemsFound[0];
                spriteBatch.Draw(currentlyEquipped.getTexture(), Vector2.Transform(new Vector2(145, 870), Matrix.Invert(transformationMatrix)), null, Color.White, 0, Vector2.One, new Vector2(1.4f, 1.4f), SpriteEffects.None, 0);
            }
            
            


        }

    }
}
