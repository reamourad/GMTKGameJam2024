using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlock : MonoBehaviour
{
    [SerializeField] List<Vector2> offsetList = new List<Vector2>();
    [SerializeField] string description = "This block is not special";
    [SerializeField] Sprite blockImage;
    [SerializeField] int tierLevel; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
