using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_NuHuoBengFa : RedCard
{
    protected override void Start()
    {
        base.Start();
    }
    public override void OnUse()
    {
        base.OnUse();
        CastDamage(GetAnger()*2);
    }

    public override void AfterUse()
    {
        base.AfterUse();
        if (IsEndCard())
        {
            if (GetAnger() >= 10)
            {
                SetAnger(10);
            }
        }
        else
        {
            if (GetAnger() >= 5)
            {
                SetAnger(5);
            }
        }
    }
}
