using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthBar;
    public Slider healthBarFalloff;
    public float lerpSpeed = 0.05f;
    public GameObject damagePopup, healingPopup, character;
    public TMP_Text popupText;

    [SerializeField]
    private int currentHealth, maxHealth;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;    

    [SerializeField]
    private bool isDead = false;

    private Color barColor;

    private void Start()
    {
        barColor = healthBar.GetComponentsInChildren<Image>()[3].color;
    }
    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
        healthBar.maxValue = maxHealth;
        healthBarFalloff.maxValue = maxHealth;
    }

    public void GetHit(int amount, GameObject sender)
    {
        if(isDead || amount <= 0)
            return;
        if(sender.layer == gameObject.layer)
            return;
            
        currentHealth -= amount;

        Vector3 randomPopup = new Vector3
        (character.transform.position.x + Random.Range(-0.75f, 0.75f), 
        character.transform.position.y + Random.Range(0.5f, 1.25f));

        GameObject DamageTextInstance = Instantiate(damagePopup, randomPopup, Quaternion.identity);
        DamageTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(amount.ToString());


        if(currentHealth > 0)
            OnHitWithReference?.Invoke(sender);
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if(currentHealth > maxHealth)
            currentHealth = maxHealth;
        
        Vector3 randomPopup = new Vector3
        (character.transform.position.x + Random.Range(-0.75f, 0.75f), 
        character.transform.position.y + Random.Range(0.5f, 1.25f));

        GameObject HealingTextInstance = Instantiate(healingPopup, randomPopup, Quaternion.identity);
        HealingTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(amount.ToString());
    }

    public void SendHeals(int amount, GameObject sender)
    {
        GetHit(-amount, sender);
    }
    private void Update()
    {
        if(healthBar.value != currentHealth)
        {
            healthBar.value = currentHealth;
        }

        if(healthBar.value != healthBarFalloff.value)
        {
            healthBarFalloff.value = Mathf.Lerp(healthBarFalloff.value, currentHealth, lerpSpeed);
        }
    }
}
