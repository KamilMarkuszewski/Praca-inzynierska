using System;
using System.Collections.Generic;
using System.Text;

namespace DragAble
{
    public class ItemsAttr
    {
        public int atk = 0;
        public int def = 0;
        public bool twohand = false;
        public int sell = 0;
        public int buy = 0;
        public Enumerations.ClassEnums.Klasa classRequired;
        public Enumerations.ClassEnums.podKlasa PodClassRequired;
        public int lvlRequired = 0;
        public int atkRequired = 0;
        public int obrRequired = 0;
        public ItemsSpecial special = new ItemsSpecial();
    }
}
