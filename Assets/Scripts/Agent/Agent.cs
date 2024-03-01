using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Agent : MonoBehaviour
{
    private AgentAnimations agentAnimations;
    private AgentMover agentMover;

    private Vector2 pointerInput, movementInput;

    public Vector2 PointerInput => pointerInput;

    private SwordParent swordParent;

    [SerializeField]
    private InputActionReference movement, attack, pointerPosition, dash;

    private void OnEnable() 
    {
        attack.action.performed += PerformAttack;
        dash.action.performed += PerformDash;
    }

    private void OnDisable() 
    {
        attack.action.performed -= PerformAttack;
        dash.action.performed -= PerformDash;
    }

    private void PerformAttack(InputAction.CallbackContext obj)
    {
        if(swordParent == null)
        {
            Debug.LogError("Weapon parent is null", gameObject);
            return;
        }
        swordParent.Attack();
    }

    private void PerformDash(InputAction.CallbackContext obj)
    {
        agentMover.Dash();
    }

    private void Update()
    {
        pointerInput = GetPointerInput();
        movementInput = movement.action.ReadValue<Vector2>().normalized;
        agentMover.MovementInput = movementInput;
        swordParent.PointerPosition = pointerInput;

        AnimateCharacter();
    }

    public void PerformAttack()
    {
        swordParent.Attack();
    }

    private void Awake()
    {
        agentAnimations = GetComponentInChildren<AgentAnimations>();
        swordParent = GetComponentInChildren<SwordParent>();
        agentMover = GetComponent<AgentMover>();
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        agentAnimations.RotateToPointer(lookDirection);
        agentAnimations.PlayAnimation(movementInput);
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

}