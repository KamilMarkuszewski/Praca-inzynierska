using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class guiStatsWindow
{
    static private int spacer = 10;
    static private int kolumn1 = 110;
    static private int kolumn2 = 30;
    static private int spacer_h = 22;
    static private int additionalSpacer = 0;
    static private Vector2 scrollViewVector = Vector2.zero;

    static public bool addStatsOn = true;
    static private int addStatsOnHeight = 0;
    static private int nr = 0;
    static private int additionalSpacerSpace = 15;


    public static void init()
    {
        //init
        addStats.countPkt();
        if (Objects.myLocalPlayer.myStats.pkt > 0)
        {
            addStatsOn = true;
            addStatsOnHeight = 150;
        }
        else
        {
            addStatsOn = false;
            addStatsOnHeight = 0;
        }
        additionalSpacer = 0;
        nr = 0;
    }

    public static void draw()
    {
        init();

        scrollViewVector = GUI.BeginScrollView(new Rect(10, 20, 160 + 5 + 5 / 2, 180), scrollViewVector, new Rect(0, 0, 150, 220 + 50 + addStatsOnHeight));
        GUI.BeginGroup(new Rect(0, 0, 150, 220 + 50 + addStatsOnHeight));




        GUI.Label(new Rect(0, nr * spacer_h + additionalSpacer, kolumn1, 22), Objects.myLocalPlayer.myChar.name);

        nr++;
        if (Objects.myLocalPlayer.myChar.sciezka == Enumerations.ClassEnums.sciezka.None)
        {
            GUI.Label(new Rect(0, nr * spacer_h + additionalSpacer, kolumn1, 22), Enumerations.ClassEnums.klasaString[(int)Objects.myLocalPlayer.myChar.klasa]);
        }
        else {
            GUI.Label(new Rect(0, nr * spacer_h + additionalSpacer, kolumn1, 22), Enumerations.ClassEnums.podKlasaString[(int)Objects.myLocalPlayer.myChar.podKlasa]);
        }


        nr++;
        GUI.Label(new Rect(0, nr * spacer_h + additionalSpacer, kolumn1, 22), "Poziom");
        GUI.Label(new Rect(kolumn1 + spacer, nr * spacer_h + additionalSpacer, kolumn2, 22), Objects.myLocalPlayer.myChar.lvl.ToString());

        nr++;
        GUI.Label(new Rect(0, nr * spacer_h + additionalSpacer, kolumn1, 22), "Doswiadczenie");
        GUI.Label(new Rect(kolumn1 + spacer, nr * spacer_h + additionalSpacer, kolumn2, 22), Objects.myLocalPlayer.myChar.exp.ToString());


        if (addStatsOn)
        {
            drawAddStats();
        }

        drawStats();

        //dodatkowy odstep
        additionalSpacer += additionalSpacerSpace;

        nr++;
        GUI.Label(new Rect(0, nr * spacer_h + additionalSpacer, kolumn1, 22), "Zabojstwa");
        GUI.Label(new Rect(kolumn1 + spacer, nr * spacer_h + additionalSpacer, kolumn2, 22), Objects.myLocalPlayer.myStats.killedOverall.ToString());


        GUI.EndGroup();
        GUI.EndScrollView();
    }

    public static void drawAddStats()
    {
        //dodatkowy odstep
        additionalSpacer += additionalSpacerSpace;

        nr++;
        GUI.Label(new Rect(0, nr * spacer_h + additionalSpacer, kolumn1, 22), "Do rozdania");
        GUI.Label(new Rect(kolumn1 + spacer, nr * spacer_h + additionalSpacer, kolumn2, 22), Objects.myLocalPlayer.myStats.pkt.ToString());

        additionalSpacer += additionalSpacerSpace;

        nr++;
        GUI.Label(new Rect(0, nr * spacer_h + additionalSpacer, kolumn1, 22),
            Enumerations.StatsEnums.StatyKlasyString[(int)Enumerations.StatsEnums.Staty.wtrzymalosc * 4 + ((int)Objects.myLocalPlayer.myChar.klasa)]);

        if (GUI.Button(new Rect(kolumn1 + spacer, nr * spacer_h + additionalSpacer, Objects.buttonsMap.Up1.width, Objects.buttonsMap.Up1.height),
            Objects.guiTextures.txtPlus) && Event.current.button != 1)
        {
            addStats.addWtrzymalosc();
        }


        nr++;
        GUI.Label(new Rect(0, nr * spacer_h + additionalSpacer, kolumn1, 22),
            Enumerations.StatsEnums.StatyKlasyString[(int)Enumerations.StatsEnums.Staty.walka * 4 + ((int)Objects.myLocalPlayer.myChar.klasa)]);
        if (GUI.Button(new Rect(kolumn1 + spacer, nr * spacer_h + additionalSpacer, Objects.buttonsMap.Up1.width, Objects.buttonsMap.Up1.height),
            Objects.guiTextures.txtPlus) && Event.current.button != 1)
        {
            addStats.addWalka();
        }

        nr++;
        GUI.Label(new Rect(0, nr * spacer_h + additionalSpacer, kolumn1, 22),
            Enumerations.StatsEnums.StatyKlasyString[(int)Enumerations.StatsEnums.Staty.obrona * 4 + ((int)Objects.myLocalPlayer.myChar.klasa)]);
        if (GUI.Button(new Rect(kolumn1 + spacer, nr * spacer_h + additionalSpacer, Objects.buttonsMap.Up1.width, Objects.buttonsMap.Up1.height),
            Objects.guiTextures.txtPlus) && Event.current.button != 1)
        {
            addStats.addObrona();
        }

        nr++;
        GUI.Label(new Rect(0, nr * spacer_h + additionalSpacer, kolumn1, 22),
            Enumerations.StatsEnums.StatyKlasyString[(int)Enumerations.StatsEnums.Staty.zwinnosc * 4 + ((int)Objects.myLocalPlayer.myChar.klasa)]);
        if (GUI.Button(new Rect(kolumn1 + spacer, nr * spacer_h + additionalSpacer, Objects.buttonsMap.Up1.width, Objects.buttonsMap.Up1.height),
            Objects.guiTextures.txtPlus) && Event.current.button != 1)
        {
            addStats.addZwinnosc();
        }

        nr++;
        GUI.Label(new Rect(0, nr * spacer_h + additionalSpacer, kolumn1, 22),
            Enumerations.StatsEnums.StatyKlasyString[(int)Enumerations.StatsEnums.Staty.wiedza * 4 + ((int)Objects.myLocalPlayer.myChar.klasa)]);
        if (GUI.Button(new Rect(kolumn1 + spacer, nr * spacer_h + additionalSpacer, Objects.buttonsMap.Up1.width, Objects.buttonsMap.Up1.height),
            Objects.guiTextures.txtPlus) && Event.current.button != 1)
        {
            addStats.addWiedza();
        }

    }


    public static void drawStats()
    {
        //dodatkowy odstep
        additionalSpacer += additionalSpacerSpace;

        nr++;
        GUI.Label(new Rect(0, nr * spacer_h + additionalSpacer, kolumn1, 22),
            Enumerations.StatsEnums.StatyKlasyString[(int)Enumerations.StatsEnums.Staty.wtrzymalosc * 4 + ((int)Objects.myLocalPlayer.myChar.klasa)]);
        GUI.Label(new Rect(kolumn1 + spacer, nr * spacer_h + additionalSpacer, kolumn2, 22), Objects.myLocalPlayer.myStats.pktZycia.ToString());

        nr++;
        GUI.Label(new Rect(0, nr * spacer_h + additionalSpacer, kolumn1, 22),
            Enumerations.StatsEnums.StatyKlasyString[(int)Enumerations.StatsEnums.Staty.walka * 4 + ((int)Objects.myLocalPlayer.myChar.klasa)]);
        GUI.Label(new Rect(kolumn1 + spacer, nr * spacer_h + additionalSpacer, kolumn2, 22), (Objects.myLocalPlayer.myTempData.itemsAtk +Objects.myLocalPlayer.myStats.pktWalka).ToString());

        nr++;
        GUI.Label(new Rect(0, nr * spacer_h + additionalSpacer, kolumn1, 22),
            Enumerations.StatsEnums.StatyKlasyString[(int)Enumerations.StatsEnums.Staty.obrona * 4 + ((int)Objects.myLocalPlayer.myChar.klasa)]);
        GUI.Label(new Rect(kolumn1 + spacer, nr * spacer_h + additionalSpacer, kolumn2, 22), (Objects.myLocalPlayer.myStats.pktObrona + Objects.myLocalPlayer.myTempData.itemsDef).ToString());

        nr++;
        GUI.Label(new Rect(0, nr * spacer_h + additionalSpacer, kolumn1, 22),
            Enumerations.StatsEnums.StatyKlasyString[(int)Enumerations.StatsEnums.Staty.zwinnosc * 4 + ((int)Objects.myLocalPlayer.myChar.klasa)]);
        GUI.Label(new Rect(kolumn1 + spacer, nr * spacer_h + additionalSpacer, kolumn2, 22), Objects.myLocalPlayer.myStats.pktZwinnosc.ToString());

        nr++;
        GUI.Label(new Rect(0, nr * spacer_h + additionalSpacer, kolumn1, 22),
            Enumerations.StatsEnums.StatyKlasyString[(int)Enumerations.StatsEnums.Staty.wiedza * 4 + ((int)Objects.myLocalPlayer.myChar.klasa)]);
        GUI.Label(new Rect(kolumn1 + spacer, nr * spacer_h + additionalSpacer, kolumn2, 22), Objects.myLocalPlayer.myStats.pktWiedza.ToString());

    }


}


