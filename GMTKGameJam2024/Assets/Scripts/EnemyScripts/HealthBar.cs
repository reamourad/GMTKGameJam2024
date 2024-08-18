using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        // Find the Slider component on this GameObject
        healthSlider = GetComponent<Slider>();
        if (healthSlider == null)
        {
            Debug.LogError("HealthBar.cs: Slider component not found on HealthBar.");
        }
    }

    // Initialize the health bar with maximum health
    public void SetMaxHealth(int maxHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }

    // Update the health bar value
    public void SetHealth(int health)
    {
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
    }
}
