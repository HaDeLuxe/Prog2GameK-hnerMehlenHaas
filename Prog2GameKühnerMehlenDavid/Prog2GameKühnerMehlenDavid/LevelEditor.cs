using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class LevelEditor {

        Vector2 mousePosition;

        Texture2D Platform_TileSheet;


        //Doublejump is 2x175 high
        int step = 64;
        //int step = 88;

        private int previousScrollValue;

        bool alreadyDragged = false;
        bool button1Pushed = false;
        bool button2Pushed = false;
        bool button3Pushed = false;
        bool button4Pushed = false;
        bool button5Pushed = false;


     
        bool alreadyDeleted = false;
        GameObject objectToDelete = null;
        List<string> outputList;
        Dictionary<string, Rectangle> PlatformsDic;
        Enums Enums;


        public LevelEditor() {
            mousePosition = new Vector2(0, 0);
            outputList = new List<string>();
            Enums = new Enums();
            PlatformsDic = new Dictionary<string, Rectangle>();
            previousScrollValue = Mouse.GetState().ScrollWheelValue;
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
        //public void DrawLvlEditorUI(Dictionary<string, Texture2D> platformTextures, SpriteBatch spriteBatch, Matrix transformationMatrix, ref List<GameObject> gameObjectList) {
        //    MouseState mouseState = Mouse.GetState();
        //    mousePosition.X = mouseState.X;
        //    mousePosition.Y = mouseState.Y;

        //    //TODO: Look if RAM gets bullshittet
        //    Vector2 mouseWorldPosition = Vector2.Transform(mousePosition, Matrix.Invert(transformationMatrix));
        //    Vector2 positionGreenPlatform_320x64 = new Vector2(1650, 100);
        //    Vector2 transformedPosGreenPlatform_320x64 = Vector2.Transform(positionGreenPlatform_320x64, Matrix.Invert(transformationMatrix));
        //    Rectangle platformRectangleGreenPlatform_320x64 = new Rectangle((int)positionGreenPlatform_320x64.X, (int)positionGreenPlatform_320x64.Y, 320, 64);
        //    if (platformRectangleGreenPlatform_320x64.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
        //    {
        //        positionGreenPlatform_320x64 = new Vector2(1450, 100);
        //        transformedPosGreenPlatform_320x64 = Vector2.Transform(positionGreenPlatform_320x64, Matrix.Invert(transformationMatrix));
        //        if (ButtonState.Pressed == mouseState.LeftButton && !button1Pushed)
        //        {
        //            button1Pushed = true;
        //            createNewPlatform(ref gameObjectList, platformTextures["Green_320_64"], transformationMatrix, platformTextures);
        //        }
        //    }
        //    else
        //    {
        //        button1Pushed = false;
        //        positionGreenPlatform_320x64 = new Vector2(1650, 100);
        //    }

        //    Vector2 positionTransparentWall_500x50 = new Vector2(1650, 200);
        //    Vector2 transformedPosTransparentWall_500x50 = Vector2.Transform(positionTransparentWall_500x50, Matrix.Invert(transformationMatrix));
        //    Rectangle platformRectangleTransparentWall_500x50 = new Rectangle((int)positionTransparentWall_500x50.X, (int)positionTransparentWall_500x50.Y, 320, 64);
        //    if (platformRectangleTransparentWall_500x50.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
        //    {
        //        positionTransparentWall_500x50 = new Vector2(1250, 200);
        //        transformedPosTransparentWall_500x50 = Vector2.Transform(positionTransparentWall_500x50, Matrix.Invert(transformationMatrix));
        //        if (ButtonState.Pressed == mouseState.LeftButton && !button2Pushed)
        //        {
        //            button2Pushed = true;
        //            createNewPlatform(ref gameObjectList, platformTextures["Transparent_500x50"], transformationMatrix, platformTextures);
        //        }
        //    }
        //    else
        //    {
        //        button2Pushed = false;
        //        positionGreenPlatform_320x64 = new Vector2(1650, 200);
        //    }

        //    Vector2 positionTransparentWall_1000x50 = new Vector2(1650, 300);
        //    Vector2 transformedPosTransparentWall_1000x50 = Vector2.Transform(positionTransparentWall_1000x50, Matrix.Invert(transformationMatrix));
        //    Rectangle platformRectangleTransparentWall_1000x50 = new Rectangle((int)positionTransparentWall_1000x50.X, (int)positionTransparentWall_1000x50.Y, 320, 64);
        //    if (platformRectangleTransparentWall_1000x50.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
        //    {
        //        positionTransparentWall_1000x50 = new Vector2(750, 300);
        //        transformedPosTransparentWall_1000x50 = Vector2.Transform(positionTransparentWall_1000x50, Matrix.Invert(transformationMatrix));
        //        if (ButtonState.Pressed == mouseState.LeftButton && !button3Pushed)
        //        {
        //            button3Pushed = true;
        //            createNewPlatform(ref gameObjectList, platformTextures["Transparent_1000x50"], transformationMatrix, platformTextures);
        //        }
        //    }
        //    else
        //    {
        //        button3Pushed = false;
        //        positionGreenPlatform_320x64 = new Vector2(1750, 400);
        //    }

        //    Vector2 position_Climbing_Plant_38x68 = new Vector2(1750, 400);
        //    Vector2 transformedPos_Climbing_Plant_38x68 = Vector2.Transform(position_Climbing_Plant_38x68, Matrix.Invert(transformationMatrix));
        //    Rectangle platformRectangle_Climbing_Plant_38x68 = new Rectangle((int)position_Climbing_Plant_38x68.X, (int)position_Climbing_Plant_38x68.Y, 320, 64);
        //    if (platformRectangle_Climbing_Plant_38x68.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
        //    {
        //        position_Climbing_Plant_38x68 = new Vector2(1550, 400);
        //        transformedPos_Climbing_Plant_38x68 = Vector2.Transform(position_Climbing_Plant_38x68, Matrix.Invert(transformationMatrix));
        //        if (ButtonState.Pressed == mouseState.LeftButton && !button5Pushed)
        //        {
        //            button5Pushed = true;
        //            createNewPlatform(ref gameObjectList, platformTextures["Climbingplant_38x64"], transformationMatrix, platformTextures);
        //        }
        //    }
        //    else
        //    {
        //        button5Pushed = false;
        //        positionGreenPlatform_320x64 = new Vector2(1750, 400);
        //    }

        //    Color color = new Color();
        //    Vector2 positionBackButton = new Vector2(1550, 900);
        //    Vector2 transformedBackButton = Vector2.Transform(positionBackButton, Matrix.Invert(transformationMatrix));
        //    Rectangle rectangleBackButton = new Rectangle((int)positionBackButton.X, (int)positionBackButton.Y, 200, 50);
        //    if (rectangleBackButton.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
        //    {
        //        if (ButtonState.Pressed == mouseState.LeftButton && !button4Pushed)
        //        {
        //            button4Pushed = true;
        //            color = Color.LightGray;
        //            Save(gameObjectList, platformTextures);

        //        }
        //        else
        //        {
        //            color = Color.White;
        //        }
        //    }
        //    else
        //    {
        //        button4Pushed = false;
        //        color = Color.White;
        //    }



        //    spriteBatch.Draw(platformTextures["Green_320_64"], transformedPosGreenPlatform_320x64, Color.White);
        //    spriteBatch.Draw(platformTextures["Transparent_500x50"], transformedPosTransparentWall_500x50, Color.White);
        //    spriteBatch.Draw(platformTextures["Transparent_1000x50"], transformedPosTransparentWall_1000x50, Color.White);
        //    spriteBatch.Draw(platformTextures["Climbingplant_38x64"], transformedPos_Climbing_Plant_38x68, Color.White);
        //    spriteBatch.Draw(platformTextures["LevelEditorUIBackButton"], transformedBackButton, color);
        //}

        private int yoffset = 0;
        private Vector2 firstPosition = new Vector2(0, 0);

        public void DrawLvlEditorUI(Dictionary<string, Texture2D> platformTextures, SpriteBatch spriteBatch, Matrix transformationMatrix, ref List<GameObject> gameObjectList, GraphicsDevice graphics) 
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 firstPosition = new Vector2(1750, 200);
            Vector2 transformedPos_firstPosition = Vector2.Transform(firstPosition, Matrix.Invert(transformationMatrix));
            for (int i = 0; i < PlatformsDic.Count(); i++)
            {
                spriteBatch.Draw(Platform_TileSheet, transformedPos_firstPosition + i * new Vector2(0, 100) - new Vector2(0,yoffset), PlatformsDic.ElementAt(i).Value , Color.White,0,Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            }
            if(ButtonState.Pressed == mouseState.LeftButton && !button1Pushed)
            {
                button1Pushed = true;
                for (int i = 0; i < PlatformsDic.Count(); i++)
                {
                    Rectangle checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + i * 100 - yoffset, 64,64)/*+ firstPosition + i * new Vector2(0, 100)*/;
                    if(checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    {
                        Console.WriteLine("Platfrom maybe created");
                        createNewPlatform(ref gameObjectList, PlatformsDic.ElementAt(i).Key, transformationMatrix, graphics);
                    }
                }
            }
            if(ButtonState.Released == mouseState.LeftButton)
            button1Pushed = false;
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
                gameObjectList.Add(new Platform(platformTextures["Green_320_64"], new Vector2(320, 64), transformedPos, (int)Enums.ObjectsID.PLATFORM, true));
            if (platformTexture == platformTextures["Transparent_500x50"])
            {
                gameObjectList.Add(new Platform(platformTextures["Transparent_500x50"], new Vector2(500, 50), transformedPos, (int)Enums.ObjectsID.PLATFORM, true));
                gameObjectList.Last().DontDrawThisObject();
            }
            if(platformTexture == platformTextures["Transparent_1000x50"])
            {
                gameObjectList.Add(new Platform(platformTextures["Transparent_1000x50"], new Vector2(1000, 50), transformedPos, (int)Enums.ObjectsID.PLATFORM, true));
                gameObjectList.Last().DontDrawThisObject();
            }
            if(platformTexture == platformTextures["Climbingplant_38x64"])
            {
                gameObjectList.Add(new Platform(platformTextures["Climbingplant_38x64"], new Vector2(38, 88), transformedPos, (int)Enums.ObjectsID.VINE, false));
            }
        }

        private void createNewPlatform(ref List<GameObject> gameObjectList, string textureName, Matrix transformationMatrix, GraphicsDevice graphics) {
            Vector2 transformedPos = Vector2.Transform(new Vector2(1000, 200), Matrix.Invert(transformationMatrix));
        
            if (textureName == "tileBrown_27") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_27"], Platform_TileSheet, graphics), new Vector2(1000, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_27, false));
            if (textureName == "tileBrown_01") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_01"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_01, true));
            if (textureName == "tileBrown_02") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_02"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_02, true));
            if (textureName == "tileBrown_03") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_03"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_03, true));
            if (textureName == "tileBrown_04") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_04"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_04, true));
            if (textureName == "tileBrown_05") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_05"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_05, true));
            if (textureName == "tileBrown_06") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_06"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_06, true));
            if (textureName == "tileBrown_07") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_07"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_07, true));
            if (textureName == "tileBrown_08") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_08"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_08, true));
            if (textureName == "tileBrown_09") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_09"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_09, false));
            if (textureName == "tileBrown_10") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_10"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_10, false));
            if (textureName == "tileBrown_11") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_11"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_11, false));
            if (textureName == "tileBrown_12") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_12"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_12, false));
            if (textureName == "tileBrown_13") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_13"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_13, false));
            if (textureName == "tileBrown_14") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_14"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_14, false));
            if (textureName == "tileBrown_15") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_15"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_15, false));
            if (textureName == "tileBrown_16") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_16"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_16, true));
            if (textureName == "tileBrown_17") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_17"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_17, true));
            if (textureName == "tileBrown_18") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_18"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_18, false));
            if (textureName == "tileBrown_19") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_19"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_19, false));
            if (textureName == "tileBrown_20") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_20"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_20, false));
            if (textureName == "tileBrown_21") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_21"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_21, false));
            if (textureName == "tileBrown_22") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_22"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_22, false));
            if (textureName == "tileBrown_23") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_23"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_23, true));
            if (textureName == "tileBrown_24") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_24"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_24, true));
            if (textureName == "tileBrown_25") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_25"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_25, true));
            if (textureName == "tileBrown_26") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBrown_26"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBrown_26, true));
            if (textureName == "tileYellow_27") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_27"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_27, false));
            if (textureName == "tileYellow_01") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_01"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_01, true));
            if (textureName == "tileYellow_02") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_02"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_02, true));
            if (textureName == "tileYellow_03") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_03"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_03, true));
            if (textureName == "tileYellow_04") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_04"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_04, false));
            if (textureName == "tileYellow_05") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_05"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_05, true));
            if (textureName == "tileYellow_06") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_06"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_06, true));
            if (textureName == "tileYellow_07") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_07"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_07, true));
            if (textureName == "tileYellow_08") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_08"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_08, true));
            if (textureName == "tileYellow_09") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_09"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_09, false));
            if (textureName == "tileYellow_10") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_10"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_10, false));
            if (textureName == "tileYellow_11") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_11"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_11, false));
            if (textureName == "tileYellow_12") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_12"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_12, false));
            if (textureName == "tileYellow_13") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_13"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_13, false));
            if (textureName == "tileYellow_14") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_14"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_14, false));
            if (textureName == "tileYellow_15") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_15"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_15, false));
            if (textureName == "tileYellow_16") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_16"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_16, true));
            if (textureName == "tileYellow_17") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_17"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_17, true));
            if (textureName == "tileYellow_18") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_18"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_18, false));
            if (textureName == "tileYellow_19") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_19"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_19, false));
            if (textureName == "tileYellow_20") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_20"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_20, false));
            if (textureName == "tileYellow_21") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_21"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_21, false));
            if (textureName == "tileYellow_22") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_22"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_22, false));
            if (textureName == "tileYellow_23") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_23"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_23, true));
            if (textureName == "tileYellow_24") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_24"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_24, true));
            if (textureName == "tileYellow_25") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_25"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_25, true));
            if (textureName == "tileYellow_26") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileYellow_26"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileYellow_26, true));
            if (textureName == "tileBlue_27") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_27"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_27, false));
            if (textureName == "tileBlue_01") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_01"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_01, true));
            if (textureName == "tileBlue_02") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_02"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_02, true));
            if (textureName == "tileBlue_03") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_03"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_03, true));
            if (textureName == "tileBlue_04") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_04"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_04, true));
            if (textureName == "tileBlue_05") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_05"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_05, true));
            if (textureName == "tileBlue_06") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_06"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_06, true));
            if (textureName == "tileBlue_07") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_07"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_07, true));
            if (textureName == "tileBlue_08") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_08"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_08, true));
            if (textureName == "tileBlue_09") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_09"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_09, false));
            if (textureName == "tileBlue_10") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_10"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_10, false));
            if (textureName == "tileBlue_11") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_11"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_11, false));
            if (textureName == "tileBlue_12") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_12"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_12, false));
            if (textureName == "tileBlue_13") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_13"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_13, false));
            if (textureName == "tileBlue_14") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_14"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_14, false));
            if (textureName == "tileBlue_15") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_15"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_15, false));
            if (textureName == "tileBlue_16") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_16"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_16, true));
            if (textureName == "tileBlue_17") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_17"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_17, true));
            if (textureName == "tileBlue_18") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_18"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_18, false));
            if (textureName == "tileBlue_19") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_19"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_19, false));
            if (textureName == "tileBlue_20") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_20"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_20, false));
            if (textureName == "tileBlue_21") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_21"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_21, false));
            if (textureName == "tileBlue_22") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_22"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_22, false));
            if (textureName == "tileBlue_23") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_23"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_23, true));
            if (textureName == "tileBlue_24") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_24"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_24, true));
            if (textureName == "tileBlue_25") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_25"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_25, true));
            if (textureName == "tileBlue_26") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileBlue_26"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileBlue_26, true));
            if (textureName == "tileGreen_27") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_27"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_27, false));
            if (textureName == "tileGreen_01") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_01"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_01, true));
            if (textureName == "tileGreen_02") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_02"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_02, true));
            if (textureName == "tileGreen_03") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_03"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_03, true));
            if (textureName == "tileGreen_04") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_04"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_04, true));
            if (textureName == "tileGreen_05") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_05"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_05, true));
            if (textureName == "tileGreen_06") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_06"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_06, true));
            if (textureName == "tileGreen_07") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_07"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_07, true));
            if (textureName == "tileGreen_08") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_08"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_08, true));
            if (textureName == "tileGreen_09") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_09"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_09, false));
            if (textureName == "tileGreen_10") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_10"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_10, false));
            if (textureName == "tileGreen_11") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_11"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_11, false));
            if (textureName == "tileGreen_12") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_12"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_12, false));
            if (textureName == "tileGreen_13") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_13"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_13, false));
            if (textureName == "tileGreen_14") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_14"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_14, false));
            if (textureName == "tileGreen_15") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_15"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_15, false));
            if (textureName == "tileGreen_16") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_16"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_16, true));
            if (textureName == "tileGreen_17") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_17"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_17, true));
            if (textureName == "tileGreen_18") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_18"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_18, false));
            if (textureName == "tileGreen_19") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_19"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_19, false));
            if (textureName == "tileGreen_20") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_20"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_20, false));
            if (textureName == "tileGreen_21") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_21"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_21, false));
            if (textureName == "tileGreen_22") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_22"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_22, false));
            if (textureName == "tileGreen_23") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_23"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_23, true));
            if (textureName == "tileGreen_24") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_24"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_24, true));
            if (textureName == "tileGreen_25") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_25"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_25, true));
            if (textureName == "tileGreen_26") gameObjectList.Add(new Platform(CreatePartImage(PlatformsDic["tileGreen_26"], Platform_TileSheet, graphics), new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.tileGreen_26, true));



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
                int currentMouseScrollValue = Mouse.GetState().ScrollWheelValue;
                if (currentMouseScrollValue < previousScrollValue)
                {
                    yoffset += 100;
                    previousScrollValue = currentMouseScrollValue;
                }
                if(currentMouseScrollValue > previousScrollValue)
                {
                    yoffset -= 100;
                    previousScrollValue = currentMouseScrollValue;
                }

            }
        }

        public void loadTextures(ContentManager ContentManager)
        {
            Platform_TileSheet = ContentManager.Load<Texture2D>("Images\\WorldObjects\\tileSheet_complete_2X");
            List<string> NamesList = new List<string>();
            NamesList = new List<string>(System.IO.File.ReadAllLines(@"PlatformTileSheetNames.txt"));
            NamesList = NamesList.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            int i = 0;
            for (int m = 0; m < 12; m++)
            {
                  for(int n = 0; n < 9; n++)
                  {
                        PlatformsDic.Add(NamesList[i].ToString(), new Rectangle(n * 64, m * 64, 64, 64));
                        //Console.WriteLine(PlatformsDic.ElementAt(i));
                        i++;
                  }
            }


        }


         /// <summary>
        /// Creates a new image from an existing image.
        /// Found on: https://www.dreamincode.net/forums/topic/260833-solved-how-do-i-split-a-texture2d-into-multiple-texture2ds/
        /// </summary>
        /// <param name="bounds">Area to use as the new image.</param>
        /// <param name="source">Source image used for getting a part image.</param>
        /// <returns>Texture2D.</returns>
        public static Texture2D CreatePartImage(Rectangle bounds, Texture2D source, GraphicsDevice graphicsDevice)
        {
            //Declare variables
            Texture2D result;
            Color[]
                sourceColors,
                resultColors;
            //Setup the result texture
            result = new Texture2D(graphicsDevice, bounds.Width, bounds.Height);
            //Setup the color arrays
            sourceColors = new Color[source.Height * source.Width];
            resultColors = new Color[bounds.Height * bounds.Width];
            //Get the source colors
            source.GetData<Color>(sourceColors);
            //Loop through colors on the y axis
            for (int y = bounds.Y; y<bounds.Height + bounds.Y; y++)
            {
                //Loop through colors on the x axis
                for (int x = bounds.X; x<bounds.Width + bounds.X; x++)
                {
                    //Get the current color
                    resultColors[x - bounds.X + (y - bounds.Y) * bounds.Width] =
                        sourceColors[x + y * source.Width];
                }
            }
            //Set the color data of the result image
            result.SetData<Color>(resultColors);
            //return the result
            return result;
        }
    }
}
