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
    [SerializeField] private Camera cam;
    [SerializeField] private float zoom, minZoom = 1f, maxZoom = 20f, velocity = 0f, smoothTime = 1f;
    public bool freeze;

    Rigidbody2D senderRB;
    Rigidbody2D receiverRB;
    Vector3 senderVelocity, receiverVelocity;

    private float freezeVelocity;
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
            freeze = true;
            zoom = 2;
            float timer = 0.25f;
            FindObjectOfType<HitLag>().Stop(timer);

            while(timer >= 0)
            {
                timer -= Time.unscaledDeltaTime;
            }
            freeze = false;
            zoom = 8;
        }
        

        if (regen != null)
        {
            StopCoroutine(regen);
        }

        regen = StartCoroutine(RegenerateStamina());
    }

    IEnumerator PoiseBreak()
    {
        float timer = 5f;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        zoom = 8;
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
        
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);

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

    public void Start()
    {
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        zoom = cam.orthographicSize;
    }
}
