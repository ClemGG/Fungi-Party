using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    [Space(10)]
    [Header("Scripts & Components : ")]
    [Space(10)]

    Transform t;
    SpriteRenderer sr;
    Animator a;

    [Space(10)]
    [Header("Case Settings : ")]
    [Space(10)]

    [HideInInspector] public Vector2 pos;
    [HideInInspector] public enum CaseType { Rien, Obstacle, LimiteTerrain, TerrainNavigable, Gélule, Snake, SnakeHead };
    public CaseType caseType;

    [HideInInspector] public AnimName caseAnim;
    [HideInInspector] public bool isVisible = true;

    public Sprite géluleSprite;

    //private void OnValidate()
    //{
    //    if (!GetComponent<Animator>())
    //    {
    //        gameObject.AddComponent<Animator>();
    //    }

    //    ChangerCaseConfiguration();
    //}


    public void ChangerCaseConfiguration()
    {
        if (!sr)
        {
            sr = GetComponent<SpriteRenderer>();
        }

        if (!a)
        {
            a = GetComponent<Animator>();
        }

        switch (caseType)
        {
            case CaseType.Rien:
                a.enabled = false;
                sr.enabled = false;
                break;

            case CaseType.Obstacle:
                a.enabled = false;
                sr.enabled = true;
                
                break;

            case CaseType.LimiteTerrain:
                a.enabled = false;
                sr.enabled = true;
                
                break;

            case CaseType.TerrainNavigable:
                a.enabled = false;
                sr.enabled = true;
                break;

            case CaseType.Gélule:
                a.enabled = true;
                sr.enabled = true;

                sr.sprite = géluleSprite;

                break;

            case CaseType.Snake:
                a.enabled = true;
                sr.enabled = true;
                caseAnim = SnakeManager.instance.GetSnakeConfiguration(this);
                break;

            case CaseType.SnakeHead:
                a.enabled = true;
                sr.enabled = true;
                caseAnim = SnakeManager.instance.GetSnakeHeadConfiguration(this);
                break;


            default:
                Debug.Log($"La fonction {caseType} n'a pas encore été implémentée");
                break;
        }

        if (a)
        {
            if(a.enabled)
                a.Play(caseAnim.animName);
        }

    }

    private void OnBecameInvisible()
    {
        if(caseType == CaseType.SnakeHead)
        {
            isVisible = false;
        }
    }

}
