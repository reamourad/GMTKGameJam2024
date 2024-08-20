using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TUnitRed : BaseBlock
{
    public override IEnumerator OnAttack() {
        int damage = 0;
        foreach (PieceFolder pieceFolder in GameManager.Instance.pieceCurrentlyInGrid)
        {
            if (pieceFolder.gameObject.activeSelf && pieceFolder.transform.GetChild(0).GetComponent<BaseBlock>().blockColor == BlockColor.Red)
            {
                damage+=3;
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
            trailRendererInstance.GetComponent<TrailRenderer>().damage = damage;
        }
        yield return new WaitForSeconds(1.5f);
       
    }
}
