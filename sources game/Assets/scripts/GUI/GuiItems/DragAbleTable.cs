using UnityEngine;
using System.Collections;
using Enumerations;
using DragAble;


public class DragAbleTable : MonoBehaviour
{
    int firstPressed = -1;


    public DragAble.DragAble[] viewItems;
    private bool wczytane = false;

    public DragAbleCorElement[] quickPanelTable;
    public DragAbleCorElement[] backpackTable;
    public DragAbleCorElement[] eqTable;

	void Start(){
		firstPressed = -1;
	}
	
    void wczytaj()
    {
        
        quickPanelTable = new DragAbleCorElement[16];
        backpackTable = new DragAbleCorElement[20];
        for (int j = 0; j < backpackTable.Length; j++)
        {
            backpackTable[j] = new DragAbleCorElement();
        }
        eqTable = new DragAbleCorElement[9];
        for (int j = 0; j < eqTable.Length; j++)
        {
            eqTable[j] = new DragAbleCorElement();
        }
        DragAbleTableInit.setQuickPanelRect(quickPanelTable);
        DragAbleTableInit.setQuickPanelKeyMappings(quickPanelTable);




        DragAbleTableInit.setBackpackRect(backpackTable);
        DragAbleTableInit.setEqRect(eqTable);


        viewItems = new DragAble.DragAble[backpackTable.Length + quickPanelTable.Length + eqTable.Length];


        DragAbleTableInit.loadItems(viewItems, quickPanelTable.Length, backpackTable.Length, eqTable.Length);

    }
    // Update is called once per frame
    void Update()
    {
        if (!wczytane)
        {
            AllItems.wczytaj();
            wczytaj();
            wczytane = true;
        }

    }

    void useSpell(Event e) {
        if (e != null)
        {
            if (e.isKey)
            {
                DragAble.DragAble spell = null;
                if (e.keyCode == KeyCode.Alpha1)
                {
                    spell = viewItems[0];
                }
                else if (e.keyCode == KeyCode.Alpha2)
                {
                    spell = viewItems[1];
                }
                else if (e.keyCode == KeyCode.Alpha3)
                {
                    spell = viewItems[2];
                }
                else if (e.keyCode == KeyCode.Alpha4)
                {
                    spell = viewItems[3];
                }
                else if (e.keyCode == KeyCode.Alpha5)
                {
                    spell = viewItems[4];
                }
                else if (e.keyCode == KeyCode.Alpha6)
                {
                    spell = viewItems[5];
                }
                else if (e.keyCode == KeyCode.Alpha7)
                {
                    spell = viewItems[6];
                }
                else if (e.keyCode == KeyCode.Alpha8)
                {
                    spell = viewItems[7];
                }

                if (spell != null)
                {
                    Objects.myData.castSpell(spell.id);
                }
            }
        }
    }

    void OnGUI()
    {

        if (wczytane == true)
        {
            DragAbleTableActions.drawItems(viewItems, quickPanelTable, backpackTable, eqTable, firstPressed);
        }
    }

    public void checkClick()
    {
        if (wczytane)
        {
            Event e = Event.current;
            if (e != null)
            {
                useSpell(e);
                int id = -1;
                int down = -1;

                id = DragAbleTableActions.findIdInQuick(id, firstPressed, viewItems, quickPanelTable, e);

                if (Objects.mapMain.backpackRectVisible == true)
                {
                    for (int i = 0; i < backpackTable.Length; i++)
                    {
                        bool ret = backpackTable[i].mouseIn(e);
                        if (ret == true)
                        {
                            if (firstPressed != -1)
                            {
                                if (!DragAbleCorElement.isSpell(viewItems[firstPressed].typeMagic))
                                {
                                    if (viewItems[firstPressed].cor.height > backpackTable[i].wspl.height)
                                    {
                                        if (i - 4 + quickPanelTable.Length > quickPanelTable.Length)
                                        {
                                            if (viewItems[i - 4 + quickPanelTable.Length] == null)
                                            {
                                                down = 0;
                                                id = i + quickPanelTable.Length;
                                            }
                                        }
                                        if (i + 4 + quickPanelTable.Length < quickPanelTable.Length + backpackTable.Length)
                                        {
                                            if (viewItems[i + 4 + quickPanelTable.Length] == null)
                                            {
                                                down = 1;
                                                id = i + quickPanelTable.Length;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (viewItems[i + quickPanelTable.Length] == null)
                                        {
                                            id = i + quickPanelTable.Length;
                                        }
                                    }

                                }
                            }
                            else
                            {
                                id = DragAbleTableActions.findSecondIdInBackpack(id, viewItems, quickPanelTable.Length, i);
                            }
                        }
                    }
                }
                if (Objects.mapMain.eqRectVisible == true)
                {
                    id = DragAbleTableActions.findIdInEq(id, firstPressed, viewItems, eqTable, e, quickPanelTable.Length, backpackTable.Length);
                }

                if (e.type == EventType.MouseDown && e.button == 0 && e.isMouse && firstPressed == -1)
                {
                    firstPressed = DragAbleTableActions.checkFirstPressed(id, viewItems);
                }
                if (e.type == EventType.MouseUp && e.button == 0 && e.isMouse && firstPressed != -1)
                {
                    DragAbleTableActions.checkXchg(id, firstPressed, down, viewItems, quickPanelTable.Length,backpackTable.Length, eqTable.Length);
                    firstPressed = -1;
                }
            }
        }
    }

    public void setBackpackRect()
    {
        DragAbleTableInit.setBackpackRect(backpackTable);
    }

    public void setEqRect()
    {
        DragAbleTableInit.setEqRect(eqTable);
    }




}
