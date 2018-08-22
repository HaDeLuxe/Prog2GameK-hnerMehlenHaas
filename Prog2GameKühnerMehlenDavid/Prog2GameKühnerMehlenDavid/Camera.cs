using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Threading.Tasks;

namespace Reggie
{
    class Camera
    {
        
        Vector2 cameraWorldPosition = new Vector2(0, 0);
        float zoom = .05f;

        public void setCameraWorldPosition(Vector2 cameraWorldPosition)
        {
            this.cameraWorldPosition = cameraWorldPosition;
        }

        public Matrix cameraTransformationMatrix(Viewport viewport, Vector2 screenCenter)
        {
             Vector2 translation = -cameraWorldPosition + screenCenter;
             Matrix cameraMatrix = Matrix.CreateTranslation(translation.X, translation.Y, 0) * Matrix.CreateScale(zoom,zoom, 1);
             
             return cameraMatrix;
        }

        public List<GameObject> GameObjectsToRender(Vector2 playerPosition, List<GameObject> gameObjectsList, ref List<GameObject> interactiveObject)
        {
            List<GameObject> objectsToRender = new List<GameObject>();
            List<GameObject> interactiveObjectsList = new List<GameObject>();
            for(int i = 0; i < gameObjectsList.Count; i++)
            {
                if (gameObjectsList[i].gameObjectPosition.X < playerPosition.X + 1350 && gameObjectsList[i].gameObjectRectangle.Right > playerPosition.X - 1350
                    && gameObjectsList[i].gameObjectPosition.Y < playerPosition.Y + 750 && gameObjectsList[i].gameObjectRectangle.Bottom > playerPosition.Y - 750)
                {
                        objectsToRender.Add(gameObjectsList[i]);
                    if (gameObjectsList[i].objectID == (int)Enums.ObjectsID.VINE 
                        || gameObjectsList[i].objectID == (int)Enums.ObjectsID.SNAILSHELL
                        || gameObjectsList[i].objectID == (int)Enums.ObjectsID.SCISSORS
                        || gameObjectsList[i].objectID == (int)Enums.ObjectsID.ARMOR
                        || gameObjectsList[i].objectID == (int)Enums.ObjectsID.SHOVEL)
                        interactiveObjectsList.Add(gameObjectsList[i]);
                }
            }
            interactiveObject = interactiveObjectsList;
            return objectsToRender;
        }

        public void SpawnEnemyOffScreen(Player wormPlayer, List<Platform> platformList, ref List<Enemy> enemyList, Texture2D enemySkinTexture)
        {
           
            for (int i = 0; i < platformList.Count; i++)
            {
                if (!platformList[i].enemySpawnCheck)
                {
                    if (platformList[i].gameObjectPosition.X < wormPlayer.gameObjectPosition.X + 1250 && platformList[i].gameObjectRectangle.Right > wormPlayer.gameObjectPosition.X - 1250 && platformList[i].gameObjectPosition.Y < wormPlayer.gameObjectPosition.Y + 750 && platformList[i].gameObjectRectangle.Bottom > wormPlayer.gameObjectPosition.Y - 750)
                        platformList[i].enemySpawnCheck = true;
                    if ((platformList[i].gameObjectPosition.X < wormPlayer.gameObjectPosition.X + 1250 && platformList[i].gameObjectPosition.X > wormPlayer.gameObjectPosition.X + 950) || (platformList[i].gameObjectRectangle.Right > wormPlayer.gameObjectPosition.X - 1250 && platformList[i].gameObjectRectangle.Right < wormPlayer.gameObjectPosition.X - 950)
                        || (platformList[i].gameObjectPosition.Y < wormPlayer.gameObjectPosition.Y + 750 && platformList[i].gameObjectPosition.Y > wormPlayer.gameObjectPosition.Y + 550) || (platformList[i].gameObjectRectangle.Bottom > wormPlayer.gameObjectPosition.Y - 750 && platformList[i].gameObjectRectangle.Bottom < wormPlayer.gameObjectPosition.Y - 550))
                    {
                        platformList[i].enemySpawnCheck = true;
                        Random rand = new Random();
                        int randomizedNumber = rand.Next(0, 100);
                        if (randomizedNumber % 2 == 0)
                        {
                            enemyList.Add(new Enemy(enemySkinTexture, new Vector2(50, 50), new Vector2(platformList[i].gameObjectPosition.X + (platformList[i].gameObjectSize.X / 2), platformList[i].gameObjectPosition.Y - 50), (int)Enums.ObjectsID.ENEMY));
                            enemyList.Last().SetPlayer(wormPlayer);
                        }

                    }
                }
            }
        
        }

       

        public List<Enemy> RenderedEnemies(Vector2 playerPosition, List<Enemy> enemyList)
        {
            List<Enemy> enemyToRender = new List<Enemy>();
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i].gameObjectPosition.X < playerPosition.X + 1050 && enemyList[i].gameObjectRectangle.Right > playerPosition.X - 1050)
                {
                    if (enemyList[i].gameObjectPosition.Y < playerPosition.Y + 550 && enemyList[i].gameObjectRectangle.Bottom > playerPosition.Y - 550)
                    {
                        enemyToRender.Add(enemyList[i]);
                    }
                }
            }

            return enemyToRender;
        }

        float timeUntilNextFrame = 0;
        int cap = 200;
        public int counterLeft = 0;
        public int counterRight = 0;

        public void IncreaseLeftCounter() { counterLeft++; }
        public void IncreaseRightCounter(){ counterRight++; }
        public void ResetLeftCounter(){ counterLeft = 0; }
        public void ResetRightCounter(){ counterRight = 0; }

        public void cameraOffset(GameTime GameTime, bool Left, bool isMoving) 
        {
            if(Game1.currentGameState == Game1.GameState.GAMELOOP)
            {
                if(isMoving) cap = 0;
                else cap = 200;
                float animationFrameTime = 1f/1000f;
            
                float gameFrameTime = (float)GameTime.ElapsedGameTime.TotalSeconds;
                timeUntilNextFrame -= gameFrameTime;

                if (timeUntilNextFrame <= 0)
                {
                    if (Left && Game1.cameraOffset.X <= cap)
                    {
                        if(counterLeft >= 25)
                        Game1.cameraOffset.X += 10;
                        else if (Game1.cameraOffset.X <= 0) Game1.cameraOffset.X += 10;
                    }
                    if (!Left && Game1.cameraOffset.X >= -cap)
                    {
                        if (counterRight >= 25)
                            Game1.cameraOffset.X -= 10;
                        else if (Game1.cameraOffset.X >= 0) Game1.cameraOffset.X -= 10;
                    }
                    if (Game1.cameraOffset.X > 200)
                    {
                        Game1.cameraOffset.X = cap;
                    }
                    if (Game1.cameraOffset.X < -200)
                    {
                        Game1.cameraOffset.X = -cap;
                    }
                    if (isMoving && Left && Game1.cameraOffset.X > 0 && Game1.cameraOffset.X > cap)
                    {
                        Game1.cameraOffset.X -= 10;
                    }
                    if (isMoving && !Left && Game1.cameraOffset.X < 0 && Game1.cameraOffset.X < cap)
                    {
                        Game1.cameraOffset.X += 10;
                    }
                    timeUntilNextFrame += animationFrameTime;

                }
            }
        }
    }
}
