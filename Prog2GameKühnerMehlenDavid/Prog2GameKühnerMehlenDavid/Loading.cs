using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class Loading {


        public void loadEverything(ContentManager Content, ref Dictionary<string, Texture2D> PlayerSpriteSheets, ref Dictionary<string, Texture2D> texturesDictionnary, ref Dictionary<string, Texture2D> EnemySpriteSheets) {
            loadPlayerSprites(Content, ref PlayerSpriteSheets);
            loadWorldSprites(Content, ref texturesDictionnary);
            loadEnemySprites(Content, ref EnemySpriteSheets);
            loadItemSprites(Content, ref texturesDictionnary);
            loadWorldObjects(Content, ref texturesDictionnary);
            loadUIElements(Content, ref texturesDictionnary);
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

            Texture2D playerMoveSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Move_Even_Smaller");
            PlayerSpriteSheets.Add("playerMoveSpriteSheet", playerMoveSpriteSheet);
            Texture2D playerMoveHatSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Move_Hat");
            PlayerSpriteSheets.Add("playerMoveHatSpriteSheet", playerMoveHatSpriteSheet);
            Texture2D playerMoveArmorSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Move_Armor");
            PlayerSpriteSheets.Add("playerMoveArmorSpriteSheet", playerMoveArmorSpriteSheet);
            Texture2D playerMoveArmorHatSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Move_Armor_Hat");
            PlayerSpriteSheets.Add("playerMoveArmorHatSpriteSheet", playerMoveArmorHatSpriteSheet);

            Texture2D playerAttackSpritesheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Attack");
            PlayerSpriteSheets.Add("playerAttackSpriteSheet", playerAttackSpritesheet);
            Texture2D playerAttackHatSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Attack_Hat");
            PlayerSpriteSheets.Add("playerAttackHatSpriteSheet", playerAttackHatSpriteSheet);
            Texture2D playerAttackArmorSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Attack_Armor");
            PlayerSpriteSheets.Add("playerAttackArmorSpriteSheet", playerAttackArmorSpriteSheet);
            Texture2D playerAttackArmorHatSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Attack_Armor_Hat");
            PlayerSpriteSheets.Add("playerAttackArmorHatSpritesheet", playerAttackArmorHatSpriteSheet);
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
        /// <param name="texturesDictionnary"></param>
        private void loadItemSprites(ContentManager Content, ref Dictionary<string, Texture2D> texturesDictionnary) {
            Texture2D SnailShell = Content.Load<Texture2D>("Images\\Schneckenhaus");
            texturesDictionnary.Add("SnailShell", SnailShell);
            Texture2D SpiderWeb = Content.Load<Texture2D>("Images\\WorldObjects\\SpiderWeb");
            texturesDictionnary.Add("Spiderweb_64x64", SpiderWeb);
            Texture2D Scissors = Content.Load<Texture2D>("Images\\Schere");
            texturesDictionnary.Add("Scissors_64x64", Scissors);
            Texture2D Armor = Content.Load<Texture2D>("Images\\Rüstung");
            texturesDictionnary.Add("Armor_64x64", Armor);
            Texture2D Shovel = Content.Load<Texture2D>("Images\\Schaufel");
            texturesDictionnary.Add("Shovel_64x64", Shovel);
            Texture2D HealthItem = Content.Load<Texture2D>("Images\\HealthPotion");
            texturesDictionnary.Add("HealthItem", HealthItem);
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
        }
    }
}
