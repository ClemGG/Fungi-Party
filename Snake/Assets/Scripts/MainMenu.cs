using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    private AudioSource audioSource;
    public EventSystem eventSystem;

    public GameObject pressStartBtn;

    public GameObject pannelTransition;
    public float transitionTime = 1f;

    public float timerCredit = 30f;


    void Start()
    {
        //audioSource.PlayOneShot(music, 1f);
        pressStartBtn.SetActive(true);
        PlayerPrefs.SetInt("NbJoueurs", 0);
    }

    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            AudioManager.instance.Play("Validation");
            pannelTransition.SetActive(true);
            PressStart();
        }

        if (Input.GetButtonDown("Credits"))
        {
            AudioManager.instance.Play("Validation");
            pannelTransition.SetActive(true);
            Invoke("Credits", transitionTime);
        }

        //Comme dans les jeux de naguère, les crédits apparaissent si on ne fait rien sur l'écran titre
        if (timerCredit > 0)
        timerCredit -= Time.deltaTime;

        if (timerCredit <= 0)
        {
            pannelTransition.SetActive(true);
            Invoke("Credits", transitionTime);
        }
    }

    public void PressStart()
    {
        //audioSource.PlayOneShot(soundSelection, 1f);
        pressStartBtn.SetActive(false);
        Invoke("Select1Player", transitionTime);
    }

    public void Select1Player()
    {
        //audioSource.PlayOneShot(soundSelection, 1f);
        SceneManager.LoadScene("Select1Player");
    }

    public void Select2Players()
    {
        //audioSource.PlayOneShot(soundSelection, 1f);
        SceneManager.LoadScene("Select2Players");
    }

    public void Credits()
    {
        //audioSource.PlayOneShot(soundSelection, 1f);
        SceneManager.LoadScene("Credits");
    }
}
