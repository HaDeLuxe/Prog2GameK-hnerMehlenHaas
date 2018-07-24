using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Reggie {
    class InputData {

        private static List<String> SpriteSheetsData { get; set; }
        private List<String> numbers;

        public InputData() {
            SpriteSheetsData = null;

        }

        
        public void ReadImageSizeDataSheet() {
            //Write Spritesheetsizes into an Array
            SpriteSheetsData = new List<string>(System.IO.File.ReadAllLines(@"Content\SpriteSheetSizes.txt"));

            //Check if Input is correct
            //System.Console.WriteLine("Spritesheet Sizes:");
            //for (int i = 0; i < SpriteSheetsData.Length; i++) System.Console.WriteLine(SpriteSheetsData[i]);
            //int PlayerMoveXSize = 0;
            //int PlayerMoveYSize = 0;
            //int firstIndex = 0;
            //int lastIndex = 0;
            Match match = null;
            string[] result = null;

            List<int> indexToDelete = new List<int>();
            for (int i = 0; i < SpriteSheetsData.Count; i++)
            {
                SpriteSheetsData[i] = Regex.Replace(SpriteSheetsData[i], "[^0-9,]", "");
                if(SpriteSheetsData[i] == "") indexToDelete.Add(i);
            }


            SpriteSheetsData = SpriteSheetsData.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            foreach(String s in SpriteSheetsData)
            {
                System.Console.WriteLine(s);
            }
            

            //for (int i = 0; i < numbers.Length; i++) Console.WriteLine(numbers[i]);


        }
    }
}
