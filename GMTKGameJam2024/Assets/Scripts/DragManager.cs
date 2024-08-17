using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    [SerializeField] private Camera referenceCamera;
    [SerializeField] GameObject dragBlock = null;
    bool isMouseDown = false;


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

        print("trying event");
        switch (m_Event.type) {
            case EventType.MouseDown:
                pickBlock(mousePosition);
                dragMode = DragMode.Click;
                isMouseDown = true;
                break;
            case EventType.MouseUp:
                isMouseDown = false;
                if (dragBlock != null) {
                    dropBlock(mousePosition);
                }
                break;
            case EventType.MouseDrag:
                dragMode = DragMode.Drag;
                break;
            default:
                break;
        }
    }

    private void pickBlock(Vector2 mousePos) {
        ray = Camera.main.ScreenPointToRay(mousePos);
        print("trying raycast "+mousePos.ToString());
        if (Physics.Raycast(ray, out hit)) {
            print(hit.collider.name);
            // TODO: update checking style. tags maybe?
        }
    }

    private void dropBlock(Vector2 mousePos) {
        Debug.Log("dropped block at"+mousePos.ToString());
    }

    private void blockFollowMouse(Vector2 mousePos) {

    }
}
