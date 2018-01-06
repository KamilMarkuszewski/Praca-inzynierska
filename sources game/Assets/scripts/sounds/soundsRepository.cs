using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class soundsRepository : MonoBehaviour
{
    public AudioClip swordSound;
    public float swordVolume = 1.0f;
    public AudioSource swordAudio;




    void Awake()
    {
        swordAudio = new AudioSource();
        swordAudio.loop = true;
        swordAudio.playOnAwake = true;
        swordAudio.clip = swordSound;
        swordAudio.volume = swordVolume;
    }

}

