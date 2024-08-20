using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class TrailRenderer : MonoBehaviour
{
    [SerializeField] private int speed;
    public GameObject selectedEnemy;
    public PieceFolder currentPieceFolder;
    public int damage = 0; 

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(selectedEnemy != null)
        {
            // Move our position a step closer to the target.
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, selectedEnemy.transform.position, step);

            // Check if the position of the cube and sphere are approximately equal.
            float checkDistance = Vector3.Distance(transform.position, selectedEnemy.transform.position);
            if (checkDistance < 1f)
            {
                if(damage == 0)
                {
                    selectedEnemy.GetComponent<Enemy>().TakeDamage(currentPieceFolder.currentPowerLevel);

                }
                else
                {
                    selectedEnemy.GetComponent<Enemy>().TakeDamage(damage);

                }
                // Swap the position of the cylinder.
                Destroy(this);
            }
        }
        else
        {
            Destroy(this);
        }
    }
}
