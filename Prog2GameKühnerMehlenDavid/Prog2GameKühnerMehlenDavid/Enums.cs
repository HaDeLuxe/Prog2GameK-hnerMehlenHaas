using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class Enums {
        public enum ObjectsID { GREEN_PLATFORM_320_64, INVISIBLE_WALL_500x50, INVSIBLE_WALL_1000x50, PLATFORM, ITEM, PLAYER, ENEMY, VINE }
        public enum MainMenuButtons { START, RESUME, OPTIONS, CREDITS, EXIT}
        public enum GameState { MAINMENU, GAMELOOP, LEVELEDITOR, CREDITS, SPLASHSCREEN, LOADSCREEN, WINSCREEN, LOSESCREEN }
    }
}
