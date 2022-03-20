using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCard : MyCard
{
    public override CardType cardType { get { return CardType.BLUE; } }
    // Start is called before the first frame update

    public override void AfterUse()
    {
        base.AfterUse();
        AddCalm(-2);
    }
    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        AddCalm(1);
    }
}
