using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Variables
    public int health;
    public int maxHealth;
    private HealthText healthTextComponent; // Reference to the HealthText component
    // TODO: if you ever come back to this, figure out why this reference doesn't seem to work (tetrisGrid.gridBlocks.Count always returns 0)
    // [SerializeField] public TetrisGrid tetrisGrid;  // Reference to TetrisGrid
    private GameManager gameManager = null;

    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure TetrisGrid reference is set
        // if (tetrisGrid == null)
        // {
            // tetrisGrid = FindObjectOfType<TetrisGrid>();
            // if (tetrisGrid == null)
            // {
            //     Debug.LogError("TetrisGrid not found in the scene.");
            // }
        // }
        gameManager = FindObjectOfType<GameManager>() as GameManager;

        // Initialize health and healthTextComponent
        healthTextComponent = GetComponentInChildren<HealthText>();
        if (healthTextComponent != null)
        {
            healthTextComponent.SetMaxHealth(maxHealth);
            healthTextComponent.SetHealth(health);
        }
        else
        {
            Debug.LogError("Enemy.cs: HealthText component not found on child objects.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(FindObjectOfType<TetrisGrid>().gridBlocks.Count);
        // Debug.Log(gameManager.tetrisGrid.gridBlocks.Count);

        if (Input.GetKeyDown("space"))
        {
            animator.Play("Hurt");
        }
    }

    public virtual void Attack() {
        List<GameObject> pieceSelectable = new List<GameObject>();
        // foreach ((Vector2Int pos, BaseBlock piece) in tetrisGrid.gridBlocks) {
        //     Debug.Log(piece.transform.parent.gameObject.activeSelf.ToString() + " " + (!pieceSelectable.Contains(piece.transform.parent.gameObject)).ToString() );
        //     if (piece.transform.parent.gameObject.activeSelf && !pieceSelectable.Contains(piece.transform.parent.gameObject)) {
        //         pieceSelectable.Add(piece.transform.parent.gameObject);
        //     }
        // }
        animator.Play("Attack");
        foreach (PieceFolder pieceFolder in gameManager.pieceCurrentlyInGrid) {
            if (pieceFolder.gameObject.activeSelf) {
                pieceSelectable.Add(pieceFolder.gameObject);
            }
        }
        if (pieceSelectable.Count > 0) {
            int randnum = Random.Range(0,pieceSelectable.Count);

            // disable block

            pieceSelectable[randnum].SetActive(false);
            pieceSelectable.RemoveAt(randnum);
        } else {
            Debug.Log("Enemy.cs: no pieces to attack.");
        }

        // this has to happen afterwards due to situations where the enemy dies from the OnAttacked() damage.
        foreach (GameObject piece in pieceSelectable) {
            StartCoroutine(piece.transform.GetChild(0).GetComponent<BaseBlock>().OnAttacked(this));
        }

        

        // // Get a random position on the grid
        // Vector2Int randomPosition = GetRandomGridPosition();

        // // Check if there is a block at the random position
        // BaseBlock block = tetrisGrid.GetBlockAtPosition(randomPosition);
        // if (block != null)
        // {
        //     // Remove the block from the grid
        //     tetrisGrid.RemoveFromGrid(block);
        //     Debug.Log($"Enemy.cs: Block at position {randomPosition} was removed.");
        // }
        // else
        // {
        //     // No block found at the position
        //     Debug.Log($"Enemy.cs: Missed attack at position {randomPosition}.");
        // }
    }

    // private Vector2Int GetRandomGridPosition()
    // {
    //     // Generate a random position within the grid size
    //     int x = Random.Range(0, tetrisGrid.size.x);
    //     int y = Random.Range(0, tetrisGrid.size.y);
    //     return new Vector2Int(x, y);
    // }

    // Method for taking damage
    public virtual void TakeDamage(int damage)
    {
        animator.Play("Hurt");
        health -= damage;

        // Update the health text
        if (healthTextComponent != null)
        {
            healthTextComponent.SetHealth(health);
        }

        // Shake the enemy when taking damage
        StartCoroutine(ShakeEnemy(0.1f, 0.1f));

        if (health <= 0)
        {
            Die();
        }
    }

    // Coroutine to shake the enemy
    private IEnumerator ShakeEnemy(float duration, float magnitude)
    {
        // Get the current position as the target position for shaking
        Vector2 originalPosition = transform.position;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            Vector2 randomOffset = Random.insideUnitCircle * magnitude;
            transform.position = originalPosition + randomOffset;
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Reset position after shaking
        transform.position = originalPosition;
    }

    // Method for dying
    protected void Die()
    {
        Debug.Log("Enemy.cs: Enemy dies.");
        // Increment the death count in the GameManager
        if (gameManager != null)
        {
            Debug.Log("Incrementing death count");
            gameManager.IncrementDeathCount();
        }
        Destroy(gameObject);
    }
}
