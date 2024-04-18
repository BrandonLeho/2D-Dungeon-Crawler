using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    public Slider manaBar;
    public Slider manaBarFalloff;
    public float lerpSpeed = 0.05f;
    [SerializeField]
    private int currentMana, maxMana;

    private Color barColor;

    private void Start()
    {
        barColor = manaBar.GetComponentsInChildren<Image>()[3].color;
    }
    public void InitializeMana(int manaValue)
    {
        currentMana = manaValue;
        maxMana = manaValue;
        manaBar.maxValue = maxMana;
        manaBarFalloff.maxValue = maxMana;
    }

    public void DrainMana(int amount)
    {
        currentMana -= amount;
    }

    public void RestoreMana(int amount)
    {
        currentMana += amount;
        if(currentMana > maxMana)
            currentMana = maxMana;
        
    }

    private void Update()
    {
        if(manaBar.value != currentMana)
        {
            manaBar.value = currentMana;
        }

        if(manaBar.value != manaBarFalloff.value)
        {
            manaBarFalloff.value = Mathf.Lerp(manaBarFalloff.value, currentMana, lerpSpeed);
        }
    }

    public int GetMana()
    {
        return currentMana;
    }
}
