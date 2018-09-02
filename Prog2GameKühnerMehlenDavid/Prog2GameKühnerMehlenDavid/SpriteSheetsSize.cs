using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Reggie {
    class SpriteSheetSizes {

        private List<String> spriteSheetsData;
        public static Dictionary<string, int> spritesSizes{get; private set; }

        //Constructor
        public SpriteSheetSizes() {
            spriteSheetsData = null;
            spritesSizes = new Dictionary<string, int>();
        }


        /// <summary>
        /// Reads the SpriteSheetSizes.txt and writes each line in a own String in the List SpriteSheetData
        /// </summary>
        public void ReadImageSizeDataSheet() 
        {
            //Write Spritesheetsizes into an Array
            spriteSheetsData = new List<string>(System.IO.File.ReadAllLines(@"SpriteSheetSizes.txt"));
            
            for (int i = 0; i < spriteSheetsData.Count; i++)
            {
                spriteSheetsData[i] = Regex.Replace(spriteSheetsData[i], "[^0-9,]", "");
            }

            spriteSheetsData = spriteSheetsData.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            List<String> spriteStringList = new List<String>();
            foreach (String stringData in spriteSheetsData)
            {
                List<String> tempStringList = stringData.Split(',').ToList();
                foreach (String st in tempStringList) spriteStringList.Add(st);
            
            }

            for(int i = 0; i < spriteStringList.Count; i++)
            {
                switch (i)
                {
                    default:
                        spritesSizes.Add("Error", 50);
                        break;
                    case 0:
                        spritesSizes.Add("Reggie_Move_X", Int32.Parse(spriteStringList[i]));
                        break;
                    case 1:
                        spritesSizes.Add("Reggie_Move_Y", Int32.Parse(spriteStringList[i]));
                        break;
                    case 2:
                        spritesSizes.Add("Reggie_Move_Hitbox_Pos_X", Int32.Parse(spriteStringList[i]));
                        break;
                    case 3:
                        spritesSizes.Add("Reggie_Move_Hitbox_Pos_Y", Int32.Parse(spriteStringList[i]));
                        break;
                    case 4:
                        spritesSizes.Add("Reggie_Move_Hitbox_Size_X", Int32.Parse(spriteStringList[i]));
                        break;
                    case 5:
                        spritesSizes.Add("Reggie_Move_Hitbox_Size_Y", Int32.Parse(spriteStringList[i]));
                        break;
                    case 6:
                        spritesSizes.Add("Reggie_Jump_X", Int32.Parse(spriteStringList[i]));
                        break;
                    case 7:
                        spritesSizes.Add("Reggie_Jump_Y", Int32.Parse(spriteStringList[i]));
                        break;
                    case 8:
                        spritesSizes.Add("Reggie_Attack_X", Int32.Parse(spriteStringList[i]));
                        break;
                    case 9:
                        spritesSizes.Add("Reggie_Attack_Y", Int32.Parse(spriteStringList[i]));
                        break;
                    case 10:
                        spritesSizes.Add("Reggie_Move_Hat_X", Int32.Parse(spriteStringList[i]));
                        break;
                    case 11:
                        spritesSizes.Add("Reggie_Move_Hat_Y", Int32.Parse(spriteStringList[i]));
                        break;
                    case 12:
                        spritesSizes.Add("Reggie_Jump_Hat_X", Int32.Parse(spriteStringList[i]));
                        break;
                    case 13:
                        spritesSizes.Add("Reggie_Jump_Hat_Y", Int32.Parse(spriteStringList[i]));
                        break;
                    case 14:
                        spritesSizes.Add("Reggie_Attack_Hat_X", Int32.Parse(spriteStringList[i]));
                        break;
                    case 15:
                        spritesSizes.Add("Reggie_Attack_Hat_Y", Int32.Parse(spriteStringList[i]));
                        break;
                    case 16:
                        spritesSizes.Add("Reggie_Jump_Armor_X", Int32.Parse(spriteStringList[i]));
                        break;
                    case 17:
                        spritesSizes.Add("Reggie_Jump_Armor_Y", Int32.Parse(spriteStringList[i]));
                        break;


                }
            }

            for(int i = 0; i < spritesSizes.Count; i++)
            {
                String s = "" + spritesSizes.ElementAt(i);
                System.Console.WriteLine(s);
            }

            
            
        }
    }
}
