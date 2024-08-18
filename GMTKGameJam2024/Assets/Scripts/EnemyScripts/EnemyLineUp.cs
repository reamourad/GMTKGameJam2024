using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineUp : MonoBehaviour
{   
    //Variables
    public Enemy[] enemyLineUp;
    public GameObject enemyPrefab;
    public int numberOfEnemies = 3;

    // Start is called before the first frame update
    void Start()
    {   
        //For testing purposes
        Debug.Log("EnemyLineUp.cs: Line up called");
        this.CreateLineUp();
        Debug.Log("EnemyLineUp.cs: Attack called");
        this.BeginAttack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateLineUp(){
        enemyLineUp = new Enemy[numberOfEnemies];

        Debug.Log("EnemyLineUp.cs: Creating enemy lineup...");

        for (int i = 0; i < numberOfEnemies; i++)
        {
            // SPAWNING IN RANDOM AREAS NEAR ORIGIN TO TEST
            Vector2 randomPosition = new Vector2(
                Random.Range(1f, 4f), 
                Random.Range(-4f, 4f)  
            );

            // Instantiate the enemy at the random position
            GameObject enemyObject = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

            //Debug.Log("Enemy " + i + " instantiated at position: " + randomPosition);

            // Get the Enemy component from the instantiated object
            Enemy enemy = enemyObject.GetComponent<Enemy>();

            // Assign the enemy to the array and the stats n stuff
            enemyLineUp[i] = enemy;
            enemy.attack = Random.Range(10, 20); // Example values
            enemy.health = Random.Range(50, 100); // Example values

            //Debug.Log("Enemy " + i + " - Attack: " + enemy.attack + ", Health: " + enemy.health);
        }

        Debug.Log("EnemyLineUp.cs: Enemy lineup creation complete. Total enemies: " + enemyLineUp.Length);
}


    public void BeginAttack() {
        //TODO: Iterate through enemy array and have each enemy attack 
        for (int i = 0; i < numberOfEnemies; i++) {
            //Debug.Log(enemyLineUp[i].attack + ", " + enemyLineUp[i].health);
            enemyLineUp[i].Attack();
        }
        Debug.Log("EnemyLineUp.cs: Attack completed");
    }
}
