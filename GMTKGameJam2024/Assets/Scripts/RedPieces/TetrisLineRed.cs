using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisLineRed : BaseBlock
{
    public override IEnumerator OnDestroyed()
    {
        //do mass attack animation 
        foreach (Enemy enemy in GameManager.Instance.enemyLineUp.enemyLineUp) { 
        
            if(enemy != null)
            {
                GameObject trailRendererInstance = Instantiate(
                         GameManager.Instance.trailRenderer,
                         this.gameObject.transform.position,
                         Quaternion.identity
                       );
                trailRendererInstance.GetComponent<TrailRenderer>().currentPieceFolder = GetComponentInParent<PieceFolder>();

                trailRendererInstance.GetComponent<TrailRenderer>().selectedEnemy = enemy.gameObject;
            }
           
        }
        yield return new WaitForSeconds(1.5f);
    }
}
