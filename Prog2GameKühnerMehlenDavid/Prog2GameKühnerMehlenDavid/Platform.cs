using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie
{
    public class Platform : GameObject
    {
        public bool usedEnemySpawn;
        public Platform(Texture2D gameObjectTexture, Vector2 gameObjectSize, Vector2 gameObjectPosition) : base(gameObjectTexture,gameObjectSize, gameObjectPosition)
        {
            usedEnemySpawn = false;
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjectList) { }
    }
}
