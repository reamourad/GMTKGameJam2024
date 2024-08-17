using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBlock : MonoBehaviour
{
    [SerializeField] List<Vector2> offsetList = new List<Vector2>();
    [SerializeField] string description = "This block is not special";
    [SerializeField] Sprite blockImage;
    [SerializeField] int tierLevel;
    [SerializeField] string color;
    
    public abstract void Activate(); 
}
