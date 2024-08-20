using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineUp : MonoBehaviour
{
    // Variables
    public Enemy[] enemyLineUp;
    public int deathCount;
    public int turnNumber;

    //PREFABS FOR ENEMIES
    public GameObject slimeEnemyPrefab;
    public GameObject wolfEnemyPrefab;
    public GameObject mageEnemyPrefab;



    public int numberOfEnemies;
    public float attackDelay = 1f; // Time in seconds between each attack
    public float moveDuration = 1f; // Time it takes for the enemy to move into position
    public Vector2[] spawnPositions; // Array of possible spawn positions

    public GameObject[] enemyPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        deathCount = 0;
        turnNumber = 0;
        spawnPositions = new Vector2[] // INITIALIZE POSSIBLE SPAWN LOCATIONS
        {
            new Vector2(3.5f, 3f),  
            new Vector2(3.5f, 0f),  
            new Vector2(3.5f, -2f),
            new Vector2(4.5f, 2f),  
            new Vector2(4.5f, -1f),  
            new Vector2(5.5f, 3f),  
            new Vector2(5.5f, 0f),  
            new Vector2(5.5f, -2f),
            new Vector2(6.5f, 2.5f),
            new Vector2(6.5f, -2f),
            new Vector2(7.5f, 2f),
            new Vector2(7.5f, -1f)
        };

        enemyPrefabs = new GameObject[]
        {
            slimeEnemyPrefab,
            wolfEnemyPrefab,
            mageEnemyPrefab
        };
        Debug.Log("Turn Number: " + turnNumber);
    }

    void Update()
    {
        // Test: Apply damage to all enemies when Enter is pressed
        /*if (Input.GetKeyDown(KeyCode.Return))
         {
             Debug.Log("EnemyLineUp: Enter key pressed, applying damage to all enemies.");
             ApplyDamageToAll(40);
         }*/
    }


    // SPAWNING LOGIC


    public void CreateLineUp()
    {
        turnNumber++;
        numberOfEnemies = Random.Range(1, 5);
        enemyLineUp = new Enemy[numberOfEnemies];

        Debug.Log("EnemyLineUp.cs: Creating enemy lineup...");

        // Shuffle the spawn positions to randomize the order
        ShuffleArray(spawnPositions);

        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Off-screen starting position to the right
            Vector2 offScreenPosition = new Vector2(10f, spawnPositions[i].y);

            // Target position in the scene from shuffled array
            Vector2 targetPosition = spawnPositions[i];

            // Choose and instantiate the enemy at the off-screen position
            GameObject enemyObject = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], offScreenPosition, Quaternion.identity);

            // Get the Enemy component from the instantiated object
            Enemy enemy = enemyObject.GetComponent<Enemy>();

            // Assign the enemy to the array and set stats with multiplier according to turn number
            int health = Random.Range(10, 20) * (turnNumber / Random.Range(1, 2)) / numberOfEnemies;
            enemyLineUp[i] = enemy;
            enemy.health = health;
            enemy.maxHealth = health;

            Debug.Log($"EnemyLineUp.cs: Enemy {i} created with stats - Type: {enemyObject.name}, Health: {enemy.health}, Max Health: {enemy.maxHealth}");
            // Start moving the enemy into position
            StartCoroutine(MoveEnemyToPosition(enemyObject, targetPosition, moveDuration));
        }

        Debug.Log("EnemyLineUp.cs: Enemy lineup creation complete. Total enemies: " + enemyLineUp.Length);
    }

    private IEnumerator MoveEnemyToPosition(GameObject enemyObject, Vector2 targetPosition, float duration)
    {
        Vector2 startPosition = enemyObject.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            enemyObject.transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        enemyObject.transform.position = targetPosition; // Ensure final position is set
    }

    private void ShuffleArray(Vector2[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Vector2 temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }


    // ATTACK LOGIC


    public void StartAttackSequence()
    {
        StartCoroutine(AttackSequenceCoroutine());
    }

    private IEnumerator AttackSequenceCoroutine()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            if (enemyLineUp[i] != null)
            {
                enemyLineUp[i].Attack();
                Debug.Log($"EnemyLineUp.cs: Enemy {i} attacked.");
                yield return new WaitForSeconds(attackDelay);
            }
            else
            {
                Debug.LogWarning($"EnemyLineUp.cs: Enemy at index {i} is null and will be skipped.");
            }
        }

        Debug.Log("EnemyLineUp.cs: All attacks completed.");
    }


    public void ApplyDamageToAll(int damage)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            if (enemyLineUp[i] != null)
            {
                enemyLineUp[i].TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning($"EnemyLineUp.cs: Enemy at index {i} is null and will be skipped.");
            }
        }
    }
}
