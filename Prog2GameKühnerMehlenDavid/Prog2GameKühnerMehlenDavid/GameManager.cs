using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class GameManager {

        public static bool SnailShellPickedUp { get; set; }
        public static bool ScissorsPickedUp { get; set; }
        public static bool ArmorPickedUp { get; set; }
        GameObject SnailShell;
        GameObject Scissors;
        GameObject Armor;

        public GameManager() 
        {
            SnailShellPickedUp = false;
            ScissorsPickedUp = false;
           
        }


        private void DestroyGameItem(Enums.ObjectsID ObjectID, ref List<GameObject> GameObjectList) {
            for(int i = 0; i < GameObjectList.Count(); i++)
            {
                if(GameObjectList[i].objectID == (int)ObjectID)
                {
                    GameObjectList.RemoveAt(i);
                }
            }
        }

        public void ManageItems(ref Player Player, ref List<GameObject> GameObjectList) 
        {
            if (SnailShell == null)
            {
                for (int i = 0; i < GameObjectList.Count(); i++)
                {
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.SNAILSHELL)
                        SnailShell = GameObjectList[i];
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.SCISSORS) Scissors = GameObjectList[i];
                }
            }
            if (SnailShell != null)
            {
                if (SnailShellPickedUp)
                {
                    DestroyGameItem(Enums.ObjectsID.SNAILSHELL, ref GameObjectList);
                }
            }

            if (Scissors == null)
            {
                for (int i = 0; i < GameObjectList.Count(); i++)
                {
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.SCISSORS)
                        Scissors = GameObjectList[i];
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.SCISSORS) Scissors = GameObjectList[i];
                }
            }
            if (Scissors != null)
            {
                if (ScissorsPickedUp)
                {
                    DestroyGameItem(Enums.ObjectsID.SCISSORS, ref GameObjectList);
                }
            }

            if (Armor == null)
            {
                for (int i = 0; i < GameObjectList.Count(); i++)
                {
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.ARMOR)
                        Armor = GameObjectList[i];
                    if (GameObjectList[i].objectID == (int)Enums.ObjectsID.ARMOR) Armor = GameObjectList[i];
                }
            }
            if (Armor != null)
            {
                if (ArmorPickedUp)
                {
                    DestroyGameItem(Enums.ObjectsID.ARMOR, ref GameObjectList);
                }
            }


        }

    }
}
