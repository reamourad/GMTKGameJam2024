using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour
{
    [SerializeField] private Camera referenceCamera;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameManager gameManager;
    public Transform dragBlock = null;
    public bool dragBlockPreviouslyLocked = false;
    bool isMouseDown = false;
    private Vector2 pickupLocation = Vector2.zero;

    public Vector2 blockMouseOffset;
    public TetrisGridDisplay tetrisGridDisplay;
    public bool tetrisGridDisplayLocked;

    // https://gamedev.stackexchange.com/questions/121994/how-to-get-which-gameobject-the-mouse-is-over-in-unity
    Ray ray;
    RaycastHit hit;

    enum DragMode {
        Click,
        Drag,
    }

    DragMode dragMode = DragMode.Click;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Vector2 mousePosition = referenceCamera.ScreenToWorldPoint(Input.mousePosition);

        // pickBlock(Input.mousePosition);

        switch (dragMode) {
            case DragMode.Click:
                if (dragBlock != null) {
                    blockFollowMouse(Input.mousePosition);
                }
                break;
            case DragMode.Drag:
                if (isMouseDown && dragBlock != null) {
                    blockFollowMouse(Input.mousePosition);
                }
                break;
            default:
                break;
        }
    }


    void OnGUI() {
        Event m_Event = Event.current;

        // flip m_Event.mousePosition's y axis. idk why it's flipped from Input.mousePosition.
        Vector2 mousePosition = m_Event.mousePosition;
        mousePosition.y = referenceCamera.pixelHeight - mousePosition.y;

        if (gameManager.phase == GameManager.Phase.Shop) {
            if (m_Event.button == 0){

                switch (m_Event.type) {
                    case EventType.MouseDown:
                        dragMode = DragMode.Click;
                        isMouseDown = true;
                        break;
                    case EventType.MouseUp:
                        isMouseDown = false;
                        if (dragBlock != null) {
                            dropBlock(mousePosition);
                        } else if (dragMode == DragMode.Click) {
                            pickBlock(mousePosition);
                        }
                        break;
                    case EventType.MouseDrag:
                        dragMode = DragMode.Drag;
                        if (dragBlock == null) {
                            pickBlock(mousePosition);
                        }
                        break;
                    default:
                        break;
                }
            }
        } else {
            dropBlock(Input.mousePosition);
        }
    }

    private void pickBlock(Vector2 mousePos) {
        // https://stackoverflow.com/questions/72872575/raycast-with-pointereventdata-raycastui
        // https://docs.unity3d.com/540/Documentation/ScriptReference/EventSystems.EventSystem.RaycastAll.html
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        PointerEventData pointerEventData  = new PointerEventData(EventSystem.current);
        pointerEventData.position = mousePos;
        eventSystem.RaycastAll(pointerEventData, raycastResults);
        foreach (RaycastResult raycastResult in raycastResults) {
            // TODO: update checking style. tags maybe? right now it's based on name...?
            Transform parent = raycastResult.gameObject.GetComponentInParent<Transform>().parent;
            if (parent.name.StartsWith("Piece")) {
                blockMouseOffset = (Vector2) (parent.position - referenceCamera.ScreenToWorldPoint(mousePos));
                pickupLocation = parent.position;
                dragBlock = parent;
                dragBlockPreviouslyLocked = false;
                if (tetrisGridDisplayLocked) {
                    tetrisGridDisplay.tetrisGrid.RemoveFromGrid(dragBlock);
                    dragBlockPreviouslyLocked = true;
                }
            }
        }
    }

    private void dropBlock(Vector2 mousePos) {
        if (dragBlock != null) {
            if (tetrisGridDisplayLocked) {
                Vector2Int cellPos = Vector2Int.RoundToInt((referenceCamera.ScreenToWorldPoint(mousePos + blockMouseOffset) - tetrisGridDisplay.transform.position) / tetrisGridDisplay.cellSize);
                if (tetrisGridDisplay.tetrisGrid.CanAddToGrid(cellPos, dragBlock)) {
                    Debug.Log("lock");
                    tetrisGridDisplay.tetrisGrid.AddToGrid(cellPos, dragBlock);
                    dragBlock.gameObject.GetComponent<PieceFolder>().isInsideGrid = true;
                    dragBlock = null;
                } else if (dragBlockPreviouslyLocked) {
                    Debug.Log("reset to previous lock");
                    dragBlock.position = (Vector2) pickupLocation;
                    cellPos = Vector2Int.RoundToInt((dragBlock.position - tetrisGridDisplay.transform.position) / tetrisGridDisplay.cellSize);
                    tetrisGridDisplay.tetrisGrid.AddToGrid(cellPos, dragBlock);
                    dragBlock.gameObject.GetComponent<PieceFolder>().isInsideGrid = true;
                    dragBlock = null;
                } else {
                    Debug.Log("reset to previous");
                    dragBlock.position = (Vector2) pickupLocation;
                    dragBlock = null;
                }
            } else {
                //check if it is bought 
                if (dragBlock.gameObject.GetComponent<PieceFolder>().isBought == false)
                {
                    dragBlock.position = (Vector2)pickupLocation;
                }
                else
                {
                    dragBlock.gameObject.GetComponent<PieceFolder>().isInsideGrid = false; 
                }
                // TODO: functionality when a block is dropped outside of the grid.
                // may need to revamp the entire lock to grid system lol this system isn't scalable at all
                dragBlock = null;
            }
        }
    }

    private void blockFollowMouse(Vector2 mousePos) {
        if (dragBlock != null) {
            // this system only supports one grid at a time.
            // which makes sense... multiple grids cause arbitrary results.
            if (tetrisGridDisplayLocked) {
                // lock to grid
                // TODO: make the "lock to grid" system less ugly. or maybe overhaul the entire system because there are problems.
                // TODO: there's some scaling issues with this that are somewhat undefined. we need to solve those.
                // maybe just a scale = cellSize and then save scale. idk. not implemented yet.
                Vector2 gridPosition = tetrisGridDisplay.transform.position;
                
                // lock the piece's position to gridPosition + (n*tetrisGridDisplay.cellSize) separately in both axes.
                Vector2 cellPos = Vector2Int.RoundToInt(((Vector2) referenceCamera.ScreenToWorldPoint(mousePos) - gridPosition) / tetrisGridDisplay.cellSize);
                dragBlock.position = gridPosition + cellPos * tetrisGridDisplay.cellSize;
            } else {
                dragBlock.position = ((Vector2) referenceCamera.ScreenToWorldPoint(mousePos))+ blockMouseOffset;
            }
        }
    }
}
