using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Threading.Tasks;

namespace Reggie {
    class Camera {
        
        Vector2 cameraWorldPosition = new Vector2(0, 0);
        float Zoom = .2f;

        public void setCameraWorldPosition(Vector2 cameraWorldPosition) {
            this.cameraWorldPosition = cameraWorldPosition;
        }

        public Matrix cameraTransformationMatrix(Viewport viewport, Vector2 screenCentre) {
             Vector2 translation = -cameraWorldPosition + screenCentre;
             Matrix cameraMatrix = Matrix.CreateTranslation(translation.X, translation.Y, 0) * Matrix.CreateScale(Zoom,Zoom, 1);
             
             return cameraMatrix;
        }

        public List<GameObject> objectsToRender(Vector2 playerPosition, List<GameObject> gameObjectsList) {
            List<GameObject> objectsToRender = new List<GameObject>();
            for(int i = 0; i < gameObjectsList.Count; i++)
            {
                if (gameObjectsList[i].Position.X < playerPosition.X + 1250 && gameObjectsList[i].SpriteRectangle.Right > playerPosition.X - 950)
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
            if(Game1.CurrentGameState == Game1.GameState.GAMELOOP)
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
