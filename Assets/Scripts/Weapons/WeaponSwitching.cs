using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GunsSwitcher : MonoBehaviour
{
    Controls playerInput;
    public float mouseScrollY;
    public int selectedWeapon = 0; 

    public UnityEvent OnWeaponSwitch;

    private void Awake()
    {
        playerInput = new Controls();
        playerInput.PlayerInput.WeaponSwitching.performed += xxHash3 => mouseScrollY = xxHash3.ReadValue<float>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousthing = selectedWeapon;
        if (mouseScrollY > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
               selectedWeapon = 0;
            else
               selectedWeapon++;   
        }
        if (mouseScrollY < 0f)
        {
            if (selectedWeapon <= 0)
               selectedWeapon = transform.childCount - 1;
            else
               selectedWeapon--;   
        }
        if (previousthing != selectedWeapon)
        {
            SelectWeapon();
        }
    }
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
           if ( i == selectedWeapon)
               weapon.gameObject.SetActive(true);
           else
               weapon.gameObject.SetActive(false);
            i++;
        }    
    }

    void OnEnable()
    {
        playerInput.Enable();
    }

    void OnDisable()
    {
        playerInput.Disable();
    }

    private void PerformAttack(InputAction.CallbackContext obj)
    {
        OnWeaponSwitch?.Invoke();
    }
}