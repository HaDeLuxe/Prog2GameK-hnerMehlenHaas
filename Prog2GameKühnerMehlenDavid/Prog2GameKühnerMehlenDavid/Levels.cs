using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class Levels {

        Rectangle TutorialRectangle = new Rectangle(5120, 0, 10240, 2048);
        Rectangle DunghillRectangle = new Rectangle(0, -2048, 4096, 4096);
        Rectangle HubRectangle = new Rectangle(-2048, 4096, 6144, 2048);
        Rectangle AntRectangle = new Rectangle(-12288, 4096, 8192, 4096);
        Rectangle GreenhouseRectangle = new Rectangle(-7168, -8192, 6144, 10240);
        Rectangle TreeRectangle = new Rectangle(-13312, -8192, 4096, 10240);
        Rectangle CrownRectangle = new Rectangle(-15360, -14336, 8192, 6144);
        Rectangle GazeboRectangle = new Rectangle(-14000, -6200, 600, 200);
        Rectangle TutHubBorderRectangle = new Rectangle(5800, 2000, 400, 1700);
        Rectangle HubToDungRectangle = new Rectangle(3300, 2600, 200, 700);
        Rectangle DungToHubRectangle = new Rectangle(3300, 3200, 200, 600);
        Rectangle DungToGreenRectangle = new Rectangle(-1000, -1500, 500, 3500);
        Rectangle GreenToDungRectangle = new Rectangle(-500, -1500, 500, 3500);
        Rectangle GreenToTreeRectangle = new Rectangle(-9200, -7400, 1000, 400);
        Rectangle TreeToGreenRectangle = new Rectangle(-8200, -7400, 1000, 400);


        List<GameObject> TutorialGameObjects;
        List<GameObject> DungHillGameObjects;
        List<GameObject> GreenHouseGameObjects;
        List<GameObject> AntGameObjects;
        List<GameObject> TreeGameObjects;
        List<GameObject> HubGameObjects;
        List<GameObject> InterLevelGameObjects;

        bool TutToHub = false;
        bool HubToDung = false;
        bool DungToHub = false;
        bool DungToGreen = false;
        bool GreenToDung = false;
        bool GreenToTree = false;
        bool TreeToGreen = false;

        Enums.Level currentLevel = Enums.Level.TUTORIAL;

        public Levels() {
            TutorialGameObjects = new List<GameObject>();
            DungHillGameObjects = new List<GameObject>();
            GreenHouseGameObjects = new List<GameObject>();
            AntGameObjects = new List<GameObject>();
            TreeGameObjects = new List<GameObject>();
            HubGameObjects = new List<GameObject>();
            InterLevelGameObjects = new List<GameObject>();
        }

        public void ManageLevels(Vector2 PlayerPos, ref List<GameObject> currentLevelGameObjects)
        {
            if (TutorialRectangle.Contains(PlayerPos)) currentLevel = Enums.Level.TUTORIAL;
            if (DunghillRectangle.Contains(PlayerPos)) currentLevel = Enums.Level.DUNG;
            if (GreenhouseRectangle.Contains(PlayerPos)) currentLevel = Enums.Level.GREENHOUSE;
            if (HubRectangle.Contains(PlayerPos)) currentLevel = Enums.Level.HUB;
            if (AntRectangle.Contains(PlayerPos)) currentLevel = Enums.Level.ANTCAVE;
            if (TreeRectangle.Contains(PlayerPos) || CrownRectangle.Contains(PlayerPos)) currentLevel = Enums.Level.TREE;


            if (TutHubBorderRectangle.Contains(PlayerPos) && !TutToHub)
            {
                currentLevelGameObjects = HubGameObjects;
                foreach (GameObject GameObject in InterLevelGameObjects) currentLevelGameObjects.Add(GameObject);
                TutToHub = true;
            }


            if (HubToDungRectangle.Contains(PlayerPos) && !DungToHubRectangle.Contains(PlayerPos) && !HubToDung)
            {
                currentLevelGameObjects = DungHillGameObjects;
                foreach (GameObject GameObject in InterLevelGameObjects) currentLevelGameObjects.Add(GameObject);
                HubToDung = true;
                DungToHub = false;

            }


            if (DungToHubRectangle.Contains(PlayerPos) && !HubToDungRectangle.Contains(PlayerPos) && !DungToHub)
            {
                currentLevelGameObjects = HubGameObjects;
                foreach (GameObject GameObject in InterLevelGameObjects) currentLevelGameObjects.Add(GameObject);
                DungToHub = true;
                HubToDung = false;
            }

            if (DungToGreenRectangle.Contains(PlayerPos) && !DungToGreen)
            {
                currentLevelGameObjects = GreenHouseGameObjects;
                foreach (GameObject GameObject in InterLevelGameObjects) currentLevelGameObjects.Add(GameObject);
                DungToGreen = true;
                GreenToDung = false;
            }
            if (GreenToDungRectangle.Contains(PlayerPos) && !GreenToDung)
            {
                currentLevelGameObjects = DungHillGameObjects;
                foreach (GameObject GameObject in InterLevelGameObjects) currentLevelGameObjects.Add(GameObject);
                GreenToDung = true;
                DungToGreen = false;
            }

            if(GreenToTreeRectangle.Contains(PlayerPos) && !GreenToTree)
            {
                currentLevelGameObjects = TreeGameObjects;
                foreach (GameObject GameObject in InterLevelGameObjects) currentLevelGameObjects.Add(GameObject);
                GreenToTree = true;
                TreeToGreen = false;
            }

            if(TreeToGreenRectangle.Contains(PlayerPos) && !TreeToGreen)
            {
                currentLevelGameObjects = GreenHouseGameObjects;
                foreach (GameObject GameObject in InterLevelGameObjects) currentLevelGameObjects.Add(GameObject);
                TreeToGreen = true;
                GreenToTree = false;
            }

            bool once = false;

            if (GazeboRectangle.Contains(PlayerPos))
            {
                
                Camera.zoom = 0.05f;

                Game1.cameraOffset = new Vector2(-100000, 10000);
                if (!once)
                {
                    //Game1.cameraOffset = new Vector2(-60000,15000);
                    Game1.cameraOffset.X += -10000;
                    once = true;
                }
            }

            switch (currentLevel)
            {
                case Enums.Level.HUB:
                    Console.WriteLine("Player is in hub");
                    break;
            }
        }

        


        public void sortGameObjects(List<GameObject> allGameObjects) 
        {
            foreach(GameObject GameObject in allGameObjects)
            {
                if (DetectCollision(GameObject, TutorialRectangle))
                    TutorialGameObjects.Add(GameObject);
                if (DetectCollision(GameObject, DunghillRectangle)) DungHillGameObjects.Add(GameObject);
                if (DetectCollision(GameObject, HubRectangle)) HubGameObjects.Add(GameObject);
                if (DetectCollision(GameObject, AntRectangle)) AntGameObjects.Add(GameObject);
                if (DetectCollision(GameObject, GreenhouseRectangle)) GreenHouseGameObjects.Add(GameObject);
                if (DetectCollision(GameObject, TreeRectangle) || DetectCollision(GameObject, CrownRectangle)) TreeGameObjects.Add(GameObject);
                if (!DetectCollision(GameObject, TutorialRectangle)
                    && !DetectCollision(GameObject, DunghillRectangle)
                    && !DetectCollision(GameObject, HubRectangle)
                    && !DetectCollision(GameObject, AntRectangle)
                    && !DetectCollision(GameObject, GreenhouseRectangle)
                    && !DetectCollision(GameObject, TreeRectangle)
                    && !DetectCollision(GameObject, CrownRectangle))
                    InterLevelGameObjects.Add(GameObject);
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

        public void drawLevelsBackground(SpriteBatch spriteBatch, Texture2D background, Texture2D Tutorial_Background, Texture2D Hub_Background, Texture2D Dunghill_Background, Texture2D Ant_Cave_Background, Texture2D Greenhouse_Background, Texture2D Tree_Background, Texture2D Crown_Background, Texture2D Roof_Background) {
            //BACKGROUND
            spriteBatch.Draw(Tutorial_Background, new Vector2(5120, -0), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Tutorial_Background, new Vector2(7168, -0), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Tutorial_Background, new Vector2(9216, -0), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Tutorial_Background, new Vector2(11264, -0), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Tutorial_Background, new Vector2(13312, 0), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

            //Hub

            spriteBatch.Draw(Hub_Background, new Vector2(-2048, 4096), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Hub_Background, new Vector2(0, 4096), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Hub_Background, new Vector2(2048, 4096), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

            //DungHill

            spriteBatch.Draw(Dunghill_Background, new Vector2(0, -2048), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Dunghill_Background, new Vector2(2048, -2048), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Dunghill_Background, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Dunghill_Background, new Vector2(2048, 0), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

            //Ant Cave

            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-12288, 4096), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-10240, 4096), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-8192, 4096), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-6144, 4096), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-12288, 6144), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-10240, 6144), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-8192, 6144), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Ant_Cave_Background, new Vector2(-6144, 6144), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

            //Greenhouse

            spriteBatch.Draw(Greenhouse_Background, new Vector2(-7168, -4096), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Greenhouse_Background, new Vector2(-5120, -4096), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Greenhouse_Background, new Vector2(-3072, -4096), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Greenhouse_Background, new Vector2(-7168, -2048), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Greenhouse_Background, new Vector2(-5120, -2048), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Greenhouse_Background, new Vector2(-3072, -2048), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Greenhouse_Background, new Vector2(-7168, -0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Greenhouse_Background, new Vector2(-5120, -0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Greenhouse_Background, new Vector2(-3072, -0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            //Roof

            spriteBatch.Draw(Roof_Background, new Vector2(-7168, -8192), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Roof_Background, new Vector2(-5120, -8192), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Roof_Background, new Vector2(-3072, -8192), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Roof_Background, new Vector2(-7168, -6144), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Roof_Background, new Vector2(-5120, -6144), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Roof_Background, new Vector2(-3072, -6144), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            //tree

            spriteBatch.Draw(Tree_Background, new Vector2(-13312, -8192), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Tree_Background, new Vector2(-11264, -8192), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Tree_Background, new Vector2(-13312, -6144), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Tree_Background, new Vector2(-11264, -6144), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Tree_Background, new Vector2(-13312, -4096), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Tree_Background, new Vector2(-11264, -4096), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Tree_Background, new Vector2(-13312, -2048), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Tree_Background, new Vector2(-11264, -2048), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Tree_Background, new Vector2(-13312, -0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Tree_Background, new Vector2(-11264, -0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            //Crown

            spriteBatch.Draw(Crown_Background, new Vector2(-15360, -14336), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Crown_Background, new Vector2(-13312, -14336), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Crown_Background, new Vector2(-11264, -14336), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Crown_Background, new Vector2(-9216, -14336), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Crown_Background, new Vector2(-15360, -12288), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Crown_Background, new Vector2(-13312, -12288), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Crown_Background, new Vector2(-11264, -12288), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Crown_Background, new Vector2(-9216, -12288), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Crown_Background, new Vector2(-15360, -10240), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Crown_Background, new Vector2(-13312, -10240), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Crown_Background, new Vector2(-11264, -10240), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Crown_Background, new Vector2(-9216, -10240), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
