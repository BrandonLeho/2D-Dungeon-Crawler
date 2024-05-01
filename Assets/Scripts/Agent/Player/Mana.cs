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

    public TMP_Text manaText;

    private Color barColor;

    private void Start()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
            manaText = manaBar.GetComponentsInChildren<TextMeshProUGUI>()[0];
        barColor = manaBar.GetComponentsInChildren<Image>()[3].color;
    }
    public void InitializeMana(int manaValue)
    {
        currentMana = manaValue;
        maxMana = manaValue;
        manaBar.maxValue = maxMana;
        manaBarFalloff.maxValue = maxMana;
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
            manaText.text = currentMana.ToString();
    }

    public void DrainMana(int amount)
    {
        currentMana -= amount;
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
            manaText.text = currentMana.ToString();
    }

    public void RestoreMana(int amount)
    {
        currentMana += amount;
        if (currentMana > maxMana)
            currentMana = maxMana;

        if (gameObject.layer == LayerMask.NameToLayer("Player"))
            manaText.text = currentMana.ToString();
    }

    private void Update()
    {
        if (manaBar.value != currentMana)
        {
            manaBar.value = currentMana;
        }

        if (manaBar.value != manaBarFalloff.value)
        {
            manaBarFalloff.value = Mathf.Lerp(manaBarFalloff.value, currentMana, lerpSpeed);
        }
    }

    public int GetMana()
    {
        return currentMana;
    }
}
