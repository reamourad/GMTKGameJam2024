using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZUnitYellow : BaseBlock
{
    public override IEnumerator OnDestroyed() { 
        GameManager.Instance.setMoneyTo(GameManager.Instance.currentMoney * 2);
        yield return null;
    }
}
