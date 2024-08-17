using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public enum BlockColor
{
    Red,
    Blue, 
    Yellow,
    White
}
public abstract class BaseBlock : MonoBehaviour
{
    public List<Vector2Int> offsetList = new List<Vector2Int>();
    public string description = "This block is not special";
    public BlockColor blockColor;
    private bool isGlowing = false;
    private bool isHoverable = true;
    private bool isSelectable = false;
    private bool isSelected = false;
    private Renderer currentRenderer;
    [SerializeField] public GameObject background;


    public void setIsSelected(bool newIsSelected)
    {
        isSelected = newIsSelected; 
        if(isSelected)
        {
            setIsGlowing(true);
        }
        else
        {
            setIsGlowing(false);
        }

    }
    public void setIsGlowing(bool newIsGlowing)
    {
        if (!currentRenderer)
        {
            return; 
        }
        isGlowing = newIsGlowing;
        if (isGlowing)
        {
            float factor = 0.03f;
            Color blockColorHex = GameManager.Instance.blockColorToColor[BlockColor.White];
            Color color = blockColorHex * factor; 
            currentRenderer.material.SetColor("_GlowColor", color);
        }
        else
        {
            float factor = 0.1f;
            Color blockColorHex = GameManager.Instance.blockColorToColor[blockColor];
            Color color = blockColorHex * factor;
            currentRenderer.material.SetColor("_GlowColor", color);
        }
    }
    public void Start()
    {
        if (GetComponent<Renderer>() != null) 
        {
            currentRenderer = GetComponent<Renderer>();
        }
        setIsGlowing(false); 
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            setIsGlowing(!isGlowing); 
        }
    }

    public void OnMouseOver()
    {
        if (isHoverable && !isSelected)
        {
            //get all children of the parent 
            var children = transform.parent.gameObject.GetComponentsInChildren<BaseBlock>();
            foreach (BaseBlock baseBlock in children)
            {
                baseBlock.setIsGlowing(true);
            }
            Debug.Log("mouse over");
        }
    }

    public void OnMouseExit()
    {
        if (isHoverable && !isSelected)
        {
            var children = transform.parent.gameObject.GetComponentsInChildren<BaseBlock>();
            foreach (BaseBlock baseBlock in children)
            {
                baseBlock.setIsGlowing(false);
            }
            Debug.Log("mouse exit");
        }
    }

    public void OnMouseDown()
    {
        if (isSelectable)
        {
            if (isSelected)
            {
                //add it to the selected list 
                var children = transform.parent.gameObject.GetComponentsInChildren<BaseBlock>();
                foreach (BaseBlock baseBlock in children)
                {
                    baseBlock.setIsSelected(false);
                }
            }
            else
            {
                //add it to the selected list 
                var children = transform.parent.gameObject.GetComponentsInChildren<BaseBlock>();
                foreach (BaseBlock baseBlock in children)
                {
                    baseBlock.setIsSelected(true);
                }
            }
           
            Debug.Log("mouse down");
        }
    }
    public abstract void Activate(); 
}
