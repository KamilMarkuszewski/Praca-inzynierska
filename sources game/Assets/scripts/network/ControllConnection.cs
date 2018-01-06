using System;
using System.Collections.Generic;
using System.Text;


class ControllConnection
{

    public static void isInWorld(){
        MyData.world = true;
    }


    public static void backToChose() {
        MyData.world = false;
        MyData.reloged = true;
        Objects.guiChooseObj.reStart();
        DatabaseAccess.Logout.LogoutMe();
    }

    public static void backToLogin()
    {
        MyData.world = false;
        MyData.reloged = true;
        Objects.guiChooseObj.reStart();
        DatabaseAccess.Logout.LogoutMe();
    }
}

