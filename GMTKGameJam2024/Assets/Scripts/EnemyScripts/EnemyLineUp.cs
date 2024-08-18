using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineUp : MonoBehaviour
{
    // Variables
    public Enemy[] enemyLineUp;
    public GameObject enemyPrefab;
    public int numberOfEnemies = 3;
    public float attackDelay = 1f; // Time in seconds between each attack

    // Start is called before the first frame update
    void Start()
    {
        // For testing purposes
        Debug.Log("EnemyLineUp: Line up created");
        this.CreateLineUp(numberOfEnemies);
        Debug.Log("EnemyLineUp: Attack called");
        StartAttackSequence(); // Helper method for attack since i dont wanna call coroutine in here
    }

    // Update is called once per frame
    void Update()
    {

    }


    //SPAWNING ENEMIES


    public void CreateLineUp(int numberOfEnemies)
    {
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

            // Get the Enemy component from the instantiated object
            Enemy enemy = enemyObject.GetComponent<Enemy>();

            // Assign the enemy to the array and set stats
            enemyLineUp[i] = enemy;
            enemy.health = Random.Range(50, 100); // Example values
        }

        Debug.Log("EnemyLineUp.cs: Enemy lineup creation complete. Total enemies: " + enemyLineUp.Length);
    }


    //ATTACK LOGIC

    private void StartAttackSequence()
    {
        StartCoroutine(AttackSequenceCoroutine());
    }

    private IEnumerator AttackSequenceCoroutine()
    {
        // Iterate through the enemy array and have each enemy attack with a delay
        for (int i = 0; i < numberOfEnemies; i++)
        {
            enemyLineUp[i].Attack();
            Debug.Log($"EnemyLineUp.cs: Enemy {i} attacked.");
            
            // Wait for the specified delay before the next enemy attacks
            yield return new WaitForSeconds(attackDelay);
        }
        
        Debug.Log("EnemyLineUp.cs: All attacks completed.");
    }

    public void ApplyDamageToAll(int damage)
    {
        // Apply damage to each enemy in the lineup
        for (int i = 0; i < numberOfEnemies; i++)
        {
            enemyLineUp[i].TakeDamage(damage);
        }
    }
}
