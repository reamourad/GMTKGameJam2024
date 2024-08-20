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
        //do mass attack animation 
        foreach (Enemy enemy in GameManager.Instance.enemyLineUp.enemyLineUp)
        {
            GameObject trailRendererInstance = Instantiate(
                          GameManager.Instance.trailRenderer,
                          this.gameObject.transform.position,
                          Quaternion.identity
                        );
            trailRendererInstance.GetComponent<TrailRenderer>().currentPieceFolder = GetComponentInParent<PieceFolder>();
            trailRendererInstance.GetComponent<TrailRenderer>().selectedEnemy = enemy.gameObject;
        }
        yield return new WaitForSeconds(1.5f);
    }
}
