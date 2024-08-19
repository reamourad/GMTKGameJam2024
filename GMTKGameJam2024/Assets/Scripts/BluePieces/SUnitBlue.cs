using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUnitBlue : BaseBlock
{
    public override IEnumerator OnDestroyed() { 
        List<PieceFolder> inactiveFolders = new List<PieceFolder>();
        foreach (PieceFolder pieceFolder in GameManager.Instance.pieceCurrentlyInGrid) {
            if (!pieceFolder.gameObject.activeSelf) {
                inactiveFolders.Add(pieceFolder);
            }
        }

        if (inactiveFolders.Count > 0) {
            inactiveFolders[Random.Range(0,inactiveFolders.Count)].gameObject.SetActive(true);
        }
        yield return null;
    }
}
