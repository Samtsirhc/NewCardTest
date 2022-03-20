using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_WanMeiLuanQuan : RedCard
{
    protected override void Start()
    {
        base.Start();
    }
    public override void OnUse()
    {
        base.OnUse();
        CastDamage((int)(GetAnger() * 0.5));
        CastDamage((int)(GetAnger() * 0.5));
        if (GetComboCount() == 1)
        {
            CastDamage((int)(GetAnger() * 0.5));
        }
        if (IsEndCard())
        {
            CastDamage((int)(GetAnger() * 0.5));
        }
        if (IsStartCard())
        {
            CastDamage((int)(GetAnger() * 0.5));
        }
    }


}
