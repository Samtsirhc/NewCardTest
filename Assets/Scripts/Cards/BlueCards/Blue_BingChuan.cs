using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_BingChuan : BlueCard
{
    public override void OnUse()
    {
        base.OnUse();
        GetArmor(GetCalm() * 1);
        CastDamage(GetCalm());
    }
}
