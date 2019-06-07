using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
    public float timerMenu = 25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerMenu -= Time.deltaTime;

        if (timerMenu <= 0 || Input.GetButtonDown("Start"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
