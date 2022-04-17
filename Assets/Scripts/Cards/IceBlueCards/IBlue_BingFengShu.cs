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
                ice = 4;
                break;
            case 2:
                damage = 7;
                ice = 4;
                break;
            case 3:
                damage = 13;
                ice = 6;
                break;
            default:
                break;
        }
    }
    public override void OnUse()
    {
        base.OnUse();
        CastDamage(damage);
        if (freezed)
        {
            BattleManager.Instance.player.ice += ice;
        }
    }

    protected override void UpdateDes()
    {
        base.UpdateDes();
        description = "";
        description += "ÉËº¦" + damage + "\n";
        description += "±ù¶³²»ÏûºÄº®±ù";
    }
}
