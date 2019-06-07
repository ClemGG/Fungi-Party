using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int nbObjetsRamassablesParBiome;
    public AnimName[] animsAJouerParTypeDeCase;

    [Space(20)]

    public Case[] toutesLesCases;



    public static Grid instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
            return;
        }
        instance = this;

        toutesLesCases = FindObjectsOfType<Case>();

        for (int i = 0; i < toutesLesCases.Length; i++)
        {
            toutesLesCases[i].pos = toutesLesCases[i].transform.position;
        }
    }


    private void Start()
    {
        toutesLesCases = FindObjectsOfType<Case>();
    }

}
