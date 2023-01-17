
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] public float time;
    [SerializeField] private Text timerText;

    public static float _timeLeft = -1f;
    public static bool _timerOn = false;

    public Image VisualTimer;
    private float r = 0;
    private Animator animateTimerColor;


    private void Start()
    {
        _timeLeft = time;
        _timerOn = true;
        animateTimerColor = GetComponent<Animator>();



    }

    private void Update()
    {
        if (_timerOn)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                UpdateTimeText();
            }
            else
            {
                _timeLeft = time;
                _timerOn = false;
            }
        }
        if (r > 0.99f)
        {
            r = 0;
        }
        r++;

    }


    private void UpdateTimeText()
    {
        if (_timeLeft < 0)
            _timeLeft = 0;

        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        float totalTime = minutes * 60 + seconds;

        VisualTimer.GetComponent<Image>().fillAmount = 1 - (time-totalTime) * ((1000/time)/1000);

        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        if (minutes <= 0 && seconds < 15) 
        {
            animateTimerColor.Play("New State");



        }
    }
}
