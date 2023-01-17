using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScrept : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource Music;
    void Start()
    {
        Music.volume = PlayerPrefs.GetFloat("music");
    }


}
