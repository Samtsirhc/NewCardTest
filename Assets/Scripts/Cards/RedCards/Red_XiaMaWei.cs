using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_XiaMaWei : RedCard
{
    protected override void Start()
    {
        base.Start();
    }
    public override void OnUse()
    {
        base.OnUse();
        CastDamage(GetAnger());
    }

    public override void PreUse()
    {
        base.PreUse();
        if (IsStartCard())
        {
            AddAnger(1);
        }
    }
}
