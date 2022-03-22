using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBlue_BingSun : IceBlueCard
{
    private int armor;
    private int ice;
    private int growIce;

    protected override void Start()
    {
        base.Start();
        growIce = 1;
        switch (cardLevel)
        {
            case 1:
                armor = 5;
                ice = 0;
                break;
            case 2:
                armor = 7;
                ice = 1;
                break;
            case 3:
                armor = 9;
                ice = 1;
                growIce = 2;
                break;
            default:
                break;
        }
    }
    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        ice += growIce;
    }
    public override void OnUse()
    {
        base.OnUse();
        GetArmor(armor);
        BattleManager.Instance.player.ice += ice;
    }
}
