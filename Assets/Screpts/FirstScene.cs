using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstScene : MonoBehaviour
{
    // Start is called before the first frame update
    public int SceneID;
    public Image image;
    void Start()
    {
        StartCoroutine(AsyncLoad());
    }


    IEnumerator AsyncLoad() { 
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneID);
        while (!operation.isDone) 
        {
            image.fillAmount = operation.progress;

            yield return null;
        }

    }
}
