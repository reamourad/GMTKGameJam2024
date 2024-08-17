using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
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

    public void Attack() {
        //TODO: Uses a randomly chosen block  in the inventory and deletes it
    }

    public void ChooseBlock() {
        //TODO: Helper function for Attack (chooses a random block)
    }
}
