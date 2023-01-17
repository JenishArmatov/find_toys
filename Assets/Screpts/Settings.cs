using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    public float soundF;
    public float musicF;
    public GameObject Slidermusic;
    public GameObject SliderSound;
    public GameObject SettingsDialog;
    public GameObject MenuDialog;

    void Start()
    {
        soundF = PlayerPrefs.GetFloat("sound");
        musicF = PlayerPrefs.GetFloat("music");
        SliderSound.GetComponent<Slider>().value = soundF;
        Slidermusic.GetComponent<Slider>().value = musicF;

    }

    // Update is called once per frame
    void Update()
    {



    }
    public void OnClickSettings()
    {
        SettingsDialog.GetComponent<Animator>().Play("Settings open");
        MenuDialog.GetComponent<Animator>().Play("Close");

    }
    public void OnClickExit()
    {
        PlayerPrefs.SetFloat("sound", SliderSound.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("music", Slidermusic.GetComponent<Slider>().value);
        SettingsDialog.GetComponent<Animator>().Play("Settings close");
        MenuDialog.GetComponent<Animator>().Play("Open");

    }
}
