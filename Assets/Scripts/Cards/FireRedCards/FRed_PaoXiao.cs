using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRed_PaoXiao : FireRedCard
{
    public int fireStart;
    protected override void Start()
    {
        base.Start();
        switch (cardLevel)
        {
            case 1:
                fire = 5;
                fireStart = 5;
                break;
            case 2:
                fire = 7;
                fireStart = 5;
                break;
            case 3:
                fire = 8;
                fireStart = 8;
                break;
            default:
                break;
        }
    }
    public override void OnUse()
    {
        base.OnUse();
        if (IsStartCard())
        {
            BattleManager.Instance.player.fire += fireStart;
        }
        BattleManager.Instance.player.fire += fire;
    }
    protected override void UpdateDes()
    {
        base.UpdateDes();
        description = "";
        description += "»ðÑæ" + fire + ";";
        description += "³õÊ¼£º»ðÑæ+" + fireStart + ";";
    }
}
