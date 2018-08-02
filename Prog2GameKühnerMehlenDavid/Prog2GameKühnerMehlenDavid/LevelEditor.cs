using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class LevelEditor {

        Vector2 mousePosition;

        //Doublejump is 2x175 high
        int step = 88;         
        //Vector2 deltaMouseGameObjectPos = new Vector2(0, 0);
        bool alreadyDragged = false;
        bool ButtonPushed = false;

        public LevelEditor() {
            mousePosition = new Vector2(0, 0);
        }

        public void movePlatforms(ref List<GameObject> gameObjects, Matrix TransformatinMatrix) 
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
                        if (!alreadyDragged)
                        {
                            gameObject.IsDragged = true;
                            alreadyDragged = true;
                        }
                    }
                    if (gameObject.IsDragged)
                    {
                        
                        if(MouseWorldPosition.X % step < step/2 && MouseWorldPosition.Y % step < step/2)
                        {
                            gameObject.Position = new Vector2((int)MouseWorldPosition.X - gameObject.SpriteRectangle.Width / 2 - (MouseWorldPosition.X % step),
                                                             (int)MouseWorldPosition.Y - gameObject.SpriteRectangle.Height / 2 - (MouseWorldPosition.Y % step));
                        }
                        else if(MouseWorldPosition.X % step < step/2 && MouseWorldPosition.Y % step > step/2)
                        {
                            gameObject.Position = new Vector2((int)MouseWorldPosition.X - gameObject.SpriteRectangle.Width / 2 - (MouseWorldPosition.X % step),
                                                             (int)MouseWorldPosition.Y - gameObject.SpriteRectangle.Height / 2 + (step - (MouseWorldPosition.Y % step)));
                        }
                        else if (MouseWorldPosition.X % step > step/2 && MouseWorldPosition.Y % step < step/2)
                        {
                            gameObject.Position = new Vector2((int)MouseWorldPosition.X - gameObject.SpriteRectangle.Width / 2 + (step - (MouseWorldPosition.X % step)),
                                                             (int)MouseWorldPosition.Y - gameObject.SpriteRectangle.Height / 2 - (MouseWorldPosition.Y % step));
                        }
                        else if (MouseWorldPosition.X % step > step/2 && MouseWorldPosition.Y % step > step/2)
                        {
                            gameObject.Position = new Vector2((int)MouseWorldPosition.X - gameObject.SpriteRectangle.Width / 2 + (step - (MouseWorldPosition.X % step)),
                                                             (int)MouseWorldPosition.Y - gameObject.SpriteRectangle.Height / 2 + (step - (MouseWorldPosition.Y % step)));
                        }
                    }
                }
            }
            else
            {
                alreadyDragged = false;
                foreach(GameObject gameObject in gameObjects)
                {
                    gameObject.IsDragged = false;
                }
            }
        }

        public void moveCamera(ref Vector2 cameraOffset) {
            if (Keyboard.GetState().IsKeyDown(Keys.U)) cameraOffset.Y += 10;
            if (Keyboard.GetState().IsKeyDown(Keys.J)) cameraOffset.Y -= 10;
            if (Keyboard.GetState().IsKeyDown(Keys.K)) cameraOffset.X -= 10;
            if (Keyboard.GetState().IsKeyDown(Keys.H)) cameraOffset.X += 10 ;
        }

        public void DrawLvlEditorUI(Dictionary<string, Texture2D> platformTextures, SpriteBatch spriteBatch, Matrix TransformationMatrix, ref List<GameObject> GameObjectList) {

            MouseState mouseState = Mouse.GetState();
            mousePosition.X = mouseState.X;
            mousePosition.Y = mouseState.Y;

            Vector2 position = new Vector2(1650, 100);
            Vector2 transformedPos = Vector2.Transform(position, Matrix.Invert(TransformationMatrix));
            Vector2 MouseWorldPosition = Vector2.Transform(mousePosition, Matrix.Invert(TransformationMatrix));
            Rectangle PlatformRectangle = new Rectangle((int)position.X, (int)position.Y, 320, 64);
            //if (PlatformRectangle.Contains(new Rectangle((int)MouseWorldPosition.X, (int)MouseWorldPosition.Y, 0, 0)))
            if (PlatformRectangle.Contains(new Point((int)mousePosition.X,(int)mousePosition.Y)))
            {
                position = new Vector2(1450, 100);
                transformedPos = Vector2.Transform(position, Matrix.Invert(TransformationMatrix));
                Console.WriteLine("Mouse Hover Detected");
                if(ButtonState.Pressed == mouseState.LeftButton && !ButtonPushed)
                {
                    ButtonPushed = true;
                    createNewPlatform(ref GameObjectList, platformTextures);
                }
            }
            else{
                ButtonPushed = false;
                position = new Vector2(1650, 100);
            }
            spriteBatch.Draw(platformTextures["Green_320_64"], transformedPos, Color.White);

        }

        private void createNewPlatform(ref List<GameObject> GameObjectList, Dictionary<string, Texture2D> platformTextures)
        {
            GameObjectList.Add(new Platform(platformTextures["Green_320_64"], new Vector2(320, 64), new Vector2(0, 200)));
            Console.WriteLine("GameObject was probably added");
        }
    }
}
