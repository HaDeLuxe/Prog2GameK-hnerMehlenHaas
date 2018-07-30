using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class LevelEditor {

        Vector2 mousePosition;
       

        public LevelEditor() {
            mousePosition = new Vector2(0, 0);
        }

        public void movePlatforms(ref List<GameObject> gameObjects) {
            var mouseState = Mouse.GetState();
            
            foreach(GameObject gO in gameObjects)
            {
                if (gO.SpriteRectangle.Contains(mousePosition))
                {
                    System.Console.WriteLine("Worked");
                }
            }
        }
    }
}
