using UnityEngine;
using System.Collections;
namespace pixelMaps
{
    public class ButtonsMap
    {


        public Rect Up1;
        public Rect Up2;
        public Rect Up3;
        public Rect Up4;
        public Rect Up5;

        public Rect Menu1;
        public Rect Menu2;
        public Rect Menu3;
        public Rect Menu4;
        public Rect Menu5;
        public Rect Menu6;
        public Rect Menu7;
        public Rect Menu8;

        public Rect HpBar;
        public Rect ManaBar;



        // Use this for initialization
        public ButtonsMap()
        {
            Up1 = new Rect(Objects.odstep + Objects.mapMain.minimapRect.width + Objects.odstep, Objects.odstep, 20, 20);
            Up2 = new Rect(Objects.odstep + Objects.mapMain.minimapRect.width + Objects.odstep, 40, 20, 20);
            Up3 = new Rect(Objects.odstep + Objects.mapMain.minimapRect.width + Objects.odstep, 70, 20, 20);
            Up4 = new Rect(Objects.odstep + Objects.mapMain.minimapRect.width + Objects.odstep, 100, 20, 20);
            Up5 = new Rect(Objects.odstep + Objects.mapMain.minimapRect.width + Objects.odstep, 130, 20, 20);

            Menu1 = new Rect(11, 180, 37, 37);
            Menu2 = new Rect(58, 180, 37, 37);
            Menu3 = new Rect(105, 180, 37, 37);
            Menu4 = new Rect(152, 180, 37, 37);
            Menu5 = new Rect(11, 227, 37, 37);
            Menu6 = new Rect(58, 227, 37, 37);
            Menu7 = new Rect(105, 227, 37, 37);
            Menu8 = new Rect(152, 227, 37, 37);

            HpBar = new Rect(10, 140 + 20, 180, 5);
            ManaBar = new Rect(10, 140 + 20 + 10, 180, 5);

        }

    }
}