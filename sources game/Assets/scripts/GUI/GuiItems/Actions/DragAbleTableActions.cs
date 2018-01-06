using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DragAble
{
    class DragAbleTableActions
    {
        static public void countItemsStats(DragAble[] viewItems, int quickPanelTableLength, int backpackTableLength, int eqTableLength)
        {
            Objects.myLocalPlayer.myTempData.itemsAtk = 0;
            Objects.myLocalPlayer.myTempData.itemsDef = 0;
            int start = quickPanelTableLength + backpackTableLength;
            for(int i = start; i< start + eqTableLength; i++){
                if (viewItems[i] == null) continue;
                Debug.Log("not null");
                Objects.myLocalPlayer.myTempData.itemsAtk += viewItems[i].itemsAttr.atk;
                Objects.myLocalPlayer.myTempData.itemsDef += viewItems[i].itemsAttr.def;
            }
            Debug.Log(Objects.myLocalPlayer.myTempData.itemsAtk);
            DatabaseAccess.SaveCharacterService.saveItemsStats();
        }



        static public void saveItems(DragAble[] viewItems, int quickPanelTableLength, int backpackTableLength, int eqTableLength, int id, int firstPressed)
        {
            bool quick = false;
            bool backpack = false;
            bool eq = false;
            if ((id < quickPanelTableLength && id >= 0) || (firstPressed < quickPanelTableLength && firstPressed >= 0))
            {
                for (int i = 0; i < quickPanelTableLength; i++)
                {
                    if (viewItems[i] != null)
                    {
                        Objects.myLocalPlayer.myQuickPanel.itemy[i] = viewItems[i].id;
                        Objects.myLocalPlayer.myQuickPanel.itemy_numbers[i] = viewItems[i].ile;
                    }
                    else
                    {
                        Objects.myLocalPlayer.myQuickPanel.itemy[i] = 0;
                        Objects.myLocalPlayer.myQuickPanel.itemy_numbers[i] = 0;
                    }
                }
                quick = true;
            }
            if ((id < quickPanelTableLength + backpackTableLength && id >= quickPanelTableLength) || (firstPressed < quickPanelTableLength + backpackTableLength && firstPressed >= quickPanelTableLength))
            {
                for (int i = 0; i < 20; i++)
                {
                    if (viewItems[i + quickPanelTableLength] != null)
                    {
                        Objects.myLocalPlayer.myBackpack.itemy[i] = viewItems[i + quickPanelTableLength].id;
                        Objects.myLocalPlayer.myBackpack.itemy_numbers[i] = viewItems[i + quickPanelTableLength].ile;
                    }
                    else
                    {
                        Objects.myLocalPlayer.myBackpack.itemy[i] = 0;
                        Objects.myLocalPlayer.myBackpack.itemy_numbers[i] = 0;
                    }
                }
                backpack = true;
            }
            if ((id < quickPanelTableLength + backpackTableLength + eqTableLength && id >= quickPanelTableLength + backpackTableLength) || (firstPressed < quickPanelTableLength + backpackTableLength + eqTableLength && firstPressed >= quickPanelTableLength + backpackTableLength))
            {
                Objects.myLocalPlayer.myEq.necklace = 0;
                Objects.myLocalPlayer.myEq.helmet = 0;
                Objects.myLocalPlayer.myEq.wings = 0;

                Objects.myLocalPlayer.myEq.leftH = 0;
                Objects.myLocalPlayer.myEq.armor = 0;
                Objects.myLocalPlayer.myEq.rightH = 0;

                Objects.myLocalPlayer.myEq.ring1 = 0;
                Objects.myLocalPlayer.myEq.ring2 = 0;
                Objects.myLocalPlayer.myEq.boots = 0;

                int j = 0;

                if (viewItems[j + quickPanelTableLength + backpackTableLength] != null)
                {
                    Objects.myLocalPlayer.myEq.necklace = viewItems[j + quickPanelTableLength + backpackTableLength].id;
                }
                j++;
                if (viewItems[j + quickPanelTableLength + backpackTableLength] != null)
                {
                    Objects.myLocalPlayer.myEq.helmet = viewItems[j + quickPanelTableLength + backpackTableLength].id;
                }
                j++;
                if (viewItems[j + quickPanelTableLength + backpackTableLength] != null)
                {
                    Objects.myLocalPlayer.myEq.wings = viewItems[j + quickPanelTableLength + backpackTableLength].id;
                }
                j++;
                if (viewItems[j + quickPanelTableLength + backpackTableLength] != null)
                {
                    Objects.myLocalPlayer.myEq.leftH = viewItems[j + quickPanelTableLength + backpackTableLength].id;
                }
                j++;
                if (viewItems[j + quickPanelTableLength + backpackTableLength] != null)
                {
                    Objects.myLocalPlayer.myEq.armor = viewItems[j + quickPanelTableLength + backpackTableLength].id;
                }
                j++;
                if (viewItems[j + quickPanelTableLength + backpackTableLength] != null)
                {
                    Objects.myLocalPlayer.myEq.rightH = viewItems[j + quickPanelTableLength + backpackTableLength].id;
                }
                j++;
                if (viewItems[j + quickPanelTableLength + backpackTableLength] != null)
                {
                    Objects.myLocalPlayer.myEq.ring1 = viewItems[j + quickPanelTableLength + backpackTableLength].id;
                }
                j++;
                if (viewItems[j + quickPanelTableLength + backpackTableLength] != null)
                {
                    Objects.myLocalPlayer.myEq.ring2 = viewItems[j + quickPanelTableLength + backpackTableLength].id;
                }
                j++;
                if (viewItems[j + quickPanelTableLength + backpackTableLength] != null)
                {
                    Objects.myLocalPlayer.myEq.boots = viewItems[j + quickPanelTableLength + backpackTableLength].id;
                }
                eq = true;
            }
            countItemsStats(viewItems, quickPanelTableLength, backpackTableLength, eqTableLength);
            DatabaseAccess.SaveCharacterService.saveItems(quick, backpack, eq);
        }

        static public void xchg(int id, int firstPressed, DragAble[] viewItems)
        {
            viewItems[id] = viewItems[firstPressed];
            viewItems[firstPressed] = null;
        }

        static public void xchgDown(int id, int firstPressed, DragAble[] viewItems)
        {
            viewItems[id] = viewItems[firstPressed];
            int lastdown = viewItems[firstPressed].down;
            viewItems[id].down = 1;
            viewItems[id + 4] = new DragAble(0);
            viewItems[id + 4].notEmpty = true;
            viewItems[id + 4].down = 1;
            viewItems[firstPressed] = null;

            if (lastdown == 1)
            {
                viewItems[firstPressed + 4] = null;
            }
            if (lastdown == 0)
            {
                viewItems[firstPressed - 4] = null;
            }
        }

        static public void xchgUp(int id, int firstPressed, DragAble[] viewItems)
        {
            viewItems[id] = viewItems[firstPressed];
            int lastdown = viewItems[firstPressed].down;
            viewItems[id].down = 0;
            viewItems[id - 4] = new DragAble(0);
            viewItems[id - 4].notEmpty = true;
            viewItems[id - 4].down = 0;
            viewItems[firstPressed] = null;

            if (lastdown == 1)
            {
                viewItems[firstPressed + 4] = null;
            }
            if (lastdown == 0)
            {
                viewItems[firstPressed - 4] = null;
            }
        }

        static public int checkFirstPressed(int id, DragAble[] viewItems)
        {
            if (id == -1 || viewItems[id] == null)
            {
                return -1;
            }
            else
            {
                return id;
            }
        }

        static public void checkXchg(int id, int firstPressed, int down, DragAble[] viewItems, int quickPanelTableLength, int backpackTableLength, int eqTableLength)
        {
            if (!(id == -1 || viewItems[id] != null))
            {
                if (down == -1)
                {
                    xchg(id, firstPressed, viewItems);
                    saveItems(viewItems, quickPanelTableLength, backpackTableLength, eqTableLength, id, firstPressed);
                }
                else
                {
                    if (down == 1)
                    {
                        xchgDown(id, firstPressed, viewItems);
                        saveItems(viewItems, quickPanelTableLength, backpackTableLength, eqTableLength, id, firstPressed);
                    }
                    else
                    {
                        xchgUp(id, firstPressed, viewItems);
                        saveItems(viewItems, quickPanelTableLength, backpackTableLength, eqTableLength, id, firstPressed);
                    }
                }
            }
        }

        static public int findIdInEq(int id, int firstPressed, DragAble[] viewItems, DragAbleCorElement[] eqTable, Event e, int quickPanelTableLength, int backpackTableLength)
        {
            for (int i = 0; i < eqTable.Length; i++)
            {
                bool ret = eqTable[i].mouseIn(e);
                if (ret == true)
                {
                    if (firstPressed != -1)
                    {
                        if (viewItems[firstPressed].type == eqTable[i].type)
                        {
                            if (eqTable[i].type == Enumerations.ItemyEnums.typyDragAble.weapon)
                            {

                                return i + quickPanelTableLength + backpackTableLength;
                            }
                            else if (eqTable[i].type == Enumerations.ItemyEnums.typyDragAble.shield)
                            {

                                return i + quickPanelTableLength + backpackTableLength;
                            }
                            else
                            {
                                return i + quickPanelTableLength + backpackTableLength;
                            }
                        }
                    }
                    else
                    {
                        return i + quickPanelTableLength + backpackTableLength;
                    }
                }
            }
            return id;
        }


        static public int findIdInQuick(int id, int firstPressed, DragAble[] viewItems, DragAbleCorElement[] quickPanelTable, Event e)
        {
            for (int i = 0; i < quickPanelTable.Length; i++)
            {
                bool ret = quickPanelTable[i].mouseIn(e);
                if (ret == true)
                {
                    if (firstPressed != -1)
                    {
                        if (viewItems[firstPressed].type == quickPanelTable[i].type)
                        {
                            return i;
                        }
                    }
                    else
                    {
                        return i;
                    }
                }
            }
            return id;
        }

        static public int findSecondIdInBackpack(int id, DragAble[] viewItems, int quickPanelTableLength, int i)
        {
            id = i + quickPanelTableLength;
            if (viewItems[id] != null)
            {
                if (viewItems[id].notEmpty == true)
                {
                    if (viewItems[id].down == 1)
                    {
                        id = id - 4;
                    }
                    else if (viewItems[id].down == 0)
                    {
                        id = id + 4;
                    }
                }
            }
            return id;
        }

        static public void drawItems(DragAble[] viewItems, DragAbleCorElement[] quickPanelTable, DragAbleCorElement[] backpackTable, DragAbleCorElement[] eqTable, int firstPressed)
        {
            if (Objects.mapMain.backpackRectVisible == true)
            {
                for (int i = 0; i < backpackTable.Length; i++)
                {
                    int now = i + quickPanelTable.Length;
                    if (!(viewItems[now] == null || firstPressed == now || viewItems[now].notEmpty == true))
                    {
                        if (viewItems[now].down == -1)
                        {
                            GUI.DrawTexture(backpackTable[i].wsplDraw, viewItems[now].txt);
                            viewItems[now].inf.setCor(backpackTable[i].wsplDraw);
                            viewItems[now].inf.checkDraw();
                        }
                        else
                        {
                            Rect tempRect;
                            if (viewItems[now].down == 1)
                            {
                                tempRect = new Rect(backpackTable[i].wsplDraw.x, backpackTable[i].wsplDraw.y, 30, 60);
                            }
                            else
                            {
                                tempRect = new Rect(backpackTable[i].wsplDraw.x, backpackTable[i].wsplDraw.y - 30, 30, 60);
                            }
                            GUI.DrawTexture(tempRect, viewItems[now].txt);
                            viewItems[now].inf.setCor(tempRect);
                            viewItems[now].inf.checkDraw();
                        }

                    }
                }
            }
            if (Objects.mapMain.eqRectVisible == true)
            {
                for (int i = 0; i < eqTable.Length; i++)
                {
                    int now = i + backpackTable.Length + quickPanelTable.Length;
                    if (viewItems[now] != null && firstPressed != now)
                    {
                        int add = 0;
                        if (eqTable[i].wsplDraw.height > viewItems[now].cor.height) add = 15;
                        Rect tmpRect = new Rect(eqTable[i].wsplDraw.x, eqTable[i].wsplDraw.y + add, viewItems[now].cor.width, viewItems[now].cor.height);
                        GUI.DrawTexture(tmpRect, viewItems[now].txt);
                        viewItems[now].inf.setCor(tmpRect);
                        viewItems[now].inf.checkDraw();
                    }
                }
            }
            for (int i = 0; i < quickPanelTable.Length; i++)
            {
                if (viewItems[i] != null && firstPressed != i)
                {
                    GUI.DrawTexture(quickPanelTable[i].wsplDraw, viewItems[i].txt);
                    viewItems[i].inf.setCor(quickPanelTable[i].wsplDraw);
                    viewItems[i].inf.checkDraw();
                }
            }

            if (firstPressed != -1)
            {

                Event e = Event.current;
                GUI.DrawTexture(
                    new Rect(e.mousePosition.x - 15, e.mousePosition.y - viewItems[firstPressed].cor.height / 2, 30, viewItems[firstPressed].cor.height),
                    viewItems[firstPressed].txt);

            }
        }

    }
}
