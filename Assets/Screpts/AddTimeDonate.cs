using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTimeDonate : MonoBehaviour
{
    public Text text;
    private float Score;
    void Start()
    {
        Score = PlayerPrefs.GetFloat(Controller.AddTimeBoost, 0);
        text.text = "" + Score;
    }


}
