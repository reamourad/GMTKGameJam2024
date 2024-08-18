using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceFolder : MonoBehaviour
{
    public bool isBought = false;
    public bool isInsideGrid = false;
    public bool isSelected = false; 

    public void setIsPieceSelected(bool newIsSelected)
    {
        isSelected = newIsSelected;
        if (isSelected)
        {
            //make all the base block glow 
            GameManager.Instance.actionList.Add(this); 
            foreach(BaseBlock baseBlock in GetComponentsInChildren<BaseBlock>())
            {
                baseBlock.setIsGlowing(true);
            }
           
        }
        else
        {
            GameManager.Instance.actionList.Remove(this);
            //make all the base block glow 
            foreach (BaseBlock baseBlock in GetComponentsInChildren<BaseBlock>())
            {
                baseBlock.setIsGlowing(false);
            }

        }
    }
}
