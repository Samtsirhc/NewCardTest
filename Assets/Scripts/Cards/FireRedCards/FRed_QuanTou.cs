using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRed_QuanTou : FireRedCard
{
    protected override void Start()
    {
        base.Start();
        switch (cardLevel)
        {
            case 1:
                damage = 5;
                fire = 1;
                break;
            case 2:
                damage = 7;
                fire = 1;
                break;
            case 3:
                damage = 12;
                fire = 2;
                break;
            default:
                break;
        }
    }
    public override void OnUse()
    {
        base.OnUse();
        CastDamage(damage);
        BattleManager.Instance.player.fire += 1;
    }

    protected override void UpdateDes()
    {
        base.UpdateDes();
        description = "";
        description += "ÉËº¦" + damage + ";";
        description += "»ðÑæ" + fire + ";";
    }
}
