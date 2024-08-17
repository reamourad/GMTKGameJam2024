using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisGridDisplay : MonoBehaviour
{
    [SerializeField] private TetrisGrid tetrisGrid;
    // one dimensional size because cells are square.
    // size is in pixels.
    [SerializeField] private float cellSize = 0.4f;
    [SerializeField] private int cellPixels = 36;
    [SerializeField] private TetrisGridCell cell;
    [SerializeField] private Camera referenceCamera;
    private Vector2Int displayedSize = Vector2Int.zero;
    private Dictionary<Vector2Int, TetrisGridCell> gridCells = new Dictionary<Vector2Int, TetrisGridCell>();
    private Vector2Int hoveredCell = Vector2Int.one * -1;
    // Start is called before the first frame update
    void Start()
    {
        UpdateSize();

    }

    // Update is called once per frame
    void Update()
    {

        // find world position based on pixel position of mouse.


        // a size of 1 represents a total height of 2.
        // finding the position of the mouse relative to the camera position.
        // TODO: fix referenceCamera.transform.localScale issues with this
        Vector2 mousePositionRelative = (((Vector2) Input.mousePosition - new Vector2(referenceCamera.pixelWidth, referenceCamera.pixelHeight) / 2) / referenceCamera.pixelHeight) * referenceCamera.transform.localScale * referenceCamera.orthographicSize * 2;
        Vector2 mousePosition = mousePositionRelative + (Vector2) referenceCamera.transform.position;

        Vector2Int currentHoveredCell = Vector2Int.RoundToInt((mousePosition - (Vector2) this.transform.position) / cellSize);
        if (currentHoveredCell != hoveredCell) {
            // print("hovered cell "+currentHoveredCell.ToString()+", unhovered cell "+hoveredCell.ToString());
            unhoverCell(hoveredCell);
            if (currentHoveredCell.x < displayedSize.x && currentHoveredCell.x >= 0 && currentHoveredCell.y < displayedSize.y && currentHoveredCell.y >= 0) {
                hoverCell(currentHoveredCell);
                hoveredCell = currentHoveredCell;
            } else if (!(hoveredCell.x < displayedSize.x && hoveredCell.x >= 0 && hoveredCell.y < displayedSize.y && hoveredCell.y >= 0)) {
                unhoverCell(hoveredCell);
                hoveredCell = Vector2Int.one * -1;
            }
        }


        if (displayedSize != tetrisGrid.size) UpdateSize();
    }

    void UpdateSize() {
        // shouldn't be possible to break now.
        Vector2Int resizeSize = tetrisGrid.size - displayedSize;
        
        // x axis
        if (resizeSize.x > 0) {
            // generate new cells as children of this.
            for (int x = displayedSize.x; x < tetrisGrid.size.x; ++x) {
                for (int y = 0; y < tetrisGrid.size.y; ++y) {
                    if (!gridCells.ContainsKey(new Vector2Int(x,y))) {
                        TetrisGridCell newCell = Object.Instantiate(cell, this.transform);
                        newCell.transform.localPosition = (new Vector2(x,y)) * cellSize;
                        newCell.transform.localScale = new Vector3(cellSize / cellPixels * 100, cellSize / cellPixels * 100, 1);
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
                        newCell.transform.localScale = new Vector3(cellSize / cellPixels * 100, cellSize / cellPixels * 100, 1);
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

    private void hoverCell(Vector2Int cell) {
        if (gridCells.ContainsKey(hoveredCell)) {
            gridCells[cell].hoverSprite.enabled = true;
            gridCells[cell].unhoverSprite.enabled = false;
        }
    }

    private void unhoverCell(Vector2Int cell) {
        if (gridCells.ContainsKey(cell)) {
            gridCells[cell].unhoverSprite.enabled = true;
            gridCells[cell].hoverSprite.enabled = false;
        }
    }
}
