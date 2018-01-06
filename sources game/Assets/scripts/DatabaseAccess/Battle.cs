using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SmartFoxClientAPI;
using SmartFoxClientAPI.Data;


namespace DatabaseAccess
{
    class Battle
    {
        static private float last_time = 0.0f;

        static public void sendAtack(SmartFoxClient smartFoxClient, int id, int addDmg)
        {
            if (last_time < Time.time && Objects.myData.alive == true)
            {
                last_time = Time.time +1.0f;

                SaveCharacterService.periodSaveCharacter();
                Hashtable load = new Hashtable();
                load.Add("atkCharacterId", id);
                load.Add("id", Objects.myLocalPlayer.myIds.myCharId);
                load.Add("addDmg", addDmg);
                smartFoxClient.SendXtMessage("gameServ", "atkCharacter", load, SmartFoxClient.XTMSG_TYPE_JSON);
            }
        }

        static public void loadAtack(SFSObject responseData)
        {
            SFSObject items = responseData.GetObj("db");

            int attackedId = Convert.ToInt16(items.GetString("attackedId"));
            int id = Convert.ToInt16(items.GetString("id"));
            int dmg = Convert.ToInt16(items.GetString("dmg"));
            if (id == Objects.myLocalPlayer.myIds.myCharId)
            {



            }
            if (attackedId == Objects.myLocalPlayer.myIds.myCharId)
            {
                Objects.myData.change_health(-dmg, id);
            }
        }

        static public void sendDied(SmartFoxClient smartFoxClient, int id)
        {
            if (last_time < Time.time)
            {
                last_time = Time.time + 1.0f;

                SaveCharacterService.periodSaveCharacter();
                Hashtable load = new Hashtable();
                load.Add("killerId", id);
                load.Add("id", Objects.myLocalPlayer.myIds.myCharId);
                smartFoxClient.SendXtMessage("gameServ", "characterDied", load, SmartFoxClient.XTMSG_TYPE_JSON);
            }
        }

        static public void loadDied(SFSObject responseData)
        {
            Debug.Log("Died");
            SFSObject items = responseData.GetObj("db");

            int id = Convert.ToInt16(items.GetString("id"));
            Debug.Log("id");
            if (id == Objects.myLocalPlayer.myIds.myCharId) {
                Debug.Log("id ...");
                int exp = Convert.ToInt16(items.GetString("exp"));
                int lvl = Convert.ToInt16(items.GetString("lvl"));
                Objects.myLocalPlayer.myChar.exp = exp;
                Objects.myLocalPlayer.myChar.lvl = lvl;
            }
        }

    }
}
