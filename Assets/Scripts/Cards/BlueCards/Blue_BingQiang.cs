using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_BingQiang : BlueCard
{
    public override void OnUse()
    {
        base.OnUse();
        GetArmor(GetCalm() * 2);
    }
}
