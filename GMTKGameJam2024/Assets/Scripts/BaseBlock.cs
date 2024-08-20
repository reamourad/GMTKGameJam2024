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
    private bool isHoverable = true;
    public bool isSelectable = false;
    public bool isBought = false; 
    private Renderer currentRenderer;
    public TypeOfBlock typeOfBlock; 
    [SerializeField] public GameObject background;

    // assigned on Start()
    private GameManager gameManager;


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

        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    public void Update()
    {
    }

    public void OnMouseOver()
    {
        PieceFolder pieceFolder = GetComponentInParent<PieceFolder>();
        if (isHoverable && !pieceFolder.isSelected)
        {
            //get all children of the parent 
            var children = transform.parent.gameObject.GetComponentsInChildren<BaseBlock>();
            foreach (BaseBlock baseBlock in children)
            {
                baseBlock.setIsGlowing(true);
            }
        }
        if (isHoverable)
        {
            GameManager.Instance.setDescriptionDisplay(GetComponentInParent<PieceFolder>().currentPowerLevel, description); 
        }
        if (isSelectable && Input.GetMouseButtonDown(0))
        {
            //get the parent and call setisselected(false)
            pieceFolder.setIsPieceSelected(true); 
        }

        if (Input.GetMouseButtonDown(1) && !pieceFolder.isInsideGrid)
        {
            Debug.Log("Rotate");
            transform.parent.Rotate(0, 0, 90); 
        }
    }

    public void OnMouseExit()
    {
        PieceFolder pieceFolder = GetComponentInParent<PieceFolder>();
        if (isHoverable && !pieceFolder.isSelected)
        {
            var children = transform.parent.gameObject.GetComponentsInChildren<BaseBlock>();
            foreach (BaseBlock baseBlock in children)
            {
                baseBlock.setIsGlowing(false);
            }
        }
    }

    public virtual IEnumerator OnAttack() { yield return null; } 
    public virtual IEnumerator OnDestroyed() { yield return null; } 
    public virtual IEnumerator OnBuy() { yield return null;}
    public virtual IEnumerator OnKill() { yield return null;}
    public virtual IEnumerator OnGainGold() { yield return null;}
    public virtual IEnumerator OnDeath() { yield return null;}
    public virtual IEnumerator OnAttacked(Enemy enemy) { yield return null;}
    virtual public void Activate()
    {
        GameManager.Instance.changeAttackScoreBy(GetComponentInParent<PieceFolder>().currentPowerLevel); 
    }

    virtual public void Deactivate()
    {
        GameManager.Instance.changeAttackScoreBy(-GetComponentInParent<PieceFolder>().currentPowerLevel);

    }


}
