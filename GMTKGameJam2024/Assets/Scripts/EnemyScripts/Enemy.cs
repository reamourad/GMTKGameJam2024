using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Variables
    public int attack;
    public int health;

    // Start is called before the first frame update
    void Start()
    {   

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method for attacking
    public virtual void Attack()
    {
        Debug.Log("Enemy attacks with " + attack + " power.");
        Debug.Log("Enemy.cs: TakeDamage called");
        TakeDamage(100);
    }

    public void ChooseBlock() {
        //TODO: Helper function for Attack (chooses a random block)
    }

    // Method for taking damage
    public void TakeDamage(int damage)
    {
        health -= damage;
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
