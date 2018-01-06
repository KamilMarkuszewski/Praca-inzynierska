using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Worlds
{
    public class ChangeWorlds
    {
        static public void loginSceene(){
            Application.LoadLevel("_sc_login");
        }

        static public void authSceene()
        {
            Application.LoadLevel("_sc_auth");
        }

        static public void choseSceene()
        {
            Application.LoadLevel("_sc_choseChar");
        }

        static public void firstSceene()
        {
            Application.LoadLevel("_sc_world1");
        }



    }
}
