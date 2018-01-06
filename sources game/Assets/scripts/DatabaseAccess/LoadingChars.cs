using UnityEngine;
using System.Collections;
using System;
using SmartFoxClientAPI;
using SmartFoxClientAPI.Data;


namespace DatabaseAccess
{
    class LoadingChars
    {
        static public int getAuth(SmartFoxClient smartFoxClient, object data, string type)
        {
            // Handle XML responses
            if (type == SmartFoxClient.XTMSG_TYPE_XML)
            {
                SFSObject responseData = (SFSObject)data;
                Hashtable logowanie = new Hashtable();
                String com = responseData.GetString("_cmd");
                if (com == "OkAuth")
                {
                    logowanie.Add("auth", "1");
                    return 1;
                }
                else if (com == "NoAuth")
                {
                    logowanie.Add("auth", "0");
                    return 0;
                }
                smartFoxClient.SetUserVariables(logowanie);
            }
            return -1;
        }

        static public void sendAuth(SmartFoxClient smartFoxClient, string login, string hash)
        {
            Hashtable logowanie = new Hashtable();
            logowanie.Add("login", login);
            logowanie.Add("pass", hash);
            logowanie.Add("auth", "-1");

            smartFoxClient.SendXtMessage("gameServ", "getAuth", logowanie, SmartFoxClient.XTMSG_TYPE_JSON);
        }

        static public LocalPlayerData.MyChars getChars(SmartFoxClient smartFoxClient, object data, string type)
        {
            LocalPlayerData.MyChars ret = new LocalPlayerData.MyChars();
            // Handle XML responses

            if (type == SmartFoxClient.XTMSG_TYPE_XML)
            {
                SFSObject responseData = (SFSObject)data;

                String com = responseData.GetString("_cmd");
                if (com == "Chars")
                {
                    SFSObject items = responseData.GetObj("db");
                    ret.nochars = false;

                    for (int i = 0; (i < items.Size() && i <= 10); i++)
                    {
                        int id = Convert.ToInt16(items.GetObj("" + i).GetString("id"));
                        int lvl = Convert.ToInt16(items.GetObj("" + i).GetString("lvl"));
                        int sciezka = Convert.ToInt16(items.GetObj("" + i).GetString("way"));
                        int klasa = Convert.ToInt16(items.GetObj("" + i).GetString("klasa"));
                        int podKlasa = klasa * 2 + (sciezka - 1) - 1;
                        string name = items.GetObj("" + i).GetString("name");

                        ret.postacie[i] = new LocalPlayerData.MyChar();

                        ret.postacie[i].id = id;
                        ret.postacie[i].lvl = lvl;
                        ret.postacie[i].sciezka = (Enumerations.ClassEnums.sciezka)sciezka;
                        ret.postacie[i].klasa = (Enumerations.ClassEnums.Klasa)klasa;
                        ret.postacie[i].podKlasa = (Enumerations.ClassEnums.podKlasa) podKlasa;
                        ret.postacie[i].name = name;
                        ret.count = i+1;
                    }

                    return ret;
                }
                if (com == "NoChars")
                {
                    ret.nochars = true;
                    return ret;
                }
            }
            ret.nochars = true;
            return ret;
        }

        static public void sendChars(SmartFoxClient smartFoxClient, string log)
        {
            Debug.Log("gameServ getChars");
            smartFoxClient.SendXtMessage("gameServ", "getChars", null, SmartFoxClient.XTMSG_TYPE_JSON);
        }


    }
}
