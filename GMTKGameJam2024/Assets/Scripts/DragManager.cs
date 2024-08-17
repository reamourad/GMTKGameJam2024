using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour
{
    [SerializeField] private Camera referenceCamera;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] public Transform dragBlock = null;
    bool isMouseDown = false;

    public Vector2 blockMouseOffset;

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
            blockMouseOffset = parent.position - referenceCamera.ScreenToWorldPoint(mousePos);
            Debug.Log("transform name "+parent.name);
            if (parent.name.StartsWith("Piece")) {
                Debug.Log("picked block");
                dragBlock = parent;
            }
        }
    }

    private void dropBlock(Vector2 mousePos) {
        Debug.Log("dropped block at"+mousePos.ToString());
        dragBlock = null;
    }

    private void blockFollowMouse(Vector2 mousePos) {
        if (dragBlock != null) {
            Debug.Log(mousePos);
            dragBlock.position = (Vector2) referenceCamera.ScreenToWorldPoint(mousePos) + blockMouseOffset;
        }
    }
}
