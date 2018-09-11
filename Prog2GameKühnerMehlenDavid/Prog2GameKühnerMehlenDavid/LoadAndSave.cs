using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media; 
using Microsoft.Xna.Framework.Audio;
using System.Linq;
using System.IO;

namespace Reggie
{
    class LoadAndSave {

        private List<GameObject> gameObjectList;
        private Dictionary<string, Texture2D> platformTextures;

        public LoadAndSave(List<GameObject> gameObjectList, Dictionary<string, Texture2D> platformTextures)
        {
            this.gameObjectList = gameObjectList;
            this.platformTextures = platformTextures;
        }

        public void loadEverything(ContentManager Content, ref Dictionary<string, Texture2D> PlayerSpriteSheets, ref Dictionary<string, Texture2D> texturesDictionary, ref Dictionary<string, Texture2D> EnemySpriteSheets, ref Dictionary<string, Song> songDictionary, ref Dictionary<string, SoundEffect> soundEffectDictionnary) {
            loadPlayerSprites(Content, ref PlayerSpriteSheets);
            loadWorldSprites(Content, ref texturesDictionary);
            loadEnemySprites(Content, ref EnemySpriteSheets);
            loadItemSprites(Content, ref texturesDictionary);
            loadWorldObjects(Content, ref texturesDictionary);
            loadUIElements(Content, ref texturesDictionary);
            loadInteractivePlatforms(Content, ref texturesDictionary);

            //Markus
            loadSongs(Content, ref songDictionary);
        }

        /// <summary>
        /// This method loads all world background images and adds them to the texturesDictionnary
        /// </summary>
        /// <param name="Content"></param>
        private void loadWorldSprites(ContentManager Content, ref Dictionary<string, Texture2D> texturesDictionnary) {
            //Load Tutorial Tiles
            Texture2D Tut_Tile_1 = Content.Load<Texture2D>("Images\\World\\Tut\\Tut_Tile_1");
            texturesDictionnary.Add("Tut_Tile_1", Tut_Tile_1);
            Texture2D Tut_Tile_2 = Content.Load<Texture2D>("Images\\World\\Tut\\Tut_Tile_2");
            texturesDictionnary.Add("Tut_Tile_2", Tut_Tile_2);
            Texture2D Tut_Tile_3 = Content.Load<Texture2D>("Images\\World\\Tut\\Tut_Tile_3");
            texturesDictionnary.Add("Tut_Tile_3", Tut_Tile_3);
            Texture2D Tut_Tile_4 = Content.Load<Texture2D>("Images\\World\\Tut\\Tut_Tile_4");
            texturesDictionnary.Add("Tut_Tile_4", Tut_Tile_4);
            Texture2D Tut_Tile_5 = Content.Load<Texture2D>("Images\\World\\Tut\\Tut_Tile_5");
            texturesDictionnary.Add("Tut_Tile_5", Tut_Tile_5);


            Texture2D Ant_Cave_Background = Content.Load<Texture2D>("Images\\World\\Ameisenhöhle");
            texturesDictionnary.Add("Ant_Cave_Background", Ant_Cave_Background);
            Texture2D Tree_Background = Content.Load<Texture2D>("Images\\World\\Baum");
            texturesDictionnary.Add("Tree_Background", Tree_Background);
            Texture2D Roof_Background = Content.Load<Texture2D>("Images\\World\\Dach");
            texturesDictionnary.Add("Roof_Background", Roof_Background);
            Texture2D Greenhouse_Background = Content.Load<Texture2D>("Images\\World\\Gewächshaus");
            texturesDictionnary.Add("Greenhouse_Background", Greenhouse_Background);
            Texture2D Hub_Background = Content.Load<Texture2D>("Images\\World\\Hub");
            texturesDictionnary.Add("Hub_Background", Hub_Background);
            Texture2D Crown_Background = Content.Load<Texture2D>("Images\\World\\krone");
            texturesDictionnary.Add("Crown_Background", Crown_Background);
            Texture2D Dunghill_Background = Content.Load<Texture2D>("Images\\World\\Misthaufen");
            texturesDictionnary.Add("Dunghill_Background", Dunghill_Background);

            Texture2D InterLevel_1 = Content.Load<Texture2D>("Images\\World\\Inter\\Interlevel_1");
            texturesDictionnary.Add("Interlevel_1", InterLevel_1);
        }


