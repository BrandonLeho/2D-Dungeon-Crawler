using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Agent : MonoBehaviour
{
    private AgentAnimations agentAnimations;
    private AgentMover agentMover;
    private Parry parry;

    private Vector2 pointerInput, movementInput;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    //private SwordParent swordParent;
    private KatanaParent katanaParent;

    public void PerformAttack()
    {
        if(katanaParent == null)
        {
            Debug.LogError("Weapon parent is null", gameObject);
            return;
        }
        katanaParent.Attack();
    }

    public void PerformDash()
    {
        agentMover.Dash();
    }

    public void PerformParry()
    {
        parry.StartBlockAndParry();
    }

    public void PerformedParry()
    {
        parry.EndBlockAndParry();
    }

    private void Update()
    {
        //pointerInput = GetPointerInput();
        //movementInput = movement.action.ReadValue<Vector2>().normalized;
        agentMover.MovementInput = movementInput;
        katanaParent.PointerPosition = pointerInput;

        AnimateCharacter();
    }

    private void Awake()
    {
        agentAnimations = GetComponentInChildren<AgentAnimations>();
        katanaParent = GetComponentInChildren<KatanaParent>();
        agentMover = GetComponent<AgentMover>();
        parry = GetComponent<Parry>();
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        agentAnimations.RotateToPointer(lookDirection);
        agentAnimations.PlayAnimation(movementInput);
    }
}