using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmenterVitesse : MonoBehaviour
{
    public float duration = 180f;
    public Vector2 camVitesseMinEtMax = new Vector2(5f, 10f);
    public Vector2 snakeVitesseMinEtMax = new Vector2(.3f, .1f);

    float timer;
    CameraMovement cameraMovement;
    Snake snakeJ1, snakeJ2;


    // Start is called before the first frame update
    void Start()
    {
        cameraMovement = FindObjectOfType<CameraMovement>();

        snakeJ1 = GameObject.Find("Snake J1").GetComponent<Snake>();
        snakeJ2 = GameObject.Find("Snake J2").GetComponent<Snake>();

        cameraMovement.cameraSpeed = camVitesseMinEtMax.x;
        snakeJ1.moveSpeed = snakeJ2.moveSpeed = snakeVitesseMinEtMax.x;

    }

    // Update is called once per frame
    void Update()
    {
        if(timer < duration)
        {
            timer += Time.deltaTime;
        }

        cameraMovement.cameraSpeed = Mathf.Lerp(camVitesseMinEtMax.x, camVitesseMinEtMax.y, timer / duration);
        snakeJ1.moveSpeed = Mathf.Lerp(snakeVitesseMinEtMax.x, snakeVitesseMinEtMax.y, timer / duration);
        snakeJ2.moveSpeed = Mathf.Lerp(snakeVitesseMinEtMax.x, snakeVitesseMinEtMax.y, timer / duration);
    }
}
