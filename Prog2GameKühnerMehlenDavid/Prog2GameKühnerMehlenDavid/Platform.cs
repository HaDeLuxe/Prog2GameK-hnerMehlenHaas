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
        public bool enemySpawnCheck;
        public bool canSpawnEnemy;
        public int PlatformType;


        public Platform(Texture2D gameObjectTexture, Vector2 gameObjectSize, Vector2 gameObjectPosition, int gameObjectID, int PlatformType, bool canSpawnEnemy) : base(gameObjectTexture,gameObjectSize, gameObjectPosition, gameObjectID)
        {
            enemySpawnCheck = false;
            this.canSpawnEnemy = canSpawnEnemy;
            //objectID = (int)Enums.ObjectsID.PLATFORM;
            this.PlatformType = PlatformType;
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjectList) { }
    }
}
