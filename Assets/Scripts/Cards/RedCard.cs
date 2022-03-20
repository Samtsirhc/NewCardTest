using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCard : MyCard
{
    public override CardType cardType { get { return CardType.RED; } }
    public int redComboCount = 0;
    // Start is called before the first frame update

    public override void OnCauseDamage()
    {
        base.OnCauseDamage();
        BattleManager.Instance.player.anger += 1;
    }


}
