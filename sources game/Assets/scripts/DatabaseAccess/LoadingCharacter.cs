using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SmartFoxClientAPI;
using SmartFoxClientAPI.Data;

namespace DatabaseAccess
{
    class LoadingCharacter
    {
        static public void sendLoadChar(SmartFoxClient smartFoxClient, int id)
        {
            Hashtable load = new Hashtable();
            load.Add("characterId", id);
            smartFoxClient.SendXtMessage("gameServ", "loadCharacter", load, SmartFoxClient.XTMSG_TYPE_JSON);
        }

        static public bool getLoadChar(SmartFoxClient smartFoxClient, object data, string type, LocalPlayerData.MyPlayer ret)
        {
            if (type == SmartFoxClient.XTMSG_TYPE_XML)
            {
                SFSObject responseData = (SFSObject)data;

                String com = responseData.GetString("_cmd");
                if (com == "Character")
                {
                    SFSObject items = responseData.GetObj("db");

                    saveChar(ret, items);
                    saveIds(ret, items);
                    saveEq(ret, items);
                    saveBackpack(ret, items);
                    saveQuick(ret, items);
                    saveTempdata(ret, items);
                    saveStats(ret, items);
                    saveSpellBook(ret, items);
                    return true;
                }
            }
            return false;
        }

        static void saveSpellBook(LocalPlayerData.MyPlayer ret, SFSObject items)
        {
            // TO DO
        }

        static void saveStats(LocalPlayerData.MyPlayer ret, SFSObject items)
        {
            ret.myStats.pktZycia = Convert.ToInt16(items.GetString("life"));
            ret.myStats.pktWalka = Convert.ToInt16(items.GetString("atk"));
            ret.myStats.pktObrona = Convert.ToInt16(items.GetString("def"));
            ret.myStats.pktZwinnosc = Convert.ToInt16(items.GetString("dex"));
            ret.myStats.pktWiedza = Convert.ToInt16(items.GetString("wis"));

            ret.myStats.skull = Convert.ToInt16(items.GetString("skull"));
            ret.myStats.killedOverall = Convert.ToInt16(items.GetString("killed_overall"));
            ret.myStats.killedLastWeek = Convert.ToInt16(items.GetString("killed_last_week"));

            ret.myStats.skullTime = Convert.ToInt16(items.GetString("skull_time"));
            // NIE JESTEM PEWIEN TEGO JAK TO ZAPISYWAC
        }

        static void saveTempdata(LocalPlayerData.MyPlayer ret, SFSObject items)
        {
            float x = (float)Convert.ToDouble(items.GetString("corX"));
            float y = ((float)Convert.ToDouble(items.GetString("corY"))) + 2.0f;
            float z = (float)Convert.ToDouble(items.GetString("corZ"));
            ret.myTempData.Coordinates = new Vector3(x, y, z);

            ret.myTempData.hp = Convert.ToInt16(items.GetString("hp"));
            ret.myTempData.mp = Convert.ToInt16(items.GetString("mp"));
            ret.myTempData.world = Convert.ToInt16(items.GetString("world"));
        }

        static void saveBackpack(LocalPlayerData.MyPlayer ret, SFSObject items)
        {
            for (int i = 0; i < 20; i++)
            {
                ret.myBackpack.itemy[i] = Convert.ToInt16(items.GetString("slot" + (i+1)));
                ret.myBackpack.itemy_numbers[i] = Convert.ToInt16(items.GetString("slot" + (i +1)+ "_number"));
            }
        }

        static void saveQuick(LocalPlayerData.MyPlayer ret, SFSObject items)
        {
            for (int i = 0; i < 16; i++)
            {
                ret.myQuickPanel.itemy[i] = Convert.ToInt16(items.GetString("qslot" + (i + 1)));
                ret.myQuickPanel.itemy_numbers[i] = Convert.ToInt16(items.GetString("qslot" + (i + 1) + "_number"));
            }
        }

        static void saveEq(LocalPlayerData.MyPlayer ret, SFSObject items)
        {
            ret.myEq.necklace = Convert.ToInt16(items.GetString("necklace"));
            ret.myEq.helmet = Convert.ToInt16(items.GetString("helmet"));
            ret.myEq.wings = Convert.ToInt16(items.GetString("wings"));
            ret.myEq.leftH = Convert.ToInt16(items.GetString("leftH"));
            ret.myEq.armor = Convert.ToInt16(items.GetString("armor"));
            ret.myEq.rightH = Convert.ToInt16(items.GetString("rightH"));
            ret.myEq.ring1 = Convert.ToInt16(items.GetString("ring1"));
            ret.myEq.ring2 = Convert.ToInt16(items.GetString("ring2"));
            ret.myEq.boots = Convert.ToInt16(items.GetString("boots"));
        }

        static void saveIds(LocalPlayerData.MyPlayer ret, SFSObject items)
        {
            int id = Convert.ToInt16(items.GetString("id"));
            int userId = Convert.ToInt16(items.GetString("userId"));

            ret.myIds.myAccId = userId;
            ret.myIds.myCharId = id;
        }

        static void saveChar(LocalPlayerData.MyPlayer ret, SFSObject items)
        {
            int exp = Convert.ToInt16(items.GetString("exp"));
            int lvl = Convert.ToInt16(items.GetString("level"));
            int sciezka = Convert.ToInt16(items.GetString("way"));
            int klasa = Convert.ToInt16(items.GetString("klasa"));
            string name = items.GetString("name");

            ret.myChar.name = name;
            ret.myChar.lvl = lvl;
            ret.myChar.exp = exp;
            ret.myChar.sciezka = (Enumerations.ClassEnums.sciezka)sciezka;
            ret.myChar.klasa = (Enumerations.ClassEnums.Klasa)klasa;
            ret.myChar.podKlasa = (Enumerations.ClassEnums.podKlasa)((klasa * 2) + (sciezka - 1) - 1);
        }

    }
}
