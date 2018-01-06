using UnityEngine;
using System.Collections;
using Enumerations;

namespace DragAble
{
    public class DragAble
    {

        public Texture2D txt;
        public Rect cor;
        public ItemyEnums.typyDragAble type;
        public ItemyEnums.typyMagicDragAble typeMagic;

        public int id;
        public int ile;
        public Info inf;

        public ItemsAttr itemsAttr;
        public MagicAttr magicAttr;


        public int down = -1;
        public bool notEmpty = false;

        public DragAble(int nr)
        {
            inf = new Info(nr);
            itemsAttr = new ItemsAttr();
            magicAttr = new MagicAttr();
        }


        public void setCor(Rect r)
        {
            cor = r;
        }

        public void setCor(int x, int y, int w, int h)
        {
            cor.x = x;
            cor.y = y;
            cor.width = w;
            cor.height = h;
        }

        public void setCor(float x, float y, float w, float h)
        {
            cor.x = x;
            cor.y = y;
            cor.width = w;
            cor.height = h;
        }







    }
}