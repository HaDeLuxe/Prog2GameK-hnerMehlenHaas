using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Reggie {
    class InputData {

        private static string[] SpriteSheetsData { get; set; }
        private string[] numbers;

        public InputData() {
            SpriteSheetsData = null;

        }

        
        public void ReadImageSizeDataSheet() {
            //Write Spritesheetsizes into an Array
            SpriteSheetsData = System.IO.File.ReadAllLines(@"Content\SpriteSheetSizes.txt");

            //Check if Input is correct
            //System.Console.WriteLine("Spritesheet Sizes:");
            //for (int i = 0; i < SpriteSheetsData.Length; i++) System.Console.WriteLine(SpriteSheetsData[i]);
            //int PlayerMoveXSize = 0;
            //int PlayerMoveYSize = 0;
            //int firstIndex = 0;
            //int lastIndex = 0;
            Match match = null;

            for (int i = 0; i < SpriteSheetsData.Length; i++)
            {
                //while (true)
                //{


                //    match = Regex.Match(SpriteSheetsData[i], "-?[0-9]+");

                //    if (match.Success)
                //    {
                //        System.Console.WriteLine(int.Parse(match.Value));
                //    }
                //    match = Regex.Match(SpriteSheetsData[i], "@");
                //    if (match.Equals('@')) break;
                //}

                foreach (var item in SpriteSheetsData)
                {
                    numbers = Regex.Split(item, "-?[0-9]+").Where(s => s != String.Empty).ToArray();
                    for (int m = 0; m < numbers.Length; m++) Console.WriteLine(numbers[m]);
                }
            }

            //for (int i = 0; i < numbers.Length; i++) Console.WriteLine(numbers[i]);


        }
    }
}
