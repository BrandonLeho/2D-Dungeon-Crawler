using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Slider sliderFalloff;
    private float health;
    public float lerpSpeed = 0.05f;
    private Coroutine updateHealthBarCoroutine = null;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        sliderFalloff.value = health; // Ensure the falloff starts at max health as well
    }
    
    public void SetHealth(int healthValue)
    {
        health = healthValue;
        slider.value = health;
        
        // Start or restart the coroutine to update the health bar smoothly
        if (updateHealthBarCoroutine != null)
        {
            StopCoroutine(updateHealthBarCoroutine);
        }
        updateHealthBarCoroutine = StartCoroutine(UpdateHealthBar());
    }

    private IEnumerator UpdateHealthBar()
    {
        // Continue the loop until the falloff slider reaches the current health value
        while (sliderFalloff.value != health)
        {
            sliderFalloff.value = Mathf.Lerp(sliderFalloff.value, health, lerpSpeed);
            yield return null; // Wait for the next frame
        }

        updateHealthBarCoroutine = null; // Reset the coroutine tracker
    }
}