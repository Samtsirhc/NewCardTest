using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBlue_ZhiLenQi : IceBlueCard
{
    protected override void Start()
    {
        base.Start();
        switch (cardLevel)
        {
            case 1:
                armor = 2;
                break;
            case 2:
                armor = 3;
                break;
            case 3:
                armor = 4;
                break;
            default:
                break;
        }
    }
    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        GetArmor(armor);
    }
    protected override void UpdateDes()
    {
        base.UpdateDes();
        description = "";
        description += "±£Áô£º»¤¼×" + armor + ";";
    }
}
