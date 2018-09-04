﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class Enums {

        /// <summary>
        /// !!!!!!!!THE ObjectsID
        /// ENUM IS RANK SENSITIVE!!!!!! DO NOT CHANGE ORDER OF OBJECTS!!!!!! ONLY ADD NEW ITEMS AT THE END!!!!!!!
        /// </summary>
        public enum ObjectsID {
            GREEN_PLATFORM_320_64,
            INVISIBLE_WALL_500x50,
            INVSIBLE_WALL_1000x50,
            PLATFORM,
            ITEM,
            PLAYER,
            ENEMY,
            VINE,
            tileBrown_01,
            tileBrown_02,
            tileBrown_03,
            tileBrown_04,
            tileBrown_05,
            tileBrown_06,
            tileBrown_07,
            tileBrown_08,
            tileBrown_09,
            tileBrown_10,
            tileBrown_11,
            tileBrown_12,
            tileBrown_13,
            tileBrown_14,
            tileBrown_15,
            tileBrown_16,
            tileBrown_17,
            tileBrown_18,
            tileBrown_19,
            tileBrown_20,
            tileBrown_21,
            tileBrown_22,
            tileBrown_23,
            tileBrown_24,
            tileBrown_25,
            tileBrown_26,
            tileBrown_27,
            tileYellow_01,
            tileYellow_02,
            tileYellow_03,
            tileYellow_04,
            tileYellow_05,
            tileYellow_06,
            tileYellow_07,
            tileYellow_08,
            tileYellow_09,
            tileYellow_10,
            tileYellow_11,
            tileYellow_12,
            tileYellow_13,
            tileYellow_14,
            tileYellow_15,
            tileYellow_16,
            tileYellow_17,
            tileYellow_18,
            tileYellow_19,
            tileYellow_20,
            tileYellow_21,
            tileYellow_22,
            tileYellow_23,
            tileYellow_24,
            tileYellow_25,
            tileYellow_26,
            tileYellow_27,
            tileBlue_01,
            tileBlue_02,
            tileBlue_03,
            tileBlue_04,
            tileBlue_05,
            tileBlue_06,
            tileBlue_07,
            tileBlue_08,
            tileBlue_09,
            tileBlue_10,
            tileBlue_11,
            tileBlue_12,
            tileBlue_13,
            tileBlue_14,
            tileBlue_15,
            tileBlue_16,
            tileBlue_17,
            tileBlue_18,
            tileBlue_19,
            tileBlue_20,
            tileBlue_21,
            tileBlue_22,
            tileBlue_23,
            tileBlue_24,
            tileBlue_25,
            tileBlue_26,
            tileBlue_27,
            tileGreen_01,
            tileGreen_02,
            tileGreen_03,
            tileGreen_04,
            tileGreen_05,
            tileGreen_06,
            tileGreen_07,
            tileGreen_08,
            tileGreen_09,
            tileGreen_10,
            tileGreen_11,
            tileGreen_12,
            tileGreen_13,
            tileGreen_14,
            tileGreen_15,
            tileGreen_16,
            tileGreen_17,
            tileGreen_18,
            tileGreen_19,
            tileGreen_20,
            tileGreen_21,
            tileGreen_22,
            tileGreen_23,
            tileGreen_24,
            tileGreen_25,
            tileGreen_26,
            tileGreen_27,
            SNAILSHELL,
            SCISSORS,
            SPIDERWEB,
            ARMOR,
            SHOVEL,
            INVISIBLE_WALL_64x64,
            HEALTHPOTION,
            ///ADD NEW ITEM HERE

        }


        public enum MainMenuButtons { START, RESUME, OPTIONS, CREDITS, EXIT}
        //public enum GameState { MAINMENU, GAMELOOP, LEVELEDITOR, CREDITS, SPLASHSCREEN, LOADSCREEN, WINSCREEN, LOSESCREEN }
        public enum Level{TUTORIAL, DUNG, GREENHOUSE, ROOF, ANTCAVE, HUB, TREE, CROWN }
        public enum EnemyAnimations{LADYBUG_FLY_LEFT, LADYBUG_FLY_RIGHT, LADYBUG_ATTACK_LEFT, LADYBUG_ATTACK_RIGHT }
    }
}
