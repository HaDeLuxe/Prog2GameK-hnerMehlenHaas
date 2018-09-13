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
        Vector2 playerPos { get; }
       
        public IngameMenus(SpriteBatch spriteBatch, Dictionary<string, Texture2D> texturesDictionary, ref Vector2 playerPos)
        {
            this.spriteBatch = spriteBatch;
            this.texturesDictionary = texturesDictionary;
            this.playerPos = playerPos;
        }

        public void drawSaveIcon()
        {
            spriteBatch.Draw(texturesDictionary["SaveIcon"], new Vector2(playerPos.X-50, playerPos.Y-50), Color.White);
        }


        


    }
}
