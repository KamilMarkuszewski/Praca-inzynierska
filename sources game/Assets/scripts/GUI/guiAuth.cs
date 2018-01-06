using UnityEngine;
using System.Collections;

using System;
using SmartFoxClientAPI;
using SmartFoxClientAPI.Data;


public class guiAuth : MonoBehaviour
{
    private static string login = "";
    private static string hash = "";
    private bool started = false;
    private static SmartFoxClient smartFoxClient;
    private static int auth = -1;

    public static SmartFoxClient GetClient()
    {
        return SmartFox.Connection;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SubscribeEvents()
    {
        SFSEvent.onObjectReceived += OnObjectReceived;
        SFSEvent.onPublicMessage += OnPublicMessage;
        SFSEvent.onExtensionResponse += OnExtensionResponse;
        SFSEvent.onRoomListUpdate += OnRoomList;

    }

    private void UnsubscribeEvents()
    {

        SFSEvent.onObjectReceived -= OnObjectReceived;
        SFSEvent.onPublicMessage -= OnPublicMessage;
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

    public void OnExtensionResponse(object data, string type)
    {
        auth = DatabaseAccess.LoadingChars.getAuth(smartFoxClient, data, type);
        if (auth == 1)
        {
            Worlds.ChangeWorlds.choseSceene();
        }
        else if (auth == 0)
        {
            Worlds.ChangeWorlds.loginSceene();
        }
        else
        {
            
        }

    }



    public void OnPublicMessage(string message, User fromUser, int roomId)
    {

    }


    void Start()
    {
        smartFoxClient = GetClient();

        if (smartFoxClient == null)
        {
            Debug.Log("smartFoxClient null");
            Worlds.ChangeWorlds.loginSceene();
            return;
        }
        SubscribeEvents();
        started = true;
        smartFoxClient.JoinRoom("Auth");
        login = MyData.getLogin();
        hash = MyData.getPassword();

        DatabaseAccess.LoadingChars.sendAuth(smartFoxClient, login, hash);
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
            // Users always have to be in a room, but we'll do that in the next level
            /*
            if (smartFox.GetActiveRoom() == null) {
                smartFox.JoinRoom("Central Square");
            }*/
            UnsubscribeEvents();
            smartFoxClient.LeaveRoom(smartFoxClient.activeRoomId);
            if (auth == 1)
            {
                Worlds.ChangeWorlds.choseSceene();
            }
            else
            {
                if (auth == 0)
                {
                    Worlds.ChangeWorlds.loginSceene();
                }
                else
                {

                }
            }

        }
        catch (Exception e)
        {
            Debug.Log("Room list error: " + e.Message + " " + e.StackTrace);
        }
    }

}
