using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRed_QuanTou : FireRedCard
{
    public override void SetLevelData()
    {
        base.SetLevelData();
        fire = 2;
        switch (cardLevel)
        {
            case 1:
                damage = 5;
                break;
            case 2:
                damage = 7;
                break;
            case 3:
                damage = 12;
                fire = 3;
                break;
            default:
                break;
        }
    }
    public override void OnUse()
    {
        base.OnUse();
        CastDamage(damage);
        AddFire(fire);
    }
    protected override void UpdateDes()
    {
        base.UpdateDes();
        description = "";
        description += "ÉËº¦" + damage + "\n";
        description += "»ðÑæ" + fire + "";
    }
}
