using System;
using System.Collections.Generic;
using System.Text;

namespace LocalPlayerData
{
    public class MyPlayer
    {
        static private string login = "";

        public MySettings mySettings;
        public MyStats myStats;
        public MyChar myChar;
        public MyTempData myTempData;
        public MyIds myIds;
        public MyEq myEq;
        public MyBackpack myBackpack;
        public MyQuickPanel myQuickPanel;

        public MyPlayer()
        {
            myQuickPanel = new MyQuickPanel();
            mySettings = new MySettings();
            myStats = new MyStats();
            myChar = new MyChar();
            myTempData = new MyTempData();
            myIds = new MyIds();
            myEq = new MyEq();
            myBackpack = new MyBackpack();
        }

    }
}
