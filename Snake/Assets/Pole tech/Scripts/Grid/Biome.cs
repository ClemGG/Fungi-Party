using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour
{
    [Space(10)]
    [Header("Scripts & Components : ")]
    [Space(10)]
    
    public Case[] biomeCases;   //Les cases qui seront comprises dans ce biome
    List<Case> géluleCases;   //Toutes les cases de ce biome qui sont censées contenir des gélules
    public Case caseStartJ1, caseStartJ2; //Les cases de départ du joueur dans ce biome


    //private void OnValidate()
    //{
    //    for (int i = 0; i < biomeCases.Length; i++)
    //    {
    //        biomeCases[i].ChangerCaseConfiguration();
    //    }
    //}

        //Penser à remplir biomeCases avec les cases de gélule avant le début d'une partie
    private void Awake()
    {
        géluleCases = new List<Case>();

        for (int i = 0; i < biomeCases.Length; i++)
        {
            if (biomeCases[i])
            {
                if (biomeCases[i].caseType == Case.CaseType.Gélule)
                {
                    géluleCases.Add(biomeCases[i]);
                }
            }
        }
    }

    public void OnEnable()
    {
        for (int i = 0; i < biomeCases.Length; i++)
        {
            if(biomeCases[i])
            biomeCases[i].gameObject.SetActive(true);
        }
    }

    public void OnDisable()
    {
        for (int i = 0; i < biomeCases.Length; i++)
        {
            if (biomeCases[i])
            {
                if(biomeCases[i].caseType != Case.CaseType.SnakeHead)
                biomeCases[i].gameObject.SetActive(false);
            }
        }
    }



    public void SpawnObjects()
    {

        //print(géluleCases.Count);
        
        for (int i = 0; i < géluleCases.Count; i++)
        {
            if (biomeCases[i])
            {
                if (géluleCases[i].caseType == Case.CaseType.TerrainNavigable)
                {
                    géluleCases[i].caseType = Case.CaseType.Gélule;
                    géluleCases[i].ChangerCaseConfiguration();
                }
            }


        }
    }
}
