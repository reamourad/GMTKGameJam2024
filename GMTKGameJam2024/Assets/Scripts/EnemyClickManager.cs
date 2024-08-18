using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyClickManager : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Camera referenceCamera;
    public GameObject selectedEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedEnemy != null) {
            target.SetActive(true);
            target.transform.position = selectedEnemy.transform.position;
        } else {
            target.SetActive(false);
        }
    }

    void OnGUI()
    {
        Event m_Event = Event.current;
        Vector2 mousePosition = m_Event.mousePosition;
        mousePosition.y = referenceCamera.pixelHeight - mousePosition.y;
        if (gameManager.phase == GameManager.Phase.Battle) {
            if (m_Event.button == 0 && m_Event.type == EventType.MouseUp) {
                List<RaycastResult> raycastResults = new List<RaycastResult>();
                PointerEventData pointerEventData  = new PointerEventData(EventSystem.current);
                pointerEventData.position = mousePosition;
                eventSystem.RaycastAll(pointerEventData, raycastResults);
                foreach (RaycastResult raycastResult in raycastResults) {
                    if (raycastResult.gameObject.GetComponentInChildren<Enemy>() != null) {
                        selectedEnemy = raycastResult.gameObject;
                    }
                }
            }
        }
    }

    private void selectEnemy(GameObject gameObject) {
        selectedEnemy = gameObject;
    }

    public void unselectEnemy() {
        selectedEnemy = null;
    }
}
