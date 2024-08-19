using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OUnitRed : BaseBlock
{
    public override IEnumerator OnDestroyed() { 
        foreach (PieceFolder pieceFolder in GameManager.Instance.pieceCurrentlyInGrid) {
            if (pieceFolder.gameObject.activeSelf && pieceFolder.transform.GetChild(0).GetComponent<BaseBlock>().blockColor == BlockColor.Red && pieceFolder.transform.GetChild(0).GetComponent<OUnitRed>() != null) {
                GameManager.Instance.enemyLineUp.ApplyDamageToAll(pieceFolder.currentPowerLevel);
            }
        }
        yield return null;
    }
}
