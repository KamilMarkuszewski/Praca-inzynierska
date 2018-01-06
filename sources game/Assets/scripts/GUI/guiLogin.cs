using UnityEngine;

using System;
using System.Collections;         // for using hash tables
using System.Security.Permissions;  // for getting the socket policy
using SmartFoxClientAPI;            // to setup SmartFox connection
using SmartFoxClientAPI.Data;      // necessary to access the room resource
using System.Security.Cryptography;
using System.Text;


public class guiLogin : MonoBehaviour
{

    // smartFox variables
    private SmartFoxClient smartFox;
    private string serverIP = "10.0.0.1";
    private int serverPort = 9933;        // default = 9339
    public string zone = "city";
    public bool debug = true;
    public bool optionAble = false;

    // variables used in script
    private string statusMessage = "";
    private string titleMessage = "Logowanie: ";
    private string username = "";
    private string password = "";


    void Awake()
    {
        Application.runInBackground = true;     
        if (SmartFox.IsInitialized())
        {
            Debug.Log("SmartFox zostal zainicjalizowany, ale odrzuca polaczenie");
            smartFox = SmartFox.Connection;
        }
        else
        {
            if (Application.platform == RuntimePlatform.WindowsWebPlayer)
            {
                Security.PrefetchSocketPolicy(serverIP, serverPort, 3);
            }
            try
            {
                Debug.Log("Tworze nowego SmartFoxClient");
                smartFox = new SmartFoxClient(debug);
                smartFox.runInQueueMode = true;
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
        SFSEvent.onConnection += OnConnection;
        SFSEvent.onConnectionLost += OnConnectionLost;
        SFSEvent.onLogin += OnLogin;
        SFSEvent.onRoomListUpdate += OnRoomList;
        SFSEvent.onDebugMessage += OnDebugMessage;
        Debug.Log("Proba polaczenia z SmartFoxServer");
        smartFox.Connect(serverIP, serverPort);
    }

    void FixedUpdate()
    {
        smartFox.ProcessEventQueue();
    }

    static string GetMd5Hash(MD5 md5Hash, string input)
    {

        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }


    void OnGUI()
    {

        GUI.Box(new Rect(Screen.width / 2 - 250, 50, 500, 250), "");
        // server IP in bottom left corner
        GUI.Label(new Rect(25, Screen.height - 25, 200, 24), "Serwer: " + serverIP + " : " + serverPort);

        // quit button in bottom right corner
        if (Application.platform != RuntimePlatform.WindowsWebPlayer)
        {
            if (GUI.Button(new Rect(Screen.width / 2 + 125 - 25, 250, 100, 25), "Wyjdz"))
            {
                statusMessage = "Rozlaczono!";
                smartFox.Disconnect();
                UnregisterSFSSceneCallbacks();
                Application.Quit();
            }
        }
        // Show login fields if connected and reconnect button if disconnect 


        if (smartFox.IsConnected())
        {


            GUI.Label(new Rect(Screen.width / 2 - 100, 125, 50, 20), "Login: ");
            username = GUI.TextField(new Rect(Screen.width / 2 - 50, 125, 150, 20), username, 25);

            GUI.Label(new Rect(Screen.width / 2 - 100, 150, 50, 20), "Haslo: ");
            password = GUI.PasswordField(new Rect(Screen.width / 2 - 50, 150, 150, 20), password, "*"[0], 25);

            if (GUI.Button(new Rect(Screen.width / 2 - 50, 250, 100, 25), "Zaloguj") || (Event.current.type == EventType.keyDown && Event.current.character == '\n'))
            {
                if (password == "" || username == "")
                {
                    statusMessage = "Podaj login i haslo!";
                }
                else
                {
                    smartFox.Logout();
                    MD5 md5Hash = MD5.Create();

                    string hash = GetMd5Hash(md5Hash, password);

                    MyData.setLogin(username);
                    MyData.setPassword(hash);

                    smartFox.Login(zone, username, hash);
                }
            }

        }
        else
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 60, 200, 120, 25), "Polacz ponownie") || (Event.current.type == EventType.keyDown && Event.current.character == '\n'))
            {
                Awake();
            }
        }

        // Draw box for status messages, if one is given
        // Contains some logic to parse message of multiple lines if necessary
        if (titleMessage.Length > 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 250 + 10, 60, 70, 25), titleMessage);
        }
        if (statusMessage.Length > 0)
        {
            GUI.Label(new Rect(25, Screen.height - 59, 200, 24), statusMessage);
        }

    }

    private void UnregisterSFSSceneCallbacks()
    {
        // This should be called when switching scenes, so callbacks from the backend do not trigger code in this scene
        SFSEvent.onConnection -= OnConnection;
        SFSEvent.onConnectionLost -= OnConnectionLost;
        SFSEvent.onLogin -= OnLogin;
        SFSEvent.onRoomListUpdate -= OnRoomList;
        SFSEvent.onDebugMessage -= OnDebugMessage;
        //SFSEvent.onJoinRoom -= OnJoinRoom;
    }

    void OnConnection(bool success, string error)
    {
        if (success)
        {
            SmartFox.Connection = smartFox;
            statusMessage = "Polaczono z serwerem";
            Debug.Log(statusMessage);
        }
        else
        {
            statusMessage = "Blad polaczenia " + error;
            Debug.Log(statusMessage);
        }
    }

    void OnConnectionLost()
    {
        statusMessage = "Zerwano polaczenie";
    }

    public void OnDebugMessage(string message)
    {
        Debug.Log("[SFS DEBUG] " + message);
    }

    public void OnLogin(bool success, string name, string error)
    {

        if (success)
        {

            // Lets wait for the room list
        }
        else
        {
            // Login failed - lets display the error message sent to us
            statusMessage = "Login error: " + error;
        }
    }



    void OnRoomList(Hashtable roomList)
    {
        try
        {
            UnregisterSFSSceneCallbacks();
            Worlds.ChangeWorlds.authSceene();
        }
        catch (Exception e)
        {
            Debug.Log("Room list error: " + e.Message + " " + e.StackTrace);
        }
    }
}