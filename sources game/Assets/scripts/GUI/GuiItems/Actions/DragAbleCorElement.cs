using System;
using UnityEngine;
using DragAble;
using Enumerations;

namespace DragAble
{
    public class DragAbleCorElement
    {
        bool leftPressed = false;

        public DragAbleCorElement()
        {
            klawisz = new KeyCode();
            wspl = new Rect();
            leftPressed = false;
        }

        public KeyCode klawisz
        {
            get;
            set;
        }

        public Rect wspl;

        public Rect wsplDraw;

        public ItemyEnums.typyDragAble type;

        public DragAble item;


        public bool mouseIn(Event e)
        {
            if (e != null)
            {
                float xNow = e.mousePosition.x;
                float yNow = e.mousePosition.y;

                if (xNow > wspl.x && xNow < wspl.x + wspl.width && yNow > wspl.y && yNow < wspl.y + wspl.height)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool isSpell(ItemyEnums.typyMagicDragAble typ) {
            if (typ == ItemyEnums.typyMagicDragAble.self || typ == ItemyEnums.typyMagicDragAble.him || typ == ItemyEnums.typyMagicDragAble.area)
            {
                return true;
            }
            else { 
                return false;
            }
        }

    }
}
