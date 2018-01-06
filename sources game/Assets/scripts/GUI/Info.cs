using UnityEngine;
using System.Collections;

public class Info
{

    private int liczba = 0;
    private string[] tablica;
    int x;
    int y;
    int w;
    int h;

    int wys;
    int szer;


    private bool pressed = false;
    static private Texture2D tgray;
    static private GUIStyle gray;


    // Use this for initialization
    void Start()
    {
        tgray = new Texture2D(10, 10);
        for (int y = 0; y < tgray.height; ++y)
        {
            for (int x = 0; x < tgray.width; ++x)
            {
                tgray.SetPixel(x, y, new Color(0.3F, 0.3F, 0.3F));
            }
        }
        tgray.Apply();
        gray = new GUIStyle();
        gray.normal.background = tgray;
    }

    // Update is called once per frame
    void Update()
    {
        checkDraw();
    }

    public Info(int _liczba)
    {
        liczba = _liczba;
        tablica = new string[liczba];
        wys = 10 + 5 + liczba * 25;
    }

    public void setString(int nr, string str)
    {
        tablica[nr] = str;
    }

    public void setSzer(int sz)
    {
        szer = sz * 7 + 20;
    }

    public void setCor(int x_, int y_, int w_, int h_)
    {
        w = w_;
        h = h_;
        x = x_;
        y = y_;
    }

    public void setCor(Rect re)
    {
        w = (int)re.width;
        h = (int)re.height;
        x = (int)re.x;
        y = (int)re.y;
    }

    public void checkDraw()
    {
        
        Event e = Event.current;
        float xNow = e.mousePosition.x;
        float yNow = e.mousePosition.y;
        if (xNow > x && xNow < x + w && yNow > y && yNow < y + h)
        {
            if (e.button == 1)
            {
                pressed = true;
            }
            if (pressed)
            {
                draw(xNow, yNow);
            }
        }
        else
        {
            pressed = false;
        }

    }

    public void draw(float xNow, float yNow)
    {
        if (xNow > Screen.width / 2)
        {
            xNow -= szer;
        }
        if (yNow > Screen.height / 2)
        {
            yNow -= wys;
        }
        GUI.Box(new Rect(xNow, yNow, szer, wys), "");

        for (int i = 0; i < liczba; i++)
        {
            GUI.Label(new Rect(xNow + 10, yNow + i * 25 + 10, szer, wys), tablica[i]);
        }

    }

}
