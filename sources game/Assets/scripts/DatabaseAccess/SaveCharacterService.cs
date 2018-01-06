using System;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace DatabaseAccess
{
    class SaveCharacterService
    {

        private static float timeHp = 0;

        static public void saveCharacter()
        {
            SaveCharacter.sendSaveCharacter(NetworkController.GetClient(), Objects.myLocalPlayer, MyData.getPassword(), MyData.getLogin());
        }

        static public void periodSaveCharacter()
        {
            SaveCharacter.sendSavePeriodCharacter(NetworkController.GetClient(), Objects.myLocalPlayer, MyData.getPassword(), MyData.getLogin());
        }

        static public void saveItemsStats()
        {
            SaveCharacter.sendSaveItemsStats(NetworkController.GetClient(), Objects.myLocalPlayer, MyData.getPassword(), MyData.getLogin());
        }

        static public void saveStats()
        {
            SaveCharacter.sendSaveStats(NetworkController.GetClient(), Objects.myLocalPlayer, MyData.getPassword(), MyData.getLogin());
        }

        static public void saveWay()
        {
            SaveCharacter.sendSaveWay(NetworkController.GetClient(), Objects.myLocalPlayer, MyData.getPassword(), MyData.getLogin());
        }

        static public void saveItems(bool quick, bool backpack, bool eq)
        {
            SaveCharacter.sendSaveItems(NetworkController.GetClient(), Objects.myLocalPlayer, MyData.getPassword(), MyData.getLogin(), quick, backpack, eq);
        }

        static public void saveCharacterHp(int health, int max_hp)
        {
            /*
            if (timeHp < Time.time)
            {
                if (Math.Abs(health / max_hp) > 0.3f || Math.Abs(health) > 50)
                {
                    timeHp = Time.time + 5.0f;
                    SaveCharacter.sendSavePeriodCharacter(NetworkController.GetClient(), Objects.myLocalPlayer, MyData.getPassword(), MyData.getLogin());

                }
                if (timeHp + 60.0f < Time.time)
                {
                    timeHp = Time.time + 5.0f;
                    SaveCharacter.sendSavePeriodCharacter(NetworkController.GetClient(), Objects.myLocalPlayer, MyData.getPassword(), MyData.getLogin());

                }
            }
            */
        }


    }
}
