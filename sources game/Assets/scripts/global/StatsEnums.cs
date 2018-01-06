using UnityEngine;
using System.Collections;


namespace Enumerations
{

    public class StatsEnums
    {
        public enum Staty { wtrzymalosc, walka, obrona, zwinnosc, wiedza};

        public enum StatyKlasy { 
            zycieWojownik, zycieSzaman, zycieMnich, zycieLucznik,
            walkaWojownik, walkaSzaman, walkaMnich, walkaLucznik,
            obronaWojownik, obronaSzaman, obronaMnich, obronaLucznik,
            zwinnoscWojownik, zwinnoscSzaman, zwinnoscMnich, zwinnoscLucznik,
            wiedzaWojownik, wiedzaSzaman, wiedzaMnich, wiedzaLucznik
        };

        static public string[] StatyKlasyString = { "none",
            "Wytrzymalosc", "Wytrzymalosc", "Wytrzymalosc", "Wytrzymalosc",
            "Walka w zwarciu", "Walka magia", "Walka magia", "Walka dystansowa", 
            "Obrona", "Obrona", "Obrona", "Obrona",
            "Zwinnosc", "Zwinnosc", "Zwinnosc", "Zwinnosc", 
            "Wiedza", "Wiedza", "Wiedza", "Wiedza"
        };

    }


}
