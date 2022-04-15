using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBlue_CiGuHanBing : IceBlueCard
{
    protected override void Start()
    {
        base.Start();
        switch (cardLevel)
        {
            case 1:
                damage = 5;
                break;
            case 2:
                damage = 7;
                break;
            case 3:
                damage = 10;
                break;
            default:
                break;
        }
    }
    
    public override void OnUse()
    {
        base.OnUse();
        CastPiercingDamage(damage);
        GetArmor(burn ? damage * burnFactor :damage);
    }

    protected override void UpdateDes()
    {
        base.UpdateDes();
        description = "";
        description += "造成"+ damage + "点穿透伤害，并窃取等量护甲";
    }
}
