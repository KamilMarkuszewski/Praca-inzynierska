using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess
{
    class Logout
    {

        static public void LogoutMe()
        {
            SaveCharacterService.periodSaveCharacter();
            SaveCharacterService.saveCharacter();
            Worlds.ChangeWorlds.choseSceene();
        }
    }
}
