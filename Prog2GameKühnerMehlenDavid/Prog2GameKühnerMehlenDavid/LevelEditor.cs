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
        int Step = 88;         
        bool AlreadyDragged = false;
        bool Button1Pushed = false;
        bool Button2Pushed = false;
        bool Button3Pushed = false;
        bool Button4Pushed = false;
        //FLASCHE SCHLEIBWEISE
        bool AlreadDeleted = false;
        GameObject ObjectToDelete = null;
        List<string> OutputList;
        Enums Enums;


        public LevelEditor() {
            mousePosition = new Vector2(0, 0);
            OutputList = new List<string>();
            Enums = new Enums();
        }
        
        /// <summary>
        /// Moving GameObjects with the mouse while holding left mouse button and deleting GameObjects with right mouse button click
        /// </summary>
        /// <param name="gameObjects"></param>
        /// <param name="TransformatinMatrix"></param>
        public void moveOrDeletePlatforms(ref List<GameObject> gameObjects, Matrix TransformatinMatrix) 
        {
            MouseState mouseState = Mouse.GetState();
            mousePosition.X = mouseState.X;
            mousePosition.Y = mouseState.Y;
            
            if (ButtonState.Pressed == mouseState.LeftButton)
            {
                foreach(GameObject gameObject in gameObjects)
                {
                    Vector2 MouseWorldPosition = Vector2.Transform(mousePosition, Matrix.Invert(TransformatinMatrix));
                    if (gameObject.SpriteRectangle.Intersects(new Rectangle((int)MouseWorldPosition.X, (int)MouseWorldPosition.Y, 0, 0)))
                    {
                        if (!AlreadyDragged)
                        {
                            gameObject.IsDragged = true;
                            AlreadyDragged = true;
                        }
                    }
                    if (gameObject.IsDragged)
                    {
                        
                        if(MouseWorldPosition.X % Step < Step/2 && MouseWorldPosition.Y % Step < Step/2)
                        {
                            gameObject.Position = new Vector2((int)MouseWorldPosition.X - gameObject.SpriteRectangle.Width / 2 - (MouseWorldPosition.X % Step),
                                                             (int)MouseWorldPosition.Y - gameObject.SpriteRectangle.Height / 2 - (MouseWorldPosition.Y % Step));
                        }
                        else if(MouseWorldPosition.X % Step < Step/2 && MouseWorldPosition.Y % Step > Step/2)
                        {
                            gameObject.Position = new Vector2((int)MouseWorldPosition.X - gameObject.SpriteRectangle.Width / 2 - (MouseWorldPosition.X % Step),
                                                             (int)MouseWorldPosition.Y - gameObject.SpriteRectangle.Height / 2 + (Step - (MouseWorldPosition.Y % Step)));
                        }
                        else if (MouseWorldPosition.X % Step > Step/2 && MouseWorldPosition.Y % Step < Step/2)
                        {
                            gameObject.Position = new Vector2((int)MouseWorldPosition.X - gameObject.SpriteRectangle.Width / 2 + (Step - (MouseWorldPosition.X % Step)),
                                                             (int)MouseWorldPosition.Y - gameObject.SpriteRectangle.Height / 2 - (MouseWorldPosition.Y % Step));
                        }
                        else if (MouseWorldPosition.X % Step > Step/2 && MouseWorldPosition.Y % Step > Step/2)
                        {
                            gameObject.Position = new Vector2((int)MouseWorldPosition.X - gameObject.SpriteRectangle.Width / 2 + (Step - (MouseWorldPosition.X % Step)),
                                                             (int)MouseWorldPosition.Y - gameObject.SpriteRectangle.Height / 2 + (Step - (MouseWorldPosition.Y % Step)));
                        }
                    }
                }
            }
            else
            {
                AlreadyDragged = false;
                foreach(GameObject gameObject in gameObjects)
                {
                    gameObject.IsDragged = false;
                }
            }

            //Delete Objects by pressing right mouse button
            if (ButtonState.Pressed == mouseState.RightButton)
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    Vector2 MouseWorldPosition = Vector2.Transform(mousePosition, Matrix.Invert(TransformatinMatrix));
                    if (gameObject.SpriteRectangle.Intersects(new Rectangle((int)MouseWorldPosition.X, (int)MouseWorldPosition.Y, 0, 0)))
                    {
                        if(!AlreadDeleted)
                        {
                            ObjectToDelete = gameObject;
                            //gameObjects.Remove(gameObject);
                            AlreadDeleted = true;
                        }
                    }
                }
            }
            else
            {
                AlreadDeleted = false;
            }
            if(ObjectToDelete != null)
            {
                gameObjects.Remove(ObjectToDelete);
                ObjectToDelete = null;
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
        /// <param name="TransformationMatrix"></param>
        /// <param name="GameObjectList"></param>
        public void DrawLvlEditorUI(Dictionary<string, Texture2D> platformTextures, SpriteBatch spriteBatch, Matrix TransformationMatrix, ref List<GameObject> GameObjectList) {
            MouseState mouseState = Mouse.GetState();
            mousePosition.X = mouseState.X;
            mousePosition.Y = mouseState.Y;

            //TODO: Look if RAM gets bullshittet
            Vector2 MouseWorldPosition = Vector2.Transform(mousePosition, Matrix.Invert(TransformationMatrix));
            Vector2 PositionGreenPlatform_320x64 = new Vector2(1650, 100);
            Vector2 transformedPosGreenPlatform_320x64 = Vector2.Transform(PositionGreenPlatform_320x64, Matrix.Invert(TransformationMatrix));
            Rectangle PlatformRectangleGreenPlatform_320x64 = new Rectangle((int)PositionGreenPlatform_320x64.X, (int)PositionGreenPlatform_320x64.Y, 320, 64);
            if (PlatformRectangleGreenPlatform_320x64.Contains(new Point((int)mousePosition.X,(int)mousePosition.Y)))
            {
                PositionGreenPlatform_320x64 = new Vector2(1450, 100);
                transformedPosGreenPlatform_320x64 = Vector2.Transform(PositionGreenPlatform_320x64, Matrix.Invert(TransformationMatrix));
                if(ButtonState.Pressed == mouseState.LeftButton && !Button1Pushed)
                {
                    Button1Pushed = true;
                    createNewPlatform(ref GameObjectList, platformTextures["Green_320_64"], TransformationMatrix, platformTextures);
                }
            }
            else{
                Button1Pushed = false;
                PositionGreenPlatform_320x64 = new Vector2(1650, 100);
            }

            Vector2 PositionTransparentWall_500x50 = new Vector2(1650, 200);
            Vector2 transformedPosTransparentWall_500x50 = Vector2.Transform(PositionTransparentWall_500x50, Matrix.Invert(TransformationMatrix));
            Rectangle PlatformRectangleTransparentWall_500x50 = new Rectangle((int)PositionTransparentWall_500x50.X, (int)PositionTransparentWall_500x50.Y, 320, 64);
            if (PlatformRectangleTransparentWall_500x50.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
            {
                PositionTransparentWall_500x50 = new Vector2(1250, 200);
                transformedPosTransparentWall_500x50 = Vector2.Transform(PositionTransparentWall_500x50, Matrix.Invert(TransformationMatrix));
                if (ButtonState.Pressed == mouseState.LeftButton && !Button2Pushed)
                {
                    Button2Pushed = true;
                    createNewPlatform(ref GameObjectList, platformTextures["Transparent_500x50"], TransformationMatrix, platformTextures);
                }
            }
            else
            {
                Button2Pushed = false;
                PositionGreenPlatform_320x64 = new Vector2(1650, 200);
            }

            Vector2 PositionTransparentWall_1000x50 = new Vector2(1650, 300);
            Vector2 transformedPosTransparentWall_1000x50 = Vector2.Transform(PositionTransparentWall_1000x50, Matrix.Invert(TransformationMatrix));
            Rectangle PlatformRectangleTransparentWall_1000x50 = new Rectangle((int)PositionTransparentWall_1000x50.X, (int)PositionTransparentWall_1000x50.Y, 320, 64);
            if (PlatformRectangleTransparentWall_1000x50.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
            {
                PositionTransparentWall_1000x50 = new Vector2(750, 300);
                transformedPosTransparentWall_1000x50 = Vector2.Transform(PositionTransparentWall_1000x50, Matrix.Invert(TransformationMatrix));
                if (ButtonState.Pressed == mouseState.LeftButton && !Button3Pushed)
                {
                    Button3Pushed = true;
                    createNewPlatform(ref GameObjectList, platformTextures["Transparent_1000x50"], TransformationMatrix, platformTextures);
                }
            }
            else
            {
                Button3Pushed = false;
                PositionGreenPlatform_320x64 = new Vector2(1650, 300);
            }

            Color color = new Color();
            Vector2 PositionBackButton = new Vector2(1550, 900);
            Vector2 transformedBackButton = Vector2.Transform(PositionBackButton, Matrix.Invert(TransformationMatrix));
            Rectangle RectangleBackButton = new Rectangle((int)PositionBackButton.X, (int)PositionBackButton.Y, 200, 50);
            if (RectangleBackButton.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y)))
            {
                if (ButtonState.Pressed == mouseState.LeftButton && !Button4Pushed)
                {
                    Button4Pushed = true;
                    color = Color.LightGray;
                    Save(GameObjectList, platformTextures);

                }
                else
                {
                    color = Color.White;
                }
            }
            else
            {
                Button4Pushed = false;
                color = Color.White;
            }



            spriteBatch.Draw(platformTextures["Green_320_64"], transformedPosGreenPlatform_320x64, Color.White);
            spriteBatch.Draw(platformTextures["Transparent_500x50"], transformedPosTransparentWall_500x50, Color.White);
            spriteBatch.Draw(platformTextures["Transparent_1000x50"], transformedPosTransparentWall_1000x50, Color.White);
            spriteBatch.Draw(platformTextures["LevelEditorUIBackButton"], transformedBackButton, color);
        }

        /// <summary>
        /// Add a new Platform to the GameObjectList and gives it the standard viewport coordinates (1000,200)
        /// </summary>
        /// <param name="GameObjectList"></param>
        /// <param name="platformTextures"></param>
        /// <param name="TransformationMatrix"></param>
        private void createNewPlatform(ref List<GameObject> GameObjectList, Texture2D platformTexture, Matrix TransformationMatrix, Dictionary<string, Texture2D> platformTextures)
        {
            Vector2 transformedPos = Vector2.Transform(new Vector2(1000,200), Matrix.Invert(TransformationMatrix));
            if(platformTexture == platformTextures["Green_320_64"])
                GameObjectList.Add(new Platform(platformTextures["Green_320_64"], new Vector2(320, 64), transformedPos));
            if (platformTexture == platformTextures["Transparent_500x50"])
            {
                GameObjectList.Add(new Platform(platformTextures["Transparent_500x50"], new Vector2(500, 50), transformedPos));
                GameObjectList.Last().DontDrawThisObject();
            }
            if(platformTexture == platformTextures["Transparent_1000x50"])
            {
                GameObjectList.Add(new Platform(platformTextures["Transparent_1000x50"], new Vector2(1000, 50), transformedPos));
                GameObjectList.Last().DontDrawThisObject();

            }
        }


        private void Save(List<GameObject> GameObjectList, Dictionary<string, Texture2D> platformTextures) {

            OutputList.RemoveRange(0, OutputList.Count());

            foreach (GameObject gameObject in GameObjectList)
            {
                string Output = "";
                if (gameObject.getTexture() == platformTextures["Green_320_64"]) Output = Enums.ObjectsID.GREEN_PLATFORM_320_64.ToString();
                if (gameObject.getTexture() == platformTextures["Transparent_500x50"]) Output =  Enums.ObjectsID.INVISIBLE_WALL_500x50.ToString();
                if (gameObject.getTexture() == platformTextures["Transparent_1000x50"]) Output = Enums.ObjectsID.INVSIBLE_WALL_1000x50.ToString();
                Output +=","+ gameObject.Position.X + "," + gameObject.Position.Y;

                OutputList.Add(Output);
            }


            using (var stream = new FileStream(@"SaveFile.txt", FileMode.Truncate))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("");
                    foreach (string line in OutputList)
                    {
                        
                        writer.WriteLine(line);
                    }
                }
            }

        }
    }
}
