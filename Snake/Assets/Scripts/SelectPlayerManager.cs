using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class SelectPlayerManager : MonoBehaviour
{
    public Text timerUI;

    public float timer = 10;

    public int sceneIndex;

    public GameObject pannelTransition;
    public float transitionTime = 1f;

        
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        timerUI.text = timer.ToString("F0");

        if (timer <= 0)
        {
            AudioManager.instance.Play("Validation");
            pannelTransition.SetActive(true);
            Invoke("ChangeScene", transitionTime);
        }

        if (Input.GetButtonDown("Start") || Input.GetButtonDown("Start_P2") && sceneIndex == 2)
        {
            AudioManager.instance.Play("Validation");
            pannelTransition.SetActive(true);
            Invoke("AnotherPlayerJoined", transitionTime);
        }

        if (Input.GetButtonDown("Start_P2") && sceneIndex == 3)
        {
            AudioManager.instance.Play("Validation");
            pannelTransition.SetActive(true);
            Invoke("ChangeScene", transitionTime);
        }
    }

    public void AnotherPlayerJoined()
    {
        pannelTransition.SetActive(true);
        SceneManager.LoadScene("Select2Players");

        PlayerPrefs.SetInt("NbJoueurs", 2);

    }

    void ChangeScene()
    {
        SceneManager.LoadScene("test grid 1");
    }
}
