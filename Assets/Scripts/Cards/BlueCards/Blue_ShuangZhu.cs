using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_ShuangZhu : BlueCard
{
    private float kkk = 0.5f;
    protected override void Start()
    {
        base.Start();
        coldAlarm = true;
    }
    public override void OnUse()
    {
        base.OnUse();
        GetArmor(CastDamage((int)(GetCalm()*kkk)));
    }
    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        kkk += 0.2f;
        if (freezed >= 1)
        {
            kkk += 0.2f;
        }
    }
}
