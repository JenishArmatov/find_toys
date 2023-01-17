using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject ExitDialog;
    public GameObject BackgroundPanel;
    private Animator animatorMenu;

    private void Start()
    {
        animatorMenu = GetComponent<Animator>();
        BackgroundPanel.GetComponent<Animator>().Play("StartMenu");
        animatorMenu.Play("Open");


    }
    public void OnClickStart() {
        animatorMenu.Play("Close");
        StartCoroutine(StartTheGame());


    }
    public void OnClickExit()
    {
        ExitDialog.SetActive(true);
        Debug.Log("OnClickExit" + ExitDialog.GetComponent<Transform>().transform.localScale.y);


    }

    public void OnClickExitYes()
    {
        Application.Quit();


    }
    public void OnClickExitNo()
    {
        ExitDialog.SetActive(false);


    }

    IEnumerator StartTheGame()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }

}
