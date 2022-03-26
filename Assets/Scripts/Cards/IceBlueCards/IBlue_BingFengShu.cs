using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBlue_BingFengShu : IceBlueCard
{
    protected override void Start()
    {
        base.Start();
        switch (cardLevel)
        {
            case 1:
                damage = 5;
                cold = 4;
                break;
            case 2:
                damage = 7;
                cold = 4;
                break;
            case 3:
                damage = 13;
                cold = 6;
                break;
            default:
                break;
        }
    }
    public override void OnUse()
    {
        base.OnUse();
        CastDamage(damage);
        if (icebound)
        {
            BattleManager.Instance.player.ice += cold;
        }
    }

    protected override void UpdateDes()
    {
        base.UpdateDes();
        description = "";
        description += "伤害" + damage + ";";
        description += "不消耗冰封之心";
    }
}
