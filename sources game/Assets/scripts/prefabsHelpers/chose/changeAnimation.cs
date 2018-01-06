using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


class changeAnimation : MonoBehaviour
{
    Animation anim;
    float timer;
    int los = 0;
    bool play = false;
    bool shortAnim = false;

    void Start()
    {

    }

    public void reload()
    {
        load();
        timer = Time.time;
        anim.CrossFade("idle");
    }

    public void load()
    {
        anim = GameObject.Find("person").GetComponent(typeof(Animation)) as Animation;
    }

    public void myUpdate()
    {

        if (los > 5) los = 0;

        if (timer + 2 < Time.time || (timer + 0.4 < Time.time && shortAnim == true))
        {
            load();
            shortAnim = false;
            timer = Time.time;
            play = true;
        }
        transform.Rotate(Vector3.up * Time.deltaTime * 25);
        if (play == true)
        {

            switch (los)
            {
                case 0:
                    {
                        anim.Play("walk");
                        break;
                    }
                case 1:
                    {
                        anim.Play("jump_pose");
                        shortAnim = true;
                        break;
                    }
                case 2:
                    {
                        anim.Play("run");
                        break;
                    }
                case 3:
                    {
                        anim.Play("jump_pose");
                        shortAnim = true;
                        break;
                    }
                case 4:
                    {
                        anim.Play("walk");
                        break;
                    }
                case 5:
                    {
                        anim.Play("idle");
                        break;
                    }

            }
            los++;
            play = false;
        }

    }

}

