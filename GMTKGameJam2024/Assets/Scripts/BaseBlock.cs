using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBlock : MonoBehaviour
{
    public List<Vector2Int> offsetList = new List<Vector2Int>();
    public string description = "This block is not special";
    public Sprite blockImage;
    public int tierLevel;
    public string color;
    
    public abstract void Activate(); 
}
