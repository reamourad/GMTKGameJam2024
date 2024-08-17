using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisGridDisplay : MonoBehaviour
{
    [SerializeField] private TetrisGrid tetrisGrid;
    // one dimensional size because cells are square.
    // size is in pixels.
    [SerializeField] private const float cellSize = 0.2f;
    [SerializeField] private TetrisGridCell cell;
    private Vector2Int displayedSize = Vector2Int.zero;
    private Dictionary<Vector2Int, TetrisGridCell> gridCells = new Dictionary<Vector2Int, TetrisGridCell>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (displayedSize != tetrisGrid.size) UpdateSize();
        // TODO: implement resizing
    }

    void UpdateSize() {
        Vector2Int resizeSize = tetrisGrid.size - displayedSize;
        
        // x axis
        if (resizeSize.x > 0) {
            // generate new cells as children of this.
            for (int x = displayedSize.x; x < tetrisGrid.size.x; ++x) {
                for (int y = 0; y < tetrisGrid.size.y; ++y) {
                    if (!gridCells.ContainsKey(new Vector2Int(x,y))) {
                        TetrisGridCell newCell = Object.Instantiate(cell, this.transform);
                        newCell.transform.localPosition = (new Vector2(x,y)) * cellSize;
                        newCell.transform.localScale = new Vector3(cellSize, cellSize, 1);
                        gridCells[new Vector2Int(x,y)] = newCell;
                    }
                }
            }
        }
        // y axis
        if (resizeSize.y > 0) {
            // generate new cells as children of this.
            for (int y = displayedSize.y; y < tetrisGrid.size.y; ++y) {
                for (int x = 0; x < tetrisGrid.size.x; ++x) {
                    if (!gridCells.ContainsKey(new Vector2Int(x,y))) {
                        TetrisGridCell newCell = Object.Instantiate(cell, this.transform);
                        newCell.transform.localPosition = (new Vector2(x,y)) * cellSize;
                        newCell.transform.localScale = new Vector3(cellSize, cellSize, 1);
                        gridCells[new Vector2Int(x,y)] = newCell;
                    }
                }
            }
        }


        // remove extraneous cells
        if (resizeSize.y < 0 || resizeSize.x < 0) {
            // there's better logic that shouldn't require iterating through cells that are known to be within tetrisGrid.size but whatever
            foreach ((Vector2Int key, TetrisGridCell gridCell) in gridCells) {
                if (key.x >= tetrisGrid.size.x || key.y >= tetrisGrid.size.y) {
                    gridCells.Remove(key);
                    Destroy(gridCell);
                }
            }
        }

        displayedSize = tetrisGrid.size;
    }
}
