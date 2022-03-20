using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_LianHuanZhang : RedCard
{
    protected override void Start()
    {
        base.Start();
    }
    public override void OnUse()
    {
        base.OnUse();
        CastDamage((int)(GetAnger() * 0.5));
        if (GetComboCount() == 1)
        {
            CastDamage((int)(GetAnger() * 0.5));
        }
    }


}
