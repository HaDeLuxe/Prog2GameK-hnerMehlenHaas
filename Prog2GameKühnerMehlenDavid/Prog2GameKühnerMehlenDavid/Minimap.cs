using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class Minimap {

        public void drawMinimap(Matrix transformationMatrix, SpriteBatch spriteBatch, Vector2 playerPos, ref Dictionary<string, Texture2D> texturesDictionary, int ViewportWidth, int ViewportHeight) {
            if(Game1.currentGameState == Game1.GameState.GAMELOOP)
            {
                spriteBatch.Draw(texturesDictionary["Minimap"], Vector2.Transform(new Vector2(ViewportWidth-151, ViewportHeight-170), Matrix.Invert(transformationMatrix)), null,Color.White,0f,Vector2.Zero,0.2f,SpriteEffects.None,0f);
            }
            if(Game1.currentGameState == Game1.GameState.MINIMAP)
            {
                Vector2 playerPosOnMiniMap = new Vector2(0,0);
                playerPosOnMiniMap.X = ((playerPos.X += 13312) / 19456) * 608;
                playerPosOnMiniMap.Y = ((playerPos.Y += 14336) / 22528) * 704;
                spriteBatch.Draw(texturesDictionary["Minimap"], Vector2.Transform(new Vector2(ViewportWidth/2 - 304, ViewportHeight/2 - 352), Matrix.Invert(transformationMatrix)), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                spriteBatch.Draw(texturesDictionary["Point"], Vector2.Transform(new Vector2(ViewportWidth / 2 - 304 + playerPosOnMiniMap.X, ViewportHeight / 2 - 352 + playerPosOnMiniMap.Y), Matrix.Invert(transformationMatrix)), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            }
        }
    }
}
