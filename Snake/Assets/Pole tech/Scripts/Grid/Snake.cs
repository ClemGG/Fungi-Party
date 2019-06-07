using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    [Space(10)]
    [Header("Scripts & Components : ")]
    [Space(10)]

    Case startCase; //Remplie par le SnakeConfiguration
    Joystick joystick;

    [Space(10)]
    [Header("Snake Settings : ")]
    [Space(10)]
    
    public int id, startSize = 0;
    public float moveSpeed = .5f;
    float timer;

    [Space(10)]
    [Header("Power-ups : ")]
    [Space(10)]

    public float powerupCooldown = 10f;
    float cooldownTimer;

    public float dashDuration = .1f, dashSpeed = .03f; //NormalSpeed sera calquée sur la vitesse du joueur avant le dash
    public float invincibilityDuration = 10f;
    public float aimantDuration = 10f;

    float dashTimer, invincibilityTimer, aimantTimer;

    bool isDead = false, isInvincible = false, isDashing = false, isBeyondCamera = false, aimant = false;


    [Space(10)]
    

    public List<Case> body;
    public List<Direction.Condition> bodyDirections;


    [Space(10)]
    [Header("UIs : ")]
    [Space(10)]

    public Image dashSlot;
    public Image invincibilitySlot;
    public Sprite dashSprite1, dashSprite2, invincibilitySprite1, invincibilitySprite2;



    Vector2 posi;

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(posi, .1f);
    }