        /// <summary>
        /// This method loads all Player Sprites and adds them to the PlayerSpriteSheets Dictionnary
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="PlayerSpriteSheets"></param>
        private void loadPlayerSprites(ContentManager Content, ref Dictionary<string, Texture2D> PlayerSpriteSheets) {
            Texture2D playerJumpSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Jump_Small");
            PlayerSpriteSheets.Add("playerJumpSpriteSheet", playerJumpSpriteSheet);
            Texture2D playerJumpHatSpritesSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Jump_Hat");
            PlayerSpriteSheets.Add("playerJumpHatSpriteSheet", playerJumpHatSpritesSheet);
            Texture2D playerJumpArmorSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Jump_Armor");
            PlayerSpriteSheets.Add("playerJumpArmorSpriteSheet", playerJumpArmorSpriteSheet);
            Texture2D playerJumpArmorHatSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Jump_Armor_Hat");
            PlayerSpriteSheets.Add("playerJumpArmorHatSpriteSheet", playerJumpArmorHatSpriteSheet);
            Texture2D playerJumpUmbrellaEmptySpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Jump_Umbrella_Empty");
            PlayerSpriteSheets.Add("playerJumpUmbrellaEmptySpriteSheet", playerJumpUmbrellaEmptySpriteSheet);

            Texture2D playerMoveSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Move_Even_Smaller");
            PlayerSpriteSheets.Add("playerMoveSpriteSheet", playerMoveSpriteSheet);
            Texture2D playerMoveHatSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Move_Hat");
            PlayerSpriteSheets.Add("playerMoveHatSpriteSheet", playerMoveHatSpriteSheet);
            Texture2D playerMoveArmorSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Move_Armor");
            PlayerSpriteSheets.Add("playerMoveArmorSpriteSheet", playerMoveArmorSpriteSheet);
            Texture2D playerMoveArmorHatSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Move_Armor_Hat");
            PlayerSpriteSheets.Add("playerMoveArmorHatSpriteSheet", playerMoveArmorHatSpriteSheet);
            Texture2D playerMoveUmbrellaEmptySpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Move_Umbrella_Empty");
            PlayerSpriteSheets.Add("playerMoveUmbrellaEmptySpriteSheet", playerMoveUmbrellaEmptySpriteSheet);

            Texture2D playerAttackSpritesheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Attack");
            PlayerSpriteSheets.Add("playerAttackSpriteSheet", playerAttackSpritesheet);
            Texture2D playerAttackHatSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Attack_Hat");
            PlayerSpriteSheets.Add("playerAttackHatSpriteSheet", playerAttackHatSpriteSheet);
            Texture2D playerAttackArmorSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Attack_Armor");
            PlayerSpriteSheets.Add("playerAttackArmorSpriteSheet", playerAttackArmorSpriteSheet);
            Texture2D playerAttackArmorHatSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Attack_Armor_Hat");
            PlayerSpriteSheets.Add("playerAttackArmorHatSpritesheet", playerAttackArmorHatSpriteSheet);
            Texture2D playerAttackUmbrellaEmptySpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Attack_Umbrella");
            PlayerSpriteSheets.Add("playerAttackUmbrellaEmptySpriteSheet", playerAttackUmbrellaEmptySpriteSheet);

            Texture2D playerJumpShovelSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Jump_Shovel");
            PlayerSpriteSheets.Add("playerJumpShovelSpriteSheet", playerJumpShovelSpriteSheet);
            Texture2D playerWalkShovelSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Move_Shovel");
            PlayerSpriteSheets.Add("playerWalkShovelSpriteSheet", playerWalkShovelSpriteSheet);
            Texture2D playerAttackShovelSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Attack_Shovel");
            PlayerSpriteSheets.Add("playerAttackShovelSpriteSheet", playerAttackShovelSpriteSheet);
            Texture2D playerFloatShovelSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Float_Shovel");
            PlayerSpriteSheets.Add("playerFloatShovelSpriteSheet", playerFloatShovelSpriteSheet);

            Texture2D playerJumpScissorsSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Jump_Scirssors");
            PlayerSpriteSheets.Add("playerJumpScissorsSpriteSheet", playerJumpScissorsSpriteSheet);
            Texture2D playerWalkScissorsSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Move_Scissors");
            PlayerSpriteSheets.Add("playerWalkScissorsSpriteSheet", playerWalkScissorsSpriteSheet);
            Texture2D playerAttackScissorsSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Attack_Scissors");
            PlayerSpriteSheets.Add("playerAttackScissorsSpriteSheet", playerAttackScissorsSpriteSheet);
            Texture2D playerFloatScissorsSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Float_Scissors");
            PlayerSpriteSheets.Add("playerFloatScissorsSpriteSheet", playerFloatScissorsSpriteSheet);

            Texture2D playerJumpGoldenSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Jump_Umbrella_Golden");
            PlayerSpriteSheets.Add("playerJumpGoldenSpriteSheet", playerJumpGoldenSpriteSheet);
            Texture2D playerWalkGoldenSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Move_Umbrella_Golden");
            PlayerSpriteSheets.Add("playerWalkGoldenSpriteSheet", playerWalkGoldenSpriteSheet);
            Texture2D playerAttackGoldenSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Attack_Umbrella_Golden");
            PlayerSpriteSheets.Add("playerAttackGoldenSpriteSheet", playerAttackGoldenSpriteSheet);
            Texture2D playerFloatGoldenSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Float_Golden");
            PlayerSpriteSheets.Add("playerFloatGoldenSpriteSheet", playerFloatGoldenSpriteSheet);

            Texture2D playerFloatingSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Float_Umbrella");
            PlayerSpriteSheets.Add("playerFloatSpriteSheet", playerFloatingSpriteSheet);

            
        }

