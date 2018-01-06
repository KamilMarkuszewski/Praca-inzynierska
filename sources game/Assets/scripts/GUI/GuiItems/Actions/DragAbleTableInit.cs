using System;
using UnityEngine;
using Enumerations;

namespace DragAble
{
    class DragAbleTableInit
    {
        static public void setBackpackRect(DragAbleCorElement[] backpackTable)
        {
            Rect backpack = Objects.mapMain.backpackRect;
            backpack.x += 13;
            backpack.y -= 2;
            for (int j = 0; j < backpackTable.Length; j++)
            {
                if (j % 4 == 0 && j != 0)
                {
                    backpack.x -= 160;
                    backpack.y += 40;
                }
                backpackTable[j].wspl = new Rect(backpack.x + j * 40, backpack.y - 3, 30, 38);
                backpackTable[j].wsplDraw = new Rect(backpack.x + j * 40 + 2, backpack.y + 7, 30, 30);
            }
        }

        static public void setEqRect(DragAbleCorElement[] eqTable)
        {
            Rect eq = Objects.mapMain.eqRect;
            eq.x += 3;
            eq.y -= 3;
            int wys = 30;
            for (int j = 0; j < eqTable.Length; j++)
            {
                if (j % 3 == 0 && j != 0)
                {
                    eq.x -= 120;
                    eq.y += 40;
                }
                if (j > 2 && j < 6)
                {
                    wys = 60;
                }
                if (j == 6)
                {
                    eq.y += 35;
                    wys = 36;
                }
                eqTable[j].wspl = new Rect(eq.x + j * 40, eq.y - 3, 30, wys);
                eqTable[j].wsplDraw = new Rect(eq.x + j * 40 + 2, eq.y + 7, 30, wys);
            }
            eqTable[0].type = ItemyEnums.typyDragAble.necklace;
            eqTable[1].type = ItemyEnums.typyDragAble.helm;
            eqTable[2].type = ItemyEnums.typyDragAble.wings;
            eqTable[3].type = ItemyEnums.typyDragAble.weapon;
            eqTable[4].type = ItemyEnums.typyDragAble.armor;
            eqTable[5].type = ItemyEnums.typyDragAble.shield;
            eqTable[6].type = ItemyEnums.typyDragAble.ring;
            eqTable[7].type = ItemyEnums.typyDragAble.boots;
            eqTable[8].type = ItemyEnums.typyDragAble.ring;
        }
        static public void setQuickPanelKeyMappings(DragAbleCorElement[] quickPanelTable)
        {
            // Set keymappings
            quickPanelTable[quickPanelTable.Length - 1].klawisz = KeyCode.Alpha1;
            quickPanelTable[quickPanelTable.Length - 2].klawisz = KeyCode.Alpha2;
            quickPanelTable[quickPanelTable.Length - 3].klawisz = KeyCode.Alpha3;
            quickPanelTable[quickPanelTable.Length - 4].klawisz = KeyCode.Alpha4;
            quickPanelTable[quickPanelTable.Length - 5].klawisz = KeyCode.Alpha5;
            quickPanelTable[quickPanelTable.Length - 6].klawisz = KeyCode.Alpha6;
            quickPanelTable[quickPanelTable.Length - 7].klawisz = KeyCode.Alpha7;
            quickPanelTable[quickPanelTable.Length - 8].klawisz = KeyCode.Alpha8;
            quickPanelTable[quickPanelTable.Length - 9].klawisz = KeyCode.Alpha9;

        }

        static public void setQuickPanelRect(DragAbleCorElement[] quickPanelTable)
        {
            Rect quick = Objects.mapMain.pasekQuickRect;
            for (int j = 0; j < quickPanelTable.Length; j++)
            {
                if (j % 4 == 0) quick.x += 8;
                quickPanelTable[j] = new DragAbleCorElement();
                quickPanelTable[j].wspl = new Rect(quick.x + j * 35, quick.y, 35, quick.height);
                quickPanelTable[j].wsplDraw = new Rect(quick.x + j * 35 + 2, quick.y + 7, 30, 30);
                quickPanelTable[j].type = ItemyEnums.typyDragAble.magic;
            }
        }

        static public void loadItems(DragAble[] viewItems, int quickPanelTableLength, int backpackTableLength, int eqTableLength)
        {

            for (int i = 0; i < quickPanelTableLength; i++)
            {
                viewItems[i] = AllItems.items[Objects.myLocalPlayer.myQuickPanel.itemy[i]];
                if (viewItems[i] != null) viewItems[i].ile = Objects.myLocalPlayer.myQuickPanel.itemy_numbers[i];
            }
            for (int i = 0; i < 20; i++)
            {
                viewItems[i + quickPanelTableLength] = AllItems.items[Objects.myLocalPlayer.myBackpack.itemy[i]];
                if (viewItems[i + quickPanelTableLength] != null) viewItems[i + quickPanelTableLength].ile = Objects.myLocalPlayer.myBackpack.itemy_numbers[i];
            }

            int j = 0;

            viewItems[j + quickPanelTableLength + backpackTableLength] = AllItems.items[Objects.myLocalPlayer.myEq.necklace];
            j++;

            viewItems[j + quickPanelTableLength + backpackTableLength] = AllItems.items[Objects.myLocalPlayer.myEq.helmet];
            j++;

            viewItems[j + quickPanelTableLength + backpackTableLength] = AllItems.items[Objects.myLocalPlayer.myEq.wings];
            j++;

            viewItems[j + quickPanelTableLength + backpackTableLength] = AllItems.items[Objects.myLocalPlayer.myEq.leftH];
            j++;

            viewItems[j + quickPanelTableLength + backpackTableLength] = AllItems.items[Objects.myLocalPlayer.myEq.armor];
            j++;

            viewItems[j + quickPanelTableLength + backpackTableLength] = AllItems.items[Objects.myLocalPlayer.myEq.rightH];
            j++;

            viewItems[j + quickPanelTableLength + backpackTableLength] = AllItems.items[Objects.myLocalPlayer.myEq.ring1];
            j++;

            viewItems[j + quickPanelTableLength + backpackTableLength] = AllItems.items[Objects.myLocalPlayer.myEq.ring2];
            j++;

            viewItems[j + quickPanelTableLength + backpackTableLength] = AllItems.items[Objects.myLocalPlayer.myEq.boots];
            j++;
            
        }


    }
}
