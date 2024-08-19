using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TUnitYellow : BaseBlock
{
    public override IEnumerator OnGainGold() { 
        GameManager.Instance.enemyLineUp.ApplyDamageToAll(1);
        yield return null;
    }
}
