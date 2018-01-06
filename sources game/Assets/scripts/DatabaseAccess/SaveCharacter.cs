using System;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using System.Collections;
using SmartFoxClientAPI;
using SmartFoxClientAPI.Data;

namespace DatabaseAccess
{
    class SaveCharacter
    {
        public static void sendSaveCharacter(SmartFoxClient smartFoxClient, LocalPlayerData.MyPlayer me, string password, string login)
        {
            Hashtable send = new Hashtable();
            saveAuth(send, me, login, password);

            //saveExp(send, me);
            //saveWay(send, me);
            saveEq(send, me);
            saveBackPack(send, me);
            //saveTempData(send, me);
            //saveTempDataHp(send, me);
            saveKillStats(send, me);
            saveSpellBook(send, me);
            saveStats(send, me);
            saveQuick(send, me);

            smartFoxClient.SendXtMessage("gameServ", "saveCharacter", send, SmartFoxClient.XTMSG_TYPE_JSON);
        }

        public static void sendSavePeriodCharacter(SmartFoxClient smartFoxClient, LocalPlayerData.MyPlayer me, string password, string login)
        {
            Hashtable send = new Hashtable();
            saveAuth(send, me, login, password);

            saveExp(send, me);
            saveTempData(send, me);
            saveTempDataHp(send, me);

            smartFoxClient.SendXtMessage("gameServ", "savePeriodCharacter", send, SmartFoxClient.XTMSG_TYPE_JSON);
        }

        public static void sendSaveWay(SmartFoxClient smartFoxClient, LocalPlayerData.MyPlayer me, string password, string login)
        {
            Hashtable send = new Hashtable();
            saveAuth(send, me, login, password);

            saveWay(send, me);

            smartFoxClient.SendXtMessage("gameServ", "saveCharacterWay", send, SmartFoxClient.XTMSG_TYPE_JSON);
        }

        public static void sendSaveItemsStats(SmartFoxClient smartFoxClient, LocalPlayerData.MyPlayer me, string password, string login)
        {
            Hashtable send = new Hashtable();
            saveAuth(send, me, login, password);

            send.Add("atk", Objects.myLocalPlayer.myTempData.itemsAtk);
            send.Add("def", Objects.myLocalPlayer.myTempData.itemsDef);

            smartFoxClient.SendXtMessage("gameServ", "saveCharacterItemsStats", send, SmartFoxClient.XTMSG_TYPE_JSON);
        }

        public static void sendSaveStats(SmartFoxClient smartFoxClient, LocalPlayerData.MyPlayer me, string password, string login)
        {
            Hashtable send = new Hashtable();
            saveAuth(send, me, login, password);

            saveStats(send, me);

            smartFoxClient.SendXtMessage("gameServ", "saveCharacterStats", send, SmartFoxClient.XTMSG_TYPE_JSON);
        }

        public static void sendSaveItems(SmartFoxClient smartFoxClient, LocalPlayerData.MyPlayer me, string password, string login, bool quick, bool backpack, bool eq)
        {
            Hashtable send = new Hashtable();
            saveAuth(send, me, login, password);

            send.Add("quick", quick);
            send.Add("backpack", backpack);
            send.Add("eq", eq);

            if (quick)
            {
                saveQuick(send, me);
            }

            if (eq)
            {
                saveEq(send, me);
            }

            if (backpack)
            {
                saveBackPack(send, me);
            }

            smartFoxClient.SendXtMessage("gameServ", "saveCharacterItems", send, SmartFoxClient.XTMSG_TYPE_JSON);
        }

        // Partial functions
        public static void saveAuth(Hashtable send, LocalPlayerData.MyPlayer me, string login, string password)
        {
            send.Add("login", login);
            send.Add("password", password);
            send.Add("userId", me.myIds.myAccId);
            send.Add("id", me.myIds.myCharId);
        }

        public static void saveTempData(Hashtable send, LocalPlayerData.MyPlayer me)
        {
            send.Add("world", me.myTempData.world);
            send.Add("corX", (int)(me.myTempData.Coordinates.x * 100));
            send.Add("corY", (int)(me.myTempData.Coordinates.y * 100));
            send.Add("corZ", (int)(me.myTempData.Coordinates.z * 100));
        }

        public static void saveTempDataHp(Hashtable send, LocalPlayerData.MyPlayer me)
        {
            send.Add("hp", me.myTempData.hp);
            send.Add("mp", me.myTempData.mp);
        }

        public static void saveStats(Hashtable send, LocalPlayerData.MyPlayer me)
        {
            send.Add("life", me.myStats.pktZycia);
            send.Add("atk", me.myStats.pktWalka);
            send.Add("def", me.myStats.pktObrona);
            send.Add("dex", me.myStats.pktZwinnosc);
            send.Add("wis", me.myStats.pktWiedza);
        }

        public static void saveKillStats(Hashtable send, LocalPlayerData.MyPlayer me)
        {
            send.Add("skull", me.myStats.skull);
            send.Add("killed_overall", me.myStats.killedOverall);
            send.Add("killed_last_week", me.myStats.killedLastWeek);
            send.Add("skull_time", me.myStats.skullTime);
        }

        public static void saveBackPack(Hashtable send, LocalPlayerData.MyPlayer me)
        {
            for (int i = 0; i < 20; i++)
            {
                send.Add("slot" + (i + 1), me.myBackpack.itemy[i]);
                send.Add("slot" + (i + 1) + "_number", me.myBackpack.itemy[i]);
            }
        }

        public static void saveQuick(Hashtable send, LocalPlayerData.MyPlayer me)
        {
            for (int i = 0; i < 16; i++)
            {
                send.Add("qslot" + (i + 1), me.myQuickPanel.itemy[i]);
                send.Add("qslot" + (i + 1) + "_number", me.myQuickPanel.itemy[i]);
            }
        }

        public static void saveEq(Hashtable send, LocalPlayerData.MyPlayer me)
        {
            send.Add("necklace", me.myEq.necklace);
            send.Add("helmet", me.myEq.helmet);
            send.Add("wings", me.myEq.wings);
            send.Add("leftH", me.myEq.leftH);
            send.Add("armor", me.myEq.armor);
            send.Add("rightH", me.myEq.rightH);
            send.Add("ring1", me.myEq.ring1);
            send.Add("ring2", me.myEq.ring2);
            send.Add("boots", me.myEq.boots);
        }

        public static void saveWay(Hashtable send, LocalPlayerData.MyPlayer me)
        {
            send.Add("way", (int)me.myChar.sciezka);
        }

        public static void saveExp(Hashtable send, LocalPlayerData.MyPlayer me)
        {
            //send.Add("exp", me.myChar.exp);
            //send.Add("level", me.myChar.lvl);
        }

        public static void saveSpellBook(Hashtable send, LocalPlayerData.MyPlayer me)
        {
            // spellbook 
            // TO DO
        }
    }
}
