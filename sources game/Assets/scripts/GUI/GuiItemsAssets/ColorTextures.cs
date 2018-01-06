using UnityEngine;
using System.Collections;

public class ColorTextures
{

    public Texture2D tred;
    public Texture2D tblue;
    public Texture2D tblack;
    public Texture2D tgray;
    public GUIStyle red;
    public GUIStyle blue;
    public GUIStyle black;
    public GUIStyle gray;



    // Update is called once per frame
    void Update()
    {

    }

    public void makeTexture()
    {
        red = new GUIStyle();
        tred = new Texture2D(100, 100);
        for (int y = 0; y < tred.height; ++y)
        {
            for (int x = 0; x < tred.width; ++x)
            {
                tred.SetPixel(x, y, new Color(0.6F, 0.0F, 0.0F));
            }
        }
        tred.Apply();
        red.normal.background = tred;


        blue = new GUIStyle();
        tblue = new Texture2D(10, 10);
        for (int y = 0; y < tblue.height; ++y)
        {
            for (int x = 0; x < tblue.width; ++x)
            {
                tblue.SetPixel(x, y, new Color(0.0F, 0.0F, 0.7F));
            }
        }
        tblue.Apply();
        blue.normal.background = tblue;

        tblack = new Texture2D(10, 10);
        for (int y = 0; y < tblack.height; ++y)
        {
            for (int x = 0; x < tblack.width; ++x)
            {
                tblack.SetPixel(x, y, new Color(0.3F, 0.3F, 0.3F));
            }
        }
        tblack.Apply();
        black = new GUIStyle();
        black.normal.background = tblack;

        tgray = new Texture2D(10, 10);
        for (int y = 0; y < tgray.height; ++y)
        {
            for (int x = 0; x < tgray.width; ++x)
            {
                tgray.SetPixel(x, y, new Color(0.13F, 0.13F, 0.13F));
            }
        }
        tgray.Apply();
        gray = new GUIStyle();
        gray.normal.background = tgray;
    }


}
