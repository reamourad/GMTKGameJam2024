using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TUnitRed : BaseBlock
{
    public override IEnumerator OnAttack() { 
        foreach (PieceFolder pieceFolder in GameManager.Instance.pieceCurrentlyInGrid) {
            if (pieceFolder.gameObject.activeSelf && pieceFolder.transform.GetChild(0).GetComponent<BaseBlock>().blockColor == BlockColor.Red) {
                GameManager.Instance.enemyLineUp.ApplyDamageToAll(3);
            }
        }
        yield return null;
    }
}
