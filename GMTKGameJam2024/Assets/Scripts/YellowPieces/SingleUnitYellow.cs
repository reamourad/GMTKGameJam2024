using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleUnitYellow : BaseBlock
{
    public override IEnumerator OnAttack() { 
        GameManager.Instance.changeMoneyBy(1);
        yield return null;
    }
}
