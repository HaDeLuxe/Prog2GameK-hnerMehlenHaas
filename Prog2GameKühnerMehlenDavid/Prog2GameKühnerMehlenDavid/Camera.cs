﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Reggie.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Threading.Tasks;

namespace Reggie
{
    /// <summary>
    /// Contains the logic to calculate the transformation matrix to manage the viewport and the gameworld positions and to change between them.
    /// Contains the logic to move the camera upon movement. Allows the player see more where the player move towards.
    /// </summary>
    class Camera
    {
        
        Vector2 cameraWorldPosition = new Vector2(0, 0);
        public static float zoom = 1f;
        public static bool enableCameraMovement = true;
        public bool spawnBoss = false;
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

        public List<GameObject> GameObjectsToRender(Vector2 playerPosition, List<GameObject> gameObjectsToRender, ref List<GameObject> interactiveObject)
        {
            List<GameObject> objectsToRender = new List<GameObject>();
            List<GameObject> interactiveObjectsList = new List<GameObject>();
            interactiveObject.Clear();
            for(int i = 0; i < gameObjectsToRender.Count; i++)
            {
                if (gameObjectsToRender[i].gameObjectPosition.X < playerPosition.X + 1350 && gameObjectsToRender[i].gameObjectRectangle.Right > playerPosition.X - 1350
                    && gameObjectsToRender[i].gameObjectPosition.Y < playerPosition.Y + 750 && gameObjectsToRender[i].gameObjectRectangle.Bottom > playerPosition.Y - 750)
                {
                        objectsToRender.Add(gameObjectsToRender[i]);
                    if (gameObjectsToRender[i].objectID == (int)Enums.ObjectsID.VINE
                        || gameObjectsToRender[i].objectID == (int)Enums.ObjectsID.SNAILSHELL
                        || gameObjectsToRender[i].objectID == (int)Enums.ObjectsID.SCISSORS
                        || gameObjectsToRender[i].objectID == (int)Enums.ObjectsID.ARMOR
                        || gameObjectsToRender[i].objectID == (int)Enums.ObjectsID.SHOVEL
                        || gameObjectsToRender[i].objectID == (int)Enums.ObjectsID.HEALTHPOTION
                        || gameObjectsToRender[i].objectID == (int)Enums.ObjectsID.JUMPPOTION
                        || gameObjectsToRender[i].objectID == (int)Enums.ObjectsID.POWERPOTION
                        || gameObjectsToRender[i].objectID == (int)Enums.ObjectsID.GOLDENUMBRELLA
                        || gameObjectsToRender[i].objectID == (int)Enums.ObjectsID.CORNNENCY)
                        interactiveObjectsList.Add(gameObjectsToRender[i]);
                }
            }
            foreach (GameObject gameObject in interactiveObjectsList) interactiveObject.Add(gameObject);
            
            return objectsToRender;
        }

        public void SpawnEnemyOffScreen(Player wormPlayer, List<Platform> platformList, ref List<Enemy> enemyList, Dictionary<string, Texture2D> enemySpriteSheets, Enums.Level currentLevel)
        {
           
            for (int i = 0; i < platformList.Count; i++)
            {
                if (!platformList[i].enemySpawnCheck)
                {
                    if (platformList[i].gameObjectPosition.X < wormPlayer.gameObjectPosition.X + 1250 && platformList[i].gameObjectRectangle.Right > wormPlayer.gameObjectPosition.X - 1250 && platformList[i].gameObjectPosition.Y < wormPlayer.gameObjectPosition.Y + 750 && platformList[i].gameObjectRectangle.Bottom > wormPlayer.gameObjectPosition.Y - 750)
                        platformList[i].enemySpawnCheck = true;
                    if ((platformList[i].gameObjectPosition.X < wormPlayer.gameObjectPosition.X + 1250 && platformList[i].gameObjectPosition.X > wormPlayer.gameObjectPosition.X + 900) || (platformList[i].gameObjectRectangle.Right > wormPlayer.gameObjectPosition.X - 1250 && platformList[i].gameObjectRectangle.Right < wormPlayer.gameObjectPosition.X - 900)
                        && (platformList[i].gameObjectPosition.Y < wormPlayer.gameObjectPosition.Y +800 && platformList[i].gameObjectPosition.Y > wormPlayer.gameObjectPosition.Y + 550) || (platformList[i].gameObjectRectangle.Bottom > wormPlayer.gameObjectPosition.Y - 1000 && platformList[i].gameObjectRectangle.Bottom < wormPlayer.gameObjectPosition.Y - 550))
                    {
                       // if(enemyList.Count() < 10)
                        {
                            platformList[i].enemySpawnCheck = true;
                            Random rand = new Random();

                            int randomizedNumber = rand.Next(0, 100);
                            if (randomizedNumber % 4 == 0 && platformList[i].canSpawnEnemy)
                            {
                               // if (currentLevel == Enums.Level.ANTCAVE)
                                enemyList.Add(new Snail(null, new Vector2(100, 50), new Vector2(platformList[i].gameObjectPosition.X + (platformList[i].gameObjectSize.X / 2), platformList[i].gameObjectPosition.Y - 50), (int)Enums.ObjectsID.SNAIL, enemySpriteSheets));

                                if (enemyList.Count() != 0)
                                    enemyList.Last().SetPlayer(wormPlayer);

                            }
                            if (randomizedNumber % 4 == 1 && platformList[i].canSpawnEnemy)
                            {
                                // if (currentLevel == Enums.Level.ANTCAVE)
                                enemyList.Add(new Spider(null, new Vector2(100, 50), new Vector2(platformList[i].gameObjectPosition.X + (platformList[i].gameObjectSize.X / 2), platformList[i].gameObjectPosition.Y - 50), (int)Enums.ObjectsID.SPIDER, enemySpriteSheets));

                                if (enemyList.Count() != 0)
                                    enemyList.Last().SetPlayer(wormPlayer);

                            }
                            if (randomizedNumber % 4 == 2 && platformList[i].canSpawnEnemy)
                            {
                                // if (currentLevel == Enums.Level.ANTCAVE)
                                enemyList.Add(new Ladybug(null, new Vector2(100, 50), new Vector2(platformList[i].gameObjectPosition.X + (platformList[i].gameObjectSize.X / 2), platformList[i].gameObjectPosition.Y - 50), (int)Enums.ObjectsID.LADYBUG, enemySpriteSheets));

                                if (enemyList.Count() != 0)
                                    enemyList.Last().SetPlayer(wormPlayer);

                            }
                           
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
                if (enemyList[i].enemyHP == 0)
                    enemyList.RemoveAt(i);
                 else if (enemyList[i].gameObjectPosition.X < playerPosition.X + 1100 && enemyList[i].gameObjectRectangle.Right > playerPosition.X - 1100)
                {
                    if (enemyList[i].gameObjectPosition.Y < playerPosition.Y + 650 && enemyList[i].gameObjectRectangle.Bottom > playerPosition.Y - 650)
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

                if (timeUntilNextFrame <= 0 && enableCameraMovement)
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
