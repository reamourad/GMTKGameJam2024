using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    [SerializeField] public TMPro.TMP_Text healthText; // Reference to the Text component

    // Initialize the health bar with maximum health

    void Start() {
    }

    public void SetMaxHealth(int maxHealth)
    {
        if (healthText != null)
        {
            healthText.text = maxHealth.ToString(); // Set the max health initially
        }
    }

    // Update the health bar value
    public void SetHealth(int health)
    {
        if (healthText != null)
        {
            healthText.text = health.ToString(); // Update the health value
        }
    }
}
