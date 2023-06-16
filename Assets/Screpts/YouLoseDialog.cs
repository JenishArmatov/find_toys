using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouLoseDialog : MonoBehaviour
{
    private Animator animator;




    void Start()
    {
        animator = GetComponentInParent<Animator>();


    }
    public void RetryClick()
    {
        animator.Play("Close");
        Controller.isFirstItem = false;
        StartCoroutine(StartLevel());

    }
    public void ExitClick()
    {
        animator.Play("Close");
        animator = null;
        StartCoroutine(StartMenu());



    }
    void Update()
    {
        if (TimeController._timeLeft == 0)
        {
            AddTime.GameOver = true;
            FindToyController.click = true;
            animator.Play("New State");
            TimeController._timeLeft = -1;

        }
        else 
        {
            AddTime.GameOver = false;
            FindToyController.click = false;
        }

    }
    IEnumerator StartMenu()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);

    }
    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);

    }
}
