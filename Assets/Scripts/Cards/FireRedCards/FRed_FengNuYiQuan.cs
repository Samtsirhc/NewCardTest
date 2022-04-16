using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRed_FengNuYiQuan : FireRedCard
{
    public override void SetLevelData()
    {
        base.SetLevelData();
        burnFactor = 3;
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
        CastDamage(damage);
    }

    protected override void UpdateDes()
    {
        base.UpdateDes();
        description = "";
        description += "…À∫¶" + damage + "\n";
        description += "»º…’…À∫¶Œ™3±∂";
    }
}
