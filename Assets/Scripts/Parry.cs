using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Parry : MonoBehaviour
{
    Rigidbody2D playerRB;
    SpriteRenderer playerSprite;
    AgentMover agentMover;

    public bool playerIsAttacking, playerIsBlocking, playerCanMove, 
     playerIsJumping, playerIsParrying, playerIsRolling;

    public float parryWindowTimer, parryWindowShort, parryDelayTimer, 
    parryWindowOriginal;

    private void Awake()
    {
        agentMover = GetComponent<AgentMover>();
    }
    
    public void StartBlockAndParry()
    {
        if (!playerIsAttacking && !playerIsRolling && !playerIsJumping)
        {
            //Debug.Log("Starting PlayerBlockAndParry Coroutine");
            StartCoroutine(PlayerBlockAndParry());
        }
    }

 
    IEnumerator PlayerBlockAndParry()
    {
        playerIsBlocking = true;
        playerIsParrying = true;
        
        agentMover.Blocking(playerIsBlocking, playerIsParrying);

        //Debug.Log(parryWindowTimer + "      " + Mathf.Epsilon);
 
        while (playerIsBlocking == true && parryWindowTimer > Mathf.Epsilon)
        {
            Debug.Log("Parrying: " + playerIsParrying);
            parryWindowTimer -= Time.deltaTime;
            yield return null;
        }
 
        playerIsParrying = false;
        //Debug.Log("Parrying: " + playerIsParrying);
        agentMover.Blocking(playerIsBlocking, playerIsParrying);
 
        yield break;
    }
 
    public void EndBlockAndParry()
    {
        if (!playerIsAttacking && !playerIsRolling && !playerIsJumping)
        {
            Debug.Log("Ended Block and Parry");
            playerIsBlocking = false;
            parryWindowTimer = parryWindowShort;
            
            agentMover.Blocking(playerIsBlocking, playerIsParrying);

            StartCoroutine(ParryDelayFunction());
        }
    }
 
    IEnumerator ParryDelayFunction()
    {
        while (playerIsBlocking == false && parryDelayTimer >= Mathf.Epsilon)
        {
            parryDelayTimer -= Time.deltaTime;
            yield return null;
        }
 
        if (parryDelayTimer <= Mathf.Epsilon)
        {
            parryWindowTimer = parryWindowOriginal;
            parryDelayTimer = 1.5f;
        }
        else if (parryDelayTimer > Mathf.Epsilon)
        {
            parryDelayTimer = 1.5f;
        }
    }
 
    [SerializeField] float parryCooldown;
    IEnumerator PlayerNewBlockAndParry()
    {
        playerIsParrying = true;
        playerCanMove = false;
        playerRB.velocity = new Vector2 (0f,0f);
        playerSprite.color = Color.green;
 
        yield return new WaitForSeconds(parryCooldown);
 
        playerSprite.color = Color.white;
        playerIsParrying = false;
        playerCanMove = true;
    }
}
