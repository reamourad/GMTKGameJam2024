using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleUnitBlue : BaseBlock
{
    public override IEnumerator OnDeath() {
        foreach (PieceFolder pieceFolder in GameManager.Instance.pieceCurrentlyInGrid) {
            pieceFolder.gameObject.SetActive(true);
        }
        yield return null;
        this.GetComponentInParent<PieceFolder>().gameObject.SetActive(false);
    }
}
