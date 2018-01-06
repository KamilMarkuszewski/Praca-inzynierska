using System;
using System.Collections.Generic;
using System.Text;
using SmartFoxClientAPI;
using SmartFoxClientAPI.Data;
using System.Collections;

namespace DatabaseAccess
{
    class CreateCharacter
    {
        public static void sendCreateCharacter(SmartFoxClient smartFoxClient, int newKlasaId, string newNick, string login)
        {
            Hashtable send = new Hashtable();
            send.Add("newNick", newNick);
            send.Add("newKlasaId", newKlasaId);
            smartFoxClient.SendXtMessage("gameServ", "createCharacter", send, SmartFoxClient.XTMSG_TYPE_JSON);
        }
        public static int getCreateCharacter(SmartFoxClient smartFoxClient, object data, string type)
        {
            if (type == SmartFoxClient.XTMSG_TYPE_XML)
            {
                SFSObject responseData = (SFSObject)data;

                String com = responseData.GetString("_cmd");
                if (com == "createCharacterOk")
                {
                    return 0;
                }
                else if (com == "createCharacterErrorNick")
                {
                    return 1;
                }

            }
            return -1;
        }

    }
}
