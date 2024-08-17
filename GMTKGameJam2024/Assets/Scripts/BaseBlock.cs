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
    public bool isGlowing = false;
    private Renderer currentRenderer;
    [SerializeField] public GameObject background; 

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
    public abstract void Activate(); 
}
