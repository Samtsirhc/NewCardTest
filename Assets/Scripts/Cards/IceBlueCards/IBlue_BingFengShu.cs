using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBlue_BingFengShu : IceBlueCard
{
    private int damage;
    public override void OnUse()
    {
        base.OnUse();
        switch (cardLevel)
        {
            case 1:
                damage = 5;
                break;
            case 2:
                damage = 7;
                break;
            case 3:
                damage = 13;
                break;
            default:
                break;
        }
        CastDamage(damage);
        if (icebound)
        {
            BattleManager.Instance.player.ice += 5;
        }
    }
}
