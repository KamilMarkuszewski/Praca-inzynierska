using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Collections;


public class ContextMenu : MonoBehaviour
{
    public Rect contextMenuRect;
    int id;
    clickPerson click;
    public void Make(Vector2 mouse, int id_, clickPerson cl)
    {
        click = cl;
        id = id_;
        doDestroy();
        Debug.Log("maker");
        contextMenuRect = new Rect(mouse.x, Screen.height - mouse.y, 80, 50);
        Objects.mapMain.contextMenu = new Rect(mouse.x, Screen.height - mouse.y, 80, 50);
    }


    public void doDestroy()
    {
        if (contextMenuRect.x != 0 && contextMenuRect.y != 0)
        {
            contextMenuRect = new Rect(0, 0, 0, 0);
            Objects.mapMain.contextMenu = contextMenuRect;
        }
    }


    void OnGUI()
    {
        GUI.Box(contextMenuRect, "", Objects.colorTextures.gray);
        GUI.BeginGroup(contextMenuRect);
        if (GUI.Button(new Rect(0, 0, 80, 25), "Atakuj"))
        {
            Debug.Log("mam");
            GameObject.Find("localPlayer").SendMessage("stopFollow");
            click.attack();
        }
        if (GUI.Button(new Rect(0, 25, 80, 25), "Handluj"))
        {
            GameObject.Find("localPlayer").SendMessage("stopFollow");

        }

        GUI.EndGroup();

    }



}