        /// <summary>
        /// This method loads all enemy sprite sheets and adds them to the EnemySpriteSheetsDictionnary
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="EnemySpriteSheets"></param>
        private void loadEnemySprites(ContentManager Content, ref Dictionary<string, Texture2D> EnemySpriteSheets) {
            Texture2D Ladybug_Fly = Content.Load<Texture2D>("Images\\Enemies Sprite Sheets\\ladybug_floating_Left_Small");
            EnemySpriteSheets.Add("Ladybug_Fly_Spritesheet", Ladybug_Fly);
            Texture2D Ladybug_Attack = Content.Load<Texture2D>("Images\\Enemies Sprite Sheets\\Ladybug_Attack_Small");
            EnemySpriteSheets.Add("Ladybug_Attack_Spritesheet", Ladybug_Attack);
        }


        /// <summary>
        /// This method laods all item sprite sheets and adds them to the texturesDictionnary
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="texturesDictionary"></param>
        private void loadItemSprites(ContentManager Content, ref Dictionary<string, Texture2D> texturesDictionary)
        {
            Texture2D snailShell = Content.Load<Texture2D>("Images\\Schneckenhaus");
            texturesDictionary.Add("SnailShell", snailShell);
            Texture2D scissors = Content.Load<Texture2D>("Images\\Schere");
            texturesDictionary.Add("Scissors_64x64", scissors);
            Texture2D armor = Content.Load<Texture2D>("Images\\Rüstung");
            texturesDictionary.Add("Armor_64x64", armor);
            Texture2D shovel = Content.Load<Texture2D>("Images\\Schaufel");
            texturesDictionary.Add("Shovel_64x64", shovel);
            Texture2D healthItem = Content.Load<Texture2D>("Images\\HealthPotion");
            texturesDictionary.Add("HealthItem", healthItem);
            Texture2D jumpPotion = Content.Load<Texture2D>("Images\\Items\\JumpPotion");
            texturesDictionary.Add("JumpPotion", jumpPotion);
            Texture2D strengthPotion = Content.Load<Texture2D>("Images\\Items\\PowerPotion");
            texturesDictionary.Add("PowerPotion", strengthPotion);
            Texture2D goldenUmbrella = Content.Load<Texture2D>("Images\\GoldenUmbrella");
            texturesDictionary.Add("GoldenUmbrella", goldenUmbrella);
            Texture2D apple = Content.Load<Texture2D>("Images\\Items\\Apple");
            texturesDictionary.Add("Apple", apple);
        }

