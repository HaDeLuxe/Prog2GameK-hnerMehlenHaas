using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class Camera {
        
        Vector2 cameraWorldPosition = new Vector2(0, 0);

        public void setCameraWorldPosition(Vector2 cameraWorldPosition) {
            this.cameraWorldPosition = cameraWorldPosition;
        }

        public Matrix cameraTransformationMatrix(Viewport viewport, Vector2 screenCentre) {
             Vector2 translation = -cameraWorldPosition + screenCentre;
             Matrix cameraMatrix = Matrix.CreateTranslation(translation.X, translation.Y, 0);
             return cameraMatrix;
        }

        public List<GameObject> objectsToRender(Vector2 playerPosition, List<GameObject> gameObjectsList) {
            List<GameObject> objectsToRender = new List<GameObject>();
            for(int i = 0; i < gameObjectsList.Count; i++)
            {
                if (gameObjectsList[i].Position.X < playerPosition.X + 950 && gameObjectsList[i].SpriteRectangle.Right > playerPosition.X - 950)
                {
                    if (gameObjectsList[i].Position.Y < playerPosition.Y + 550 && gameObjectsList[i].SpriteRectangle.Bottom > playerPosition.Y - 550)
                    {
                        objectsToRender.Add(gameObjectsList[i]);
                    }
                }
            }

            return objectsToRender;
        }
        public List<Enemy> RenderedEnemies(Vector2 playerPosition, List<Enemy> EnemyList)
        {
            List<Enemy> EnemyToRender = new List<Enemy>();
            for (int i = 0; i < EnemyList.Count; i++)
            {
                if (EnemyList[i].Position.X < playerPosition.X + 950 && EnemyList[i].SpriteRectangle.Right > playerPosition.X - 950)
                {
                    if (EnemyList[i].Position.Y < playerPosition.Y + 550 && EnemyList[i].SpriteRectangle.Bottom > playerPosition.Y - 550)
                    {
                        EnemyToRender.Add(EnemyList[i]);
                    }
                }
            }

            return EnemyToRender;
        }
    }
}
