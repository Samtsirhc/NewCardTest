using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_BingShuangChongJi : BlueCard
{
    private float kkk = 1;
    protected override void Start()
    {
        base.Start();
        coldAlarm = true;
    }
    public override void OnUse()
    {
        base.OnUse();
        CastDamage((int)(GetCalm()*kkk));
        AddCold(GetCalm() / 5);
    }
    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        kkk += 0.2f;
    }
}
