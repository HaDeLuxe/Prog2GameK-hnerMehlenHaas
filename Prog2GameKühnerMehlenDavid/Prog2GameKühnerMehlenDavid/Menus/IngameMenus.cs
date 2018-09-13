using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Menus
{
    public class IngameMenus
    {
        SpriteBatch spriteBatch { get; }
        Dictionary<string, Texture2D> texturesDictionary { get; }
       
        public IngameMenus(SpriteBatch spriteBatch, Dictionary<string, Texture2D> texturesDictionary)
        {
            this.spriteBatch = spriteBatch;
            this.texturesDictionary = texturesDictionary;
        }

        public void drawSaveIcon(Vector2 playerPosition)
        {
            spriteBatch.Draw(texturesDictionary["SaveIcon"], new Vector2(playerPosition.X, playerPosition.Y -70), Color.White);
        }


        


    }
}
