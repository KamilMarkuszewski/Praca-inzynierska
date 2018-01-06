using UnityEngine;
using System.Collections;

public class DrawMain
{

    private static Camera mainCam;
    private static Camera mapCam;

    public DrawMain()
    {
        mainCam = Camera.main;
        Camera[] kamery = Camera.allCameras;
        for (int i = 0; i < Camera.allCameras.Length; i++)
        {
            if (kamery[i] != Camera.main) mapCam = kamery[i];
        }
    }

    public void mapa()
    {
        mainCam.pixelRect = Objects.mapMain.mainCamRect;
        GUI.Box(Objects.mapMain.panelRect, "");
        mapCam.pixelRect = Objects.mapMain.minimapRect;
    }

    public void pasekQuick()
    {
        GUI.BeginGroup(Objects.mapMain.pasekQuickRect);
        GUI.DrawTexture(new Rect(0, 0, Objects.mapMain.pasekQuickRect.width, Objects.mapMain.pasekQuickRect.height), Objects.guiTextures.txtQuickPanel);
        GUI.EndGroup();
    }

    public void camPlus()
    {
        int actSize = (int)mapCam.orthographicSize;
        if (actSize > 50)
        {
            mapCam.orthographicSize -= 50.0f;
            float rozm = (float)((actSize - 50) / 50 * 2.5);
            GameObject.Find("MiniMapLocalPlayerPoint").transform.localScale = new Vector3(rozm, rozm, rozm);
        }
    }

    public void camMinus()
    {
        int actSize = (int)mapCam.orthographicSize;
        if (actSize < 300)
        {
            mapCam.orthographicSize += 50.0f;
            float rozm = (float)((actSize + 50) / 50 * 2.5);
            GameObject.Find("MiniMapLocalPlayerPoint").transform.localScale = new Vector3(rozm, rozm, rozm);
        }
    }



}
