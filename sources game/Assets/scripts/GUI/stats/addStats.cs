using System;
using System.Collections.Generic;
using System.Text;

class addStats
{
    public static void countPkt() { 
        int pkt = Objects.myLocalPlayer.myChar.lvl;

        pkt -= Objects.myLocalPlayer.myStats.pktZycia;
        pkt -= Objects.myLocalPlayer.myStats.pktWalka;
        pkt -= Objects.myLocalPlayer.myStats.pktObrona;
        pkt -= Objects.myLocalPlayer.myStats.pktZwinnosc;
        pkt -= Objects.myLocalPlayer.myStats.pktWiedza;

        Objects.myLocalPlayer.myStats.pkt = pkt;
    }


    public static void addWiedza()
    {
        Objects.myLocalPlayer.myStats.pktWiedza++;
        Objects.myLocalPlayer.myStats.pkt--;
        DatabaseAccess.SaveCharacterService.saveStats();
        addStats.countPkt();
    }

    public static void addZwinnosc()
    {
        Objects.myLocalPlayer.myStats.pktZwinnosc++;
        Objects.myLocalPlayer.myStats.pkt--;
        DatabaseAccess.SaveCharacterService.saveStats();
        addStats.countPkt();
    }

    public static void addObrona()
    {
        Objects.myLocalPlayer.myStats.pktObrona++;
        Objects.myLocalPlayer.myStats.pkt--;
        DatabaseAccess.SaveCharacterService.saveStats();
        addStats.countPkt();
    }

    public static void addWtrzymalosc()
    {
        Objects.myLocalPlayer.myStats.pktZycia++;
        Objects.myLocalPlayer.myStats.pkt--;
        DatabaseAccess.SaveCharacterService.saveStats();
        addStats.countPkt();
    }

    public static void addWalka()
    {
        Objects.myLocalPlayer.myStats.pktWalka++;
        Objects.myLocalPlayer.myStats.pkt--;
        DatabaseAccess.SaveCharacterService.saveStats();
        addStats.countPkt();
    }

}

