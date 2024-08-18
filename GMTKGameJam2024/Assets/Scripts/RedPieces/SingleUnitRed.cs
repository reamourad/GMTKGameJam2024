 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleUnitRed : BaseBlock
{
    public override IEnumerator OnAttack()
    {
        foreach (PieceFolder pieceFolder in GameManager.Instance.pieceCurrentlyInGrid)
        {
            pieceFolder.currentPowerLevel += 2; 
        }
        yield return null;
    }
}
