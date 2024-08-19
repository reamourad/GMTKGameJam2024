using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisLineRed : BaseBlock
{
    public override IEnumerator OnDestroyed()
    {
        GameManager.Instance.enemyLineUp.ApplyDamageToAll(GetComponentInParent<PieceFolder>().currentPowerLevel); 
        yield return null; 
    }
}
