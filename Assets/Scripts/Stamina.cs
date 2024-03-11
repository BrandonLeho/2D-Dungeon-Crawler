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
    public GameObject stancePopup;
    [SerializeField]private int currentStamina, maxStamina;
    [SerializeField]public int dashCost;
    [SerializeField] private Parry parry;
    [SerializeField] private Camera cam;
    [SerializeField] public float finalZoom, zoom, targetZoom = 3, minZoom = 1f, maxZoom = 20f, velocity = 0.25f, smoothTime = 0.25f;
    public bool freeze, isStunned;
    private Coroutine regen;
    public UnityEvent<GameObject> OnStaminaWithReference, OnRecoverWithReference;
    Animator animator;

    

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
        if(isStunned)
        {
            return;
        }

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
            gameObject.GetComponent<Knockback>().PlayFeedback(200, sender); 
            if(gameObject.layer == enemy)
            {
                isStunned = true;
                gameObject.GetComponent<AIEnemy>().Stunned(isStunned);
            }
            freeze = true;
            float timer = 0.25f;
            FindObjectOfType<HitLag>().Stop(timer);

            while(timer >= 0)
            {
                cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref velocity, smoothTime);
                timer -= Time.unscaledDeltaTime;
            }
            freeze = false;
            zoom = finalZoom;

            StartCoroutine(Stunned());
            return;
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

    private IEnumerator Stunned()
    {
        StopCoroutine(regen);
        yield return new WaitForSeconds(0.25f);
        GameObject StanceTextInstance = Instantiate(stancePopup, gameObject.transform.position, Quaternion.identity);
        StanceTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("Stance Break");
        yield return new WaitForSeconds(3);
        isStunned = false;
        regen = StartCoroutine(RegenerateStamina());
        currentStamina += (int)(maxStamina / 1.33);
        if(gameObject.layer == LayerMask.NameToLayer("Enemy"))
            gameObject.GetComponent<AIEnemy>().Stunned(isStunned);
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
        finalZoom = cam.orthographicSize;
        zoom = Mathf.Clamp(finalZoom, minZoom, maxZoom);
    }
}
