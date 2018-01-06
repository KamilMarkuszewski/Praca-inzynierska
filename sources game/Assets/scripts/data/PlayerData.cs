using UnityEngine;
using System.Collections;
using System;

using SmartFoxClientAPI;
using SmartFoxClientAPI.Data;


public class PlayerData : MonoBehaviour
{

    int player_hp = 100;
    int player_max_hp = 100;

    Texture2D tblack;
    Texture2D tred;

    static private GUIStyle red;
    static private GUIStyle black;

    Vector2 v2d;
    Camera gl;
    GUIStyle styl;
    string login;
    int id;
    int login_dl;
    int odl;
    static public int dlugosc = 80;

    private bool died = false;

    // Use this for initialization
    void Start()
    {
        ((GameObject)transform.Find("skelet").gameObject).SetActiveRecursively(false);
        tred = new Texture2D(10, 10);
        for (int y = 0; y < tred.height; ++y)
        {
            for (int x = 0; x < tred.width; ++x)
            {
                tred.SetPixel(x, y, new Color(0.6F, 0.0F, 0.0F));
            }
        }
        tred.Apply();

        red = new GUIStyle();
        red.normal.background = tred;

        tblack = new Texture2D(10, 10);
        for (int y = 0; y < tblack.height; ++y)
        {
            for (int x = 0; x < tblack.width; ++x)
            {
                tblack.SetPixel(x, y, new Color(0.3F, 0.3F, 0.3F));
            }
        }
        tblack.Apply();
        //red.normal.background = tred;
        black = new GUIStyle();
        black.normal.background = tblack;

        gl = (Camera.main);
        styl = new GUIStyle();
        styl.alignment = TextAnchor.UpperCenter;

        styl.normal.textColor = new Color((float)0.8, (float)0.8, (float)0.8, (float)0.8);


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 poprawiony = transform.position;
        poprawiony.y += (float)1.0;
        v2d = gl.WorldToScreenPoint(poprawiony);
        int x = (int)(GameObject.Find("localPlayer").transform.position.x - transform.position.x);
        int z = (int)(GameObject.Find("localPlayer").transform.position.z - transform.position.z);
        odl = (int)Math.Sqrt(x * x + z * z);
        if (login == null || id == null || id == 0)
        {
            SmartFoxClient client = NetworkController.GetClient();
            SFSObject data = new SFSObject();
            data.Put("_cmd", "getNick");  //We put _cmd = "t" here to know that this object contains transform sync data. 
            client.SendObject(data);
            transform.Find("skelet").active = false;
        }

    }

    public void setPlayerHp(int _hp, int _max_hp)
    {
        player_hp = _hp;
        player_max_hp = _max_hp;
        if (player_hp < 1)
        {
            setPlayerDied(true);
        }
        else
        {
            setPlayerDied(false);
        }
    }

    


    public void setPlayerDied(bool died_)
    {
        if (died != died_)
        {
            if (died_ == true)
            {
                ((GameObject)transform.Find("Bip001").gameObject).SetActiveRecursively(false);
                ((GameObject)transform.Find("skelet").gameObject).SetActiveRecursively(true);
            }
            else if (died_ == false)
            {
                ((GameObject)transform.Find("Bip001").gameObject).SetActiveRecursively(true);
                ((GameObject)transform.Find("skelet").gameObject).SetActiveRecursively(false);
            }
            died = died_;
            clickPerson cl = transform.Find("Capsule").GetComponent(typeof(clickPerson)) as clickPerson;
            cl.died = died;
        }
    }

    void OnGUI()
    {
        bool world = true;
        if (world)
        {
            if (odl < 60)
            {
                GUI.Label(new Rect(v2d.x, Screen.height - v2d.y, 70, 25), login, styl);
                GUI.Box(new Rect(v2d.x - dlugosc / 2, Screen.height - v2d.y - 15, dlugosc, 3), "", black);
                GUI.Box(new Rect(v2d.x - dlugosc / 2, Screen.height - v2d.y - 15, (dlugosc * player_hp / player_max_hp), 3), "", red);

            }
        }
    }


    void SetLogin(SFSObject data)
    {
        if (styl != null)
        {
            login = data.GetString("nick");
            id = Convert.ToInt16(data.GetString("id"));
            login_dl = login.Length;
            styl.contentOffset = new Vector2(-login_dl * 8 / 2, -10);
            clickPerson cl = transform.Find("Capsule").GetComponent(typeof(clickPerson)) as clickPerson;
            cl.id = id;
        }

    }


}
