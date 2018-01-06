using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


class putChooseChar : MonoBehaviour
{
    UnityEngine.Object charNoneObj;
    public Transform charNonePrefab;
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
        charNoneObj = GameObject.Find("displayCharacterNone(Clone)");
        if (charNoneObj != null) Destroy(charNoneObj);
    }

    public void putcharNone()
    {
        charNoneObj = Instantiate(charNonePrefab, new Vector3(srodek.x, srodek.y, srodek.z), new Quaternion(0, 0, 0, 1));
        anim = GameObject.Find("displayCharacterNone(Clone)").GetComponent(typeof(changeAnimation)) as changeAnimation;
        anim.reload();
    }

    void Update()
    {
        if (anim != null)
        {
            anim.myUpdate();
        }
        else {
            if (charNoneObj != null) {
                anim = GameObject.Find("displayCharacterNone(Clone)").GetComponent(typeof(changeAnimation)) as changeAnimation;
                anim.reload();
            }
        }

    }

}

