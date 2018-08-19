using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class GameManager {

        public static bool SnailShellPickedUp { get; set; }
        public static bool ScissorsPickedUp { get; set; }
        GameObject SnailShell;
        GameObject Scissors;

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


        }

    }
}
