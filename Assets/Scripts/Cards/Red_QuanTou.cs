using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_QuanTou : RedCard
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
}
