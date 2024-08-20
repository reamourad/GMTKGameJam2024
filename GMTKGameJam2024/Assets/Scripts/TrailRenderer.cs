using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class TrailRenderer : MonoBehaviour
{
    [SerializeField] private int speed;
    private GameObject selectedEnemy;
    public PieceFolder currentPieceFolder; 

    private void Start()
    {
        selectedEnemy = GameManager.Instance.enemyClickManager.selectedEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        // Move our position a step closer to the target.
        var step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, selectedEnemy.transform.position, step);

        // Check if the position of the cube and sphere are approximately equal.
        float checkDistance = Vector3.Distance(transform.position, selectedEnemy.transform.position);
        Debug.Log(checkDistance); 
        if ( checkDistance < 1f)
        {
            if (selectedEnemy != null)
            {
                selectedEnemy.GetComponent<Enemy>().TakeDamage(currentPieceFolder.currentPowerLevel);
            }
            // Swap the position of the cylinder.
            Destroy(this); 
        }
    }
}
