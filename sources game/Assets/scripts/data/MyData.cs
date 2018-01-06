using UnityEngine;
using System.Collections;
using System;


public class MyData : MonoBehaviour
{

    static private string login = "";
    static private string password = "";
    static public bool reloged = false;
    static public bool world = false;


    public int max_hp = 100;
    public int hp = 100;

    public int max_mp = 100;
    public int mp = 100;
    public bool alive = true;
    private static float timePeriod = 0;
    public bool Controllable = true;
    public Transform spell1Pref;


    public NetworkTransform netTransform;

    // Use this for initialization
    void Start()
    {
        //initHpMp();
        //hp = max_hp;
        //mp = max_mp;
    }

    public MyData()
    {

    }

    public void castSpell(int id)
    {
        if (id == 5)
        {
            if (mp > 4)
            {
                change_mana(-4);
                change_health(5, 0);
            }
        }
        if (id == 6)
        {
            //clickPerson cl = transform.Find("Capsule").GetComponent(typeof(clickPerson)) as clickPerson;
            if (mp > 9)
            {
                change_mana(-9);
                //cl.addDmg = 17;
                UnityEngine.Object spell1 = Instantiate(spell1Pref, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion(0, 0, 0, 1));
            }
        }


    }

    void initHpMp()
    {
        max_hp = 100 + Objects.myLocalPlayer.myStats.pktZycia * 2;
        max_mp = 100 + Objects.myLocalPlayer.myStats.pktWiedza * 2;
    }

    public void change_health(int health) { 
        change_health(health, 0);
    }

    public void change_health(int health, int id)
    {
        initHpMp();
        if (health > 0)
        {
            if (hp + health <= max_hp)
            {
                hp += health;
            }
            if (hp + health > max_hp)
            {
                hp = max_hp;
            }
        }
        if (health < 0)
        {
            if (hp + health > 0)
            {
                hp += health;
            }
            if (hp + health <= 0)
            {
                hp = 0;
                PlayerDead(id);
            }
        }
        Objects.myLocalPlayer.myTempData.hp = hp;
        netTransform.setHP(hp, max_hp);
        if (health == 0)
        {

        }
        DatabaseAccess.SaveCharacterService.saveCharacterHp(health, max_hp);
    }

    public void change_mana(int mana)
    {
        if (mana > 0)
        {
            if (mp + mana <= max_mp)
            {
                mp += mana;
            }
            if (mp + mana > max_mp)
            {
                mp = max_mp;
            }
        }
        if (mana < 0)
        {
            if (mp + mana > 0)
            {
                mp += mana;
            }
            if (mp + mana <= 0)
            {
                mp = 0;
            }
        }
        Objects.myLocalPlayer.myTempData.mp = mp;
    }

    // Use this for initialization
    void PlayerDead(int id)
    {
        netTransform.setDied(true);
        SendMessage("setControllable", false);
        Controllable = false;
        alive = false;
        ((GameObject)transform.Find("Bip001").gameObject).SetActiveRecursively(false);
        ((GameObject)transform.Find("skelet").gameObject).SetActiveRecursively(true);
        DatabaseAccess.Battle.sendDied(NetworkController.GetClient(), id);
    }

    void OnGUI()
    {
        if (world)
        {
            //LeftPanel.CallUpdate();
        }
        if (!alive)
        {
            reSpawn();
        }
    }
    public void spawn(bool re)
    {
        alive = true;
        if (Objects.myLocalPlayer.myTempData.hp > 0 && re == false)
        {
            hp = Objects.myLocalPlayer.myTempData.hp;
            if (hp > max_hp) hp = max_hp;
        }
        else
        {
            hp = max_hp;
        }

        if (Objects.myLocalPlayer.myTempData.mp > 0 && re == false)
        {
            mp = Objects.myLocalPlayer.myTempData.mp;
            if (mp > max_mp) mp = max_mp;
        }
        else
        {
            mp = max_mp;
        }

        SendMessage("setControllable", true);
        Controllable = true;
        ((GameObject)transform.Find("skelet").gameObject).SetActiveRecursively(false);
        change_health(0, -1);
        netTransform.setDied(false);
    }


    // Update is called once per frame
    void reSpawn()
    {
        if (GUI.Button(new Rect(Screen.width / 2 + 100 - 50, Screen.height / 2, 100, 25), "Respawn"))
        {
            Objects.playerSpawnController.SpawnMe(true);
            spawn(true);
            ((GameObject)transform.Find("Bip001").gameObject).SetActiveRecursively(true);
            ((GameObject)transform.Find("skelet").gameObject).SetActiveRecursively(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (world)
        {
            float timer = Time.time;
            if (timePeriod < timer)
            {
                change_mana(Objects.myLocalPlayer.myStats.pktWiedza + 5);
                timePeriod = timer + 10.0f;
                DatabaseAccess.SaveCharacterService.periodSaveCharacter();
            }
        }
        
    }

    public Vector3 getCoordinates()
    {
        return Objects.myLocalPlayer.myTempData.Coordinates;
    }
    public void setCoordinates(Vector3 co)
    {
        Objects.myLocalPlayer.myTempData.Coordinates = co;
    }

    public float getCamera_h()
    {
        return Objects.myLocalPlayer.mySettings.camera_h;
    }
    public void setCamera_h(float co)
    {
        Objects.myLocalPlayer.mySettings.camera_h = co;
    }

    public float getCamera_d()
    {
        return Objects.myLocalPlayer.mySettings.camera_d;
    }
    public void setCamera_d(float co)
    {
        Objects.myLocalPlayer.mySettings.camera_d = co;
    }


    public bool getWorld()
    {
        return world;
    }

    public void setWorld(bool on)
    {
        world = on;
    }

    static public void setLogin(string log)
    {
        login = log;
    }

    static public void setPassword(string pass)
    {
        password = pass;
    }

    static public string getLogin()
    {
        return login;
    }
    static public string getPassword()
    {
        return password;
    }

    public int getHp()
    {
        return hp;
    }


}
