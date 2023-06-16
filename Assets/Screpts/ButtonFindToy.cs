using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FindToyController : MonoBehaviour
{
    public Text text;
    public GameObject[] GameObjects;
    public GameObject plane;
    private GameObject g;
    private bool animMoveObject = false;
    public GameObject playstationPlane;
    private AudioSource audio;
    private Material material;
    private float oldScale;
    public int Score = 999;
    public static bool click = false;
    public ParticleSystem system;
    public ParticleSystem SmallBoom1;
    public ParticleSystem SmallBoom2;
    public void OnClick() {

        if (!click) {
            click = true;
            audio = plane.GetComponent<AudioSource>();
            MeshRenderer mesh = plane.GetComponent<MeshRenderer>();
            material = mesh.materials[0];
            text.text = Score + "";
            GameObjects = GameObject.FindGameObjectsWithTag("GameObjects");

            Debug.Log("OnClick//" + GameObjects.Length);
            if (Score > 0)
            {
                if (GameObjects != null)
                {
                    if (Controller.oldGameObject != null)
                    {
                        for (int i = 0; i < GameObjects.Length; i++)
                        {
                            if (Controller.oldGameObject.GetHashCode() != GameObjects[i].GetHashCode() &&
                                Controller.oldGameObject.name.Equals(GameObjects[i].name))
                            {
                                Debug.Log("OnClick// Find");


                                g = GameObjects[i].gameObject;
                                oldScale = g.transform.localScale.x;
                                animMoveObject = true;
                                break;

                            }

                        }
                    }
                    else {
                        click = false;
                    }

                   

                }
            }
        }


    }
    private void Update()
    {
        if (animMoveObject) {
            g.transform.position = Vector3.MoveTowards(
                                        g.transform.position, new Vector3(
                                            plane.transform.position.x - 1,
                                            plane.transform.position.y + 1.0f,
                                            plane.transform.position.z),
                                                Time.deltaTime * 40);
            if (g.transform.position.z == plane.transform.position.z)
            {
                animeDestroyd();
            }

        }

    }
    void Start()
    {

        Score = (int) PlayerPrefs.GetFloat(Controller.FindToysBoost, 0);

        audio = plane.GetComponent<AudioSource>();
        MeshRenderer mesh = plane.GetComponent<MeshRenderer>();
        material = mesh.materials[0];
        text.text = Score + "";


    }
    
    void animeDestroyd()
    {
        material.color = Color.green;

        SmallBoom1.transform.position = new Vector3(
            g.transform.position.x,
            g.transform.position.y,
            g.transform.position.z);
        SmallBoom2.transform.position = new Vector3(
            Controller.oldGameObject.transform.position.x,
            Controller.oldGameObject.transform.position.y,
            Controller.oldGameObject.transform.position.z);
        audio.Play();
        system.Play();
        SmallBoom1.Play();
        SmallBoom2.Play();

        Destroy(g);
        Destroy(Controller.oldGameObject);
        float score = PlayerPrefs.GetFloat(Controller.SCORE_STING) + 10;

        PlayerPrefs.SetFloat(Controller.SCORE_STING, score);
        Controller.SCORE_FLOAT = score;

        animMoveObject = false;
        material.color = Color.white;
        Controller.isFirstItem = false;
        Score--;
        text.text = Score + "";
        click = false;

    }
    
}
