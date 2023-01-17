using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindToyDonate : MonoBehaviour
{
    public Text text;
    void Start()
    {
        text.text = "" + PlayerPrefs.GetFloat(Controller.FindToysBoost, 0);
    }


}