        /// <summary>
        /// This method loads all interactive game objects sprites and adds them to the texturesDictionnary
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="texturesDictionary"></param>
        private void loadInteractivePlatforms(ContentManager Content, ref Dictionary<string, Texture2D> texturesDictionary)
        {
            Texture2D vineDoor = Content.Load<Texture2D>("Images\\WorldObjects\\VineDOORBLOCK_Reggie");
            texturesDictionary.Add("VineDoor", vineDoor);
            Texture2D spiderWeb = Content.Load<Texture2D>("Images\\WorldObjects\\SpiderWeb");
            texturesDictionary.Add("Spiderweb_64x64", spiderWeb);
        }

        /// <summary>
        /// This method loads all World Objects sprites and adds them to the texturesDictionnary
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="texturesDictionnary"></param>
        private void loadWorldObjects(ContentManager Content, ref Dictionary<string, Texture2D> texturesDictionnary) {
            Texture2D Transparent_Wall_512x64 = Content.Load<Texture2D>("Images\\Transparent_Wall_512x64");
            texturesDictionnary.Add("Transparent_500x50", Transparent_Wall_512x64);
            Texture2D Transparent_Wall_64x64 = Content.Load<Texture2D>("Images\\WorldObjects\\Transparent - 64x64");
            texturesDictionnary.Add("Transparent_64x64", Transparent_Wall_64x64);
            Texture2D Transparent_Wall_1024x64 = Content.Load<Texture2D>("Images\\Transparent_Wall_1024x64");
            texturesDictionnary.Add("Transparent_1000x50", Transparent_Wall_1024x64);
            Texture2D ClimbinPlant_38_64 = Content.Load<Texture2D>("Images\\WorldObjects\\plantLeaves_1");
            texturesDictionnary.Add("Climbingplant_38x64", ClimbinPlant_38_64);
        }


        /// <summary>
        /// This method loads all UI Elements ands adds them to the texturesDictionnary
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="texturesDictionnary"></param>
        private void loadUIElements(ContentManager Content, ref Dictionary<string, Texture2D> texturesDictionnary) {
            Texture2D levelEditorUIBackButton = Content.Load<Texture2D>("Images\\UI\\LvlEdtorSaveButton");
            texturesDictionnary.Add("LevelEditorUIBackButton", levelEditorUIBackButton);
            Texture2D UserInterface = Content.Load<Texture2D>("Images\\UI\\UI");
            texturesDictionnary.Add("UI", UserInterface);
            Texture2D playerHealthbar = Content.Load<Texture2D>("Images\\UI\\Healthbar");
            texturesDictionnary.Add("Healthbar", playerHealthbar);
            Texture2D L1ButtonIcon = Content.Load<Texture2D>("Images\\UI\\buttonL1");
            texturesDictionnary.Add("buttonL1", L1ButtonIcon);
            Texture2D L2ButtonIcon = Content.Load<Texture2D>("Images\\UI\\buttonL2");
            texturesDictionnary.Add("buttonL2", L2ButtonIcon);
            Texture2D R1ButtonIcon = Content.Load<Texture2D>("Images\\UI\\buttonR1");
            texturesDictionnary.Add("buttonR1", R1ButtonIcon);
            Texture2D R2ButtonIcon = Content.Load<Texture2D>("Images\\UI\\buttonR2");
            texturesDictionnary.Add("buttonR2", R2ButtonIcon);

            //Minimap loading thingis
            Texture2D Minimap = Content.Load<Texture2D>("Images\\Minimap\\MiniMap");
            texturesDictionnary.Add("Minimap", Minimap);
            Texture2D Point = Content.Load<Texture2D>("Images\\Minimap\\Point");
            texturesDictionnary.Add("Point", Point);

            //LoadMenuPic
            Texture2D MainMenu1 = Content.Load<Texture2D>("Images\\MainMenu\\HauptMenu-1");
            texturesDictionnary.Add("MainMenu1", MainMenu1);
            Texture2D MainMenu2 = Content.Load<Texture2D>("Images\\MainMenu\\HauptMenu-2");
            texturesDictionnary.Add("MainMenu2", MainMenu2);
            Texture2D MainMenu3 = Content.Load<Texture2D>("Images\\MainMenu\\HauptMenu-3");
            texturesDictionnary.Add("MainMenu3", MainMenu3);
            Texture2D MainMenu4 = Content.Load<Texture2D>("Images\\MainMenu\\HauptMenu-4");
            texturesDictionnary.Add("MainMenu4", MainMenu4);
            Texture2D MainMenu5 = Content.Load<Texture2D>("Images\\MainMenu\\HauptMenu-5");
            texturesDictionnary.Add("MainMenu5", MainMenu5);
        }