#endif

    public void SetupSnake()
    {
        for (int i = 0; i < startSize; i++)
        {
            AjouterNouvelleCaseAuCorps(bodyDirections[bodyDirections.Count - 1]); //On ajoute une nouvelle case après la dernière case avec la même orientation que cette dernière
        }
    }



    private void Start()
    {
        dashSlot.sprite = dashSprite1;
        invincibilitySlot.sprite = invincibilitySprite1;
        cooldownTimer = powerupCooldown;
    }

    #region Mouvement


    // Update is called once per frame
    void Update()
    {
        if(cooldownTimer < powerupCooldown)
        {
            cooldownTimer += Time.deltaTime;
        }
        else
        {

            dashSlot.sprite = dashSprite1;
            invincibilitySlot.sprite = invincibilitySprite1;

            if (Input.GetButtonDown("Dash" + id))
            {
                print("Je dashe");
                isDashing = true;
                cooldownTimer = 0f;
                dashTimer = 0f;


                dashSlot.sprite = dashSprite2;
                invincibilitySlot.sprite = invincibilitySprite2;
            }
            if (Input.GetButtonDown("Invincibilité" + id))
            {
                print("Je suis invincible"); //Ca fonctionne
                isInvincible = true;
                cooldownTimer = 0f;
                invincibilityTimer = 0f;


                dashSlot.sprite = dashSprite2;
                invincibilitySlot.sprite = invincibilitySprite2;
            }
            //if (Input.GetButtonDown("Aimant" + id))
            //{
            //    aimant = true;
            //    cooldownTimer = 0f;
            //    aimantTimer = 0f;
            //}
        }

        isBeyondCamera = !body[0].isVisible;

        if (dashTimer < dashDuration)
        {
            dashTimer += Time.deltaTime;
        }
        else
        {
            isDashing = false;
        }

        if (invincibilityTimer < invincibilityDuration)
        {
            invincibilityTimer += Time.deltaTime;
        }
        else
        {
            isInvincible = false;
        }

        if (aimantTimer < aimantDuration)
        {
            aimantTimer += Time.deltaTime;
        }
        else
        {
            aimant = false;
        }

        

        
        if (!ScoreManager.instance.partieGagnée && !isDead)
        {
            if (isDashing)
            {
                if (timer < dashSpeed)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    timer = 0f;
                    GoToNextCase(body[0], bodyDirections[0]);
                }
            }
            else
            {
                if (timer < moveSpeed)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    timer = 0f;
                    GoToNextCase(body[0], GetDirectionFromInput());
                }
            }
        }
    }






    private Direction.Condition GetDirectionFromInput()
    {
        float horizontal = Input.GetAxisRaw("Joystick" + id + "Horizontal");
        float vertical = Input.GetAxisRaw("Joystick" + id + "Vertical");

        float inputToUse = 0f;
        bool useHorizontal;

        if(Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            inputToUse = horizontal;
            useHorizontal = true;
        }
        else
        {
            inputToUse = vertical;
            useHorizontal = false;
        }

        Direction.Condition nouvelleDirection = Direction.Condition.Up;

        if (Mathf.Approximately(inputToUse, 0f))
        {
            return bodyDirections[0];
        }
        else
        {
            switch (inputToUse)
            {
                case -1f:
                    nouvelleDirection = (useHorizontal) ? Direction.Condition.Left : Direction.Condition.Down;
                    break;
                case 1f:
                    nouvelleDirection = (useHorizontal) ? Direction.Condition.Right : Direction.Condition.Up;
                    break;
                default:
                    return bodyDirections[0];
            }
        }

        switch (nouvelleDirection)
        {
            case Direction.Condition.Up:
                return (bodyDirections[0] == Direction.Condition.Down) ? bodyDirections[0] : nouvelleDirection;
            case Direction.Condition.Down:
                return (bodyDirections[0] == Direction.Condition.Up) ? bodyDirections[0] : nouvelleDirection;
            case Direction.Condition.Left:
                return (bodyDirections[0] == Direction.Condition.Right) ? bodyDirections[0] : nouvelleDirection;
            case Direction.Condition.Right:
                return (bodyDirections[0] == Direction.Condition.Left) ? bodyDirections[0] : nouvelleDirection;
            default:
                return bodyDirections[0];
        }
    }

    float Sign(float n)
    {
        return n < 0f ? -1f : (n > 0f ? 1f : 0f);
    }




    private void GoToNextCase(Case head, Direction.Condition directionDuMouvement)
    {
        Case nextCase = GetNextCase(head.pos, directionDuMouvement);

        if (GetNextCase(head.pos, directionDuMouvement) != null)
        {

            if ((nextCase.caseType == Case.CaseType.Obstacle && !isInvincible) ||
                nextCase.caseType == Case.CaseType.LimiteTerrain ||
                (nextCase.caseType == Case.CaseType.Snake && !isInvincible) ||
                (nextCase.caseType == Case.CaseType.SnakeHead && !isInvincible) ||
                isBeyondCamera)
            {
                print((isBeyondCamera) ? "hors champ" : "obstacle");
                isDead = true;
                ScoreManager.instance.KillPlayer(id);
            }
            else if (nextCase.caseType == Case.CaseType.Gélule)
            {

                //AvancerUneCase(directionDuMouvement);
                AvancerUneCase(bodyDirections[0]);
                AjouterNouvelleCaseAuCorps(bodyDirections[bodyDirections.Count - 1]);
                ScoreManager.instance.AddPoint(id);

                nextCase.caseType = Case.CaseType.TerrainNavigable;
                nextCase.GetComponent<SpriteRenderer>().sprite = null;
                nextCase.ChangerCaseConfiguration();
            }
            else
            {
                print("j'avance");
                AvancerUneCase(directionDuMouvement);
            }
        }
        else
        {
            print("est dans un chunk désactivé");
            isDead = true;
            ScoreManager.instance.KillPlayer(id);
        }
    }





    private void AvancerUneCase(Direction.Condition directionDuMouvement)
    {
        Case nouvelleCase = GetNextCase(body[0].pos, directionDuMouvement);


        Vector2 casePrécédente = body[0].pos;
        Direction.Condition directionPrécédente = bodyDirections[0];


        Vector2 casePrécédenteAvantTranslation;
        Direction.Condition directionPrécédenteAvantTranslation;


        body[0].transform.position = body[0].pos = nouvelleCase.pos;
        bodyDirections[0] = directionDuMouvement;
        body[0].ChangerCaseConfiguration();

        for (int i = 1; i < body.Count; i++)
        {

            casePrécédenteAvantTranslation = body[i].pos;
            directionPrécédenteAvantTranslation = bodyDirections[i];


            body[i].transform.position = body[i].pos = casePrécédente;
            bodyDirections[i] = directionPrécédente;
            body[i].ChangerCaseConfiguration();

            casePrécédente = casePrécédenteAvantTranslation;
            directionPrécédente = directionPrécédenteAvantTranslation;
        }
    }






    private Case GetNextCase(Vector2 pos, Direction.Condition newCaseDirection)
    {
        Vector2 nextPos = pos;
        Case nextCase = null;

        switch (newCaseDirection)
        {
            case Direction.Condition.Up:
                nextPos += Vector2.up * 0.6666667f;
                break;

            case Direction.Condition.Down:
                nextPos += Vector2.down * 0.6666667f;
                break;

            case Direction.Condition.Left:
                nextPos += Vector2.left * 0.6666667f;
                break;

            case Direction.Condition.Right:
                nextPos += Vector2.right * 0.6666667f;
                break;

        }
        posi = nextPos;
        //print(body[0].pos + " ; " + posi);
        //print(Physics2D.OverlapPoint(nextPos).GetComponent<Case>().caseType);
        if (Physics2D.OverlapPoint(nextPos))
        {
            nextCase = Physics2D.OverlapPoint(nextPos).GetComponent<Case>();
        }


        //print(nextCase);
        return nextCase;
    }


    #endregion





    #region Ajouter une nouvelle case au corps

    private void AjouterNouvelleCaseAuCorps(Direction.Condition newCaseDirection)
    {
        body.Add(GetNewCase(body[body.Count-1].pos, newCaseDirection));

        body[body.Count - 1].caseType = Case.CaseType.Snake;
        body[body.Count - 1].ChangerCaseConfiguration();

        bodyDirections.Add(newCaseDirection);
    }

    private Case GetNewCase(Vector2 pos, Direction.Condition newCaseDirection)
    {
        Vector2 nextPos = pos;
        Case nextCase = null;

        switch (newCaseDirection)
        {
            case Direction.Condition.Up:
                nextPos += Vector2.down * 0.6666667f;
                break;

            case Direction.Condition.Down:
                nextPos += Vector2.up * 0.6666667f;
                break;

            case Direction.Condition.Left:
                nextPos += Vector2.right * 0.6666667f;
                break;

            case Direction.Condition.Right:
                nextPos += Vector2.left * 0.6666667f;
                break;

        }

        //nextCase = Physics2D.OverlapPoint(nextPos).GetComponent<Case>();
        nextCase = ObjectPooler.instance.SpawnFromPool("CaseSnake", nextPos, Quaternion.identity).GetComponent<Case>();

        return nextCase;
    }


    #endregion
}
