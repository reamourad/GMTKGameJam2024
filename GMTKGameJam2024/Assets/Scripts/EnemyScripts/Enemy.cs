using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Variables
    public int health;
    public int maxHealth;
    [SerializeField] private Text healthText; // Reference to the UI Text for health display
    [SerializeField] private TetrisGrid tetrisGrid;  // Reference to TetrisGrid

    // Start is called before the first frame update
    void Start()
    {
        // Ensure TetrisGrid reference is set
        if (tetrisGrid == null)
        {
            tetrisGrid = FindObjectOfType<TetrisGrid>();
            if (tetrisGrid == null)
            {
                Debug.LogError("TetrisGrid not found in the scene.");
            }
        }

        // Initialize health  
        healthText.text = health.ToString();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Attack()
    {
        Debug.Log("Enemy.cs: Attack started.");

        // Get a random position on the grid
        Vector2Int randomPosition = GetRandomGridPosition();

        // Check if there is a block at the random position
        BaseBlock block = tetrisGrid.GetBlockAtPosition(randomPosition);
        if (block != null)
        {
            // Remove the block from the grid
            tetrisGrid.RemoveFromGrid(block);
            Debug.Log($"Enemy.cs: Block at position {randomPosition} was removed.");
        }
        else
        {
            // No block found at the position
            Debug.Log($"Enemy.cs: Missed attack at position {randomPosition}.");
        }
    }

    private Vector2Int GetRandomGridPosition()
    {
        // Generate a random position within the grid size
        int x = Random.Range(0, tetrisGrid.size.x);
        int y = Random.Range(0, tetrisGrid.size.y);
        return new Vector2Int(x, y);
    }

    // Method for taking damage
    public virtual void TakeDamage(int damage)
    {
        health -= damage;

        // Update the health text
        if (healthText != null)
        {
            healthText.text = health.ToString();
        }

        if (health <= 0)
        {
            Die();
        }
    }

    // Method for dying
    protected void Die()
    {
        Debug.Log("Enemy.cs: Enemy dies.");
        // Add death behavior here
        Destroy(gameObject);
    }
}
