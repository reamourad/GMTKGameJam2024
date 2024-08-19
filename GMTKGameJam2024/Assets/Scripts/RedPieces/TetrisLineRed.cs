using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisLineRed : BaseBlock
{
    public override IEnumerator OnDestroyed()
    {
        yield return null; 
    }
}
