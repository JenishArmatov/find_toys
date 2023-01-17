using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTime : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer = TimeController._timeLeft;
    public Text AddTimeScore;
    public int Score = 10;
    public static bool GameOver = false;
    private void Start()
    {
        AddTimeScore.text = Score + "";

    }
    public void OnClick() {
        if (Score > 0 && !GameOver)
        {
            TimeController._timeLeft = TimeController._timeLeft + 30;
            Score--;
            AddTimeScore.text = Score + "";

        }
    }
}
