using System;
using System.Collections.Generic;
using System.Text;

namespace Enumerations
{
    public class ClassEnums
    {
        public enum Klasa { None, Wojownik, Szaman, Mnich, Lucznik }
        public enum podKlasa { None, Najemnik, Paladyn, Nekromanta, Druid, Czaroksieznik, Mag, Skrytobojca, Strzelec }
        public enum sciezka { None, zlo, dobro }

        static public string[] podKlasaString = { "Brak", "Najemnik", "Paladyn", "Nekromanta", "Druid", "Czaroksieznik", "Mag", "Skrytobojca", "Strzelec" };
        static public string[] klasaString = { "Brak", "Wojownik", "Szaman", "Mnich", "Lucznik" };
        static public string[] sciezkaString = { "Brak", "Cienie", "Blask i Honor" };
    }
}
