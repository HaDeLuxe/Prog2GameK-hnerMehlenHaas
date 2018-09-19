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

        
        int step = 64;
        //int step = 88;

        private int previousScrollValue;

        bool alreadyDragged = false;
        bool button1Pushed = false;
        bool button2Pushed = false;


     
        bool alreadyDeleted = false;
        GameObject objectToDelete = null;
        //List<string> outputList;
        Dictionary<string, Rectangle> PlatformsDic;
        Enums Enums;


        public LevelEditor() {
            mousePosition = new Vector2(0, 0);
            
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
                foreach(GameObject gameObject in gameObjects.Reverse<GameObject>())
                {
                    Vector2 mouseWorldPosition = Vector2.Transform(mousePosition, Matrix.Invert(transformatinMatrix));
                    if (gameObject.gameObjectRectangle.Intersects(new Rectangle((int)mouseWorldPosition.X, (int)mouseWorldPosition.Y, 0, 0))&& mousePosition.X < 1700)
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
                            gameObject.gameObjectPosition = new Vector2((int)mouseWorldPosition.X - gameObject.gameObjectRectangle.Width  - (mouseWorldPosition.X % step),
                                                             (int)mouseWorldPosition.Y - gameObject.gameObjectRectangle.Height  - (mouseWorldPosition.Y % step));
                        }
                        else if(mouseWorldPosition.X % step < step/2 && mouseWorldPosition.Y % step > step/2)
                        {
                            gameObject.gameObjectPosition = new Vector2((int)mouseWorldPosition.X - gameObject.gameObjectRectangle.Width  - (mouseWorldPosition.X % step),
                                                             (int)mouseWorldPosition.Y - gameObject.gameObjectRectangle.Height  + (step - (mouseWorldPosition.Y % step)));
                        }
                        else if (mouseWorldPosition.X % step > step/2 && mouseWorldPosition.Y % step < step/2)
                        {
                            gameObject.gameObjectPosition = new Vector2((int)mouseWorldPosition.X - gameObject.gameObjectRectangle.Width  + (step - (mouseWorldPosition.X % step)),
                                                             (int)mouseWorldPosition.Y - gameObject.gameObjectRectangle.Height  - (mouseWorldPosition.Y % step));
                        }
                        else if (mouseWorldPosition.X % step > step/2 && mouseWorldPosition.Y % step > step/2)
                        {
                            gameObject.gameObjectPosition = new Vector2((int)mouseWorldPosition.X - gameObject.gameObjectRectangle.Width  + (step - (mouseWorldPosition.X % step)),
                                                             (int)mouseWorldPosition.Y - gameObject.gameObjectRectangle.Height  + (step - (mouseWorldPosition.Y % step)));
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
            if (Keyboard.GetState().IsKeyDown(Keys.U)) cameraOffset.Y += 20;
            if (Keyboard.GetState().IsKeyDown(Keys.J)) cameraOffset.Y -= 20;
            if (Keyboard.GetState().IsKeyDown(Keys.K)) cameraOffset.X -= 20;
            if (Keyboard.GetState().IsKeyDown(Keys.H)) cameraOffset.X += 20;
        }

        

        private int yoffset = 0;
        private Vector2 firstPosition = new Vector2(0, 0);

        /// <summary>
        /// Draws the Level Editor UI and finds out if element was clicked
        /// </summary>
        /// <param name="texturesDictionary"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="transformationMatrix"></param>
        /// <param name="gameObjectList"></param>
        /// <param name="graphics"></param>
        public void DrawLvlEditorUI(Dictionary<string, Texture2D> texturesDictionary, SpriteBatch spriteBatch, Matrix transformationMatrix, ref List<GameObject> gameObjectList, ref List<GameObject> levelGameObjects, GraphicsDevice graphics, ref LoadAndSave loadAndSave, ref Levels levelManager) 
        {
            int j = 0;
            MouseState mouseState = Mouse.GetState();
            Vector2 firstPosition = new Vector2(1750, 200);
            Vector2 transformedPos_firstPosition = Vector2.Transform(firstPosition, Matrix.Invert(transformationMatrix));
            
            spriteBatch.Draw(texturesDictionary["Transparent_500x50"], transformedPos_firstPosition + j * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            j++;
            spriteBatch.Draw(texturesDictionary["Transparent_1000x50"], transformedPos_firstPosition + j * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            j++;
            spriteBatch.Draw(texturesDictionary["Climbingplant_38x64"], transformedPos_firstPosition + j * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            j++;
            spriteBatch.Draw(texturesDictionary["Transparent_64x64"], transformedPos_firstPosition + j * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            j++;

            for (int i = 0; i < PlatformsDic.Count(); i++)
            {
                spriteBatch.Draw(Platform_TileSheet, transformedPos_firstPosition + j * new Vector2(0, 100) - new Vector2(0, yoffset), PlatformsDic.ElementAt(i).Value, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                j++;
            }

            spriteBatch.Draw(texturesDictionary["SnailShell"], transformedPos_firstPosition + j * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            j++;
            spriteBatch.Draw(texturesDictionary["Armor_64x64"], transformedPos_firstPosition + j * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            j++;
            spriteBatch.Draw(texturesDictionary["Shovel_64x64"], transformedPos_firstPosition + j * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            j++;
            spriteBatch.Draw(texturesDictionary["Scissors_64x64"], transformedPos_firstPosition + j * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            j++;
            spriteBatch.Draw(texturesDictionary["HealthItem"], transformedPos_firstPosition + j * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            j++;
            spriteBatch.Draw(texturesDictionary["PowerPotion"], transformedPos_firstPosition + j * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            j++;
            spriteBatch.Draw(texturesDictionary["JumpPotion"], transformedPos_firstPosition + j * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            j++;
            spriteBatch.Draw(texturesDictionary["GoldenUmbrella"], transformedPos_firstPosition + j * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            j++;
            spriteBatch.Draw(texturesDictionary["Spiderweb_64x64"], transformedPos_firstPosition + j * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            j++;
            spriteBatch.Draw(texturesDictionary["VineDoor"], transformedPos_firstPosition + j * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            j++;
            spriteBatch.Draw(texturesDictionary["Apple"], transformedPos_firstPosition + j * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            j++;
            j++;
            spriteBatch.Draw(texturesDictionary["EnemySpawnPoint"], transformedPos_firstPosition + j * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            j++;
            spriteBatch.Draw(texturesDictionary["cornnency"], transformedPos_firstPosition + j * new Vector2(0, 100) - new Vector2(0, yoffset), Color.White);
            j++;


            if (ButtonState.Pressed == mouseState.LeftButton && !button1Pushed)
            {
                Vector2 transformedPos = Vector2.Transform(new Vector2(1000, 200), Matrix.Invert(transformationMatrix));

                Rectangle checkRectangle;
                button1Pushed = true;

                for (int i = 4; i < PlatformsDic.Count()+4; i++)
                {
                    checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + i * 100 - yoffset, 64,64);
                    if(checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    {
                        createNewPlatform(ref gameObjectList, PlatformsDic.ElementAt(i-4).Key, transformationMatrix, graphics, texturesDictionary);
                    }
                }
               
                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + 0 * 100 - yoffset, 500, 50);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                {
                    gameObjectList.Add(new Platform(texturesDictionary["Transparent_500x50"], new Vector2(512, 64), transformedPos, (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.INVISIBLE_WALL_500x50, false));
                    gameObjectList.Last().DontDrawThisObject();
                }
                
                    checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + 1 * 100 - yoffset, 1000, 50);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                {
                    gameObjectList.Add(new Platform(texturesDictionary["Transparent_1000x50"], new Vector2(1024, 64), transformedPos, (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.INVSIBLE_WALL_1000x50, false));
                    gameObjectList.Last().DontDrawThisObject();
                }

                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + 2 * 100 - yoffset, 38, 64);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    gameObjectList.Add(new Platform(texturesDictionary["Climbingplant_38x64"], new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.VINE, (int)Enums.ObjectsID.VINE, false));

                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + 3 * 100 - yoffset, 64, 64);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                {
                    gameObjectList.Add(new Platform(texturesDictionary["Transparent_64x64"], new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.INVISIBLE_WALL_64x64, false));
                    gameObjectList.Last().DontDrawThisObject();
                }

                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + ((PlatformsDic.Count()+4) * 100) - yoffset, 64, 64);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    gameObjectList.Add(new Item(texturesDictionary["SnailShell"], new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.SNAILSHELL));

                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + ((PlatformsDic.Count() + 5) * 100) - yoffset, 64, 64);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    gameObjectList.Add(new Item(texturesDictionary["Armor_64x64"], new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.ARMOR));

                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + ((PlatformsDic.Count() + 6) * 100) - yoffset, 64, 64);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    gameObjectList.Add(new Item(texturesDictionary["Shovel_64x64"], new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.SHOVEL));

                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + ((PlatformsDic.Count() + 7) * 100) - yoffset, 64, 64);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    gameObjectList.Add(new Item(texturesDictionary["Scissors_64x64"], new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.SCISSORS));

                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + ((PlatformsDic.Count() + 8) * 100) - yoffset, 64, 64);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    gameObjectList.Add(new Item(texturesDictionary["HealthItem"], new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.HEALTHPOTION));

                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + ((PlatformsDic.Count() + 9) * 100) - yoffset, 64, 64);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    gameObjectList.Add(new Item(texturesDictionary["PowerPotion"], new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.POWERPOTION));

                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + ((PlatformsDic.Count() + 10) * 100) - yoffset, 64, 64);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    gameObjectList.Add(new Item(texturesDictionary["JumpPotion"], new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.JUMPPOTION));

                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + ((PlatformsDic.Count() + 11) * 100) - yoffset, 64, 64);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    gameObjectList.Add(new Item(texturesDictionary["GoldenUmbrella"], new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.GOLDENUMBRELLA));

                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + ((PlatformsDic.Count() + 12) * 100) - yoffset, 64, 64);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    gameObjectList.Add(new Platform(texturesDictionary["Spiderweb_64x64"], new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.SPIDERWEB, false));

                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + ((PlatformsDic.Count() + 13) * 100) - yoffset, 64, 64);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    gameObjectList.Add(new Platform(texturesDictionary["VineDoor"], new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.VINEDOOR, false));

                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + ((PlatformsDic.Count() + 14) * 100) - yoffset, 128, 128);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    gameObjectList.Add(new Item(texturesDictionary["Apple"], new Vector2(128,128), transformedPos, (int)Enums.ObjectsID.APPLE));

                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + ((PlatformsDic.Count() + 16) * 100) - yoffset, 64, 64);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                {
                    gameObjectList.Add(new Platform(texturesDictionary["EnemySpawnPoint"], new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.ENEMYSPAWNPOINT, true));
                    gameObjectList.Last().DontDrawThisObject();
                }

                checkRectangle = new Rectangle((int)firstPosition.X, (int)firstPosition.Y + ((PlatformsDic.Count() + 17) * 100) - yoffset, 128, 128);
                if (checkRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
                    gameObjectList.Add(new Item(texturesDictionary["cornnency"], new Vector2(128, 128), transformedPos, (int)Enums.ObjectsID.CORNNENCY));
            }


            
            if (ButtonState.Released == mouseState.LeftButton)
            button1Pushed = false;


            Color color = new Color();
            Vector2 positionBackButton = new Vector2(1550, 900);
            Vector2 transformedBackButton = Vector2.Transform(positionBackButton, Matrix.Invert(transformationMatrix));
            Rectangle rectangleBackButton = new Rectangle((int)positionBackButton.X, (int)positionBackButton.Y, 200, 50);
            if (rectangleBackButton.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
            {
                if (ButtonState.Pressed == mouseState.LeftButton && !button2Pushed)
                {
                    button2Pushed = true;
                    color = Color.LightGray;
                    foreach (GameObject gameObject in gameObjectList) levelGameObjects.Add(gameObject);

                    levelManager.sortGameObjects();
                    loadAndSave.Save();
                }
                else
                {
                    color = Color.White;
                }
            }
            else
            {
                button2Pushed = false;
                color = Color.White;
            }


            spriteBatch.Draw(texturesDictionary["LevelEditorUIBackButton"], transformedBackButton, color);
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
                if(textureName == temp)
                {
                    if (textureName == "tileBrown_01" || textureName == "tileYellow_01" || textureName == "tileBlue_01" || textureName == "tileGreen_01")
                        gameObjectList.Add(new Platform(platformTextures[textureName], new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.PLATFORM, (int)tempObjectID, false));

                    gameObjectList.Add(new Platform(platformTextures[textureName], new Vector2(64, 64), transformedPos, (int)Enums.ObjectsID.PLATFORM, (int)tempObjectID, false));
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
            int i = 0;
            for (int m = 0; m < 12; m++)
            {
                  for(int n = 0; n < 9; n++)
                  {
                        PlatformsDic.Add(NamesList[i].ToString(), new Rectangle(n * 64, m * 64, 64, 64));
                        platformTextures.Add(NamesList[i].ToString(), CreatePartImage(PlatformsDic[NamesList[i].ToString()], Platform_TileSheet, graphics));
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
