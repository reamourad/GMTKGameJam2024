using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBlock : MonoBehaviour
{
    [SerializeField] public List<Vector2Int> offsetList = new List<Vector2Int>();
    [SerializeField] string description = "This block is not special";
    [SerializeField] Sprite blockImage;
    [SerializeField] int tierLevel;
    [SerializeField] string color;
    
    public abstract void Activate(); 
}
