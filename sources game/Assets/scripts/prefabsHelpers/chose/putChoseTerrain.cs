using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


class putChoseTerrain : MonoBehaviour
{
    UnityEngine.Object neutralObj;
    public Transform neutralPrefab;

    UnityEngine.Object evilObj;
    public Transform evilPrefab;

    UnityEngine.Object saintObj;
    public Transform saintPrefab;

    Vector3 srodek;
    changeAnimation anim;

    void Start()
    {
        srodek = new Vector3(3.5f, -5.0f, -7.2f);
    }

    public void clear()
    {
        destroyPrefabs();
    }

    private void destroyPrefabs()
    {
        neutralObj = GameObject.Find("Neutral(Clone)");
        if (neutralObj != null) Destroy(neutralObj);

        evilObj = GameObject.Find("Evil(Clone)");
        if (evilObj != null) Destroy(evilObj);

        saintObj = GameObject.Find("Saint(Clone)");
        if (saintObj != null) Destroy(saintObj);
    }

    public void putNeutral()
    {
        neutralObj = Instantiate(neutralPrefab, new Vector3(srodek.x, srodek.y, srodek.z), new Quaternion(0, 0, 0, 1));
    }

    public void putEvil()
    {
        evilObj = Instantiate(evilPrefab, new Vector3(srodek.x, srodek.y, srodek.z), new Quaternion(0, 0, 0, 1));
    }

    public void putSaint()
    {
        saintObj = Instantiate(saintPrefab, new Vector3(srodek.x, srodek.y, srodek.z), new Quaternion(0, 0, 0, 1));
    }
}

