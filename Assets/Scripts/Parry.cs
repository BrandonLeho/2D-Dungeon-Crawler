using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Parry : MonoBehaviour
{
    Rigidbody2D playerRB;
    SpriteRenderer playerSprite;
    AgentMover agentMover;

    public bool playerIsBlocking, playerIsParrying, playerIsRolling, cantParry;

    public float parryWindowTimer = 0.25f, parryWindowShort = 0.25f, parryDelayTimer = 0.15f, 
    parryWindowOriginal = 0.25f, parryCooldown = 0.25f;

    private void Awake()
    {
        agentMover = GetComponent<AgentMover>();
    }

    public void Update()
    {
        if(parryWindowTimer < 0)
        {
            cantParry = false;
        }
    }
    
    public void StartBlockAndParry()
    {
        if (!playerIsParrying && !cantParry)
        {
            //Debug.Log("Starting PlayerBlockAndParry Coroutine");
            playerIsParrying = true;
            cantParry = true;
            StartCoroutine(PlayerBlockAndParry());
        }
    }

 
    IEnumerator PlayerBlockAndParry()
    { 
        agentMover.Blocking(playerIsBlocking, playerIsParrying);

        //Debug.Log(parryWindowTimer + "      " + Mathf.Epsilon);
 
        while (playerIsParrying == true && parryWindowTimer > 0)
        {
            //Debug.Log("Parrying: " + playerIsParrying);
            parryWindowTimer -= Time.deltaTime;
            yield return null;
        }
        playerIsBlocking = true;
        playerIsParrying = false;
        //Debug.Log("Parrying: " + playerIsParrying);
        agentMover.Blocking(playerIsBlocking, playerIsParrying);
 
        yield break;
    }
 
    public void EndBlockAndParry()
    {
        if(parryDelayTimer == parryCooldown && cantParry || playerIsBlocking)
        {
            //Debug.Log("Ended Block and Parry");
            StartCoroutine(ParryDelayFunction());
        }
    }
 
    IEnumerator ParryDelayFunction()
    {
        while (playerIsParrying)
        {
            yield return null;
        }
        parryWindowTimer = parryWindowShort;
        playerIsBlocking = false;
        agentMover.Blocking(playerIsBlocking, playerIsParrying);

        while (parryDelayTimer >= 0)
        {
            cantParry = true;
            parryDelayTimer -= Time.deltaTime;
            yield return null;
        }
 
        if (parryDelayTimer <= 0)
        {
            parryWindowTimer = parryWindowOriginal;
            parryDelayTimer = parryCooldown;
            cantParry = false;
        }
    }
    public bool GetParryState()
    {
        return playerIsParrying;
    }

    public bool GetBlockState()
    {
        return playerIsBlocking;
    }
}
