using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public Text scoreTextJ1;
    public Text scoreTextJ2;

    private int scoreJ1;
    private int scoreJ2;

    public int valueToWin;

    public GameObject pannelButtonP1;
    public GameObject pannelButtonP2;


    public GameObject pannelResult1Player;
    public GameObject pannelResult2Players;
    public Text textUISolo;
    public Text textUIP1;
    public Text textUIP2;

    public Text timerUI1Player;
    public Text timerUI2Players;

    public float timer = 10;

    public GameObject pannelTransition;
    public float transitionTime = 1f;

    public bool partieGagnée = false;

    public static ScoreManager instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
            return;
        }

        instance = this;
       // pannelResult.SetActive(false);

    }

    void Start()
    {

        //Si deux joueurs
        if (pannelButtonP2)
        {
            pannelButtonP2.SetActive(true);
        }

        if(PlayerPrefs.GetInt("NbJoueurs") != 2)
        {
            pannelButtonP2.SetActive(false);
        }

    }

    void Update()
    {
        if (pannelResult1Player)
        {
            if (pannelResult1Player.activeSelf && timer > 0)
            {
                timer -= Time.deltaTime;
                timerUI1Player.text = "RETURN TO MENU IN " + timer.ToString("F0") + "...";
            }
        }

        if (pannelResult2Players)
        {
            if (pannelResult2Players.activeSelf && timer > 0)
            {
                timer -= Time.deltaTime;
                timerUI2Players.text = "RETURN TO MENU IN " + timer.ToString("F0") + "...";
            }
        }

        if (timer <= 0 || pannelResult2Players.activeSelf && Input.GetButtonDown("Start") || pannelResult1Player.activeSelf && Input.GetButtonDown("Start"))
        {
            pannelTransition.SetActive(true);
            Invoke("ChangeScene", transitionTime);
        }
    }

    public void KillPlayer(int id)
    {
        AudioManager.instance.Play("HitP" + id);
        AudioManager.instance.Stop("JeuMusic");

        //Solo
        if (id == 1 && PlayerPrefs.GetInt("NbJoueurs") != 2)
        {
            if (pannelResult1Player)
            {
                AudioManager.instance.Stop("MusicGame");
                AudioManager.instance.Play("GameOver");
                pannelResult1Player.SetActive(true);
                textUISolo.text = "GAME OVER";
                partieGagnée = true;
            }
        }


        //Multi
        if (id == 1 && PlayerPrefs.GetInt("NbJoueurs") == 2)
        {
            if (pannelResult2Players)
            {
                AudioManager.instance.Play("Victoire");
                pannelResult2Players.SetActive(true);
                textUIP1.text = "PLAYER 1 LOSES...!";
                textUIP2.text = "PLAYER 2 WINS !";
                partieGagnée = true;
            }

        }

        if (id == 2 && PlayerPrefs.GetInt("NbJoueurs") == 2)
        {
            if (pannelResult2Players)
            {
                AudioManager.instance.Play("Victoire");
                pannelResult2Players.SetActive(true);
                textUIP1.text = "PLAYER 1 WINS !";
                textUIP2.text = "PLAYER 2 LOSES...";
                partieGagnée = true;
            }

        }
    }

    public void AddPoint(int id)
    {

        if (id == 1)
        {
            AudioManager.instance.Play("PowerUp");
            Debug.Log("Score 1 added");
            scoreJ1++;
        }
        else
        {
            AudioManager.instance.Play("PowerUp");
            Debug.Log("Score 2 added");
            scoreJ2++;
        }

        if(scoreTextJ1)
        scoreTextJ1.text = ("ScoreJ1 : " + scoreJ1 + " / " + valueToWin);
        if(scoreTextJ2)
        scoreTextJ2.text = ("ScoreJ2 : " + scoreJ2 + " / " + valueToWin);

        CheckWinCondition();
    }

    public void CheckWinCondition()
    {
        //Solo
        if (scoreJ1 >= valueToWin)
        {
            if (pannelResult1Player && PlayerPrefs.GetInt("NbJoueurs") != 2)
            {
                AudioManager.instance.Play("Victoire");
                pannelResult1Player.SetActive(true);
                textUISolo.text = "YOU WIN !";
                partieGagnée = true;
            }
        }


        //Multi
        if (scoreJ1 >= valueToWin)
        {
            if (pannelResult2Players && PlayerPrefs.GetInt("NbJoueurs") == 2)
            {
                AudioManager.instance.Play("Victoire");
                pannelResult2Players.SetActive(true);
                textUIP1.text = "PLAYER 1 WINS !";
                textUIP2.text = "PLAYER 2 LOSES...";
                partieGagnée = true;
            }

        }

        if (scoreJ2 >= valueToWin)
        {
            if (pannelResult2Players && PlayerPrefs.GetInt("NbJoueurs") == 2)
            {
                AudioManager.instance.Play("Victoire");
                pannelResult2Players.SetActive(true);
                textUIP2.text = "PLAYER 2 WINS !";
                textUIP1.text = "PLAYER 1 LOSES !";
                partieGagnée = true;
            }

        }
    }

    void ChangeScene()
    {

        SceneManager.LoadScene("MainMenu");
    }

}
