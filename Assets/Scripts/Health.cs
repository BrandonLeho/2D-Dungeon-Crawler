using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public HealthBar healthBar;
    public GameObject damagePopup, character;
    public TMP_Text popupText;

    [SerializeField]
    private int currentHealth, maxHealth;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;    

    [SerializeField]
    private bool isDead = false;

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        healthBar.SetMaxHealth(maxHealth);
        isDead = false;
    }

    public void GetHit(int amount, GameObject sender)
    {
        if(isDead)
            return;
        if(sender.layer == gameObject.layer)
            return;
        
        currentHealth -= amount;

        healthBar.SetHealth((int)((float)currentHealth/(float)maxHealth * 1000));

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
            Destroy(gameObject);
        }
    }
}
