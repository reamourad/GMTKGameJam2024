using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUnitYellow : BaseBlock
{
    public override IEnumerator OnBuy() { 
        if (GameManager.Instance.currentPrice > 0) {
            GameManager.Instance.currentPrice -= 1;
        }
        yield return null;
    }
}
