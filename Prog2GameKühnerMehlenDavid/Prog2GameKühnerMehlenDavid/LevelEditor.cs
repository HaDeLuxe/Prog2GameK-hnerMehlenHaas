using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class LevelEditor {

        Vector2 mousePosition;

        //Doublejump is 2x175 high
        int step = 64;
        //int step = 88;
        bool alreadyDragged = false;
        bool button1Pushed = false;
        bool button2Pushed = false;
        bool button3Pushed = false;
        bool button4Pushed = false;
        bool button5Pushed = false;
     
        bool alreadyDeleted = false;
        GameObject objectToDelete = null;
        List<string> outputList;
        Enums Enums;


        public LevelEditor() {
            mousePosition = new Vector2(0, 0);
            outputList = new List<string>();
            Enums = new Enums();
        }
        
        /// <summary>
        /// Moving GameObjects with the mouse while holding left mouse button and deleting GameObjects with right mouse button click
        /// </summary>
        /// <param name="gameObjects"></param>
        /// <param name="transformatinMatrix"></param>
        public void moveOrDeletePlatforms(ref List<GameObject> gameObjects, Matrix transformatinMatrix) 
        {
            MouseState mouseState = Mouse.GetState();
            mousePosition.X = mouseState.X;
            mousePosition.Y = mouseState.Y;
            
            if (ButtonState.Pressed == mouseState.LeftButton)
            {
                foreach(GameObject gameObject in gameObjects)
                {
                    Vector2 mouseWorldPosition = Vector2.Transform(mousePosition, Matrix.Invert(transformatinMatrix));
                    if (gameObject.gameObjectRectangle.Intersects(new Rectangle((int)mouseWorldPosition.X, (int)mouseWorldPosition.Y, 0, 0)))
                    {
                        if (!alreadyDragged)
                        {
                            gameObject.isDragged = true;
                            alreadyDragged = true;
                        }
                    }
                    if (gameObject.isDragged)
                    {
                        
                        if(mouseWorldPosition.X % step < step/2 && mouseWorldPosition.Y % step < step/2)
                        {
                            gameObject.gameObjectPosition = new Vector2((int)mouseWorldPosition.X - gameObject.gameObjectRectangle.Width / 2 - (mouseWorldPosition.X % step),
                                                             (int)mouseWorldPosition.Y - gameObject.gameObjectRectangle.Height / 2 - (mouseWorldPosition.Y % step));
                        }
                        else if(mouseWorldPosition.X % step < step/2 && mouseWorldPosition.Y % step > step/2)
                        {
                            gameObject.gameObjectPosition = new Vector2((int)mouseWorldPosition.X - gameObject.gameObjectRectangle.Width / 2 - (mouseWorldPosition.X % step),
                                                             (int)mouseWorldPosition.Y - gameObject.gameObjectRectangle.Height / 2 + (step - (mouseWorldPosition.Y % step)));
                        }
                        else if (mouseWorldPosition.X % step > step/2 && mouseWorldPosition.Y % step < step/2)
                        {
                            gameObject.gameObjectPosition = new Vector2((int)mouseWorldPosition.X - gameObject.gameObjectRectangle.Width / 2 + (step - (mouseWorldPosition.X % step)),
                                                             (int)mouseWorldPosition.Y - gameObject.gameObjectRectangle.Height / 2 - (mouseWorldPosition.Y % step));
                        }
                        else if (mouseWorldPosition.X % step > step/2 && mouseWorldPosition.Y % step > step/2)
                        {
                            gameObject.gameObjectPosition = new Vector2((int)mouseWorldPosition.X - gameObject.gameObjectRectangle.Width / 2 + (step - (mouseWorldPosition.X % step)),
                                                             (int)mouseWorldPosition.Y - gameObject.gameObjectRectangle.Height / 2 + (step - (mouseWorldPosition.Y % step)));
                        }
                    }
                }
            }
            else
            {
                alreadyDragged = false;
                foreach(GameObject gameObject in gameObjects)
                {
                    gameObject.isDragged = false;
                }
            }

            //Delete Objects by pressing right mouse button
            if (ButtonState.Pressed == mouseState.RightButton)
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    Vector2 mouseWorldPosition = Vector2.Transform(mousePosition, Matrix.Invert(transformatinMatrix));
                    if (gameObject.gameObjectRectangle.Intersects(new Rectangle((int)mouseWorldPosition.X, (int)mouseWorldPosition.Y, 0, 0)))
                    {
                        if(!alreadyDeleted)
                        {
                            objectToDelete = gameObject;
                            //gameObjects.Remove(gameObject);
                            alreadyDeleted = true;
                        }
                    }
                }
            }
            else
            {
                alreadyDeleted = false;
            }
            if(objectToDelete != null)
            {
                gameObjects.Remove(objectToDelete);
                objectToDelete = null;
            }
        }

        /// <summary>
        /// Moving Camera while in Level Editor Mode
        /// </summary>
        /// <param name="cameraOffset"></param>
        public void moveCamera(ref Vector2 cameraOffset) {
            if (Keyboard.GetState().IsKeyDown(Keys.U)) cameraOffset.Y += 10;
            if (Keyboard.GetState().IsKeyDown(Keys.J)) cameraOffset.Y -= 10;
            if (Keyboard.GetState().IsKeyDown(Keys.K)) cameraOffset.X -= 10;
            if (Keyboard.GetState().IsKeyDown(Keys.H)) cameraOffset.X += 10 ;
        }
        
        /// <summary>
        /// Draws the Level Editor User Interface
        /// </summary>
        /// <param name="platformTextures"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="transformationMatrix"></param>
        /// <param name="gameObjectList"></param>
        public void DrawLvlEditorUI(Dictionary<string, Texture2D> platformTextures, SpriteBatch spriteBatch, Matrix transformationMatrix, ref List<GameObject> gameObjectList) {
            MouseState mouseState = Mouse.GetState();
            mousePosition.X = mouseState.X;
            mousePosition.Y = mouseState.Y;

            //TODO: Look if RAM gets bullshittet
            Vector2 mouseWorldPosition = Vector2.Transform(mousePosition, Matrix.Invert(transformationMatrix));
            Vector2 positionGreenPlatform_320x64 = new Vector2(1650, 100);
            Vector2 transformedPosGreenPlatform_320x64 = Vector2.Transform(positionGreenPlatform_320x64, Matrix.Invert(transformationMatrix));
            Rectangle platformRectangleGreenPlatform_320x64 = new Rectangle((int)positionGreenPlatform_320x64.X, (int)positionGreenPlatform_320x64.Y, 320, 64);
            if (platformRectangleGreenPlatform_320x64.Contains(new Point((int)mousePosition.X,(int)mousePosition.Y)))
            {
                positionGreenPlatform_320x64 = new Vector2(1450, 100);
                transformedPosGreenPlatform_320x64 = Vector2.Transform(positionGreenPlatform_320x64, Matrix.Invert(transformationMatrix));
                if(ButtonState.Pressed == mouseState.LeftButton && !button1Pushed)
                {
                    button1Pushed = true;
                    createNewPlatform(ref gameObjectList, platformTextures["Green_320_64"], transformationMatrix, platformTextures);
                }
            }
            else{
                button1Pushed = false;
                positionGreenPlatform_320x64 = new Vector2(1650, 100);
            }

            Vector2 positionTransparentWall_500x50 = new Vector2(1650, 200);
            Vector2 transformedPosTransparentWall_500x50 = Vector2.Transform(positionTransparentWall_500x50, Matrix.Invert(transformationMatrix));
            Rectangle platformRectangleTransparentWall_500x50 = new Rectangle((int)positionTransparentWall_500x50.X, (int)positionTransparentWall_500x50.Y, 320, 64);
            if (platformRectangleTransparentWall_500x50.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
            {
                positionTransparentWall_500x50 = new Vector2(1250, 200);
                transformedPosTransparentWall_500x50 = Vector2.Transform(positionTransparentWall_500x50, Matrix.Invert(transformationMatrix));
                if (ButtonState.Pressed == mouseState.LeftButton && !button2Pushed)
                {
                    button2Pushed = true;
                    createNewPlatform(ref gameObjectList, platformTextures["Transparent_500x50"], transformationMatrix, platformTextures);
                }
            }
            else
            {
                button2Pushed = false;
                positionGreenPlatform_320x64 = new Vector2(1650, 200);
            }

            Vector2 positionTransparentWall_1000x50 = new Vector2(1650, 300);
            Vector2 transformedPosTransparentWall_1000x50 = Vector2.Transform(positionTransparentWall_1000x50, Matrix.Invert(transformationMatrix));
            Rectangle platformRectangleTransparentWall_1000x50 = new Rectangle((int)positionTransparentWall_1000x50.X, (int)positionTransparentWall_1000x50.Y, 320, 64);
            if (platformRectangleTransparentWall_1000x50.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
            {
                positionTransparentWall_1000x50 = new Vector2(750, 300);
                transformedPosTransparentWall_1000x50 = Vector2.Transform(positionTransparentWall_1000x50, Matrix.Invert(transformationMatrix));
                if (ButtonState.Pressed == mouseState.LeftButton && !button3Pushed)
                {
                    button3Pushed = true;
                    createNewPlatform(ref gameObjectList, platformTextures["Transparent_1000x50"], transformationMatrix, platformTextures);
                }
            }
            else
            {
                button3Pushed = false;
                positionGreenPlatform_320x64 = new Vector2(1750, 400);
            }

            Vector2 position_Climbing_Plant_38x68 = new Vector2(1750, 400);
            Vector2 transformedPos_Climbing_Plant_38x68 = Vector2.Transform(position_Climbing_Plant_38x68, Matrix.Invert(transformationMatrix));
            Rectangle platformRectangle_Climbing_Plant_38x68 = new Rectangle((int)position_Climbing_Plant_38x68.X, (int)position_Climbing_Plant_38x68.Y, 320, 64);
            if (platformRectangle_Climbing_Plant_38x68.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
            {
                position_Climbing_Plant_38x68 = new Vector2(1550, 400);
                transformedPos_Climbing_Plant_38x68 = Vector2.Transform(position_Climbing_Plant_38x68, Matrix.Invert(transformationMatrix));
                if (ButtonState.Pressed == mouseState.LeftButton && !button5Pushed)
                {
                    button5Pushed = true;
                    createNewPlatform(ref gameObjectList, platformTextures["Climbingplant_38x64"], transformationMatrix, platformTextures);
                }
            }
            else
            {
                button5Pushed = false;
                positionGreenPlatform_320x64 = new Vector2(1750, 400);
            }

            Color color = new Color();
            Vector2 positionBackButton = new Vector2(1550, 900);
            Vector2 transformedBackButton = Vector2.Transform(positionBackButton, Matrix.Invert(transformationMatrix));
            Rectangle rectangleBackButton = new Rectangle((int)positionBackButton.X, (int)positionBackButton.Y, 200, 50);
            if (rectangleBackButton.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
            {
                if (ButtonState.Pressed == mouseState.LeftButton && !button4Pushed)
                {
                    button4Pushed = true;
                    color = Color.LightGray;
                    Save(gameObjectList, platformTextures);

                }
                else
                {
                    color = Color.White;
                }
            }
            else
            {
                button4Pushed = false;
                color = Color.White;
            }



            spriteBatch.Draw(platformTextures["Green_320_64"], transformedPosGreenPlatform_320x64, Color.White);
            spriteBatch.Draw(platformTextures["Transparent_500x50"], transformedPosTransparentWall_500x50, Color.White);
            spriteBatch.Draw(platformTextures["Transparent_1000x50"], transformedPosTransparentWall_1000x50, Color.White);
            spriteBatch.Draw(platformTextures["Climbingplant_38x64"], transformedPos_Climbing_Plant_38x68, Color.White);
            spriteBatch.Draw(platformTextures["LevelEditorUIBackButton"], transformedBackButton, color);
        }

        /// <summary>
        /// Add a new Platform to the GameObjectList and gives it the standard viewport coordinates (1000,200)
        /// </summary>
        /// <param name="gameObjectList"></param>
        /// <param name="platformTextures"></param>
        /// <param name="transformationMatrix"></param>
        private void createNewPlatform(ref List<GameObject> gameObjectList, Texture2D platformTexture, Matrix transformationMatrix, Dictionary<string, Texture2D> platformTextures)
        {
            Vector2 transformedPos = Vector2.Transform(new Vector2(1000,200), Matrix.Invert(transformationMatrix));
            if(platformTexture == platformTextures["Green_320_64"])
                gameObjectList.Add(new Platform(platformTextures["Green_320_64"], new Vector2(320, 64), transformedPos, (int)Enums.ObjectsID.PLATFORM));
            if (platformTexture == platformTextures["Transparent_500x50"])
            {
                gameObjectList.Add(new Platform(platformTextures["Transparent_500x50"], new Vector2(500, 50), transformedPos, (int)Enums.ObjectsID.PLATFORM));
                gameObjectList.Last().DontDrawThisObject();
            }
            if(platformTexture == platformTextures["Transparent_1000x50"])
            {
                gameObjectList.Add(new Platform(platformTextures["Transparent_1000x50"], new Vector2(1000, 50), transformedPos, (int)Enums.ObjectsID.PLATFORM));
                gameObjectList.Last().DontDrawThisObject();
            }
            if(platformTexture == platformTextures["Climbingplant_38x64"])
            {
                gameObjectList.Add(new Platform(platformTextures["Climbingplant_38x64"], new Vector2(38, 88), transformedPos, (int)Enums.ObjectsID.VINE));
            }
        }


        private void Save(List<GameObject> GameObjectList, Dictionary<string, Texture2D> platformTextures) {

            outputList.RemoveRange(0, outputList.Count());

            foreach (GameObject gameObject in GameObjectList)
            {
                string Output = "";
                if (gameObject.getTexture() == platformTextures["Green_320_64"]) Output = Enums.ObjectsID.GREEN_PLATFORM_320_64.ToString();
                if (gameObject.getTexture() == platformTextures["Transparent_500x50"]) Output =  Enums.ObjectsID.INVISIBLE_WALL_500x50.ToString();
                if (gameObject.getTexture() == platformTextures["Transparent_1000x50"]) Output = Enums.ObjectsID.INVSIBLE_WALL_1000x50.ToString();
                if (gameObject.getTexture() == platformTextures["Climbingplant_38x64"]) Output = Enums.ObjectsID.VINE.ToString();

                Output += ","+ gameObject.gameObjectPosition.X + "," + gameObject.gameObjectPosition.Y;

                outputList.Add(Output);
            }


            using (var stream = new FileStream(@"SaveFile.txt", FileMode.Truncate))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("");
                    foreach (string line in outputList)
                    {
                        
                        writer.WriteLine(line);
                    }
                }
            }

        }

        public void HandleLevelEditorEvents()
        {
            if (Game1.currentGameState == Game1.GameState.LEVELEDITOR)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.L) && !Game1.previousState.IsKeyDown(Keys.L))
                    Game1.currentGameState = Game1.GameState.GAMELOOP;
                Game1.previousState = Keyboard.GetState();
            }
        }

    }
}
