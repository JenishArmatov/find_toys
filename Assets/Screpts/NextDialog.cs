using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextDialog : MonoBehaviour
{
    private GameObject[] gameObjects;
    private Animator animator;
    private bool StopAnimate = false;
    public ParticleSystem VictoryClapLeft;
    public ParticleSystem VictoryClapRight;
    public AudioSource victorySound;
    public Text _scoreText;
    private float _scoreFloat;

    private void Start()
    {
        _scoreFloat = PlayerPrefs.GetFloat(Controller.SCORE_STING);
        Controller.SCORE_FLOAT = _scoreFloat;

    }
    public void Update()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("GameObjects");
        _scoreText.text = "Score: " + PlayerPrefs.GetFloat(Controller.SCORE_STING);
        if (gameObjects != null && !StopAnimate)
        {
            if (gameObjects.Length == 0)
            {
                victorySound.Play();
                StopAnimate = true;

                VictoryClapLeft.Play();
                VictoryClapRight.Play();
                AddTime.GameOver = true;
                FindToyController.click = true;
                TimeController._timerOn = false;
                StopAnimate = true;
                StartCoroutine(EnamStartDialogVictory());


            }
            else 
            {
                AddTime.GameOver = false;
                FindToyController.click = false;
            }

        }
    }
    public void OnClick() 
    {


        PlayerPrefs.SetInt("unlockLevels", PlayerPrefs.GetInt("unlockLevels") + 1);

        animator = GetComponentInParent<Animator>();

        animator.Play("Close");
        StartCoroutine(StartNextScene());
    }

    public void ExitClick()
    {
        animator = GetComponentInParent<Animator>();

        animator.Play("Close");
        StartCoroutine(StartMenu());




    }
    IEnumerator StartMenu()
    {

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);

    }
    IEnumerator StartNextScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);

    }
    IEnumerator EnamStartDialogVictory()
    {
        yield return new WaitForSeconds(2.0f);
        StartDialogVictory();

    }
    private void StartDialogVictory() 
    {
        animator = GetComponentInParent<Animator>();
        animator.Play("New State");

    }
}
