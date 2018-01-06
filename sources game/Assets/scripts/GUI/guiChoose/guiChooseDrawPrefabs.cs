using System;
using System.Collections.Generic;
using System.Text;


class guiChooseDrawPrefabs
{

    static public void drawChar(putChoseTerrain displayTerrain, putChooseChar displayChar,int  act, LocalPlayerData.MyChars postacie)
    {
        if (act > 0 && act < 11)
        {
            displayChar.clear();
            displayChar.putcharNone();

            displayTerrain.clear();

            if ((int)(postacie.postacie[act - 1].sciezka) == 0)
            {
                displayTerrain.putNeutral();
            }
            else if ((int)(postacie.postacie[act - 1].sciezka) == 1)
            {
                displayTerrain.putEvil();
            }
            else if ((int)(postacie.postacie[act - 1].sciezka) == 2)
            {
                displayTerrain.putSaint();
            }
        }
    }
}

