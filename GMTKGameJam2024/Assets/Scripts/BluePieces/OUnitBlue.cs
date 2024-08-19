using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OUnitBlue : BaseBlock
{
    public override IEnumerator OnAttacked(Enemy enemy) {
        GameManager.Instance.enemyLineUp.ApplyDamageToAll(10);
        yield return null;
    }
}
