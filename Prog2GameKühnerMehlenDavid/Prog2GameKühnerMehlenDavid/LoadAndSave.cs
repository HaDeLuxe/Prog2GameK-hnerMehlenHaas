using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Reggie
{
        public class LoadAndSave {

        private List<GameObject> gameObjectList;
        private Dictionary<string, Texture2D> texturesDictionary;

        public LoadAndSave(List<GameObject> gameObjectList, Dictionary<string, Texture2D> platformTextures)
        {
            this.gameObjectList = gameObjectList;
            this.texturesDictionary = platformTextures;
        }

        public void loadEverything(ContentManager Content, ref Dictionary<string, Texture2D> PlayerSpriteSheets, ref Dictionary<string, Texture2D> texturesDictionary, ref Dictionary<string, Texture2D> EnemySpriteSheets) {
            loadPlayerSprites(Content, ref PlayerSpriteSheets);
            loadWorldSprites(Content, ref texturesDictionary);
            loadEnemySprites(Content, ref EnemySpriteSheets);
            loadItemSprites(Content, ref texturesDictionary);
            loadWorldObjects(Content, ref texturesDictionary);
            loadUIElements(Content, ref texturesDictionary);
            loadInteractivePlatforms(Content, ref texturesDictionary);
        }

        /// <summary>
        /// This method loads all world background images and adds them to the texturesDictionary
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

            //Hawk
            Texture2D hawk_Flight = Content.Load<Texture2D>("Images\\Enemies Sprite Sheets\\StaticFlightLeft");
            EnemySpriteSheets.Add("hawkFlightSpriteSheet", hawk_Flight);
            Texture2D hawk_Attack = Content.Load<Texture2D>("Images\\Enemies Sprite Sheets\\AttackLeft");
            EnemySpriteSheets.Add("hawkAttackSpriteSheet", hawk_Attack);

            //Snail
            Texture2D snail_Moving = Content.Load<Texture2D>("Images\\Enemies Sprite Sheets\\Snail_Walking");
            EnemySpriteSheets.Add("snailMoveSpriteSheet", snail_Moving);
            Texture2D snail_Attack = Content.Load<Texture2D>("Images\\Enemies Sprite Sheets\\Snail_Attack");
            EnemySpriteSheets.Add("snailAttackSpriteSheet", snail_Attack);
            Texture2D snail_Transf = Content.Load<Texture2D>("Images\\Enemies Sprite Sheets\\Snail_Trans");
            EnemySpriteSheets.Add("snailTransfSpriteSheet", snail_Transf);
            Texture2D snail_Aggressive = Content.Load<Texture2D>("Images\\Enemies Sprite Sheets\\Snail_Aggressive");
            EnemySpriteSheets.Add("snailAggressiveSpriteSheet", snail_Transf);

            //Ant
            Texture2D antMoving = Content.Load<Texture2D>("Images\\Enemies Sprite Sheets\\AntWalkAnimation_Small");
            EnemySpriteSheets.Add("antMovingSpriteSheet", antMoving);
            Texture2D antAttack = Content.Load<Texture2D>("Images\\Enemies Sprite Sheets\\AntAttackAnimation_Small");
            EnemySpriteSheets.Add("antAttackSpriteSheet", antAttack);

            //Spider
            Texture2D spiderMoving = Content.Load<Texture2D>("Images\\Enemies Sprite Sheets\\Spider_Move");
            EnemySpriteSheets.Add("spiderMovingSpriteSheet", spiderMoving);
            Texture2D spiderAttack = Content.Load<Texture2D>("Images\\Enemies Sprite Sheets\\Spider_Attack");
            EnemySpriteSheets.Add("spiderAttackSpriteSheet", spiderAttack);

        }

        /// <summary>
        /// This method loads all item sprite sheets and adds them to the texturesDictionary
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
            Texture2D currency = Content.Load<Texture2D>("Images\\Items\\Corn_Currency");
            texturesDictionary.Add("cornnency", currency);
        }

        /// <summary>
        /// This method loads all interactive game objects sprites and adds them to the texturesDictionary
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
        /// This method loads all World Objects sprites and adds them to the texturesDictionary
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
            Texture2D EnemySpawnPoint = Content.Load<Texture2D>("Images\\WorldObjects\\EnemySpawnPoint");
            texturesDictionary.Add("EnemySpawnPoint", EnemySpawnPoint);

            //Load Shop Assets
            
            Texture2D IdleShopkeeper = Content.Load<Texture2D>("Images\\Shop\\IdleShopkeeper");
            texturesDictionary.Add("Shopkeeper_SpriteSheet_Idle", IdleShopkeeper);
            Texture2D ShopkeeperWaving = Content.Load<Texture2D>("Images\\Shop\\WavingShopkeeper");
            texturesDictionary.Add("Shopkeeper_SpriteSheet_Waving", ShopkeeperWaving);
        }


        /// <summary>
        /// This method loads all UI Elements ands adds them to the texturesDictionary
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="texturesDictionnary"></param>
        private void loadUIElements(ContentManager Content, ref Dictionary<string, Texture2D> texturesDictionnary)
        {
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
            Texture2D SaveIcon = Content.Load<Texture2D>("Images\\UI\\Save Icon");
            texturesDictionnary.Add("SaveIcon", SaveIcon);
            Texture2D saveAnimSpriteSheet = Content.Load<Texture2D>("Images\\UI\\SaveIconAnimation");
            texturesDictionnary.Add("SaveAnimationSpriteSheet", saveAnimSpriteSheet);
            Texture2D moveTutorialAnimSpriteSheet = Content.Load<Texture2D>("Images\\UI\\Move_Tutorial");
            texturesDictionnary.Add("moveTutorial", moveTutorialAnimSpriteSheet);
            Texture2D glideTutorial = Content.Load<Texture2D>("Images\\UI\\GlideTut");
            texturesDictionary.Add("glideTutorial", glideTutorial);
            Texture2D attackTutorial = Content.Load<Texture2D>("Images\\UI\\Attack_Tut");
            texturesDictionary.Add("attackTutorial", attackTutorial);
            Texture2D sliderbar = Content.Load<Texture2D>("Images\\UI\\Sliderbar");
            texturesDictionary.Add("sliderbar", sliderbar);
            Texture2D sliderknob = Content.Load<Texture2D>("Images\\UI\\Sliderknob");
            texturesDictionary.Add("sliderknob", sliderknob);



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

            //Load Shop
            Texture2D idleShop = Content.Load<Texture2D>("Images\\Shop\\IdleShopkeeper");
            texturesDictionary.Add("idleShop", idleShop);
            Texture2D wavingShop = Content.Load<Texture2D>("Images\\Shop\\WavingShopkeeper");
            texturesDictionary.Add("wavingShop", wavingShop);
            //ShopInterface
            Texture2D ShopKeeperInterface_Background = Content.Load<Texture2D>("Images\\Shop\\ShopInterface\\ShopInterface_Background");
            texturesDictionary.Add("ShopKeeperInterface_Background", ShopKeeperInterface_Background);
            Texture2D ShopInterface_AllPotions = Content.Load<Texture2D>("Images\\Shop\\ShopInterface\\ShopInterface_AllPotions");
            texturesDictionary.Add("ShopKeeperInterface_AllPotions", ShopInterface_AllPotions);
            //Texture2D ShopInterface_AllPotions = Content.Load<Texture2D>("Images\\Shop\\ShopInterface\\ShopInterface_AllPotions");
            //texturesDictionary.Add("ShopKeeperInterface_AllPotions", ShopInterface_AllPotions);
            //woaw

            Texture2D ShopKeeperInterface_StrengthPotion_Highlighted = Content.Load<Texture2D>("Images\\Shop\\ShopInterface\\ShopInterface_HighlightedStrengthPotion");
            texturesDictionary.Add("ShopKeeperInterface_StrengthPotion_Highlighted", ShopKeeperInterface_StrengthPotion_Highlighted);
           
            Texture2D ShopKeeperInterface_JumpPotion_Highlighted = Content.Load<Texture2D>("Images\\Shop\\ShopInterface\\ShopInterface_HighlightedJumpPotion");
            texturesDictionary.Add("ShopKeeperInterface_JumpPotion_Highlighted", ShopKeeperInterface_JumpPotion_Highlighted);
            
            Texture2D ShopKeeperInterface_HealtPotion_Highlighted = Content.Load<Texture2D>("Images\\Shop\\ShopInterface\\ShopInterface_HighlightedHealthPotion");
            texturesDictionary.Add("ShopKeeperInterface_HealtPotion_Highlighted", ShopKeeperInterface_HealtPotion_Highlighted);
           
            
        }


        private void loadSongs(ContentManager Content, ref Dictionary<string, Song> songDictionnary)
        {
           // Song temp = Content.Load<Song>("Audio\\Reggie_UNI");
            //songDictionnary.Add("IngameMusic", temp);

            //MediaPlayer.Play(Game1.songDictionnary["IngameMusic"]);
            //MediaPlayer.IsRepeating = true;
        }

        private void loadSoundEffects(ContentManager Content, ref Dictionary<string, SoundEffect> soundEffectDictionnary)
        {
            //fail super mario sound
            //SoundEffect temp = Content.Load<SoundEffect>("Audio\\houseChord");
            //soundEffectDictionnary.Add("houseChord", temp);

            //// Fire and forget play
            //Game1.soundEffectDictionnary["houseChord"].Play();

            //// Play that can be manipulated after the fact
            //var instance = soundEffects[0].CreateInstance();
            //instance.IsLooped = true;
            //instance.Play();
        }

        public void Save()
        {
            List<string> outputList = new List<string>();
            outputList.RemoveRange(0, outputList.Count());


            foreach (GameObject GameObject in gameObjectList)
            {
                string Output = "";

                
                if (GameObject.getTexture() == texturesDictionary["Transparent_500x50"]) Output = Enums.ObjectsID.INVISIBLE_WALL_500x50.ToString();
                if (GameObject.getTexture() == texturesDictionary["Transparent_1000x50"]) Output = Enums.ObjectsID.INVSIBLE_WALL_1000x50.ToString();
                if (GameObject.getTexture() == texturesDictionary["Transparent_64x64"]) Output = Enums.ObjectsID.INVISIBLE_WALL_64x64.ToString();
                if (GameObject.getTexture() == texturesDictionary["Climbingplant_38x64"]) Output = Enums.ObjectsID.VINE.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_01"]) Output = Enums.ObjectsID.tileBrown_01.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_02"]) Output = Enums.ObjectsID.tileBrown_02.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_03"]) Output = Enums.ObjectsID.tileBrown_03.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_04"]) Output = Enums.ObjectsID.tileBrown_04.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_05"]) Output = Enums.ObjectsID.tileBrown_05.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_06"]) Output = Enums.ObjectsID.tileBrown_06.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_07"]) Output = Enums.ObjectsID.tileBrown_07.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_08"]) Output = Enums.ObjectsID.tileBrown_08.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_09"]) Output = Enums.ObjectsID.tileBrown_09.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_10"]) Output = Enums.ObjectsID.tileBrown_10.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_11"]) Output = Enums.ObjectsID.tileBrown_11.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_12"]) Output = Enums.ObjectsID.tileBrown_12.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_13"]) Output = Enums.ObjectsID.tileBrown_13.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_14"]) Output = Enums.ObjectsID.tileBrown_14.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_15"]) Output = Enums.ObjectsID.tileBrown_15.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_16"]) Output = Enums.ObjectsID.tileBrown_16.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_17"]) Output = Enums.ObjectsID.tileBrown_17.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_18"]) Output = Enums.ObjectsID.tileBrown_18.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_19"]) Output = Enums.ObjectsID.tileBrown_19.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_20"]) Output = Enums.ObjectsID.tileBrown_20.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_21"]) Output = Enums.ObjectsID.tileBrown_21.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_22"]) Output = Enums.ObjectsID.tileBrown_22.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_23"]) Output = Enums.ObjectsID.tileBrown_23.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_24"]) Output = Enums.ObjectsID.tileBrown_24.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_25"]) Output = Enums.ObjectsID.tileBrown_25.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_26"]) Output = Enums.ObjectsID.tileBrown_26.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBrown_27"]) Output = Enums.ObjectsID.tileBrown_27.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_01"]) Output = Enums.ObjectsID.tileYellow_01.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_02"]) Output = Enums.ObjectsID.tileYellow_02.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_03"]) Output = Enums.ObjectsID.tileYellow_03.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_04"]) Output = Enums.ObjectsID.tileYellow_04.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_05"]) Output = Enums.ObjectsID.tileYellow_05.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_06"]) Output = Enums.ObjectsID.tileYellow_06.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_07"]) Output = Enums.ObjectsID.tileYellow_07.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_08"]) Output = Enums.ObjectsID.tileYellow_08.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_09"]) Output = Enums.ObjectsID.tileYellow_09.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_10"]) Output = Enums.ObjectsID.tileYellow_10.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_11"]) Output = Enums.ObjectsID.tileYellow_11.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_12"]) Output = Enums.ObjectsID.tileYellow_12.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_13"]) Output = Enums.ObjectsID.tileYellow_13.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_14"]) Output = Enums.ObjectsID.tileYellow_14.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_15"]) Output = Enums.ObjectsID.tileYellow_15.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_16"]) Output = Enums.ObjectsID.tileYellow_16.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_17"]) Output = Enums.ObjectsID.tileYellow_17.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_18"]) Output = Enums.ObjectsID.tileYellow_18.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_19"]) Output = Enums.ObjectsID.tileYellow_19.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_20"]) Output = Enums.ObjectsID.tileYellow_20.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_21"]) Output = Enums.ObjectsID.tileYellow_21.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_22"]) Output = Enums.ObjectsID.tileYellow_22.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_23"]) Output = Enums.ObjectsID.tileYellow_23.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_24"]) Output = Enums.ObjectsID.tileYellow_24.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_25"]) Output = Enums.ObjectsID.tileYellow_25.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_26"]) Output = Enums.ObjectsID.tileYellow_26.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileYellow_27"]) Output = Enums.ObjectsID.tileYellow_27.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_01"]) Output = Enums.ObjectsID.tileBlue_01.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_02"]) Output = Enums.ObjectsID.tileBlue_02.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_03"]) Output = Enums.ObjectsID.tileBlue_03.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_04"]) Output = Enums.ObjectsID.tileBlue_04.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_05"]) Output = Enums.ObjectsID.tileBlue_05.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_06"]) Output = Enums.ObjectsID.tileBlue_06.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_07"]) Output = Enums.ObjectsID.tileBlue_07.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_08"]) Output = Enums.ObjectsID.tileBlue_08.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_09"]) Output = Enums.ObjectsID.tileBlue_09.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_10"]) Output = Enums.ObjectsID.tileBlue_10.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_11"]) Output = Enums.ObjectsID.tileBlue_11.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_12"]) Output = Enums.ObjectsID.tileBlue_12.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_13"]) Output = Enums.ObjectsID.tileBlue_13.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_14"]) Output = Enums.ObjectsID.tileBlue_14.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_15"]) Output = Enums.ObjectsID.tileBlue_15.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_16"]) Output = Enums.ObjectsID.tileBlue_16.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_17"]) Output = Enums.ObjectsID.tileBlue_17.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_18"]) Output = Enums.ObjectsID.tileBlue_18.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_19"]) Output = Enums.ObjectsID.tileBlue_19.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_20"]) Output = Enums.ObjectsID.tileBlue_20.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_21"]) Output = Enums.ObjectsID.tileBlue_21.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_22"]) Output = Enums.ObjectsID.tileBlue_22.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_23"]) Output = Enums.ObjectsID.tileBlue_23.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_24"]) Output = Enums.ObjectsID.tileBlue_24.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_25"]) Output = Enums.ObjectsID.tileBlue_25.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_26"]) Output = Enums.ObjectsID.tileBlue_26.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileBlue_27"]) Output = Enums.ObjectsID.tileBlue_27.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_01"]) Output = Enums.ObjectsID.tileGreen_01.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_02"]) Output = Enums.ObjectsID.tileGreen_02.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_03"]) Output = Enums.ObjectsID.tileGreen_03.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_04"]) Output = Enums.ObjectsID.tileGreen_04.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_05"]) Output = Enums.ObjectsID.tileGreen_05.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_06"]) Output = Enums.ObjectsID.tileGreen_06.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_07"]) Output = Enums.ObjectsID.tileGreen_07.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_08"]) Output = Enums.ObjectsID.tileGreen_08.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_09"]) Output = Enums.ObjectsID.tileGreen_09.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_10"]) Output = Enums.ObjectsID.tileGreen_10.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_11"]) Output = Enums.ObjectsID.tileGreen_11.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_12"]) Output = Enums.ObjectsID.tileGreen_12.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_13"]) Output = Enums.ObjectsID.tileGreen_13.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_14"]) Output = Enums.ObjectsID.tileGreen_14.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_15"]) Output = Enums.ObjectsID.tileGreen_15.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_16"]) Output = Enums.ObjectsID.tileGreen_16.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_17"]) Output = Enums.ObjectsID.tileGreen_17.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_18"]) Output = Enums.ObjectsID.tileGreen_18.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_19"]) Output = Enums.ObjectsID.tileGreen_19.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_20"]) Output = Enums.ObjectsID.tileGreen_20.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_21"]) Output = Enums.ObjectsID.tileGreen_21.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_22"]) Output = Enums.ObjectsID.tileGreen_22.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_23"]) Output = Enums.ObjectsID.tileGreen_23.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_24"]) Output = Enums.ObjectsID.tileGreen_24.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_25"]) Output = Enums.ObjectsID.tileGreen_25.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_26"]) Output = Enums.ObjectsID.tileGreen_26.ToString();
                if (GameObject.getTexture() == texturesDictionary["tileGreen_27"]) Output = Enums.ObjectsID.tileGreen_27.ToString();
                if (GameObject.getTexture() == texturesDictionary["Apple"]) Output = Enums.ObjectsID.APPLE.ToString();
                if (GameObject.getTexture() == texturesDictionary["HealthItem"]) Output = Enums.ObjectsID.HEALTHPOTION.ToString();
                if (GameObject.getTexture() == texturesDictionary["PowerPotion"]) Output = Enums.ObjectsID.POWERPOTION.ToString();
                if (GameObject.getTexture() == texturesDictionary["JumpPotion"]) Output = Enums.ObjectsID.JUMPPOTION.ToString();
                if (GameObject.getTexture() == texturesDictionary["Armor_64x64"]) Output = Enums.ObjectsID.ARMOR.ToString();
                if (GameObject.getTexture() == texturesDictionary["SnailShell"]) Output = Enums.ObjectsID.SNAILSHELL.ToString();
                if (GameObject.getTexture() == texturesDictionary["Shovel_64x64"]) Output = Enums.ObjectsID.SHOVEL.ToString();
                if (GameObject.getTexture() == texturesDictionary["Scissors_64x64"]) Output = Enums.ObjectsID.SCISSORS.ToString();
                if (GameObject.getTexture() == texturesDictionary["GoldenUmbrella"]) Output = Enums.ObjectsID.GOLDENUMBRELLA.ToString();
                if (GameObject.getTexture() == texturesDictionary["VineDoor"]) Output = Enums.ObjectsID.VINEDOOR.ToString();
                if (GameObject.getTexture() == texturesDictionary["Spiderweb_64x64"]) Output = Enums.ObjectsID.SPIDERWEB.ToString();
                if (GameObject.getTexture() == texturesDictionary["EnemySpawnPoint"]) Output = Enums.ObjectsID.ENEMYSPAWNPOINT.ToString();
                if (GameObject.getTexture() == texturesDictionary["cornnency"]) Output = Enums.ObjectsID.CORNNENCY.ToString();


                Output += "," + GameObject.gameObjectPosition.X + "," + GameObject.gameObjectPosition.Y;

                outputList.Add(Output);
            }

            string OutputLine = "Playerposition, " + Game1.wormPlayer.gameObjectPosition.X + "," + Game1.wormPlayer.gameObjectPosition.Y;
            outputList.Add(OutputLine);
            OutputLine = "ArmorBOOL, " + ItemUIManager.armorPickedUp;
            outputList.Add(OutputLine);
            OutputLine = "HelmetBOOL, " + ItemUIManager.snailShellPickedUp;
            outputList.Add(OutputLine);
            OutputLine = "HealthPotionBOOL," + ItemUIManager.healthPickedUp;
            outputList.Add(OutputLine);
            OutputLine = "JumpPotionBOOL, " + ItemUIManager.jumpPickedUp;
            outputList.Add(OutputLine);
            OutputLine = "PowerPotionBOOL, " + ItemUIManager.powerPickedUp;
            outputList.Add(OutputLine);
            OutputLine = "ScissorsBOOL, " + ItemUIManager.scissorsPickedUp;
            outputList.Add(OutputLine);
            OutputLine = "ShovelBOOL, " + ItemUIManager.shovelPickedUp;
            outputList.Add(OutputLine);
            OutputLine = "GoldenUmbrellaBOOL, " + ItemUIManager.goldenUmbrellaPickedUp;
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


        public void LoadGameObjects(ref List<GameObject> allGameObjectList, ref Player wormPlayer)
        {
            allGameObjectList.Clear();
            List<string> data = new List<string>(System.IO.File.ReadAllLines(@"SaveFile.txt"));

            List<String> dataSeperated = new List<String>();
            foreach (String s in data)
            {
                List<String> tempStringList = s.Split(',').ToList();
                foreach (String st in tempStringList) dataSeperated.Add(st);
            }

            for (int i = 0; i < dataSeperated.Count(); i++)
            {

                if (dataSeperated[i] == Enums.ObjectsID.INVISIBLE_WALL_500x50.ToString())
                {
                    allGameObjectList.Add(new Platform(texturesDictionary["Transparent_500x50"], new Vector2(512, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.INVISIBLE_WALL_500x50, false));
                    allGameObjectList.Last().DontDrawThisObject();
                }
                if (dataSeperated[i] == Enums.ObjectsID.INVSIBLE_WALL_1000x50.ToString())
                {
                    allGameObjectList.Add(new Platform(texturesDictionary["Transparent_1000x50"], new Vector2(1024, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.INVSIBLE_WALL_1000x50, false));
                    allGameObjectList.Last().DontDrawThisObject();
                }
                if (dataSeperated[i] == Enums.ObjectsID.VINE.ToString())
                {
                    allGameObjectList.Add(new Platform(texturesDictionary["Climbingplant_38x64"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.VINE, (int)Enums.ObjectsID.VINE, false));
                }
                if (dataSeperated[i] == Enums.ObjectsID.INVISIBLE_WALL_64x64.ToString())
                {
                    allGameObjectList.Add(new Platform(texturesDictionary["Transparent_64x64"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.INVISIBLE_WALL_64x64, false));
                    allGameObjectList.Last().DontDrawThisObject();
                }
                if (dataSeperated[i] == Enums.ObjectsID.APPLE.ToString())
                {
                    allGameObjectList.Add(new Item(texturesDictionary["Apple"], new Vector2(128, 128), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.APPLE));
                }
                if (dataSeperated[i] == Enums.ObjectsID.ARMOR.ToString())
                    allGameObjectList.Add(new Item(texturesDictionary["Armor_64x64"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.ARMOR));
                if (dataSeperated[i] == Enums.ObjectsID.SNAILSHELL.ToString())
                    allGameObjectList.Add(new Item(texturesDictionary["SnailShell"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.SNAILSHELL));
                if (dataSeperated[i] == Enums.ObjectsID.HEALTHPOTION.ToString())
                    allGameObjectList.Add(new Item(texturesDictionary["HealthItem"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.HEALTHPOTION));
                if (dataSeperated[i] == Enums.ObjectsID.POWERPOTION.ToString())
                    allGameObjectList.Add(new Item(texturesDictionary["PowerPotion"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.POWERPOTION));
                if (dataSeperated[i] == Enums.ObjectsID.JUMPPOTION.ToString())
                    allGameObjectList.Add(new Item(texturesDictionary["JumpPotion"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.JUMPPOTION));
                if (dataSeperated[i] == Enums.ObjectsID.SHOVEL.ToString())
                    allGameObjectList.Add(new Item(texturesDictionary["Shovel_64x64"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.SHOVEL));
                if (dataSeperated[i] == Enums.ObjectsID.SCISSORS.ToString())
                    allGameObjectList.Add(new Item(texturesDictionary["Scissors_64x64"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.SCISSORS));
                if (dataSeperated[i] == Enums.ObjectsID.GOLDENUMBRELLA.ToString())
                    allGameObjectList.Add(new Item(texturesDictionary["GoldenUmbrella"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.GOLDENUMBRELLA));
                if (dataSeperated[i] == Enums.ObjectsID.VINEDOOR.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["VineDoor"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.VINEDOOR, false));
                if (dataSeperated[i] == Enums.ObjectsID.SPIDERWEB.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["Spiderweb_64x64"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.SPIDERWEB, false));
                if (dataSeperated[i] == Enums.ObjectsID.ENEMYSPAWNPOINT.ToString())
                {
                    allGameObjectList.Add(new Platform(texturesDictionary["EnemySpawnPoint"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.ENEMYSPAWNPOINT, true));
                    allGameObjectList.Last().DontDrawThisObject();
                }
                if (dataSeperated[i] == Enums.ObjectsID.CORNNENCY.ToString())
                    allGameObjectList.Add(new Item(texturesDictionary["cornnency"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.CORNNENCY));


                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_01.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_01"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_01, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_02.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_02"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_02, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_03.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_03"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_03, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_04.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_04"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_04, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_05.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_05"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_05, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_06.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_06"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_06, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_07.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_07"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_07, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_08.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_08"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_08, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_09.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_09"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_09, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_10.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_10"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_10, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_11.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_11"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_11, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_12.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_12"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_12, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_13.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_13"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_13, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_14.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_14"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_14, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_15.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_15"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_15, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_16.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_16"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_16, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_17.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_17"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_17, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_18.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_18"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_18, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_19.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_19"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_19, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_20.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_20"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_20, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_21.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_21"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_21, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_22.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_22"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_22, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_23.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_23"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_23, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_24.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_24"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_24, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_25.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_25"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_25, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_26.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_26"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_26, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_27.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_27"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_27, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_01.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_01"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_01, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_02.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_02"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_02, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_03.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_03"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_03, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_04.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_04"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_04, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_05.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_05"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_05, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_06.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_06"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_06, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_07.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_07"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_07, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_08.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_08"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_08, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_09.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_09"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_09, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_10.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_10"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_10, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_11.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_11"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_11, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_12.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_12"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_12, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_13.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_13"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_13, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_14.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_14"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_14, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_15.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_15"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_15, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_16.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_16"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_16, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_17.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_17"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_17, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_18.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_18"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_18, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_19.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_19"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_19, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_20.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_20"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_20, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_21.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_21"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_21, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_22.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_22"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_22, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_23.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_23"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_23, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_24.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_24"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_24, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_25.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_25"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_25, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_26.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_26"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_26, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_27.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_27"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_27, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_01.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_01"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_01, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_02.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_02"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_02, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_03.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_03"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_03, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_04.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_04"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_04, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_05.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_05"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_05, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_06.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_06"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_06, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_07.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_07"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_07, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_08.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_08"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_08, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_09.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_09"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_09, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_10.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_10"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_10, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_11.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_11"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_11, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_12.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_12"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_12, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_13.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_13"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_13, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_14.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_14"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_14, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_15.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_15"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_15, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_16.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_16"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_16, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_17.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_17"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_17, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_18.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_18"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_18, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_19.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_19"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_19, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_20.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_20"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_20, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_21.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_21"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_21, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_22.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_22"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_22, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_23.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_23"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_23, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_24.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_24"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_24, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_25.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_25"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_25, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_26.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_26"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_26, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_27.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_27"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_27, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_01.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_01"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_01, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_02.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_02"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_02, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_03.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_03"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_03, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_04.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_04"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_04, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_05.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_05"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_05, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_06.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_06"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_06, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_07.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_07"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_07, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_08.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_08"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_08, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_09.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_09"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_09, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_10.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_10"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_10, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_11.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_11"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_11, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_12.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_12"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_12, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_13.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_13"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_13, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_14.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_14"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_14, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_15.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_15"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_15, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_16.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_16"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_16, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_17.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_17"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_17, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_18.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_18"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_18, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_19.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_19"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_19, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_20.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_20"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_20, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_21.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_21"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_21, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_22.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_22"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_22, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_23.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_23"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_23, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_24.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_24"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_24, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_25.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_25"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_25, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_26.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_26"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_26, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_27.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_27"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_27, false));
                if (dataSeperated[i] == "Playerposition")
                {
                    wormPlayer.gameObjectPosition.X = float.Parse(dataSeperated[i + 1]);
                    wormPlayer.gameObjectPosition.Y = float.Parse(dataSeperated[i + 2]);
                }
                if (dataSeperated[i] == "ArmorBOOL") ItemUIManager.armorPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "HelmetBOOL") ItemUIManager.snailShellPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "HealthPotionBOOL") ItemUIManager.healthPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "JumpPotionBOOL") ItemUIManager.jumpPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "PowerPotionBOOL") ItemUIManager.powerPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "ScissorsBOOL") ItemUIManager.scissorsPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "ShovelBOOL") ItemUIManager.shovelPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "GoldenUmbrellaBOOL") ItemUIManager.goldenUmbrellaPickedUp = bool.Parse(dataSeperated[i + 1]);
            }
        }

        public void LoadGameObjectsNewGame(ref List<GameObject> allGameObjectList, ref Player wormPlayer)
        {
            allGameObjectList.Clear();
            List<string> data = new List<string>(System.IO.File.ReadAllLines(@"NewGameSaveFile.txt"));

            List<String> dataSeperated = new List<String>();
            foreach (String s in data)
            {
                List<String> tempStringList = s.Split(',').ToList();
                foreach (String st in tempStringList) dataSeperated.Add(st);
            }

            for (int i = 0; i < dataSeperated.Count(); i++)
            {

                if (dataSeperated[i] == Enums.ObjectsID.INVISIBLE_WALL_500x50.ToString())
                {
                    allGameObjectList.Add(new Platform(texturesDictionary["Transparent_500x50"], new Vector2(512, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.INVISIBLE_WALL_500x50, false));
                    allGameObjectList.Last().DontDrawThisObject();
                }
                if (dataSeperated[i] == Enums.ObjectsID.INVSIBLE_WALL_1000x50.ToString())
                {
                    allGameObjectList.Add(new Platform(texturesDictionary["Transparent_1000x50"], new Vector2(1024, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.INVSIBLE_WALL_1000x50, false));
                    allGameObjectList.Last().DontDrawThisObject();
                }
                if (dataSeperated[i] == Enums.ObjectsID.VINE.ToString())
                {
                    allGameObjectList.Add(new Platform(texturesDictionary["Climbingplant_38x64"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.VINE, (int)Enums.ObjectsID.VINE, false));
                }
                if (dataSeperated[i] == Enums.ObjectsID.INVISIBLE_WALL_64x64.ToString())
                {
                    allGameObjectList.Add(new Platform(texturesDictionary["Transparent_64x64"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.INVISIBLE_WALL_64x64, false));
                    allGameObjectList.Last().DontDrawThisObject();
                }
                if (dataSeperated[i] == Enums.ObjectsID.APPLE.ToString())
                {
                    allGameObjectList.Add(new Item(texturesDictionary["Apple"], new Vector2(128, 128), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.APPLE));
                }
                if (dataSeperated[i] == Enums.ObjectsID.ARMOR.ToString())
                    allGameObjectList.Add(new Item(texturesDictionary["Armor_64x64"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.ARMOR));
                if (dataSeperated[i] == Enums.ObjectsID.SNAILSHELL.ToString())
                    allGameObjectList.Add(new Item(texturesDictionary["SnailShell"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.SNAILSHELL));
                if (dataSeperated[i] == Enums.ObjectsID.HEALTHPOTION.ToString())
                    allGameObjectList.Add(new Item(texturesDictionary["HealthItem"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.HEALTHPOTION));
                if (dataSeperated[i] == Enums.ObjectsID.POWERPOTION.ToString())
                    allGameObjectList.Add(new Item(texturesDictionary["PowerPotion"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.POWERPOTION));
                if (dataSeperated[i] == Enums.ObjectsID.JUMPPOTION.ToString())
                    allGameObjectList.Add(new Item(texturesDictionary["JumpPotion"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.JUMPPOTION));
                if (dataSeperated[i] == Enums.ObjectsID.SHOVEL.ToString())
                    allGameObjectList.Add(new Item(texturesDictionary["Shovel_64x64"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.SHOVEL));
                if (dataSeperated[i] == Enums.ObjectsID.SCISSORS.ToString())
                    allGameObjectList.Add(new Item(texturesDictionary["Scissors_64x64"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.SCISSORS));
                if (dataSeperated[i] == Enums.ObjectsID.GOLDENUMBRELLA.ToString())
                    allGameObjectList.Add(new Item(texturesDictionary["GoldenUmbrella"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.GOLDENUMBRELLA));
                if (dataSeperated[i] == Enums.ObjectsID.VINEDOOR.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["VineDoor"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.VINEDOOR, false));
                if (dataSeperated[i] == Enums.ObjectsID.SPIDERWEB.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["Spiderweb_64x64"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.SPIDERWEB, false));
                if (dataSeperated[i] == Enums.ObjectsID.ENEMYSPAWNPOINT.ToString())
                {
                    allGameObjectList.Add(new Platform(texturesDictionary["EnemySpawnPoint"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.ENEMYSPAWNPOINT, true));
                    allGameObjectList.Last().DontDrawThisObject();
                }
                if (dataSeperated[i] == Enums.ObjectsID.CORNNENCY.ToString())
                    allGameObjectList.Add(new Item(texturesDictionary["cornnency"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.CORNNENCY));


                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_01.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_01"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_01, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_02.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_02"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_02, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_03.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_03"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_03, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_04.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_04"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_04, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_05.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_05"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_05, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_06.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_06"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_06, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_07.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_07"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_07, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_08.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_08"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_08, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_09.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_09"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_09, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_10.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_10"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_10, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_11.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_11"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_11, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_12.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_12"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_12, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_13.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_13"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_13, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_14.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_14"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_14, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_15.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_15"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_15, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_16.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_16"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_16, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_17.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_17"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_17, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_18.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_18"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_18, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_19.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_19"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_19, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_20.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_20"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_20, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_21.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_21"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_21, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_22.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_22"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_22, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_23.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_23"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_23, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_24.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_24"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_24, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_25.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_25"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_25, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_26.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_26"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_26, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_27.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBrown_27"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_27, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_01.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_01"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_01, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_02.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_02"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_02, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_03.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_03"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_03, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_04.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_04"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_04, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_05.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_05"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_05, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_06.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_06"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_06, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_07.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_07"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_07, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_08.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_08"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_08, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_09.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_09"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_09, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_10.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_10"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_10, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_11.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_11"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_11, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_12.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_12"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_12, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_13.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_13"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_13, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_14.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_14"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_14, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_15.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_15"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_15, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_16.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_16"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_16, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_17.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_17"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_17, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_18.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_18"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_18, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_19.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_19"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_19, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_20.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_20"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_20, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_21.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_21"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_21, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_22.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_22"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_22, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_23.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_23"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_23, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_24.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_24"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_24, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_25.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_25"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_25, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_26.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_26"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_26, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_27.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileYellow_27"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_27, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_01.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_01"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_01, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_02.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_02"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_02, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_03.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_03"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_03, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_04.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_04"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_04, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_05.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_05"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_05, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_06.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_06"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_06, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_07.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_07"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_07, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_08.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_08"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_08, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_09.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_09"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_09, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_10.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_10"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_10, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_11.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_11"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_11, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_12.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_12"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_12, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_13.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_13"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_13, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_14.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_14"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_14, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_15.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_15"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_15, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_16.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_16"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_16, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_17.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_17"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_17, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_18.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_18"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_18, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_19.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_19"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_19, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_20.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_20"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_20, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_21.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_21"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_21, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_22.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_22"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_22, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_23.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_23"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_23, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_24.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_24"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_24, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_25.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_25"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_25, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_26.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_26"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_26, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_27.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileBlue_27"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_27, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_01.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_01"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_01, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_02.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_02"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_02, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_03.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_03"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_03, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_04.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_04"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_04, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_05.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_05"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_05, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_06.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_06"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_06, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_07.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_07"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_07, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_08.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_08"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_08, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_09.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_09"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_09, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_10.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_10"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_10, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_11.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_11"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_11, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_12.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_12"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_12, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_13.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_13"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_13, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_14.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_14"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_14, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_15.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_15"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_15, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_16.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_16"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_16, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_17.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_17"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_17, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_18.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_18"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_18, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_19.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_19"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_19, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_20.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_20"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_20, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_21.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_21"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_21, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_22.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_22"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_22, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_23.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_23"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_23, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_24.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_24"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_24, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_25.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_25"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_25, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_26.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_26"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_26, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_27.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionary["tileGreen_27"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_27, false));
                if (dataSeperated[i] == "Playerposition")
                {
                    wormPlayer.gameObjectPosition.X = float.Parse(dataSeperated[i + 1]);
                    wormPlayer.gameObjectPosition.Y = float.Parse(dataSeperated[i + 2]);
                }
                if (dataSeperated[i] == "ArmorBOOL") ItemUIManager.armorPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "HelmetBOOL") ItemUIManager.snailShellPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "HealthPotionBOOL") ItemUIManager.healthPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "JumpPotionBOOL") ItemUIManager.jumpPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "PowerPotionBOOL") ItemUIManager.powerPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "ScissorsBOOL") ItemUIManager.scissorsPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "ShovelBOOL") ItemUIManager.shovelPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "GoldenUmbrellaBOOL") ItemUIManager.goldenUmbrellaPickedUp = bool.Parse(dataSeperated[i + 1]);
            }
        }

    }
}
