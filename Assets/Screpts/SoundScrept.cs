using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScrept : MonoBehaviour
{
    public AudioSource sound;
    void Start()
    {
        sound.volume = PlayerPrefs.GetFloat("sound");   
    }


}
