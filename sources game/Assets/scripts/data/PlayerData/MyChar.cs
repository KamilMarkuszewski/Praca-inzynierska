using UnityEngine;
using System.Collections;

namespace LocalPlayerData
{
    public class MyChar
    {

        public string name = "";
        public int lvl = -1;
        public int exp = -1;
        public int id = -1;

        public Enumerations.ClassEnums.Klasa klasa;	// jedna z 4 g³ównych klas
        public Enumerations.ClassEnums.podKlasa podKlasa;
        public Enumerations.ClassEnums.sciezka sciezka;
        public int slawa = -1;
        public int nieslawa = -1;
    }
}