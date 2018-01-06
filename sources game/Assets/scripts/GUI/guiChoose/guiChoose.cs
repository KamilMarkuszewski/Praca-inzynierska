using UnityEngine;
using System.Collections;

using System;
using SmartFoxClientAPI;
using SmartFoxClientAPI.Data;


public class guiChoose : MonoBehaviour
{

    private bool tworzenie = false;
    private bool loaded = false;
    private bool makingCharOn = false;
    private bool started = false;
    private static SmartFoxClient smartFoxClient;
    public LocalPlayerData.MyChars postacie;
    private int act = 0;
    putChooseChar displayChar;
    putChoseTerrain displayTerrain;
    private string newNick = "";
    private string newKlasa = "Wybierz klase";
    private int newKlasaId = 0;
    private bool makingCharDropListOn = false;
    private bool work = false;


    public static SmartFoxClient GetClient()
    {
        return SmartFox.Connection;
    }

    // Update is called once per frame
    void Update()
    {
        if (loaded == false && work == false && started == true)
        {
            string log = MyData.getLogin();
            DatabaseAccess.LoadingChars.sendChars(smartFoxClient, log);
            work = true;
        }
    }



    public void OnExtensionResponse(object data, string type)
    {
        if (loaded == false)
        {
            postacie = DatabaseAccess.LoadingChars.getChars(smartFoxClient, data, type);
            loaded = true;
            Debug.Log("loadChars");
            if (postacie.count == 0)
            {
                act = 0;
            }
            else
            {
                act = 1;
                guiChooseDrawPrefabs.drawChar(displayTerrain, displayChar, act, postacie);
            }
        }
        else
        {
            if (tworzenie == true && started == true)
            {
                Debug.Log("Create");
                int ret = DatabaseAccess.CreateCharacter.getCreateCharacter(smartFoxClient, data, type);
                if (ret == 0) {
                    tworzenie = false;
                    loaded = false;
                    displayChar.clear();
                    displayTerrain.clear();
                    string log = MyData.getLogin();
                    makingCharOn = false;
                    DatabaseAccess.LoadingChars.sendChars(smartFoxClient, log);
                } else if (ret == -1) {
                    newNick = "Wystapil blad";
                }else if(ret == 1){
                    newNick = "Ten nick jest zajety";
                }

            }
            else if (postacie.nochars == false && started == true)
            {
                Debug.Log("IN!");
                if (DatabaseAccess.LoadingCharacter.getLoadChar(smartFoxClient, data, type, Objects.myLocalPlayer))
                {
                    started = false;
                    Worlds.ChangeWorlds.firstSceene();
                }

            }
        }
    }



    public void OnPublicMessage(string message, User fromUser, int roomId)
    {

    }

