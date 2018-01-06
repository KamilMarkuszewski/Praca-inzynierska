using UnityEngine;
using System.Collections;

using System;
using SmartFoxClientAPI;
using SmartFoxClientAPI.Data;

public class NetworkController : MonoBehaviour
{

    private static SmartFoxClient smartFoxClient;

    public static SmartFoxClient GetClient()
    {
        return SmartFox.Connection;
    }

    #region Events
    SFSObject data;
    private bool started = false;

    private void SubscribeEvents()
    {
        SFSEvent.onJoinRoom += OnJoinRoom;
        SFSEvent.onUserEnterRoom += OnUserEnterRoom;
        SFSEvent.onUserLeaveRoom += OnUserLeaveRoom;
        SFSEvent.onObjectReceived += OnObjectReceived;
        SFSEvent.onPublicMessage += OnPublicMessage;
        SFSEvent.onExtensionResponse += OnExtensionResponse;
        SFSEvent.onConnectionLost += OnConnectionLost;
    }

    private void UnsubscribeEvents()
    {
        SFSEvent.onJoinRoom -= OnJoinRoom;
        SFSEvent.onUserEnterRoom -= OnUserEnterRoom;
        SFSEvent.onUserLeaveRoom -= OnUserLeaveRoom;
        SFSEvent.onObjectReceived -= OnObjectReceived;
        SFSEvent.onPublicMessage -= OnPublicMessage;
        SFSEvent.onExtensionResponse -= OnExtensionResponse;
        SFSEvent.onConnectionLost -= OnConnectionLost;
    }

    void FixedUpdate()
    {
        if (started)
        {
            smartFoxClient.ProcessEventQueue();
        }
    }

    #endregion Events

    // We start working from here
    void Update()
    {
        if (MyData.reloged && MyData.world)
        {
            OnMeJoinRoom();
            MyData.reloged = false;
        }

    }

    void Start()
    {
        Application.runInBackground = true; // Let the application be running whyle the window is not active.
        smartFoxClient = GetClient();
        if (smartFoxClient == null)
        {
            Debug.Log("smartFoxClient null");
            Worlds.ChangeWorlds.loginSceene();
            return;
        }
        SubscribeEvents();
        started = true;
        smartFoxClient.JoinRoom("Central Square");
    }

    // We should unsubscribe all delegates before quitting the application to avoid probleems.
    // Also we should Disconnect from server
    void OnApplicationQuit()
    {
        UnsubscribeEvents();
        smartFoxClient.Disconnect();
    }


    private void OnJoinRoom(Room room)
    {
        OnMeJoinRoom();
    }

    private void OnMeJoinRoom()
    {
        SendMessage("SpawnPlayers");
        Debug.Log("Connected !");
        Objects.myData.setWorld(true);
        data = new SFSObject();
        data.Put("_cmd", "init");  //We put _cmd = "t" here to know that this object contains transform sync data. 
        data.Put("nick", MyData.getLogin());
        data.Put("id", Convert.ToString(Objects.myLocalPlayer.myIds.myCharId));

        smartFoxClient.SendObject(data);
    }

    // This will be invoked when remote player enters our room
    private void OnUserEnterRoom(int roomId, User user)
    {
        //We assume here that we have only one room - so no need to check roomId
        SendMessage("UserEnterRoom", user);
        data = new SFSObject();
        data.Put("_cmd", "init");
        data.Put("nick", MyData.getLogin());
        data.Put("id", Convert.ToString(Objects.myLocalPlayer.myIds.myCharId));

        smartFoxClient.SendObject(data);
    }

    // This will be invoked when a remote player lefts our room
    private void OnUserLeaveRoom(int roomId, int userId, string userName)
    {
        SendMessage("UserLeaveRoom", userId);
    }

    // Here we process incoming SFS objects
    private void OnObjectReceived(SFSObject data, User fromUser)
    {
        //First determine the type of this object - what it contains ?

        String _cmd = data.GetString("_cmd");
        switch (_cmd)
        {
            case "t":  // "t" - means transform sync data
                SendTransformToRemotePlayerObject(data, fromUser);
                break;

            case "f":  // "f" - means force our local player to send his transform
                ForceSendTransform(data);
                break;

            case "a": // "a" - for animation message received
                SendAnimationMessageToRemotePlayerObject(data, fromUser);
                break;

            case "init":
                SendInitRemotePlayer(data, fromUser);
                break;

            case "getNick":
                data = new SFSObject();
                data.Put("_cmd", "init");
                data.Put("nick", Objects.myLocalPlayer.myChar.name);
                data.Put("id", Convert.ToString(Objects.myLocalPlayer.myIds.myCharId));

                smartFoxClient.SendObject(data);
                break;

        }
    }
    private void SendInitRemotePlayer(SFSObject data, User fromUser)
    {
        int userId = fromUser.GetId();
        if (userId != smartFoxClient.myUserId)
        {  // If it's not myself
            //Find user object with such Id
            GameObject user = GameObject.Find("remote_" + userId);

            //If found - send him message with transform data
            if (user) user.SendMessage("SetLogin", data);
        }
    }


    private void SendTransformToRemotePlayerObject(SFSObject data, User fromUser)
    {
        int userId = fromUser.GetId();
        if (userId != smartFoxClient.myUserId)
        {  // If it's not myself
            //Find user object with such Id
            GameObject user = GameObject.Find("remote_" + userId);

            //If found - send him message with transform data
            if (user) user.SendMessage("ReceiveTransform", data);
        }
    }

    private void ForceSendTransform(SFSObject data)
    {
        //if this message is addressed to this user
        if ((int)data.GetNumber("to_uid") == smartFoxClient.myUserId)
        {
            // Find local player object
            GameObject user = GameObject.Find("localPlayer");
            // Send him message
            if (user) user.SendMessage("ForceSendTransform");
        }
    }

    private void SendAnimationMessageToRemotePlayerObject(SFSObject data, User fromUser)
    {
        int userId = fromUser.GetId();
        if (userId != smartFoxClient.myUserId)
        {  // If it's not myself
            //Find user object with such Id
            GameObject user = GameObject.Find("remote_" + userId);
            //If found - send him animation message
            if (user) user.SendMessage("PlayAnimation", data.GetString("mes"));
        }
    }


    public void OnPublicMessage(string message, User fromUser, int roomId)
    {
        int userId = fromUser.GetId();
        if (userId != smartFoxClient.myUserId)
        {  // If it's not myself
            string mes = fromUser.GetName() + ": " + message;

            // Send chat message to the Chat Controller			
            SendMessage("AddChatMessage", mes);

            //Find user object with such Id
            GameObject user = GameObject.Find("remote_" + userId);
            //If found - send him bubble message
            if (user)
            {
                user.SendMessage("ShowBubble", mes);
            }
        }
    }

    public void OnExtensionResponse(object data, string type)
    {
        SFSObject responseData = (SFSObject)data;
        String _cmd = responseData.GetString("_cmd");
        switch (_cmd)
        {
            case "AtkCharacter":
                DatabaseAccess.Battle.loadAtack(responseData);
                break;
            case "characterDied":
                Debug.Log("characterDied");
                DatabaseAccess.Battle.loadDied(responseData);
                break;
        }

    }

    void OnConnectionLost()
    {
        ControllConnection.backToLogin();
    }

}
