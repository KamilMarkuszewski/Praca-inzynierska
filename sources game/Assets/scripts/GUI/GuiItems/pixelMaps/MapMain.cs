using UnityEngine;
using System.Collections;

namespace pixelMaps
{
    public class MapMain
    {

        public Rect contextMenu;


        public Rect panelRect;
        public Rect minimapRect;
        public Rect pasekQuickRect;
        public Rect mainCamRect;
        public Rect backpackRect;
        public Rect eqRect;
        public bool backpackRectVisible;
        public bool eqRectVisible;

        public MapMain()
        {
            panelRect = new Rect(0, 0, 200, Screen.height);
            minimapRect = new Rect(Objects.odstep, Screen.height - 140 - Objects.odstep, 150, 140);
            pasekQuickRect = new Rect(Screen.width / 2 + (panelRect.width / 2) - (600 / 2), Screen.height - 40, 600, 40);
            mainCamRect = new Rect(panelRect.width, 0, Screen.width - panelRect.width, Screen.height);
            backpackRectVisible = false;
            eqRectVisible = false;
            backpackRect = new Rect(0, 0, 0, 0);
            eqRect = new Rect(0, 0, 0, 0);
            contextMenu = new Rect(0, 0, 0, 0);
        }



    }

}
