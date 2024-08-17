using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineUp : MonoBehaviour
{   
    //Variables
    public BaseEnemy[] enemyLineUp;
    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Line up called");
        CreateLineUp(); //For testing purposes
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateLineUp()
{
    int numberOfEnemies = 3;
    enemyLineUp = new BaseEnemy[numberOfEnemies];

    Debug.Log("Creating enemy lineup...");

    for (int i = 0; i < numberOfEnemies; i++)
    {
        // Generate a random position near the origin (0,0) within a range
        Vector2 randomPosition = new Vector2(
            Random.Range(-5f, 5f), // Random x position between -5 and 5 units
            Random.Range(-5f, 5f)  // Random y position between -5 and 5 units
        );

        // Instantiate the enemy at the random position
        GameObject enemyObject = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

        Debug.Log("Enemy " + i + " instantiated at position: " + randomPosition);

        // Get the BaseEnemy component from the instantiated object
        BaseEnemy enemy = enemyObject.GetComponent<BaseEnemy>();

        // Assign the enemy to the array
        enemyLineUp[i] = enemy;

        // Optionally, set attack and health stats
        enemy.attack = Random.Range(10, 20); // Example values
        enemy.health = Random.Range(50, 100); // Example values

        Debug.Log("Enemy " + i + " - Attack: " + enemy.attack + ", Health: " + enemy.health);
    }

    Debug.Log("Enemy lineup creation complete. Total enemies: " + enemyLineUp.Length);
}


    public void BeginAttack() {
        //TODO: Iterate through enemy array and have each enemy attack 
    }
}
