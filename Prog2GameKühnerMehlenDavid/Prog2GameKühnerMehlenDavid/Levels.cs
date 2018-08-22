using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class Levels {

        Rectangle TutorialRectangle = new Rectangle(5000, -1000, 10000, 2000);
        Rectangle DunghillRectangle = new Rectangle(0, -3000, 4000, 4000);
        Rectangle HubRectangle = new Rectangle(-500, 2000, 4000, 2000);
        Rectangle AntRectangle = new Rectangle(-15500, 2000, 18000, 4000);
        Rectangle GreenhouseRectangle = new Rectangle(-7000, -9000, 6000, 10000);
        Rectangle TreeRectangle = new Rectangle(-12000, -9000, 4000, 10000);
        Rectangle CrownRectangle = new Rectangle(-14000, 15000, 8000, 4000);



        public void sortGameObjects(List<GameObject> allGameObjects, ref List<GameObject> TutorialGameObjects, ref List<GameObject> DungHillGameObjects, ref List<GameObject> GreenHouseGameObjects, ref List<GameObject> AntGameObjects, ref List<GameObject> TreeGameObjects, ref List<GameObject> HubGameObjects) 
        {
            foreach(GameObject GameObject in allGameObjects)
            {
                if (DetectCollision(GameObject, TutorialRectangle)) TutorialGameObjects.Add(GameObject);
                if (DetectCollision(GameObject, DunghillRectangle)) DungHillGameObjects.Add(GameObject);
                if (DetectCollision(GameObject, HubRectangle)) HubGameObjects.Add(GameObject);
                if (DetectCollision(GameObject, AntRectangle)) AntGameObjects.Add(GameObject);
                if (DetectCollision(GameObject, GreenhouseRectangle)) GreenHouseGameObjects.Add(GameObject);
                if (DetectCollision(GameObject, TreeRectangle) || DetectCollision(GameObject, CrownRectangle)) TreeGameObjects.Add(GameObject);
            }
        }



        private bool DetectCollision(GameObject gameObject, Rectangle rec) 
        {
            if (rec.Right >= gameObject.gameObjectRectangle.Left &&
               rec.Left <= gameObject.gameObjectRectangle.Left &&
               rec.Bottom > gameObject.gameObjectRectangle.Top &&
               rec.Top < gameObject.gameObjectRectangle.Bottom)
                return true;
            else if (rec.Left <= gameObject.gameObjectRectangle.Right &&
                rec.Right >= gameObject.gameObjectRectangle.Right &&
                rec.Bottom > gameObject.gameObjectRectangle.Top &&
                rec.Top < gameObject.gameObjectRectangle.Bottom)
                return true;
            else if (rec.Bottom > gameObject.gameObjectRectangle.Top &&
                rec.Top < gameObject.gameObjectRectangle.Top &&
                rec.Right > gameObject.gameObjectRectangle.Left &&
                rec.Left < gameObject.gameObjectRectangle.Right)
                return true;
            else if (rec.Top < gameObject.gameObjectRectangle.Bottom &&
                rec.Bottom > gameObject.gameObjectRectangle.Bottom &&
                rec.Right > gameObject.gameObjectRectangle.Left &&
                rec.Left < gameObject.gameObjectRectangle.Right)
                return true;
            else
                return false;
        }

        public void drawLevelsBackground(SpriteBatch spriteBatch, Texture2D background, Texture2D Sky_2000_1000, Texture2D Hub_Background, Texture2D Dunghill_Background, Texture2D Ant_Cave_Background, Texture2D Greenhouse_Background, Texture2D Tree_Background, Texture2D Crown_Background, Texture2D Roof_Background) {
            //BACKGROUND
            spriteBatch.Draw(background, new Vector2(5000, -0), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(background, new Vector2(7000, -0), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(background, new Vector2(9000, -0), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(background, new Vector2(11000, -0), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(background, new Vector2(13000, 0), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Sky_2000_1000, new Vector2(5000, -1000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Sky_2000_1000, new Vector2(7000, -1000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Sky_2000_1000, new Vector2(9000, -1000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Sky_2000_1000, new Vector2(11000, -1000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Sky_2000_1000, new Vector2(13000, -1000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

            //spriteBatch.Draw(Ground_Tutorial_2000_1000, new Vector2(5000, 1000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(Ground_Tutorial_2000_1000, new Vector2(7000, 1000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(Ground_Tutorial_2000_1000, new Vector2(9000, 1000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(Ground_Tutorial_2000_1000, new Vector2(11000, 1000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(Ground_Tutorial_2000_1000, new Vector2(13000, 1000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

            //Hub

            spriteBatch.Draw(Hub_Background, new Vector2(1500, 2000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Hub_Background, new Vector2(-500, 2000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

            //DungHill

            spriteBatch.Draw(Dunghill_Background, new Vector2(0, -1000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Dunghill_Background, new Vector2(2000, -1000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Dunghill_Background, new Vector2(0, -3000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Dunghill_Background, new Vector2(2000, -3000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

            //Ant Cave

            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-5500, 2000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-7500, 2000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-9500, 2000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-11500, 2000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-13500, 2000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-15500, 2000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);


            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-5500, 4000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-7500, 4000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-9500, 4000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-11500, 4000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-13500, 4000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-15500, 4000), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

            //Greenhouse

            spriteBatch.Draw(Greenhouse_Background, new Vector2(-3000, -1000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Greenhouse_Background, new Vector2(-5000, -1000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Greenhouse_Background, new Vector2(-7000, -1000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Greenhouse_Background, new Vector2(-3000, -3000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Greenhouse_Background, new Vector2(-5000, -3000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Greenhouse_Background, new Vector2(-7000, -3000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Greenhouse_Background, new Vector2(-3000, -5000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Greenhouse_Background, new Vector2(-5000, -5000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Greenhouse_Background, new Vector2(-7000, -5000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            //Roof

            spriteBatch.Draw(Roof_Background, new Vector2(-3000, -7000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Roof_Background, new Vector2(-5000, -7000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Roof_Background, new Vector2(-7000, -7000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Roof_Background, new Vector2(-3000, -9000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Roof_Background, new Vector2(-5000, -9000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Roof_Background, new Vector2(-7000, -9000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            //tree

            spriteBatch.Draw(Tree_Background, new Vector2(-10000, -1000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Tree_Background, new Vector2(-12000, -1000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Tree_Background, new Vector2(-10000, -3000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Tree_Background, new Vector2(-12000, -3000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Tree_Background, new Vector2(-10000, -5000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Tree_Background, new Vector2(-12000, -5000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Tree_Background, new Vector2(-10000, -7000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Tree_Background, new Vector2(-12000, -7000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Tree_Background, new Vector2(-10000, -9000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Tree_Background, new Vector2(-12000, -9000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            //Crown

            spriteBatch.Draw(Crown_Background, new Vector2(-10000, -11000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Crown_Background, new Vector2(-12000, -11000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Crown_Background, new Vector2(-8000, -11000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Crown_Background, new Vector2(-14000, -11000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Crown_Background, new Vector2(-10000, -13000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Crown_Background, new Vector2(-12000, -13000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Crown_Background, new Vector2(-8000, -13000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Crown_Background, new Vector2(-14000, -13000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Crown_Background, new Vector2(-8000, -15000), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
