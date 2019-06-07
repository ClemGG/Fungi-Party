using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{


    public Snake snakeJ1, snakeJ2;
    public Case caseStartJ1, caseStartJ2;
    public AnimName[] animationsJ1, animationsJ2;



    public static SnakeManager instance;


    private void Start()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;

        snakeJ1 = GameObject.Find("Snake J1").GetComponent<Snake>();
        snakeJ2 = GameObject.Find("Snake J2").GetComponent<Snake>();

        CameraMovement cameraMovement = FindObjectOfType<CameraMovement>();

        caseStartJ1 = cameraMovement.currentBiome.caseStartJ1;
        caseStartJ2 = cameraMovement.currentBiome.caseStartJ2;

        snakeJ1.body.Add(caseStartJ1);
        snakeJ1.bodyDirections.Add(Direction.Condition.Up);

        if (PlayerPrefs.GetInt("NbJoueurs") == 2)
        {
            snakeJ2.body.Add(caseStartJ2);
            snakeJ2.bodyDirections.Add(Direction.Condition.Up);
        }

        caseStartJ1.ChangerCaseConfiguration();

        if (PlayerPrefs.GetInt("NbJoueurs") == 2)
            caseStartJ2.ChangerCaseConfiguration();

        snakeJ1.SetupSnake();

        if (PlayerPrefs.GetInt("NbJoueurs") == 2)
        snakeJ2.SetupSnake();

        if (PlayerPrefs.GetInt("NbJoueurs") != 2)
        {
            Destroy(snakeJ2.gameObject);
            Destroy(caseStartJ2.gameObject);
        }


        cameraMovement.currentBiome.caseStartJ1 = null;
        cameraMovement.currentBiome.caseStartJ2 = null;
    }





    public AnimName GetSnakeConfiguration(Case @case)
    {
        bool caseHasBeenFound = false;
        Snake joueur = null;
        Direction.Condition orientationDeLaCase;

        while (!caseHasBeenFound)
        {
            for (int i = 0; i < snakeJ1.body.Count; i++)
            {
                if (snakeJ1.body.Contains(@case))
                {
                    caseHasBeenFound = true;
                    orientationDeLaCase = snakeJ1.bodyDirections[i];
                    joueur = snakeJ1;
                    break;
                }

                if (snakeJ2.body.Contains(@case))
                {
                    caseHasBeenFound = true;
                    orientationDeLaCase = snakeJ2.bodyDirections[i];
                    joueur = snakeJ2;
                    break;
                }
            }
        }

        if (caseHasBeenFound)
        {
            //print(joueur.name);

            List<Direction.Condition> configsJoueur = joueur.bodyDirections;
            AnimName[] animsToUse = joueur.id == snakeJ1.id ? animationsJ1 : animationsJ2;


            for (int i = 0; i < configsJoueur.Count; i++)
            {
                switch (configsJoueur[i])
                {
                    case Direction.Condition.Up:
                        return animsToUse[0];
                    case Direction.Condition.Down:
                        return animsToUse[1];
                    case Direction.Condition.Left:
                        return animsToUse[2];
                    case Direction.Condition.Right:
                        return animsToUse[3];
                }


            }

        }


        Debug.LogError("Erreur : La configuration n'a pas été trouvée.");
        return null;



    }

    public AnimName GetSnakeHeadConfiguration(Case @case)
    {
        bool caseHasBeenFound = false;
        Snake joueur = null;
        Direction.Condition orientationDeLaCase = Direction.Condition.Up;

        while (!caseHasBeenFound)
        {
            for (int i = 0; i < snakeJ1.body.Count; i++)
            {
                if (snakeJ1.body.Contains(@case))
                {
                    caseHasBeenFound = true;
                    orientationDeLaCase = snakeJ1.bodyDirections[0];
                    joueur = snakeJ1;
                    break;
                }

                if (snakeJ2.body.Contains(@case))
                {
                    caseHasBeenFound = true;
                    orientationDeLaCase = snakeJ2.bodyDirections[0];
                    joueur = snakeJ2;
                    break;
                }
            }
        }

        if (caseHasBeenFound)
        {
            //print(joueur.name);
            
            AnimName[] animsToUse = joueur.id == snakeJ1.id ? animationsJ1 : animationsJ2;

            
                switch (orientationDeLaCase)
                {
                    case Direction.Condition.Up:
                        return animsToUse[0];
                    case Direction.Condition.Down:
                        return animsToUse[1];
                    case Direction.Condition.Left:
                        return animsToUse[2];
                    case Direction.Condition.Right:
                        return animsToUse[3];
                }
            

        }


        Debug.LogError("Erreur : La configuration n'a pas été trouvée.");
        return null;



    }
}
