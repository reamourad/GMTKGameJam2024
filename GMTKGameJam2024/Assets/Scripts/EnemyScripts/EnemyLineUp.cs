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
    public float moveDuration = 1f; // Time it takes for the enemy to move into position

    // Start is called before the first frame update
    void Start()
    {
        //this.CreateLineUp(numberOfEnemies);
    }

    void Update()
    {
        // Test: Apply damage to all enemies when Enter is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("EnemyLineUp: Enter key pressed, applying damage to all enemies.");
            ApplyDamageToAll(40);
        }
    }

    // SPAWNING ENEMIES

    public void CreateLineUp(int numberOfEnemies)
    {
        enemyLineUp = new Enemy[numberOfEnemies];

        Debug.Log("EnemyLineUp.cs: Creating enemy lineup...");

        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Off-screen starting position to the right
            Vector2 offScreenPosition = new Vector2(10f, Random.Range(-4f, 4f));

            // Target position in the scene
            Vector2 targetPosition = new Vector2(Random.Range(2.6f, 3.5f), offScreenPosition.y);

            // Instantiate the enemy at the off-screen position
            GameObject enemyObject = Instantiate(enemyPrefab, offScreenPosition, Quaternion.identity);

            // Get the Enemy component from the instantiated object
            Enemy enemy = enemyObject.GetComponent<Enemy>();

            // Assign the enemy to the array and set stats
            int health = Random.Range(50, 100);
            enemyLineUp[i] = enemy;
            enemy.health = health;
            enemy.maxHealth = health;

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

    // ATTACK LOGIC

    private void StartAttackSequence()
    {
        StartCoroutine(AttackSequenceCoroutine());
    }

    private IEnumerator AttackSequenceCoroutine()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            enemyLineUp[i].Attack();
            Debug.Log($"EnemyLineUp.cs: Enemy {i} attacked.");

            yield return new WaitForSeconds(attackDelay);
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
