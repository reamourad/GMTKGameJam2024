using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangUnitBlue : BaseBlock
{
    public override IEnumerator OnAttacked(Enemy enemy) { 
        enemy.TakeDamage(GetComponentInParent<PieceFolder>().currentPowerLevel);
        yield return null;
    }
}