        private void loadSongs(ContentManager Content, ref Dictionary<string, Song> songDictionnary)
        {
            Song temp = Content.Load<Song>("Audio\\houseChord");
            songDictionnary.Add("houseChord", temp);

            //MediaPlayer.Play(testSong);
            //MediaPlayer.IsRepeating = true;
        }

        private void loadSoundEffects(ContentManager Content, ref Dictionary<string, SoundEffect> soundEffectDictionnary)
        {
            SoundEffect temp = Content.Load<SoundEffect>("Audio\\houseChord");
            soundEffectDictionnary.Add("houseChord", temp);
        }

        public void SavePlatforms()
        {
            List<string> outputList = new List<string>();
            outputList.RemoveRange(0, outputList.Count());


            foreach (GameObject GameObject in gameObjectList)
            {
                string Output = "";

                //if (platform.PlatformType >= 8 && platform.PlatformType <= 115)
                //{
                //    Output = "" + platform.PlatformType;
                //}
                //if (platform.PlatformType >= (int)Enums.ObjectsID.tileBrown_01 && platform.PlatformType <= (int)Enums.ObjectsID.tileGreen_27) Output = "" + platform.PlatformType;

                if (GameObject.getTexture() == platformTextures["Transparent_500x50"]) Output = Enums.ObjectsID.INVISIBLE_WALL_500x50.ToString();
                if (GameObject.getTexture() == platformTextures["Transparent_1000x50"]) Output = Enums.ObjectsID.INVSIBLE_WALL_1000x50.ToString();
                if (GameObject.getTexture() == platformTextures["Transparent_64x64"]) Output = Enums.ObjectsID.INVISIBLE_WALL_64x64.ToString();
                if (GameObject.getTexture() == platformTextures["Climbingplant_38x64"]) Output = Enums.ObjectsID.VINE.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_01"]) Output = Enums.ObjectsID.tileBrown_01.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_02"]) Output = Enums.ObjectsID.tileBrown_02.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_03"]) Output = Enums.ObjectsID.tileBrown_03.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_04"]) Output = Enums.ObjectsID.tileBrown_04.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_05"]) Output = Enums.ObjectsID.tileBrown_05.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_06"]) Output = Enums.ObjectsID.tileBrown_06.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_07"]) Output = Enums.ObjectsID.tileBrown_07.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_08"]) Output = Enums.ObjectsID.tileBrown_08.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_09"]) Output = Enums.ObjectsID.tileBrown_09.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_10"]) Output = Enums.ObjectsID.tileBrown_10.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_11"]) Output = Enums.ObjectsID.tileBrown_11.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_12"]) Output = Enums.ObjectsID.tileBrown_12.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_13"]) Output = Enums.ObjectsID.tileBrown_13.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_14"]) Output = Enums.ObjectsID.tileBrown_14.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_15"]) Output = Enums.ObjectsID.tileBrown_15.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_16"]) Output = Enums.ObjectsID.tileBrown_16.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_17"]) Output = Enums.ObjectsID.tileBrown_17.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_18"]) Output = Enums.ObjectsID.tileBrown_18.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_19"]) Output = Enums.ObjectsID.tileBrown_19.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_20"]) Output = Enums.ObjectsID.tileBrown_20.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_21"]) Output = Enums.ObjectsID.tileBrown_21.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_22"]) Output = Enums.ObjectsID.tileBrown_22.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_23"]) Output = Enums.ObjectsID.tileBrown_23.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_24"]) Output = Enums.ObjectsID.tileBrown_24.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_25"]) Output = Enums.ObjectsID.tileBrown_25.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_26"]) Output = Enums.ObjectsID.tileBrown_26.ToString();
                if (GameObject.getTexture() == platformTextures["tileBrown_27"]) Output = Enums.ObjectsID.tileBrown_27.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_01"]) Output = Enums.ObjectsID.tileYellow_01.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_02"]) Output = Enums.ObjectsID.tileYellow_02.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_03"]) Output = Enums.ObjectsID.tileYellow_03.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_04"]) Output = Enums.ObjectsID.tileYellow_04.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_05"]) Output = Enums.ObjectsID.tileYellow_05.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_06"]) Output = Enums.ObjectsID.tileYellow_06.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_07"]) Output = Enums.ObjectsID.tileYellow_07.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_08"]) Output = Enums.ObjectsID.tileYellow_08.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_09"]) Output = Enums.ObjectsID.tileYellow_09.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_10"]) Output = Enums.ObjectsID.tileYellow_10.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_11"]) Output = Enums.ObjectsID.tileYellow_11.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_12"]) Output = Enums.ObjectsID.tileYellow_12.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_13"]) Output = Enums.ObjectsID.tileYellow_13.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_14"]) Output = Enums.ObjectsID.tileYellow_14.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_15"]) Output = Enums.ObjectsID.tileYellow_15.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_16"]) Output = Enums.ObjectsID.tileYellow_16.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_17"]) Output = Enums.ObjectsID.tileYellow_17.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_18"]) Output = Enums.ObjectsID.tileYellow_18.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_19"]) Output = Enums.ObjectsID.tileYellow_19.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_20"]) Output = Enums.ObjectsID.tileYellow_20.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_21"]) Output = Enums.ObjectsID.tileYellow_21.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_22"]) Output = Enums.ObjectsID.tileYellow_22.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_23"]) Output = Enums.ObjectsID.tileYellow_23.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_24"]) Output = Enums.ObjectsID.tileYellow_24.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_25"]) Output = Enums.ObjectsID.tileYellow_25.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_26"]) Output = Enums.ObjectsID.tileYellow_26.ToString();
                if (GameObject.getTexture() == platformTextures["tileYellow_27"]) Output = Enums.ObjectsID.tileYellow_27.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_01"]) Output = Enums.ObjectsID.tileBlue_01.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_02"]) Output = Enums.ObjectsID.tileBlue_02.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_03"]) Output = Enums.ObjectsID.tileBlue_03.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_04"]) Output = Enums.ObjectsID.tileBlue_04.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_05"]) Output = Enums.ObjectsID.tileBlue_05.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_06"]) Output = Enums.ObjectsID.tileBlue_06.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_07"]) Output = Enums.ObjectsID.tileBlue_07.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_08"]) Output = Enums.ObjectsID.tileBlue_08.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_09"]) Output = Enums.ObjectsID.tileBlue_09.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_10"]) Output = Enums.ObjectsID.tileBlue_10.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_11"]) Output = Enums.ObjectsID.tileBlue_11.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_12"]) Output = Enums.ObjectsID.tileBlue_12.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_13"]) Output = Enums.ObjectsID.tileBlue_13.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_14"]) Output = Enums.ObjectsID.tileBlue_14.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_15"]) Output = Enums.ObjectsID.tileBlue_15.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_16"]) Output = Enums.ObjectsID.tileBlue_16.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_17"]) Output = Enums.ObjectsID.tileBlue_17.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_18"]) Output = Enums.ObjectsID.tileBlue_18.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_19"]) Output = Enums.ObjectsID.tileBlue_19.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_20"]) Output = Enums.ObjectsID.tileBlue_20.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_21"]) Output = Enums.ObjectsID.tileBlue_21.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_22"]) Output = Enums.ObjectsID.tileBlue_22.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_23"]) Output = Enums.ObjectsID.tileBlue_23.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_24"]) Output = Enums.ObjectsID.tileBlue_24.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_25"]) Output = Enums.ObjectsID.tileBlue_25.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_26"]) Output = Enums.ObjectsID.tileBlue_26.ToString();
                if (GameObject.getTexture() == platformTextures["tileBlue_27"]) Output = Enums.ObjectsID.tileBlue_27.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_01"]) Output = Enums.ObjectsID.tileGreen_01.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_02"]) Output = Enums.ObjectsID.tileGreen_02.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_03"]) Output = Enums.ObjectsID.tileGreen_03.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_04"]) Output = Enums.ObjectsID.tileGreen_04.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_05"]) Output = Enums.ObjectsID.tileGreen_05.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_06"]) Output = Enums.ObjectsID.tileGreen_06.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_07"]) Output = Enums.ObjectsID.tileGreen_07.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_08"]) Output = Enums.ObjectsID.tileGreen_08.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_09"]) Output = Enums.ObjectsID.tileGreen_09.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_10"]) Output = Enums.ObjectsID.tileGreen_10.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_11"]) Output = Enums.ObjectsID.tileGreen_11.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_12"]) Output = Enums.ObjectsID.tileGreen_12.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_13"]) Output = Enums.ObjectsID.tileGreen_13.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_14"]) Output = Enums.ObjectsID.tileGreen_14.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_15"]) Output = Enums.ObjectsID.tileGreen_15.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_16"]) Output = Enums.ObjectsID.tileGreen_16.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_17"]) Output = Enums.ObjectsID.tileGreen_17.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_18"]) Output = Enums.ObjectsID.tileGreen_18.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_19"]) Output = Enums.ObjectsID.tileGreen_19.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_20"]) Output = Enums.ObjectsID.tileGreen_20.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_21"]) Output = Enums.ObjectsID.tileGreen_21.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_22"]) Output = Enums.ObjectsID.tileGreen_22.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_23"]) Output = Enums.ObjectsID.tileGreen_23.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_24"]) Output = Enums.ObjectsID.tileGreen_24.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_25"]) Output = Enums.ObjectsID.tileGreen_25.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_26"]) Output = Enums.ObjectsID.tileGreen_26.ToString();
                if (GameObject.getTexture() == platformTextures["tileGreen_27"]) Output = Enums.ObjectsID.tileGreen_27.ToString();
                if (GameObject.getTexture() == platformTextures["Apple"]) Output = Enums.ObjectsID.APPLE.ToString();


                Output += "," + GameObject.gameObjectPosition.X + "," + GameObject.gameObjectPosition.Y;

                outputList.Add(Output);
            }

            string OutputLine = "Playerposition, " + Game1.wormPlayer.gameObjectPosition.X + "," + Game1.wormPlayer.gameObjectPosition.Y;
            outputList.Add(OutputLine);
            OutputLine = "Armor," + ItemUIManager.armorPickedUp;
            outputList.Add(OutputLine);
            OutputLine = "Helmet, " + ItemUIManager.snailShellPickedUp;
            outputList.Add(OutputLine);
            OutputLine = "HealthPotion," + ItemUIManager.healthPickedUp;
            outputList.Add(OutputLine);
            OutputLine = "JumpPotion, " + ItemUIManager.jumpPickedUp;
            outputList.Add(OutputLine);
            OutputLine = "PowerPotion, " + ItemUIManager.powerPickedUp;
            outputList.Add(OutputLine);
            OutputLine = "Scissors, " + ItemUIManager.scissorsPickedUp;
            outputList.Add(OutputLine);
            OutputLine = "Shovel," + ItemUIManager.shovelPickedUp;
            outputList.Add(OutputLine);
            OutputLine = "GoldenUmbrella," + ItemUIManager.goldenUmbrellaPickedUp;
            outputList.Add(OutputLine);
            



            using (var stream = new FileStream(@"SaveFile.txt", FileMode.Truncate))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("");
                    foreach (string line in outputList) writer.WriteLine(line);
                }
            }

        }
    }
}
