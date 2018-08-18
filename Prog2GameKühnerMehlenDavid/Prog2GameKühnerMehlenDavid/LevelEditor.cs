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

        

        private int yoffset = 0;
        private Vector2 firstPosition = new Vector2(0, 0);

        /// <summary>
        /// Draws the Level Editor UI and finds out if element was clicked
        /// </summary>
        /// <param name="platformTextures"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="transformationMatrix"></param>
        /// <param name="gameObjectList"></param>
        /// <param name="graphics"></param>
        public void DrawLvlEditorUI(Dictionary<string, Texture2D> platformTextures, SpriteBatch spriteBatch, Matrix transformationMatrix, ref List<GameObject> gameObjectList, GraphicsDevice graphics) 
        {
            int m = 0;
            MouseState mouseState = Mouse.GetState();
            Vector2 firstPosition = new Vector2(1750, 200);
            Vector2 transformedPos_firstPosition = Vector2.Transform(firstPosition, Matrix.Invert(transformationMatrix));
            
            spriteBatch.Draw(platformTextures["Transparent_500x50"], transformedPos_firstPosition + m * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            m++;
            spriteBatch.Draw(platformTextures["Transparent_1000x50"], transformedPos_firstPosition + m * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            m++;
            spriteBatch.Draw(platformTextures["Climbingplant_38x64"], transformedPos_firstPosition + m * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            m++;

            for (int i = 0; i < PlatformsDic.Count(); i++)
            {
                spriteBatch.Draw(Platform_TileSheet, transformedPos_firstPosition + m * new Vector2(0, 100) - new Vector2(0, yoffset), PlatformsDic.ElementAt(i).Value, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                m++;
            }
           


            if(ButtonState.Pressed == mouseState.LeftButton && !button1Pushed)
            {
                Rectangle checkRectangle;
                button1Pushed = true;
                for (int i = 3; i < PlatformsDic.Count()+3; i++)
                {
                    checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + i * 100 - yoffset, 64,64);
                    if(checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    {
                        createNewPlatform(ref gameObjectList, PlatformsDic.ElementAt(i-3).Key, transformationMatrix, graphics, platformTextures);
                    }
                }
               
                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + 0 * 100 - yoffset, 500, 50);
                if(checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    createNewPlatform(ref gameObjectList, platformTextures["Transparent_500x50"], transformationMatrix, platformTextures);
                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + 1 * 100 - yoffset, 1000, 50);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    createNewPlatform(ref gameObjectList, platformTextures["Transparent_1000x50"], transformationMatrix, platformTextures);
                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + 2 * 100 - yoffset, 38, 64);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    createNewPlatform(ref gameObjectList, platformTextures["Climbingplant_38x64"], transformationMatrix, platformTextures);
            }
            if (ButtonState.Released == mouseState.LeftButton)
            button1Pushed = false;


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
                    SavePlatforms(gameObjectList, platformTextures);

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
                gameObjectList.Add(new Platform(platformTextures["Green_320_64"], new Vector2(320, 64), transformedPos, (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.GREEN_PLATFORM_320_64,true));
            if (platformTexture == platformTextures["Transparent_500x50"])
            {
                gameObjectList.Add(new Platform(platformTextures["Transparent_500x50"], new Vector2(500, 50), transformedPos, (int)Enums.ObjectsID.PLATFORM,(int)Enums.ObjectsID.INVISIBLE_WALL_500x50, true));
                gameObjectList.Last().DontDrawThisObject();
            }
            if(platformTexture == platformTextures["Transparent_1000x50"])
            {
                gameObjectList.Add(new Platform(platformTextures["Transparent_1000x50"], new Vector2(1000, 50), transformedPos, (int)Enums.ObjectsID.PLATFORM,(int)Enums.ObjectsID.INVSIBLE_WALL_1000x50, true));
                gameObjectList.Last().DontDrawThisObject();
            }
            if(platformTexture == platformTextures["Climbingplant_38x64"])
            {
                gameObjectList.Add(new Platform(platformTextures["Climbingplant_38x64"], new Vector2(38, 88), transformedPos, (int)Enums.ObjectsID.VINE,(int)Enums.ObjectsID.VINE, false));
            }
        }

        private void createNewPlatform(ref List<GameObject> gameObjectList, string textureName, Matrix transformationMatrix, GraphicsDevice graphics, Dictionary<string, Texture2D> platformTextures) {
            Vector2 transformedPos = Vector2.Transform(new Vector2(1000, 200), Matrix.Invert(transformationMatrix));

            string temp;

            for (int i = 1; i <= 108; i++)
            {
                temp = "tileBrown_01";
                if(i >= 1 && i <= 27)
                {
                    if (i < 10) temp = "tileBrown_0" + i;
                    else temp = "tileBrown_" + i;
                }
                if (i > 27 && i <= 54)
                {
                    if (i < 37)
                        temp = "tileYellow_0" + (i-27);
                    else temp = "tileYellow_" + (i-27);
                }
                if (i > 54 && i <= 81)
                {
                    if (i < 65) temp = "tileBlue_0" + (i-54);
                    else temp = "tileBlue_" + (i-54);
                }
                if (i > 81 && i <= 108)
                {
                    if (i < 91)
                        temp = "tileGreen_0" + (i - 81);
                    else temp = "tileGreen_" + (i - 81);
                }
                Enums.ObjectsID tempObjectID = Enums.ObjectsID.tileBrown_01;
                //switch (temp)
                //{
                //    case "tileBrown_01":
                //        tempObjectID = Enums.ObjectsID.tileBrown_01;
                //        break;
                //}
                if(textureName == temp)
                {
                    gameObjectList.Add(new Platform(platformTextures[textureName], new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.PLATFORM, (int)tempObjectID, false));
                }
            }

            

           
        }


        private void SavePlatforms(List<GameObject> GameObjectList, Dictionary<string, Texture2D> platformTextures) {

            outputList.RemoveRange(0, outputList.Count());

           
            foreach (Platform platform in GameObjectList)
            {
                string Output = "";

                //if (platform.PlatformType >= 8 && platform.PlatformType <= 115)
                //{
                //    Output = "" + platform.PlatformType;
                //}
                //if (platform.PlatformType >= (int)Enums.ObjectsID.tileBrown_01 && platform.PlatformType <= (int)Enums.ObjectsID.tileGreen_27) Output = "" + platform.PlatformType;

                if (platform.getTexture() == platformTextures["Green_320_64"]) Output = Enums.ObjectsID.GREEN_PLATFORM_320_64.ToString();
                if (platform.getTexture() == platformTextures["Transparent_500x50"]) Output = Enums.ObjectsID.INVISIBLE_WALL_500x50.ToString();
                if (platform.getTexture() == platformTextures["Transparent_1000x50"]) Output = Enums.ObjectsID.INVSIBLE_WALL_1000x50.ToString();
                if (platform.getTexture() == platformTextures["Climbingplant_38x64"]) Output = Enums.ObjectsID.VINE.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_01"]) Output = Enums.ObjectsID.tileBrown_01.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_02"]) Output = Enums.ObjectsID.tileBrown_02.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_03"]) Output = Enums.ObjectsID.tileBrown_03.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_04"]) Output = Enums.ObjectsID.tileBrown_04.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_05"]) Output = Enums.ObjectsID.tileBrown_05.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_06"]) Output = Enums.ObjectsID.tileBrown_06.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_07"]) Output = Enums.ObjectsID.tileBrown_07.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_08"]) Output = Enums.ObjectsID.tileBrown_08.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_09"]) Output = Enums.ObjectsID.tileBrown_09.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_10"]) Output = Enums.ObjectsID.tileBrown_10.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_11"]) Output = Enums.ObjectsID.tileBrown_11.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_12"]) Output = Enums.ObjectsID.tileBrown_12.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_13"]) Output = Enums.ObjectsID.tileBrown_13.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_14"]) Output = Enums.ObjectsID.tileBrown_14.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_15"]) Output = Enums.ObjectsID.tileBrown_15.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_16"]) Output = Enums.ObjectsID.tileBrown_16.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_17"]) Output = Enums.ObjectsID.tileBrown_17.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_18"]) Output = Enums.ObjectsID.tileBrown_18.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_19"]) Output = Enums.ObjectsID.tileBrown_19.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_20"]) Output = Enums.ObjectsID.tileBrown_20.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_21"]) Output = Enums.ObjectsID.tileBrown_21.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_22"]) Output = Enums.ObjectsID.tileBrown_22.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_23"]) Output = Enums.ObjectsID.tileBrown_23.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_24"]) Output = Enums.ObjectsID.tileBrown_24.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_25"]) Output = Enums.ObjectsID.tileBrown_25.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_26"]) Output = Enums.ObjectsID.tileBrown_26.ToString();
                if (platform.getTexture() == platformTextures["tileBrown_27"]) Output = Enums.ObjectsID.tileBrown_27.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_01"]) Output = Enums.ObjectsID.tileYellow_01.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_02"]) Output = Enums.ObjectsID.tileYellow_02.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_03"]) Output = Enums.ObjectsID.tileYellow_03.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_04"]) Output = Enums.ObjectsID.tileYellow_04.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_05"]) Output = Enums.ObjectsID.tileYellow_05.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_06"]) Output = Enums.ObjectsID.tileYellow_06.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_07"]) Output = Enums.ObjectsID.tileYellow_07.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_08"]) Output = Enums.ObjectsID.tileYellow_08.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_09"]) Output = Enums.ObjectsID.tileYellow_09.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_10"]) Output = Enums.ObjectsID.tileYellow_10.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_11"]) Output = Enums.ObjectsID.tileYellow_11.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_12"]) Output = Enums.ObjectsID.tileYellow_12.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_13"]) Output = Enums.ObjectsID.tileYellow_13.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_14"]) Output = Enums.ObjectsID.tileYellow_14.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_15"]) Output = Enums.ObjectsID.tileYellow_15.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_16"]) Output = Enums.ObjectsID.tileYellow_16.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_17"]) Output = Enums.ObjectsID.tileYellow_17.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_18"]) Output = Enums.ObjectsID.tileYellow_18.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_19"]) Output = Enums.ObjectsID.tileYellow_19.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_20"]) Output = Enums.ObjectsID.tileYellow_20.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_21"]) Output = Enums.ObjectsID.tileYellow_21.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_22"]) Output = Enums.ObjectsID.tileYellow_22.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_23"]) Output = Enums.ObjectsID.tileYellow_23.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_24"]) Output = Enums.ObjectsID.tileYellow_24.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_25"]) Output = Enums.ObjectsID.tileYellow_25.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_26"]) Output = Enums.ObjectsID.tileYellow_26.ToString();
                if (platform.getTexture() == platformTextures["tileYellow_27"]) Output = Enums.ObjectsID.tileYellow_27.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_01"]) Output = Enums.ObjectsID.tileBlue_01.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_02"]) Output = Enums.ObjectsID.tileBlue_02.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_03"]) Output = Enums.ObjectsID.tileBlue_03.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_04"]) Output = Enums.ObjectsID.tileBlue_04.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_05"]) Output = Enums.ObjectsID.tileBlue_05.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_06"]) Output = Enums.ObjectsID.tileBlue_06.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_07"]) Output = Enums.ObjectsID.tileBlue_07.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_08"]) Output = Enums.ObjectsID.tileBlue_08.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_09"]) Output = Enums.ObjectsID.tileBlue_09.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_10"]) Output = Enums.ObjectsID.tileBlue_10.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_11"]) Output = Enums.ObjectsID.tileBlue_11.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_12"]) Output = Enums.ObjectsID.tileBlue_12.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_13"]) Output = Enums.ObjectsID.tileBlue_13.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_14"]) Output = Enums.ObjectsID.tileBlue_14.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_15"]) Output = Enums.ObjectsID.tileBlue_15.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_16"]) Output = Enums.ObjectsID.tileBlue_16.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_17"]) Output = Enums.ObjectsID.tileBlue_17.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_18"]) Output = Enums.ObjectsID.tileBlue_18.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_19"]) Output = Enums.ObjectsID.tileBlue_19.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_20"]) Output = Enums.ObjectsID.tileBlue_20.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_21"]) Output = Enums.ObjectsID.tileBlue_21.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_22"]) Output = Enums.ObjectsID.tileBlue_22.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_23"]) Output = Enums.ObjectsID.tileBlue_23.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_24"]) Output = Enums.ObjectsID.tileBlue_24.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_25"]) Output = Enums.ObjectsID.tileBlue_25.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_26"]) Output = Enums.ObjectsID.tileBlue_26.ToString();
                if (platform.getTexture() == platformTextures["tileBlue_27"]) Output = Enums.ObjectsID.tileBlue_27.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_01"]) Output = Enums.ObjectsID.tileGreen_01.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_02"]) Output = Enums.ObjectsID.tileGreen_02.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_03"]) Output = Enums.ObjectsID.tileGreen_03.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_04"]) Output = Enums.ObjectsID.tileGreen_04.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_05"]) Output = Enums.ObjectsID.tileGreen_05.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_06"]) Output = Enums.ObjectsID.tileGreen_06.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_07"]) Output = Enums.ObjectsID.tileGreen_07.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_08"]) Output = Enums.ObjectsID.tileGreen_08.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_09"]) Output = Enums.ObjectsID.tileGreen_09.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_10"]) Output = Enums.ObjectsID.tileGreen_10.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_11"]) Output = Enums.ObjectsID.tileGreen_11.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_12"]) Output = Enums.ObjectsID.tileGreen_12.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_13"]) Output = Enums.ObjectsID.tileGreen_13.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_14"]) Output = Enums.ObjectsID.tileGreen_14.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_15"]) Output = Enums.ObjectsID.tileGreen_15.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_16"]) Output = Enums.ObjectsID.tileGreen_16.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_17"]) Output = Enums.ObjectsID.tileGreen_17.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_18"]) Output = Enums.ObjectsID.tileGreen_18.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_19"]) Output = Enums.ObjectsID.tileGreen_19.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_20"]) Output = Enums.ObjectsID.tileGreen_20.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_21"]) Output = Enums.ObjectsID.tileGreen_21.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_22"]) Output = Enums.ObjectsID.tileGreen_22.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_23"]) Output = Enums.ObjectsID.tileGreen_23.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_24"]) Output = Enums.ObjectsID.tileGreen_24.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_25"]) Output = Enums.ObjectsID.tileGreen_25.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_26"]) Output = Enums.ObjectsID.tileGreen_26.ToString();
                if (platform.getTexture() == platformTextures["tileGreen_27"]) Output = Enums.ObjectsID.tileGreen_27.ToString();


                Output += "," + platform.gameObjectPosition.X + "," + platform.gameObjectPosition.Y;

                outputList.Add(Output);
            }


            using (var stream = new FileStream(@"SaveFile.txt", FileMode.Truncate))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("");
                    foreach (string line in outputList) writer.WriteLine(line);
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

        public void loadTextures(ContentManager ContentManager, ref Dictionary<string, Texture2D> platformTextures, GraphicsDevice graphics)
        {
            Platform_TileSheet = ContentManager.Load<Texture2D>("Images\\WorldObjects\\tileSheet_complete_2X");
            List<string> NamesList = new List<string>();
            NamesList = new List<string>(System.IO.File.ReadAllLines(@"PlatformTileSheetNames.txt"));
            NamesList = NamesList.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            Console.WriteLine(NamesList);
            int i = 0;
            for (int m = 0; m < 12; m++)
            {
                  for(int n = 0; n < 9; n++)
                  {
                        PlatformsDic.Add(NamesList[i].ToString(), new Rectangle(n * 64, m * 64, 64, 64));
                        platformTextures.Add(NamesList[i].ToString(), CreatePartImage(PlatformsDic[NamesList[i].ToString()], Platform_TileSheet, graphics));
                    //Console.WriteLine(NamesList[i]);
                    //Console.WriteLine(platformTextures.ElementAt(i));
                    Console.WriteLine(NamesList[i]);
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
