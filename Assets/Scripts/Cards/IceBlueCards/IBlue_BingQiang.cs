using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBlue_BingQiang : IceBlueCard
{
    private int armor;
    public override void OnUse()
    {
        base.OnUse();
        switch (cardLevel)
        {
            case 1:
                armor = 5;
                break;
            case 2:
                armor = 7;
                break;
            case 3:
                armor = 13;
                break;
            default:
                break;
        }
        GetArmor(armor);
    }
}
