using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack, OnDash, OnParry, OnPerformedParry, OnFire, OnPerformedFire;

    [SerializeField]
    private InputActionReference movement, attack, pointerPosition, dash, parry, fire;

    private void Update()
    {
        OnMovementInput?.Invoke(movement.action.ReadValue<Vector2>().normalized);
        OnPointerInput?.Invoke(GetPointerInput());
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnEnable() 
    {
        attack.action.canceled += PerformAttack;
        dash.action.performed += PerformDash;
        parry.action.started += PerformParry;
        parry.action.canceled += PerformedParry;
        fire.action.started += PerformFire;
        fire.action.canceled += PerformedFire;
    }

    private void OnDisable() 
    {
        attack.action.canceled -= PerformAttack;
        dash.action.performed -= PerformDash;
        parry.action.started -= PerformParry;
        parry.action.canceled-= PerformedParry;
        fire.action.started -= PerformFire;
        fire.action.canceled -= PerformedFire;
    }

    private void PerformAttack(InputAction.CallbackContext obj)
    {
        OnAttack?.Invoke();
    }

    private void PerformDash(InputAction.CallbackContext obj)
    {
        OnDash?.Invoke();
    }

    private void PerformParry(InputAction.CallbackContext obj)
    {
        OnParry?.Invoke();
    }

    private void PerformedParry(InputAction.CallbackContext obj)
    {
        OnPerformedParry?.Invoke();
    }

    private void PerformFire(InputAction.CallbackContext obj)
    {
        OnFire?.Invoke();
    }

    private void PerformedFire(InputAction.CallbackContext obj)
    {
        OnPerformedFire?.Invoke();
    }

}
