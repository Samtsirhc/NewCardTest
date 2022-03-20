using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_BingDongJingJue : BlueCard
{
    protected override void Start()
    {
        base.Start();
        coldAlarm = true;
    }
    public override void OnUse()
    {
        base.OnUse();
        AddCalm(3);
    }
}