    void OnGUI()
    {


        GUI.Box(new Rect(Screen.width / 2 - 250, Screen.height - 150 + 30, 500, 100), "");
        if (loaded)
        {
            if (postacie.nochars)
            {
                GUI.Label(new Rect(Screen.width / 2 - 40, Screen.height - 140 + 30, 80, 25), "Brak postaci");
            }

            if (postacie.count < 10)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 55, Screen.height - 140 + 30 + 45, 110, 25), "Stworz postac"))
                {
                    newNick = "";
                    newKlasa = "Wybierz klase";
                    newKlasaId = 0;
                    makingCharDropListOn = false;
                    displayChar.clear();
                    displayTerrain.clear();
                    makingCharOn = true;
                }
            }
            if (!postacie.nochars && !makingCharOn)
            {

                if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 140 + 30, 100, 25), "Wejdz do gry"))
                {
                    loadCharacter();
                }

                if (act != 0 && act > 1)
                {
                    if (GUI.Button(new Rect(Screen.width / 2 - 75 - 25 - 25 - 100, Screen.height - 140 + 30, 150, 25), "< Poprzednia postac"))
                    {
                        act--;
                        guiChooseDrawPrefabs.drawChar(displayTerrain, displayChar, act, postacie);
                    }
                }
                if (act != 0 && act < postacie.count)
                {
                    if (GUI.Button(new Rect(Screen.width / 2 - 75 + 25 + 25 + 100, Screen.height - 140 + 30, 150, 25), "Nastepna postac >"))
                    {
                        act++;
                        guiChooseDrawPrefabs.drawChar(displayTerrain, displayChar, act, postacie);
                    }
                }
                if (act != 0)
                {
                    GUI.Box(new Rect(Screen.width / 2 - 250 - 10, 150 - 10, 170, 120), "");
                    GUI.Label(new Rect(Screen.width / 2 - 250, 150, 150, 25), "Nick: " + postacie.postacie[act - 1].name);
                    GUI.Label(new Rect(Screen.width / 2 - 250, 150 + 25, 150, 25), "Poziom: " + postacie.postacie[act - 1].lvl);
                    if (postacie.postacie[act - 1].sciezka == Enumerations.ClassEnums.sciezka.None)
                    {
                        GUI.Label(new Rect(Screen.width / 2 - 250, 150 + 25 + 25, 150, 25), "Klasa: " + Enumerations.ClassEnums.klasaString[(int)(postacie.postacie[act - 1].klasa)]);
                    }
                    else
                    {
                        GUI.Label(new Rect(Screen.width / 2 - 250, 150 + 25 + 25, 150, 25), "Klasa: " + Enumerations.ClassEnums.podKlasaString[(int)(postacie.postacie[act - 1].podKlasa)]);
                    }
                    GUI.Label(new Rect(Screen.width / 2 - 250, 150 + 25 + 25 + 25, 150, 25), "Sciezka: " + Enumerations.ClassEnums.sciezkaString[(int)(postacie.postacie[act - 1].sciezka)]);
                }
            }
            else
            {
                if (makingCharOn)
                {
                    GUI.Box(new Rect(Screen.width / 2 - 250, Screen.height - 350 + 10 - 100, 500, 300), "");
                    GUI.Label(new Rect(Screen.width / 2 - 55, Screen.height - 350 + 30 - 100, 110, 25), "Towrzenie postaci");

                    if (GUI.Button(new Rect(Screen.width / 2 + 250 - 110, Screen.height - 150 + 30 - 55, 100, 25), "Anuluj"))
                    {
                        makingCharOn = false;
                        guiChooseDrawPrefabs.drawChar(displayTerrain, displayChar, act, postacie);
                    }
                    if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 150 + 30 - 55, 100, 25), "Stworz"))
                    {
                        if (newKlasaId != 0 && newNick.Length > 0 && newNick.Length < 16)
                        {
                            tworzenie = true;
                            string log = MyData.getLogin();
                            DatabaseAccess.CreateCharacter.sendCreateCharacter(smartFoxClient, newKlasaId, newNick, log);
                        }
                    }
                    GUI.Label(new Rect(Screen.width / 2 - 130, Screen.height - 350 + 25 - 100 + 2 + 50, 50, 25), "Nick: ");
                    GUI.Label(new Rect(Screen.width / 2 - 130, Screen.height - 350 + 60 - 100 + 2 + 50, 50, 25), "Klasa: ");

                    newNick = GUI.TextField(new Rect(Screen.width / 2 - 70, Screen.height - 350 - 100 + 25 + 50, 200, 20), newNick, 25);
                    newNick.Trim();
                    if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height - 350 - 100 + 60 + 50, 200, 25), newKlasa))
                    {
                        makingCharDropListOn = !makingCharDropListOn;
                        newKlasaId = 0;
                        newKlasa = "Wybierz klase";
                    }
                    if (makingCharDropListOn)
                    {

                        GUI.BeginGroup(new Rect(Screen.width / 2 - 70, Screen.height - 350 - 100 + 60 + 75, 200, 100));

                        //GUI.Box(new Rect(0, 0, 200, 100), "");
                        if (GUI.Button(new Rect(0, 0, 200, 25), Enumerations.ClassEnums.klasaString[1]))
                        {
                            newKlasa = Enumerations.ClassEnums.klasaString[1];
                            newKlasaId = 1;
                            makingCharDropListOn = !makingCharDropListOn;
                        }
                        if (GUI.Button(new Rect(0, 25, 200, 25), Enumerations.ClassEnums.klasaString[2]))
                        {
                            newKlasa = Enumerations.ClassEnums.klasaString[2];
                            newKlasaId = 2;
                            makingCharDropListOn = !makingCharDropListOn;
                        }
                        if (GUI.Button(new Rect(0, 50, 200, 25), Enumerations.ClassEnums.klasaString[3]))
                        {
                            newKlasa = Enumerations.ClassEnums.klasaString[3];
                            newKlasaId = 3;
                            makingCharDropListOn = !makingCharDropListOn;
                        }
                        if (GUI.Button(new Rect(0, 75, 200, 25), Enumerations.ClassEnums.klasaString[4]))
                        {
                            newKlasa = Enumerations.ClassEnums.klasaString[4];
                            newKlasaId = 4;
                            makingCharDropListOn = !makingCharDropListOn;
                        }
                        GUI.EndGroup();
                    }
                }
            }

        }
    }



    void loadCharacter()
    {
        DatabaseAccess.LoadingCharacter.sendLoadChar(smartFoxClient, postacie.postacie[act - 1].id);
    }

    public void reStart()
    {

    }


    void Start()
    {
        Debug.Log("start");
        smartFoxClient = GetClient();
        if (smartFoxClient == null)
        {
            Debug.Log("smartFoxClient null");
            Worlds.ChangeWorlds.loginSceene();

            return;
        }
        Objects.CreateInChoose();
        SubscribeEvents();
        loaded = false;
        
        tworzenie = false;
        work = false;
        postacie = null;
        makingCharOn = false;
        act = 0;
        newNick = "";
        newKlasaId = 0;
        newKlasa = "Wybierz klase";
        makingCharDropListOn = false;
        work = false;




        displayChar = GameObject.Find("Main Camera").GetComponent(typeof(putChooseChar)) as putChooseChar;
        displayTerrain = GameObject.Find("Main Camera").GetComponent(typeof(putChoseTerrain)) as putChoseTerrain;
        Objects.saveGuiChoose();
        started = true;
    }






    // Here we process incoming SFS objects
    private void OnObjectReceived(SFSObject data, User fromUser)
    {
        //First determine the type of this object - what it contains ?
    }

    void OnRoomList(Hashtable roomList)
    {
        try
        {
            foreach (int roomId in roomList.Keys)
            {
                Room room = (Room)roomList[roomId];
                if (room.IsPrivate())
                {
                    Debug.Log("Room id: " + roomId + " has name: " + room.GetName() + "(private)");
                }
                Debug.Log("Room id: " + roomId + " has name: " + room.GetName());
            }
            UnsubscribeEvents();
            smartFoxClient.LeaveRoom(smartFoxClient.activeRoomId);
        }
        catch (Exception e)
        {
            Debug.Log("Room list error: " + e.Message + " " + e.StackTrace);
        }
    }
    private void SubscribeEvents()
    {
        SFSEvent.onObjectReceived += OnObjectReceived;

        SFSEvent.onExtensionResponse += OnExtensionResponse;
        SFSEvent.onRoomListUpdate += OnRoomList;

    }

    private void UnsubscribeEvents()
    {

        SFSEvent.onObjectReceived -= OnObjectReceived;
        SFSEvent.onExtensionResponse -= OnExtensionResponse;
        SFSEvent.onRoomListUpdate -= OnRoomList;

    }

    void OnApplicationQuit()
    {
        UnsubscribeEvents();
        smartFoxClient.Disconnect();
    }

    void FixedUpdate()
    {
        if (started)
        {
            smartFoxClient.ProcessEventQueue();
        }
    }
}
