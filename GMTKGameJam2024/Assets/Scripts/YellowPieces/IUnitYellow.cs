using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IUnitYellow : BaseBlock
{
    public override IEnumerator OnKill() { 
        GameManager.Instance.changeMoneyBy(3);
        yield return null;
    }
}
