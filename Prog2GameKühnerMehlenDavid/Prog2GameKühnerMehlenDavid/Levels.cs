using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    internal class Levels {

        Rectangle TutorialRectangle = new Rectangle(5120, 0, 10240, 2048);
        //Rectangle DunghillRectangle = new Rectangle(0, -2048, 4096, 4096);
        Rectangle HubRectangle = new Rectangle(2048, 4096, 2048, 2048);
        Rectangle AntRectangle = new Rectangle(-8192, 4096, 8192, 4096);
        Rectangle GreenhouseRectangle = new Rectangle(-2048, -4096, 6144, 6144);
        Rectangle TreeRectangle = new Rectangle(-8192, -8192, 4096, 10240);
        Rectangle CrownRectangle = new Rectangle(-10240, -14336, 8192, 6144);
        //TODO: Gazebo Rectangle
        Rectangle GazeboRectangle = new Rectangle(-14000, -6200, 600, 200);
        Rectangle TutHubBorderRectangle = new Rectangle(5800, 2000, 400, 1700);

        Rectangle HubToGreenRectangle = new Rectangle(3300, 2600, 200, 700);
        Rectangle GreenToHubRectangle = new Rectangle(3300, 3200, 200, 600);

        Rectangle GreenToTreeRectangle = new Rectangle(-4000, 0, 900, 2000);
        Rectangle TreeToGreenRectangle = new Rectangle(-3100, 0, 900, 2000);
        
        Rectangle HubToAntRectangle = new Rectangle(0, 4000, 1000, 2000);
        Rectangle AntToHubRectangle = new Rectangle(1000, 4000, 1000, 2000);

        Rectangle AntToTreeRectangle = new Rectangle(-8000, 2100, 2000, 950);
        Rectangle TreeToAntRectangle = new Rectangle(-8000, 3050, 2000, 950);
        


        public List<GameObject> TutorialGameObjects;
        public List<GameObject> GreenHouseGameObjects;
        public List<GameObject> AntGameObjects;
        public List<GameObject> TreeGameObjects;
        public List<GameObject> HubGameObjects;
        public List<GameObject> InterLevelGameObjects;
        public List<GameObject> allGameObjects;
        public List<GameObject> currentLevelGameObjects;
        public List<GameObject> currentLevelCornnency;

        bool TutToHub = false;
        bool HubToGreen = false;
        bool GreenToHub = false;
        //bool DungToGreen = false;
        //bool GreenToDung = false;
        bool GreenToTree = false;
        bool TreeToGreen = false;
        bool HubToAnt = false;
        bool AntToHub = false;
        bool AntToTree = false;
        bool TreeToAnt = false;
        bool TreeToAntBottom = false;

        public Enums.Level currentLevel = Enums.Level.TUTORIAL;

        public Vector2 PlayerPos;

        public Levels(ref Vector2 PlayerPos, ref List<GameObject> currentLevelGameObjects, ref List<GameObject> allGameObjects) {
            TutorialGameObjects = new List<GameObject>();
            GreenHouseGameObjects = new List<GameObject>();
            AntGameObjects = new List<GameObject>();
            TreeGameObjects = new List<GameObject>();
            HubGameObjects = new List<GameObject>();
            InterLevelGameObjects = new List<GameObject>();
            this.PlayerPos = PlayerPos;
            this.currentLevelGameObjects = currentLevelGameObjects;
            this.allGameObjects = allGameObjects;
        }

        public void ManageLevels(Vector2 PlayerPos)
        {
            if (TutorialRectangle.Contains(PlayerPos))
                currentLevel = Enums.Level.TUTORIAL;
            if (GreenhouseRectangle.Contains(PlayerPos))
                currentLevel = Enums.Level.GREENHOUSE;
            if (HubRectangle.Contains(PlayerPos))
                currentLevel = Enums.Level.HUB;
            if (AntRectangle.Contains(PlayerPos))
                currentLevel = Enums.Level.ANTCAVE;
            if (TreeRectangle.Contains(PlayerPos) || CrownRectangle.Contains(PlayerPos))
                currentLevel = Enums.Level.TREE;


            if (TutHubBorderRectangle.Contains(PlayerPos) && !TutToHub)
            {
                currentLevelGameObjects.Clear();
                foreach(GameObject gameObject in HubGameObjects)
                {
                    currentLevelGameObjects.Add(gameObject);
                }
                foreach (GameObject GameObject in InterLevelGameObjects) currentLevelGameObjects.Add(GameObject);
                TutToHub = true;
            }


            if (HubToGreenRectangle.Contains(PlayerPos) && !GreenToHubRectangle.Contains(PlayerPos) && !HubToGreen)
            {
                currentLevelGameObjects.Clear();
                foreach (GameObject gameObject in GreenHouseGameObjects) currentLevelGameObjects.Add(gameObject);
                foreach (GameObject GameObject in InterLevelGameObjects) currentLevelGameObjects.Add(GameObject);
                HubToGreen = true;
                GreenToHub = false;

            }


            if (GreenToHubRectangle.Contains(PlayerPos) && !HubToGreenRectangle.Contains(PlayerPos) && !GreenToHub)
            {
                currentLevelGameObjects.Clear();
                foreach (GameObject gameObject in HubGameObjects) currentLevelGameObjects.Add(gameObject);
                foreach (GameObject GameObject in InterLevelGameObjects) currentLevelGameObjects.Add(GameObject);
                GreenToHub = true;
                HubToGreen = false;
            }

           

            if(GreenToTreeRectangle.Contains(PlayerPos) && !GreenToTree)
            {
                currentLevelGameObjects.Clear();
                foreach (GameObject gameObject in TreeGameObjects) currentLevelGameObjects.Add(gameObject);
                foreach (GameObject GameObject in InterLevelGameObjects) currentLevelGameObjects.Add(GameObject);
                GreenToTree = true;
                TreeToGreen = false;
            }

            if(TreeToGreenRectangle.Contains(PlayerPos) && !TreeToGreen)
            {
                currentLevelGameObjects.Clear();
                foreach (GameObject gameObject in GreenHouseGameObjects) currentLevelGameObjects.Add(gameObject);
                foreach (GameObject GameObject in InterLevelGameObjects) currentLevelGameObjects.Add(GameObject);
                TreeToGreen = true;
                GreenToTree = false;
            }

           

            if (HubToAntRectangle.Contains(PlayerPos) && !HubToAnt)
            {
                currentLevelGameObjects.Clear();
                foreach (GameObject gameObject in AntGameObjects) currentLevelGameObjects.Add(gameObject);
                foreach (GameObject GameObject in InterLevelGameObjects) currentLevelGameObjects.Add(GameObject);
                HubToAnt = true;
                AntToHub = false;
            }

            if(AntToHubRectangle.Contains(PlayerPos) && !AntToHub)
            {
                currentLevelGameObjects.Clear();
                foreach (GameObject gameObject in HubGameObjects) currentLevelGameObjects.Add(gameObject);
                foreach (GameObject GameObject in InterLevelGameObjects) currentLevelGameObjects.Add(GameObject);
                AntToHub = true;
                HubToAnt = false;
            }

            if(AntToTreeRectangle.Contains(PlayerPos) && !AntToTree)
            {
                currentLevelGameObjects.Clear();
                foreach (GameObject gameObject in TreeGameObjects) currentLevelGameObjects.Add(gameObject);
                foreach (GameObject GameObject in InterLevelGameObjects) currentLevelGameObjects.Add(GameObject);
                AntToTree = true;
                TreeToAnt = false;
            }

            if(TreeToAntRectangle.Contains(PlayerPos) && !TreeToAnt)
            {
                currentLevelGameObjects.Clear();
                foreach (GameObject gameObject in AntGameObjects) currentLevelGameObjects.Add(gameObject);
                foreach (GameObject GameObject in InterLevelGameObjects) currentLevelGameObjects.Add(GameObject);
                TreeToAnt = true;
                AntToTree = false;
            }

            if (GazeboRectangle.Contains(PlayerPos))
            {
                Camera.enableCameraMovement = false;
                
                Game1.cameraOffset = new Vector2(5000, 8000);
                Camera.zoom = 0.05f;
            }
            else
            {
                Camera.enableCameraMovement = true;
                //Game1.cameraOffset = new Vector2(0, 0);
                Camera.zoom = 1f;
            }

           
        }

        


        public void sortGameObjects() 
        {
            currentLevelGameObjects.Clear();
            foreach(GameObject gameObject in allGameObjects)
            {
                currentLevelGameObjects.Add(gameObject);
            }
            TutorialGameObjects.Clear();
            HubGameObjects.Clear();
            AntGameObjects.Clear();
            GreenHouseGameObjects.Clear();
            TreeGameObjects.Clear();
            InterLevelGameObjects.Clear();


            //for (int i = 0; i < currentLevelGameObjects.Count(); i++)
            //{
            //    if (TutorialRectangle.Contains(currentLevelGameObjects[i].gameObjectPosition))
            //        TutorialGameObjects.Add(currentLevelGameObjects[i]);
            //    if (DunghillRectangle.Contains(currentLevelGameObjects[i].gameObjectPosition))
            //        DungHillGameObjects.Add(currentLevelGameObjects[i]);
            //    if (HubRectangle.Contains(currentLevelGameObjects[i].gameObjectPosition))
            //        HubGameObjects.Add(currentLevelGameObjects[i]);
            //    if (AntToHubRectangle.Contains(currentLevelGameObjects[i].gameObjectPosition))
            //        AntGameObjects.Add(currentLevelGameObjects[i]);
            //    if (GreenhouseRectangle.Contains(currentLevelGameObjects[i].gameObjectPosition))
            //        GreenHouseGameObjects.Add(currentLevelGameObjects[i]);
            //    if (TreeRectangle.Contains(currentLevelGameObjects[i].gameObjectPosition) || CrownRectangle.Contains(currentLevelGameObjects[i].gameObjectPosition))
            //        TreeGameObjects.Add(currentLevelGameObjects[i]);
            //    if (!TutorialRectangle.Contains(currentLevelGameObjects[i].gameObjectPosition)
            //        || !DunghillRectangle.Contains(currentLevelGameObjects[i].gameObjectPosition)
            //        || !HubRectangle.Contains(currentLevelGameObjects[i].gameObjectPosition)
            //        || !AntRectangle.Contains(currentLevelGameObjects[i].gameObjectPosition)
            //        || !GreenhouseRectangle.Contains(currentLevelGameObjects[i].gameObjectPosition)
            //        || !TreeRectangle.Contains(currentLevelGameObjects[i].gameObjectPosition)
            //        || !CrownRectangle.Contains(currentLevelGameObjects[i].gameObjectPosition))
            //        InterLevelGameObjects.Add(currentLevelGameObjects[i]);
            //}




            foreach (GameObject GameObject in currentLevelGameObjects)
            {
                if (DetectCollision(GameObject, TutorialRectangle))
                    TutorialGameObjects.Add(GameObject);
                if (DetectCollision(GameObject, HubRectangle)) HubGameObjects.Add(GameObject);
                if (DetectCollision(GameObject, AntRectangle)) AntGameObjects.Add(GameObject);
                if (DetectCollision(GameObject, GreenhouseRectangle)) GreenHouseGameObjects.Add(GameObject);
                if (DetectCollision(GameObject, TreeRectangle) || DetectCollision(GameObject, CrownRectangle)) TreeGameObjects.Add(GameObject);
                if (!DetectCollision(GameObject, TutorialRectangle)
                    && !DetectCollision(GameObject, HubRectangle)
                    && !DetectCollision(GameObject, AntRectangle)
                    && !DetectCollision(GameObject, GreenhouseRectangle)
                    && !DetectCollision(GameObject, TreeRectangle)
                    && !DetectCollision(GameObject, CrownRectangle))
                    InterLevelGameObjects.Add(GameObject);
            }
        }

        public Enums.Level PlayerLevelLocation()
        {
            return currentLevel;
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

        public void drawLevelsBackground(SpriteBatch spriteBatch, Dictionary<string, Texture2D> texturesDictionary) {
            //BACKGROUND
            spriteBatch.Draw(texturesDictionary["Tut_Tile_1"], new Vector2(5120, -0), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Tut_Tile_2"], new Vector2(7168, -0), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Tut_Tile_3"], new Vector2(9216, -0), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Tut_Tile_4"], new Vector2(11264, -0), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Tut_Tile_5"], new Vector2(13312, 0), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

            //Hub
            
            spriteBatch.Draw(texturesDictionary["Hub_Background"], new Vector2(2048, 4096), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

            //Ant Cave

            
            spriteBatch.Draw(texturesDictionary["Ant_Cave_Background"], new Vector2(-8192, 4096), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Ant_Cave_Background"], new Vector2(-6144, 4096), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Ant_Cave_Background"], new Vector2(-4096, 4096), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Ant_Cave_Background"], new Vector2(-2048, 4096), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

            
            spriteBatch.Draw(texturesDictionary["Ant_Cave_Background"], new Vector2(-8192, 6144), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Ant_Cave_Background"], new Vector2(-6144, 6144), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Ant_Cave_Background"], new Vector2(-4096, 6144), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Ant_Cave_Background"], new Vector2(-2048, 6144), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

            //Greenhouse

            spriteBatch.Draw(texturesDictionary["Greenhouse_Background"], new Vector2( -2048, -4096), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Greenhouse_Background"], new Vector2(     0, -4096), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Greenhouse_Background"], new Vector2( 2048, -4096), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(texturesDictionary["Greenhouse_Background"], new Vector2(-2048, -2048), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Greenhouse_Background"], new Vector2(-0, -2048), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Greenhouse_Background"], new Vector2(2048, -2048), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(texturesDictionary["Greenhouse_Background"], new Vector2( -2048, -0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Greenhouse_Background"], new Vector2(    -0, -0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Greenhouse_Background"], new Vector2(  2048, -0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            
            //tree

            spriteBatch.Draw(texturesDictionary["Tree_Background"], new Vector2(-8192, -8192), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Tree_Background"], new Vector2(-6144, -8192), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(texturesDictionary["Tree_Background"], new Vector2(-8192, -6144), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Tree_Background"], new Vector2(-6144, -6144), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(texturesDictionary["Tree_Background"], new Vector2(-8192, -4096), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Tree_Background"], new Vector2(-6144, -4096), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(texturesDictionary["Tree_Background"], new Vector2(-8192, -2048), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Tree_Background"], new Vector2(-6144, -2048), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(texturesDictionary["Tree_Background"], new Vector2(-8192, -0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Tree_Background"], new Vector2(-6144, -0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            //Crown

            spriteBatch.Draw(texturesDictionary["Crown_Background"], new Vector2(-10240, -14336), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Crown_Background"], new Vector2(-8192, -14336), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Crown_Background"], new Vector2(-6144, -14336), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Crown_Background"], new Vector2(-4096, -14336), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(texturesDictionary["Crown_Background"], new Vector2(-10240, -12288), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Crown_Background"], new Vector2(-8192, -12288), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Crown_Background"], new Vector2(-6144, -12288), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Crown_Background"], new Vector2(-4096, -12288), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(texturesDictionary["Crown_Background"], new Vector2(-10240, -10240), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Crown_Background"], new Vector2(-8192, -10240), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Crown_Background"], new Vector2(-6144, -10240), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Crown_Background"], new Vector2(-4096, -10240), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            //Interlevel

            spriteBatch.Draw(texturesDictionary["Interlevel_1"], new Vector2(5120, 2048), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Inter_Ground"], new Vector2(0, 2048), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Interlevel_2"], new Vector2(2048,2048), null, Color.White, 0f,Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Interlevel_3"], new Vector2(4096, 2048), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texturesDictionary["Interlevel_4"], new Vector2(4096, 4096), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(texturesDictionary["Interlevel_5"], new Vector2(0, 4096), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
