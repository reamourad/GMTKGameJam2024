using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TetrisGrid : MonoBehaviour
{
    // TetrisGrid defines the bottom left corner as (0,0) and all squares are positive.
    [SerializeField] public Vector2Int size = Vector2Int.one;
    
    // a dictionary of positions which contain the blocks in those positions.
    // the dictionary should not contain the key for a block if the position is empty.
    Dictionary<Vector2Int, BaseBlock> gridBlocks = new Dictionary<Vector2Int, BaseBlock>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public bool CanAddToGrid(Vector2Int position, BaseBlock block) {
        foreach (Vector2Int blockOffset in block.offsetList) {
            Vector2Int globalPosition = blockOffset + position;
            // check if outside of grid
            if (globalPosition.x < 0 || globalPosition.y < 0 || globalPosition.x >= size.x || globalPosition.y > size.y) {
                return false;
            }
            
            // check if position is occupied
            if (gridBlocks.ContainsKey(globalPosition)) {
                return false;
            }
        }
        return true;
    }

    public bool CanAddToGrid(Vector2Int position, Transform dragBlock) {
        // AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        if (dragBlock.childCount > 0) {
            return CanAddToGrid(position, dragBlock.GetChild(0).GetComponent<BaseBlock>() as BaseBlock);
        }
        return false;
    }

    public void AddToGrid(Vector2Int position, BaseBlock block) {
        // assume CanAddToGrid was checked.
        foreach (Vector2Int blockOffset in block.offsetList) {
            gridBlocks[position + blockOffset] = block;
        }
    }
    public void AddToGrid(Vector2Int position, Transform dragBlock) {
        if (dragBlock.childCount > 0) {
            AddToGrid(position, dragBlock.GetChild(0).GetComponent<BaseBlock>() as BaseBlock);
        }
    }

    public void RemoveFromGrid(BaseBlock block) {
        // assume the block is in the grid.
        // it silently fails otherwise.
        // memory inefficent but logically efficient.
        var keys = new List<Vector2Int>(gridBlocks.Keys);
        foreach (Vector2Int key in keys) {
            if (gridBlocks[key] == block) {
                gridBlocks.Remove(key);
            }
        }
    }
    public void RemoveFromGrid(Transform dragBlock) {
        if (dragBlock.childCount > 0) {
            RemoveFromGrid(dragBlock.GetChild(0).GetComponent<BaseBlock>() as BaseBlock);
        }
    }

    public BaseBlock GetBlockAtPosition(Vector2Int position) {
        if (gridBlocks.ContainsKey(position)) {
            return gridBlocks[position];
        }
        return null;
    }

    public void ResizeGrid(Vector2Int newSize) {
        size = newSize;
        // TODO: when resize smaller need to figure out what to do with overflow size.
    }
}
