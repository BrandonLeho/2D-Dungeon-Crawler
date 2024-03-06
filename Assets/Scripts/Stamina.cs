using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Slider staminaBar;
    public Slider staminaBarFalloff;
    public float lerpSpeed = 0.05f;
    public GameObject damagePopup;

    [SerializeField]
    private int currentStamina, maxStamina;
    [SerializeField]
    public int dashCost;
    [SerializeField] private Parry parry;

    private Coroutine regen;

    public UnityEvent<GameObject> OnStaminaWithReference, OnRecoverWithReference;

    public void InitializeStamina(int staminaValue)
    {
        maxStamina = staminaValue;
        currentStamina = staminaValue;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    public void UseStamina(int amount)
    {   
        currentStamina -= amount;

        if (regen != null)
        {
            StopCoroutine(regen);
        }

        regen = StartCoroutine(RegenerateStamina());
    }

    

    public void damageStamina(int amount, GameObject sender)
    {
        int enemy = LayerMask.NameToLayer("Enemy");

        if(sender.layer == gameObject.layer)
            return;
        
        if(sender.layer == enemy)
        {
            currentStamina -= 0;
        }
        if(gameObject.GetComponent<Parry>().GetParryState())
        {
            return;
        }
        else if(gameObject.GetComponent<Parry>().GetBlockState())
        {
            amount /= 2;
            currentStamina -= amount;
        }
        else
        {
            currentStamina -= amount;
        }

        if(currentStamina <= 0)
        {
            //TODO
        }
        

        if (regen != null)
        {
            StopCoroutine(regen);
        }

        regen = StartCoroutine(RegenerateStamina());
    }

    private IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(1);
 
        while(currentStamina < maxStamina)
        {
            if(parry.GetParryState())
            {
                currentStamina += 0;
            }
            else if(parry.GetBlockState())
                currentStamina += maxStamina / 100 / 2;
            else
                currentStamina += maxStamina / 100;

            staminaBar.value = currentStamina;
            yield return new WaitForSeconds(0.1f);
        }
        regen = null;
    }

    private void Update()
    {
        if(staminaBar.value != currentStamina)
        {
            staminaBar.value = currentStamina;
        }

        if(staminaBar.value != staminaBarFalloff.value)
        {
            staminaBarFalloff.value = Mathf.Lerp(staminaBarFalloff.value, currentStamina, lerpSpeed);
        }
    }

    public int GetCurrentStamina()
    {
        return currentStamina;
    }
}
