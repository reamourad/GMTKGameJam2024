using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TUnitBlue : BaseBlock
{
    public override IEnumerator OnAttack() { 
        foreach (PieceFolder pieceFolder in GameManager.Instance.actionList) {
            if (pieceFolder.gameObject.activeSelf && pieceFolder.transform.GetChild(0).GetComponent<BaseBlock>().blockColor == BlockColor.Blue) {
                GameManager.Instance.currentAttackScore *= 2;
            }
        }
        yield return null;
    }
}
