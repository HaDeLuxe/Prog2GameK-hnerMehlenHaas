using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Reggie {
    class SpriteSheetSizes {

        private List<String> SpriteSheetsData;
        public static Dictionary<string, int> SpritesSizes{get; private set; }

        //Constructor
        public SpriteSheetSizes() {
            SpriteSheetsData = null;
            SpritesSizes = new Dictionary<string, int>();
        }


        /// <summary>
        /// Reads the SpriteSheetSizes.txt and writes each line in a own String in the List SpriteSheetData
        /// </summary>
        public void ReadImageSizeDataSheet() 
        {
            //Write Spritesheetsizes into an Array
            SpriteSheetsData = new List<string>(System.IO.File.ReadAllLines(@"SpriteSheetSizes.txt"));
            
            for (int i = 0; i < SpriteSheetsData.Count; i++)
            {
                SpriteSheetsData[i] = Regex.Replace(SpriteSheetsData[i], "[^0-9,]", "");
            }

            SpriteSheetsData = SpriteSheetsData.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            List<String> SpriteStringList = new List<String>();
            foreach (String s in SpriteSheetsData)
            {
                List<String> tempStringList = s.Split(',').ToList();
                foreach (String st in tempStringList) SpriteStringList.Add(st);
                //System.Console.WriteLine(s);
            
            }

            for(int i = 0; i < SpriteStringList.Count; i++)
            {
                switch (i)
                {
                    default:
                        SpritesSizes.Add("Error", 50);
                        break;
                    case 0:
                        SpritesSizes.Add("Reggie_Move_X", Int32.Parse(SpriteStringList[i]));
                        break;
                    case 1:
                        SpritesSizes.Add("Reggie_Move_Y", Int32.Parse(SpriteStringList[i]));
                        break;
                    case 2:
                        SpritesSizes.Add("Reggie_Move_Hitbox_Pos_X", Int32.Parse(SpriteStringList[i]));
                        break;
                    case 3:
                        SpritesSizes.Add("Reggie_Move_Hitbox_Pos_Y", Int32.Parse(SpriteStringList[i]));
                        break;
                    case 4:
                        SpritesSizes.Add("Reggie_Move_Hitbox_Size_X", Int32.Parse(SpriteStringList[i]));
                        break;
                    case 5:
                        SpritesSizes.Add("Reggie_Move_Hitbox_Size_Y", Int32.Parse(SpriteStringList[i]));
                        break;
                    case 6:
                        SpritesSizes.Add("Reggie_Jump_X", Int32.Parse(SpriteStringList[i]));
                        break;
                    case 7:
                        SpritesSizes.Add("Reggie_Jump_Y", Int32.Parse(SpriteStringList[i]));
                        break;
                }
            }

            for(int i = 0; i < SpritesSizes.Count; i++)
            {
                String s = "" + SpritesSizes.ElementAt(i);
                System.Console.WriteLine(s);
            }
            
        }
    }
}
