using UnityEngine;
using System.Collections;

using SmartFoxClientAPI;
using SmartFoxClientAPI.Data;

// We use this class here to store and work with transform states
public class NetworkTransform
{

    public Vector3 position;
    public Quaternion rotation;
    private GameObject obj;
    public int hp = 100;
    public int max_hp = 100;
    public bool died = false;

    public int last_hp = 100;
    public bool last_died = false;


    public NetworkTransform(GameObject obj)
    {
        this.obj = obj;
        InitFromCurrent();
    }

    public void setHP(int _hp, int _max_hp)
    {
        this.last_hp = hp;
        hp = _hp;
        max_hp = _max_hp;
    }

    public void setDied(bool die)
    {
        this.last_died = died;
        died = die;
    }

    // Updates last state to the current transform state if the current state was changed and return true if so or false if not
    public bool UpdateIfDifferent()
    {
        if (obj.transform.position != this.position || obj.transform.rotation != this.rotation || this.last_hp != this.hp || this.last_died != this.died)
        {
            InitFromCurrent();
            return true;
        }
        else
        {
            return false;
        }
    }

    // Send transform to all other users
    public void DoSend()
    {

        SmartFoxClient client = NetworkController.GetClient();
        SFSObject data = new SFSObject();


        data.Put("_cmd", "t");  //We put _cmd = "t" here to know that this object contains transform sync data. 
        data.Put("x", this.position.x);
        data.Put("y", this.position.y);
        data.Put("z", this.position.z);

        data.Put("rx", this.rotation.x);
        data.Put("ry", this.rotation.y);
        data.Put("rz", this.rotation.z);
        data.Put("w", this.rotation.w);
        data.Put("hp", this.hp);
        data.Put("max_hp", this.max_hp);
        data.Put("died", this.died);
        // We send data using SendObject method here. To optimize this you can use SendXtMessage method with custum formatted method
        // Also an extension on the server side could decide which users really need to receive the transform



        client.SendObject(data);

    }

    public void InitFromValues(Vector3 pos, Quaternion rot)
    {
        this.position = pos;
        this.rotation = rot;
    }

    // To compare with Unity transform and itself
    public override bool Equals(System.Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        Transform t = obj as Transform;
        NetworkTransform n = obj as NetworkTransform;

        if (t != null)
        {
            return (t.position == this.position && t.rotation == this.rotation);
        }
        else if (n != null)
        {
            return (n.position == this.position && n.rotation == this.rotation);
        }
        else
        {
            return false;
        }
    }


    private void InitFromCurrent()
    {
        this.position = obj.transform.position;
        this.rotation = obj.transform.rotation;
    }

    private void InitFromGiven(Transform trans)
    {
        this.position = trans.position;
        this.rotation = trans.rotation;
    }

}

